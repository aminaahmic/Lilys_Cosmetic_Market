using Lilys_CM.Application.Abstractions;
using Lilys_CM.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/products/{productId:int}/images")]
public class ProductImagesController : ControllerBase
{
    private const long MaxImageSizeInBytes = 5 * 1024 * 1024; // 5 MB

    private static readonly string[] AllowedExtensions =
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".webp"
    };

    private readonly IAppDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductImagesController(IAppDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int productId, CancellationToken cancellationToken)
    {
        var images = await _context.ProductImages
            .Where(x => x.ProductId == productId)
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.IsMain)
            .Select(x => new
            {
                x.Id,
                x.ProductId,
                x.ImageUrl,
                x.FileName,
                x.IsMain,
                x.SortOrder
            })
            .ToListAsync(cancellationToken);

        return Ok(images);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Upload(
        int productId,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == productId && !x.IsDeleted, cancellationToken);

        if (product is null)
            return NotFound("Product not found.");

        if (file is null || file.Length == 0)
            return BadRequest("Image file is required.");

        if (file.Length > MaxImageSizeInBytes)
            return BadRequest("Image file must be 5 MB or smaller.");

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
            return BadRequest("Only jpg, jpeg, png and webp images are allowed.");

        var root = GetWebRootPath();

        var folder = Path.Combine(root, "uploads", "products", productId.ToString());
        Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(folder, fileName);

        await using (var stream = System.IO.File.Create(filePath))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        var hasMainImage = await _context.ProductImages
            .AnyAsync(x => x.ProductId == productId && x.IsMain, cancellationToken);

        var image = new ProductImageEntity
        {
            ProductId = productId,
            FileName = fileName,
            ImageUrl = $"/uploads/products/{productId}/{fileName}",
            IsMain = !hasMainImage,
            SortOrder = 0,
            CreatedAtUtc = DateTime.UtcNow
        };

        _context.ProductImages.Add(image);

        if (!hasMainImage)
        {
            product.ImageUrl = image.ImageUrl;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Ok(new
        {
            image.Id,
            image.ProductId,
            image.ImageUrl,
            image.FileName,
            image.IsMain,
            image.SortOrder
        });
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{imageId:int}/main")]
    public async Task<IActionResult> SetMain(
        int productId,
        int imageId,
        CancellationToken cancellationToken)
    {
        var images = await _context.ProductImages
            .Where(x => x.ProductId == productId)
            .ToListAsync(cancellationToken);

        var selected = images.FirstOrDefault(x => x.Id == imageId);

        if (selected is null)
            return NotFound("Image not found.");

        foreach (var image in images)
        {
            image.IsMain = image.Id == imageId;
        }

        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == productId && !x.IsDeleted, cancellationToken);

        if (product is not null)
        {
            product.ImageUrl = selected.ImageUrl;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{imageId:int}")]
    public async Task<IActionResult> Delete(
        int productId,
        int imageId,
        CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == productId && !x.IsDeleted, cancellationToken);

        if (product is null)
            return NotFound("Product not found.");

        var image = await _context.ProductImages
            .FirstOrDefaultAsync(x => x.Id == imageId && x.ProductId == productId, cancellationToken);

        if (image is null)
            return NotFound("Image not found.");

        var deletedImageUrl = image.ImageUrl;
        var wasMainImage = image.IsMain || product.ImageUrl == image.ImageUrl;

        var physicalFilePath = GetPhysicalFilePathFromImageUrl(image.ImageUrl);

        _context.ProductImages.Remove(image);
        await _context.SaveChangesAsync(cancellationToken);

        DeletePhysicalFileIfExists(physicalFilePath);

        if (wasMainImage)
        {
            var nextMainImage = await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .OrderBy(x => x.SortOrder)
                .ThenBy(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (nextMainImage is null)
            {
                product.ImageUrl = null;
            }
            else
            {
                nextMainImage.IsMain = true;
                product.ImageUrl = nextMainImage.ImageUrl;
            }
        }
        else if (product.ImageUrl == deletedImageUrl)
        {
            product.ImageUrl = null;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    private string GetWebRootPath()
    {
        if (!string.IsNullOrWhiteSpace(_environment.WebRootPath))
            return _environment.WebRootPath;

        return Path.Combine(_environment.ContentRootPath, "wwwroot");
    }

    private string GetPhysicalFilePathFromImageUrl(string? imageUrl)
    {
        var root = GetWebRootPath();

        if (string.IsNullOrWhiteSpace(imageUrl))
            return string.Empty;

        var relativePath = imageUrl
            .TrimStart('/')
            .Replace('/', Path.DirectorySeparatorChar);

        return Path.Combine(root, relativePath);
    }

    private static void DeletePhysicalFileIfExists(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            return;

        if (!System.IO.File.Exists(filePath))
            return;

        System.IO.File.Delete(filePath);
    }
}
namespace Lilys_CM.Application.Common.EmailTemplates;

public static class AuthEmailTemplates
{
    public static string BuildResetPasswordEmail(string resetUrl, string appName, int expiryMinutes)
    {
        return $"""
<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8" />
  <meta name="viewport" content="width=device-width, initial-scale=1.0" />
  <title>Reset your password</title>
</head>
<body style="margin:0; padding:0; background-color:#f4f4f7; font-family:Arial,Helvetica,sans-serif; color:#1f2937;">
  <table role="presentation" cellpadding="0" cellspacing="0" border="0" width="100%" style="background-color:#f4f4f7; margin:0; padding:24px 0;">
    <tr>
      <td align="center">
        <table role="presentation" cellpadding="0" cellspacing="0" border="0" width="100%" style="max-width:600px; background-color:#ffffff; border-radius:16px; overflow:hidden; box-shadow:0 8px 24px rgba(0,0,0,0.08);">
          
          <tr>
            <td style="background:linear-gradient(135deg,#7c3aed,#2563eb); padding:32px 24px; text-align:center;">
              <h1 style="margin:0; font-size:28px; line-height:36px; color:#ffffff; font-weight:700;">
                {appName}
              </h1>
              <p style="margin:10px 0 0 0; font-size:15px; line-height:22px; color:#e9ddff;">
                Password reset request
              </p>
            </td>
          </tr>

          <tr>
            <td style="padding:36px 32px 24px 32px;">
              <h2 style="margin:0 0 16px 0; font-size:24px; line-height:32px; color:#111827;">
                Reset your password
              </h2>

              <p style="margin:0 0 16px 0; font-size:16px; line-height:26px; color:#374151;">
                We received a request to reset the password for your account.
              </p>

              <p style="margin:0 0 24px 0; font-size:16px; line-height:26px; color:#374151;">
                Click the button below to choose a new password. This link will expire in <strong>{expiryMinutes} minutes</strong>.
              </p>

              <table role="presentation" cellpadding="0" cellspacing="0" border="0" style="margin:0 0 28px 0;">
                <tr>
                  <td align="center" bgcolor="#2563eb" style="border-radius:10px;">
                    <a href="{resetUrl}"
                       style="display:inline-block; padding:14px 28px; font-size:16px; font-weight:700; color:#ffffff; text-decoration:none; border-radius:10px;">
                      Reset password
                    </a>
                  </td>
                </tr>
              </table>

              <p style="margin:0 0 10px 0; font-size:14px; line-height:22px; color:#6b7280;">
                If the button does not work, copy and paste this link into your browser:
              </p>

              <p style="margin:0 0 24px 0; font-size:14px; line-height:22px; word-break:break-word;">
                <a href="{resetUrl}" style="color:#2563eb; text-decoration:underline;">{resetUrl}</a>
              </p>

              <div style="margin:24px 0; padding:16px; background-color:#f9fafb; border-left:4px solid #7c3aed; border-radius:8px;">
                <p style="margin:0; font-size:14px; line-height:22px; color:#4b5563;">
                  If you did not request a password reset, you can safely ignore this email. Your password will remain unchanged.
                </p>
              </div>

              <p style="margin:0; font-size:13px; line-height:21px; color:#9ca3af;">
                This is an automated message from {appName}. Please do not reply directly to this email.
              </p>
            </td>
          </tr>

          <tr>
            <td style="padding:20px 32px; background-color:#f9fafb; text-align:center;">
              <p style="margin:0; font-size:12px; line-height:18px; color:#9ca3af;">
                © {DateTime.UtcNow.Year} {appName}. All rights reserved.
              </p>
            </td>
          </tr>

        </table>
      </td>
    </tr>
  </table>
</body>
</html>
""";
    }
}
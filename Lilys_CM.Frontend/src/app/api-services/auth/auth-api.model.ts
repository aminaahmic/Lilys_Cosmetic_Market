export interface LoginCommand {
  email: string;
  password: string;
  fingerprint?: string | null;
}

export interface LoginCommandDto {
  accessToken: string;
  refreshToken: string;
  expiresAtUtc: string;
}

export interface RegisterCommand {
  name: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface RegisterCommandDto {
  userId: number;
  name: string;
  email: string;
  message: string;
}

export interface ForgotPasswordCommand {
  email: string;
}

export interface ForgotPasswordCommandDto {
  message: string;
  resetToken?: string | null;
}

export interface ResetPasswordCommand {
  token: string;
  newPassword: string;
  confirmPassword: string;
}

export interface ResetPasswordCommandDto {
  message: string;
}

export interface RefreshTokenCommand {
  refreshToken: string;
  fingerprint?: string | null;
}

export interface RefreshTokenCommandDto {
  accessToken: string;
  refreshToken: string;
  accessTokenExpiresAtUtc: string;
  refreshTokenExpiresAtUtc: string;
}

export interface LogoutCommand {
  refreshToken: string;
}
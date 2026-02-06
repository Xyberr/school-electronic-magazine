export interface UserCredentials {
  login: string;
  password: string;
}

export interface LoginData {
  token: string,
  refreshToken: string,
  role: string[]
}
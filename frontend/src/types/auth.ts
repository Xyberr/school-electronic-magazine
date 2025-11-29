export interface TokenPayload {
  nameid: string;
  iat: number;
  nbf: number;
  exp: number;
  iss: string;
  aud: string;
}

export interface UserCredentials {
  login: string;
  password: string;
}

export interface LoginData {
  token: string,
  refreshToken: string,
  role: []
}
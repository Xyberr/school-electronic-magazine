import { jwtDecode } from "jwt-decode";
import { type JwtPayload } from "jwt-decode";

interface TokenPayload extends JwtPayload {
  nameid: string;
}

export function getJwtPayload(token: string | null): TokenPayload | null {
  if (!token) return null;

  try {
    const jwt = jwtDecode<TokenPayload>(token);

    return jwt;
  } catch {
    return null;
  }
}
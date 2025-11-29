import type { TokenPayload } from "@/types/auth";
import { jwtDecode } from "jwt-decode";

export function getJwtPayload(token: string | null): TokenPayload | null {
  if (!token) return null;

  try {
    const jwt = jwtDecode<TokenPayload>(token);

    return jwt;
  } catch {
    return null;
  }
}
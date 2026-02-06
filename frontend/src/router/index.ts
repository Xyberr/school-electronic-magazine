import { getJwtPayload } from "@/utils/jwt";
import { createRouter, createWebHistory } from "vue-router";
import { routes, handleHotUpdate } from "vue-router/auto-routes";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

if (import.meta.hot) {
  handleHotUpdate(router);
}

// public routes that do NOT require authentication
// /login is handled separately because it has special logic
const PUBLIC_PATHS = new Set(["/template"])

// auth check
router.beforeEach((to) => {
  if (to.name === "/[...unknown]") {
    return;
  }

  const token = localStorage.getItem("token");

  if (to.path == "/login") {
    if (!token) {
      return;
    }

    const decoded = getJwtPayload(token);
    if (decoded === null) {
      localStorage.removeItem("token");
      return "/login";
    }

    const expired = decoded.exp * 1000 < Date.now();
    if (expired) {
      localStorage.removeItem("token");
      return "/login?reason=expired";
    }

    return "/private";
  }

  if (PUBLIC_PATHS.has(to.path)) {
    return;
  }

  if (!token) {
    return "/login";
  }

  const decoded = getJwtPayload(token);
  if (decoded === null) {
    localStorage.removeItem("token");
    return "/login";
  }

  const expired = decoded.exp * 1000 < Date.now();
  if (expired) {
    localStorage.removeItem("token");
    return "/login?reason=expired";
  }
});

export default router;

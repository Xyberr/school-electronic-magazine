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

// auth check
router.beforeEach((to) => {
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

  if (!to.meta.requiresAuth) {
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

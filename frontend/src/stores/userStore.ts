import { computed, reactive, watch } from "vue";
import { createGlobalState, useAsyncState, useLocalStorage } from "@vueuse/core";
import { getJwtPayload } from "@/utils/jwt";
import { postApiAuthLogin } from "@/heyapi";
import { useRouter } from "vue-router";
import type { LoginData, UserCredentials } from "@/types/auth";

export const useUserStore = createGlobalState(() => {
  const token = useLocalStorage<string | null>("token", null)
  const router = useRouter()

  const jwtPayload = computed(() => getJwtPayload(token.value))

  watch(token, () => {
    if (!token.value) logOut()
  },
  { 
    immediate: true 
  });

  const { isLoading: isLoginLoading, execute: loginAsync } = useAsyncState(
  (credentials: UserCredentials) => postApiAuthLogin({
    body: {
      login: credentials.login,
      password: credentials.password
    }
  }),
  null,
    {
      resetOnExecute: false,
      shallow: false,
      immediate: false,
      throwError: true,
      onSuccess(res) {
        const data = res?.data as LoginData
        token.value = data.token
        router.push("/private");
      },
    },
  )

  function logOut() {
    token.value = null
  }

  return reactive({
    token,
    jwtPayload,
    isLoginLoading,
    loginAsync,
    logOut,
  })
});
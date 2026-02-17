import { computed, reactive, watch } from "vue";
import { createGlobalState, useAsyncState, useLocalStorage } from "@vueuse/core";
import { getJwtPayload } from "@/utils/jwt";
import { postApiAuthLogin } from "@/heyapi";
import { useRouter } from "vue-router";
import type { LoginData, UserCredentials } from "@/types/auth";
import { useToast } from 'primevue/usetoast';

export const useUserStore = createGlobalState(() => {
  const token = useLocalStorage<string | null>("token", null)
  const router = useRouter()
  const toast = useToast()

  const jwtPayload = computed(() => getJwtPayload(token.value))

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

        if (!data || !data.token) {
          throw new Error("Invalid login or password");
        }

        token.value = data.token
        router.push("/private");
      },
    },
  )

  function logOut(reason?: string) {
    token.value = null
    router.push('/login')
    if (reason) {
      toast.add({severity:'info', summary: 'Logged out', detail: reason, life: 10000})
      return
    }
  }

  function expireSession() {
    token.value = null

    toast.add({
      severity: 'info',
      summary: 'Logged out',
      detail: 'Session has expired',
      life: 10000,
    })
  }

  return reactive({
    token,
    jwtPayload,
    isLoginLoading,
    loginAsync,
    logOut,
    expireSession,
  })
});
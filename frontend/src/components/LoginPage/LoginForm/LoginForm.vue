<script setup lang="ts">
import Card from 'primevue/card';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import Password from 'primevue/password';
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { postApiAuthLogin } from '@/heyapi';
import * as z from "zod";

interface loginData {
  token: string,
  refreshToken: string,
  role: []
}

const UserScheme = z.object({
  login: z
    .string()
    .min(5, "Invalid login length")
    .max(32, "Invalid login length"),
  password: z
    .string()
    .min(5, "Invalid password length")
    .max(64, "Invalid password length"),
})

const login = ref("")
const password = ref("")
const { logIn } = useUserStore()
const router = useRouter()

const loginError = ref("")
const passwordError = ref("")
const serverError = ref("")

const onBtnLogin = async () => {
  loginError.value = ""
  passwordError.value = ""
  serverError.value = ""

  const validation = UserScheme.safeParse({
    login: login.value,
    password: password.value
  })

  if (!validation.success) {
    const issues = z.treeifyError(validation.error)

    loginError.value = issues.properties?.login?.errors[0] || ""
    passwordError.value = issues.properties?.password?.errors[0] || ""

    return
  }

  // sending req if fields are fine
  try {
    const res = await postApiAuthLogin({
      body: {
        login: login.value,
        password: password.value
      }
    })

    if (res.response.status === 200) {
      const data = res.data as loginData
      logIn(login.value, data.token)
      router.push('/private')
    } else {
      serverError.value = `Server error: ${res.response.status}`
    }
  } catch (error) {
    serverError.value = `Server error: ${error}`
  }
}
</script>

<template>
  <Card class="loginForm">
    <template #content>
      <div class="formContent">
        <h1>Login Form</h1>

        <InputText type="text" placeholder="Login" v-model="login" :invalid="!!loginError" />

        <p v-if="loginError" class="error">{{ loginError }}</p>

        <Password :feedback="false" toggleMask placeholder="Password" v-model="password" :invalid="!!passwordError" />

        <p v-if="passwordError" class="error">{{ passwordError }}</p>

        <p v-if="serverError" class="error">{{ serverError }}</p>

        <Button label="Log in" class="loginButton" @click="onBtnLogin" />

        <!-- <RouterLink to="/" class="link">
          Forgot password?
        </RouterLink> -->
      </div>
    </template>
  </Card>
</template>

<style scoped>
.loginForm {
  width: fit-content;
  max-width: 295px;
}

.formContent {
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.loginButton {
  margin-top: 8px;
}

.error {
  color: #ff0000;
}
</style>

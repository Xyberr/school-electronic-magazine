<script setup lang="ts">
import Card from 'primevue/card';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import Password from 'primevue/password';
import { ref } from 'vue';
import * as z from 'zod';
import { useUserStore } from '@/stores/userStore';

const userStore = useUserStore();

const UserScheme = z.object({
  login: z
    .string()
    .min(5, 'Invalid login length')
    .max(32, 'Invalid login length'),
  password: z
    .string()
    .min(5, 'Invalid password length')
    .max(64, 'Invalid password length'),
})

const login = ref('')
const password = ref('')

const loginError = ref('')
const passwordError = ref('')
const serverError = ref('')

const onBtnLogin = async () => {
  loginError.value = ''
  passwordError.value = ''
  serverError.value = ''

  const validation = UserScheme.safeParse({
    login: login.value,
    password: password.value
  })

  if (!validation.success) {
    const issues = z.treeifyError(validation.error)

    loginError.value = issues.properties?.login?.errors[0] || ''
    passwordError.value = issues.properties?.password?.errors[0] || ''

    return
  }

  // sending req if fields are fine
  try {
    await userStore.loginAsync(0, {
      login: login.value,
      password: password.value
    });
  } catch (error) {
    let message = 'Unknown error'

    if (error instanceof Error) {
      message = error.message
    } else if (typeof error === 'string') {
      message = error
    } else {
      try {
        message = JSON.stringify(error)
      } catch {
        message = String(error)
      }
    }

    serverError.value = `${message}`
  }
}
</script>

<template>
  <Card class="loginForm">
    <template #content>
      <form class="formContent" @submit.prevent="onBtnLogin">
        <h1>Login Form</h1>

        <InputText type="text" placeholder="Login" v-model="login" :invalid="!!loginError" :disabled="userStore.isLoginLoading"/>

        <p v-if="loginError" class="error">{{ loginError }}</p>

        <Password :feedback="false" toggleMask placeholder="Password" v-model="password" :invalid="!!passwordError" :disabled="userStore.isLoginLoading"/>

        <p v-if="passwordError" class="error">{{ passwordError }}</p>

        <p v-if="serverError" class="error">{{ serverError }}</p>

        <Button label="Log in" class="loginButton" type="submit" :disabled="userStore.isLoginLoading" />

        <!-- <RouterLink to="/" class="link">
          Forgot password?
        </RouterLink> -->
      </form>
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

<script setup lang="ts">
import Card from 'primevue/card';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import Password from 'primevue/password';
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { postApiAuthLogin } from '@/heyapi';

interface loginData {
  token: string,
  refreshToken: string,
  role: []
}

const login = ref("")
const password = ref("")
const { logIn } = useUserStore()
const router = useRouter()

const loginError = ref(false)
const passwordError = ref(false)
const serverError = ref("")

const onBtnLogin = async () => {
  loginError.value = false
  passwordError.value = false
  serverError.value = ""

  // validating form
  if (!login.value) loginError.value = true
  if (!password.value) passwordError.value = true
  if (loginError.value || passwordError.value) return

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

        <InputText type="text" placeholder="Login" v-model="login" :invalid="loginError" />

        <Password :feedback="false" toggleMask placeholder="Password" v-model="password" :invalid="passwordError" />

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

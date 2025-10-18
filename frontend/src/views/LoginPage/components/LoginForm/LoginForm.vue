<script setup>
import Card from 'primevue/card';
import InputText from 'primevue/inputtext';
import Button from 'primevue/button';
import Password from 'primevue/password';
import { useRouter } from 'vue-router';
import { ref } from 'vue';
import { useUserStore } from '@/stores/userStore';
import { onLogin } from '@/API/authAPI';

const login = ref("")
const password = ref("")
const userStore = useUserStore()
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
    const res = await onLogin(login.value, password.value)
    if (res.status === 200) {
      const { user, token } = res.data
      userStore.logIn(user, token)
      router.push('privateRoute')
    } else {
      serverError.value = res.data.message
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

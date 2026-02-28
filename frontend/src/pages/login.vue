<script setup lang="ts">
import { onMounted } from 'vue';
import LoginForm from '../components/LoginPage/LoginForm.vue'
import { useRoute } from 'vue-router'
import { useUserStore } from '@/stores/userStore'

const route = useRoute()
const userStore = useUserStore()

onMounted(() => {
  if (route.query.reason === 'expired') {
    userStore.expireSession()
  }
})

definePage({
  meta: {
    layout: 'main',
  },
})
</script>

<template>
  <div class="loginPage">
    <LoginForm />
  </div>
</template>

<style scoped>
.loginPage {
  display: flex;
  align-items: center;
  justify-content: center;
  height: 100vh;
}

.content {
  max-width: 300px;
}
</style>

<script setup lang="ts">
import { useUserStore } from '@/stores/userStore';
import { useRouter } from 'vue-router';

const { logOut } = useUserStore()
const router = useRouter()

const onLogOut = () => {
  logOut()
  router.push('/')
}

definePage({
  meta: {
    requiresAuth: true,
  },
})

</script>

<template>
  <h1>Private Route</h1>
  <p>Just a template for the future</p>
  <button @click="onLogOut">Log Out</button>
</template>

<style scoped></style>

<script setup>
import { onLogout } from '@/API/authAPI';
import { useUserStore } from '@/stores/userStore';
import { useRouter } from 'vue-router';

const { logOut } = useUserStore()
const router = useRouter()

const onBtnLogOut = async () => {
  try {
    const res = await onLogout()

    if (res.status === 200) {
      logOut()
      router.push('/')
    }
  } catch (error) {
    console.log(error)
  }
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
  <button @click="onBtnLogOut">Log Out</button>
</template>

<style scoped></style>

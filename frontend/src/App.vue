<script setup>
import { RouterView, useRouter } from 'vue-router';
import { useUserStore } from '@/stores/userStore';
import { watch } from 'vue';

// tryin to auth with token if we have one
const userStore = useUserStore()
const router = useRouter()
userStore.checkAuth()

// use () => userStore.isLogin instead of userStore.isLogin
// because https://ru.vuejs.org/guide/essentials/watchers#watch-source-types
watch(
  () => userStore.isLogin,
  (val) => {
    if (val) {
      router.push('/privateRoute')
    }
  },
  { immediate: true }
)
</script>

<template>
  <RouterView />
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&display=swap');

* {
  padding: 0;
  margin: 0;
  font-family: "Montserrat", sans-serif;
}

body {
  background-color: #eeeeee;
}

.link {
  text-decoration: none;
  color: #6366f1;
}
</style>

<script setup lang="ts">
import Button from 'primevue/button';
import { useRouter } from 'vue-router';

const router = useRouter()
const onLeave = () => {
  router.push("/login")
}

definePage({
  meta: {
    layout: 'main',
  },
})
</script>

<template>
  <div class="page404">
    <h1>Error 404</h1>
    <Button label="Leave" @click="onLeave"/>
  </div>
</template>

<style scoped>
  .page404 {
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
    flex-direction: column;
    gap: 8px;
  }
</style>

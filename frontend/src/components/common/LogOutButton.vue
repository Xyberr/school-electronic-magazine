<script setup>
import { useUserStore } from '@/stores/userStore';
import Button from 'primevue/button';

const userStore = useUserStore()

const onLogOut = () => {
  userStore.logOut()
}
</script>

<template>
   <Button icon="pi pi-sign-out" label="Log Out" @click="onLogOut" />
</template>
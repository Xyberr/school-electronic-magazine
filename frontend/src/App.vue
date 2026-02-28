<script setup lang="ts">
import { useRoute } from 'vue-router';
import Toast from 'primevue/toast';
import { computed } from 'vue';
import MainLayout from './layouts/MainLayout.vue';
import AuthLayout from './layouts/AuthLayout.vue';

const route = useRoute()

const layoutComponent = computed(() => {
  const layout = route.meta.layout

  if (layout === 'main') {
    return MainLayout
  }

  return AuthLayout
})
</script>

<template>
  <component :is="layoutComponent">
    <RouterView />
  </component>
  <Toast position="bottom-right" />
</template>

<style>
@import url('https://fonts.googleapis.com/css2?family=Montserrat:ital,wght@0,100..900;1,100..900&display=swap');
@import 'primeicons/primeicons.css';

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

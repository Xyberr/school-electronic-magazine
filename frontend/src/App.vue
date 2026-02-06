<script setup lang="ts">
import { RouterView } from 'vue-router';
import Toast from 'primevue/toast';
</script>

<template>
  <RouterView />
  <Toast position="bottom-right" />
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

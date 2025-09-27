<script setup>
import Button from 'primevue/button';

const onClick = () => {
  console.log("clicked")
}
</script>

<template>
  <Button label="Submit" @click="onClick" />
</template>

<style scoped></style>

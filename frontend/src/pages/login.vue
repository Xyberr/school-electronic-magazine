<script setup lang="ts">
import LoginForm from '../components/LoginPage/LoginForm/LoginForm.vue'
import Dialog from 'primevue/dialog';
import { onMounted, ref } from "vue";
import { Button } from 'primevue';
import { useRoute } from 'vue-router';

const visible = ref(false);
const showDialog = () => {
  visible.value = !visible.value
}

onMounted(() => {
  const route = useRoute()
  const reason = route.query.reason

  if (reason === "expired") {
    showDialog()
  }
})
</script>

<template>
  <div class="loginPage">
    <Dialog 
      v-model:visible="visible" 
      modal header="Authorization expired"
    >
      <div class="content">
        <p>Your authorization has expired. Please log in again.</p>
      </div>
      <template #footer >
        <Button label="Okay" @click="showDialog" />
      </template>
    </Dialog>
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

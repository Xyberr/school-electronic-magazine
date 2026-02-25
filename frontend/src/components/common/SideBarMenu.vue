<script setup>
import Button from 'primevue/button';
import Drawer from 'primevue/drawer';
import { useUserStore } from '@/stores/userStore';
import { ref } from 'vue';
import SideBarLinks from './SideBarLinks.vue';

const visible = ref(false);

const { logOut } = useUserStore()

const onLogOut = () => {
  logOut()
}
</script>

<template>
    <div class="sidebar">
        <SideBarLinks />

        <Button 
            icon="pi pi-sign-out" 
            label="Log Out" 
            @click="onLogOut"
        />
    </div>

    <div class="mobileSidebar">
        <Button 
            icon="pi pi-bars"
            variant="text"
            severity="contrast"
            @click="visible = !visible" 
        />
        <Drawer v-model:visible="visible" header="Menu">
            <div class="mobileSideBarContent">
                <SideBarLinks />
    
                <Button icon="pi pi-sign-out" label="Log Out" @click="onLogOut">
                </Button>
            </div>
        </Drawer>

    </div>
</template>

<style>
.sidebar {
    height: 100vh;
    background-color: #fff;
    padding: 16px;
    position: relative;
    width: fit-content;
    min-width: 200px;
    max-width: 300px;
    left: 0;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

.mobileSidebar {
    display: none;
    padding: 16px;
    background-color: #fff;
    height: 100vh;
}

.mobileSideBarContent {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100%;
}

.mobileSidebar .pi-bars {
    cursor: pointer;
}

@media screen and (max-width: 768px) {
    .sidebar {
        display: none;
    }

    .mobileSidebar {
        display: block;
    }
}
</style>
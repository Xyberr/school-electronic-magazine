<script setup>
import Button from 'primevue/button';
import Drawer from 'primevue/drawer';
import { ref } from 'vue';
import SideBarLinks from './SideBarLinks.vue';
import LogOutButton from './LogOutButton.vue';

const visible = ref(false);
</script>

<template>
    <div class="sidebar">
        <SideBarLinks />

        <LogOutButton />
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
    
                <LogOutButton />
            </div>
        </Drawer>

    </div>
</template>

<style scoped>
.sidebar {
    height: 100vh;
    background-color: #fff;
    padding: 16px;
    position: relative;
    width: clamp(200px, 22vw, 300px);
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
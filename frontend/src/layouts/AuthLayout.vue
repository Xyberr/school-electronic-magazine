<script setup>
import SideBarMenu from '@/components/common/SideBarMenu.vue';
import { RouterView } from 'vue-router';
</script>

<template>
    <div class="layout">
        <SideBarMenu />
        <div class="content">
            <slot></slot>
        </div>
    </div>
</template>

<style scoped>
.layout {
    display: flex;
}
</style>
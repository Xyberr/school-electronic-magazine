<script setup>
import SideBarMenu from '@/components/common/SideBarMenu.vue';
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

.content {
    padding: 16px;
}
</style>
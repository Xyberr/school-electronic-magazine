<script setup>
const pages = [
    { name: 'Page 1', path: '/page1' },
    { name: 'Page 2', path: '/page2' },
    { name: 'Page 3', path: '/page3' },
    { name: 'Page 4', path: '/page4' },
]
</script>

<template>
   <div class="pagesList">
        <RouterLink 
            class="pagesListItem" 
            v-for="page in pages" 
            :key="page.name" 
            :to="page.path"
        >
            {{ page.name }}
        </RouterLink>
    </div>
</template>

<style scoped>
.pagesList {
    display: flex;
    flex-direction: column;
    gap: 16px;
}

.pagesListItem {
    text-decoration: none;
    color: #000;
    font-weight: 500;
    width: fit-content;
}
</style>
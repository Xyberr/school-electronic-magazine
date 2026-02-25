<script setup>
</script>

<template>
   <div class="pagesList">
        <RouterLink class="pagesListItem" to="/page1">Page 1</RouterLink>
        <RouterLink class="pagesListItem" to="/page2">Page 2</RouterLink>
        <RouterLink class="pagesListItem" to="/page3">Page 3</RouterLink>
        <RouterLink class="pagesListItem" to="/page4">Page 4</RouterLink>
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
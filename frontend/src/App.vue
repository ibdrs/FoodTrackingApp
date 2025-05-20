<template>
  <div id="app-container">
    <AppNavigation />
    <Toast position="bottom-right" />
    <div class="page-content p-4">
      <router-view />
    </div>
  </div>
</template>

<script setup>
import AppNavigation from './components/AppNavigation.vue';
</script>

<style>
/* Global styles can go in assets/main.css or here */
body {
  font-family: var(--font-family); /* PrimeVue font family */
  margin: 0;
  background-color: #f4f4f4; /* Light background for contrast */
  color: var(--text-color);
}

#app-container {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

.page-content {
  flex-grow: 1;
  max-width: 1200px; /* Max width for content */
  margin: 0 auto; /* Center content */
  width: 100%;
}

/* PrimeVue component overrides or enhancements can go here if needed */
.p-card {
  margin-bottom: 1rem;
}
.p-panel .p-panel-header {
  background-color: var(--primary-color);
  color: var(--primary-color-text);
  border-bottom: 1px solid var(--primary-color-text);
}
</style>
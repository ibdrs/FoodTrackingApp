<template>
    <Menubar :model="items">
      <template #start>
        <div class="flex align-items-center mr-4">
          <i class="pi pi-apple mr-2" style="font-size: 1.5rem; color: var(--primary-color)"></i>
          <span style="font-size: 1.2rem; font-weight: bold;">Food Tracker</span>
        </div>
      </template>
    </Menubar>
  </template>
  
  <script setup>
  import { ref } from 'vue';
  import { useRouter } from 'vue-router';
  
  const router = useRouter();
  
  const items = ref([
    {
      label: 'Overview',
      icon: 'pi pi-fw pi-calendar',
      command: () => router.push('/overview')
    },
    {
      label: 'Add Meal',
      icon: 'pi pi-fw pi-plus-circle',
      command: () => router.push('/add-meal')
    },
    {
      label: 'Preset Meals',
      icon: 'pi pi-fw pi-star',
      command: () => router.push('/preset-meals')
    },
    {
      label: 'History',
      icon: 'pi pi-fw pi-history',
      command: () => router.push('/history')
    }
  ]);
  </script>
  
  <style scoped>
  .p-menubar {
    border-radius: 0;
    border-bottom: 2px solid var(--primary-color);
  }
  </style>
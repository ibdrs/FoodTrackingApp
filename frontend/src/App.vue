<template>
  <div class="app-wrapper">
    <!-- Top Navigation -->
    <header class="app-header">
      <div class="header-content">
        <div class="header-left">
          <Button
            icon="pi pi-bars"
            class="p-button-text menu-toggle"
            @click="toggleSidebar"
            aria-label="Toggle Menu"
          />
          <h1 class="app-title">Food Tracking App</h1>
        </div>
        <div class="header-right">
          <span class="p-input-icon-left search-box">
            <i class="pi pi-search" />
            <InputText v-model="searchQuery" placeholder="Search..." />
          </span>
          <Button
            icon="pi pi-bell"
            class="p-button-text"
            @click="toggleNotifications"
            aria-label="Notifications"
          />
          <Menu ref="userMenu" :model="userMenuItems" :popup="true" />
          <Button
            icon="pi pi-user"
            class="p-button-text"
            @click="toggleUserMenu"
            aria-label="User Menu"
          />
        </div>
      </div>
    </header>

    <!-- Sidebar -->
    <Sidebar
      v-model:visible="sidebarVisible"
      :baseZIndex="1000"
      class="app-sidebar"
    >
      <div class="sidebar-content">
        <nav class="sidebar-nav">
          <router-link
            v-for="item in navigationItems"
            :key="item.path"
            :to="item.path"
            class="nav-item"
            :class="{ active: isActiveRoute(item.path) }"
          >
            <i :class="item.icon"></i>
            <span>{{ item.label }}</span>
          </router-link>
        </nav>
      </div>
    </Sidebar>

    <!-- Main Content -->
    <main class="app-main" :class="{ 'sidebar-visible': sidebarVisible }">
      <div class="app-container">
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </div>
    </main>

    <!-- Toast for notifications -->
    <Toast position="top-right" />

    <!-- Confirmation Dialog -->
    <ConfirmDialog />
  </div>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'

// State
const sidebarVisible = ref(false)
const searchQuery = ref('')
const userMenu = ref()

// Navigation items
const navigationItems = [
  { path: '/', label: 'Dashboard', icon: 'pi pi-home' },
  { path: '/food', label: 'Food Database', icon: 'pi pi-list' },
  { path: '/meals', label: 'Meal Tracking', icon: 'pi pi-calendar' },
  { path: '/reports', label: 'Reports', icon: 'pi pi-chart-bar' },
  { path: '/settings', label: 'Settings', icon: 'pi pi-cog' }
]

// User menu items
const userMenuItems = [
  {
    label: 'Profile',
    icon: 'pi pi-user',
    command: () => navigateTo('/profile')
  },
  {
    label: 'Settings',
    icon: 'pi pi-cog',
    command: () => navigateTo('/settings')
  },
  {
    separator: true
  },
  {
    label: 'Sign Out',
    icon: 'pi pi-sign-out',
    command: () => handleSignOut()
  }
]

// Methods
const toggleSidebar = () => {
  sidebarVisible.value = !sidebarVisible.value
}

const toggleUserMenu = (event: Event) => {
  userMenu.value?.toggle(event)
}

const toggleNotifications = () => {
  // Implement notifications panel
}

const isActiveRoute = (path: string) => {
  const route = useRoute()
  return route.path === path
}

const navigateTo = (path: string) => {
  // Implement navigation
}

const handleSignOut = () => {
  // Implement sign out
}
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_variables.scss";
@import "@/assets/styles/_mixins.scss";

.app-wrapper {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.app-header {
  background-color: white;
  border-bottom: 1px solid rgba(0, 0, 0, 0.1);
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: $z-index-fixed;
  height: 64px;

  .header-content {
    @include container;
    @include flex(row, space-between, center);
    height: 100%;
  }

  .header-left {
    @include flex(row, flex-start, center);
    gap: $spacing-md;
  }

  .header-right {
    @include flex(row, flex-end, center);
    gap: $spacing-md;
  }

  .app-title {
    font-size: $font-size-xl;
    margin: 0;
  }

  .search-box {
    width: 300px;
    position: relative;

    .p-input-icon-left {
      width: 100%;

      i.pi-search {
        position: absolute;
        left: -7rem !important;
        top: 50%;
        transform: translateY(-50%);
        z-index: 1;
      }

      input {
        padding-left: 2.5rem;
      }
    }
  }
}

.app-sidebar {
  :deep(.p-sidebar) {
    width: 280px;
    background-color: white;
    border-right: 1px solid rgba(0, 0, 0, 0.1);
  }

  .sidebar-content {
    padding: $spacing-md;
  }

  .sidebar-nav {
    @include flex(column);
    gap: $spacing-sm;
  }

  .nav-item {
    @include flex(row, flex-start, center);
    gap: $spacing-sm;
    padding: $spacing-sm;
    border-radius: $border-radius-md;
    color: $text-color;
    text-decoration: none;
    @include transition;

    &:hover {
      background-color: rgba($primary-color, 0.1);
    }

    &.active {
      background-color: $primary-color;
      color: white;
    }

    i {
      font-size: $font-size-lg;
    }
  }
}

.app-main {
  margin-top: 64px;
  flex: 1;
  padding: $spacing-lg;
  transition: margin-left $transition-base;

  &.sidebar-visible {
    @include responsive(lg) {
      margin-left: 280px;
    }
  }
}

// Responsive adjustments
@include responsive(lg) {
  .menu-toggle {
    display: none;
  }

  .app-sidebar {
    transform: none !important;
    visibility: visible !important;
    position: fixed;
    top: 64px;
    left: 0;
    bottom: 0;
    width: 280px;
  }
}
</style>
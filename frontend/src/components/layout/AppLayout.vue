<template>
  <div class="app-layout">
    <!-- Sidebar -->
    <aside class="sidebar" :class="{ 'sidebar-collapsed': isSidebarCollapsed }">
      <div class="sidebar-header">
        <img src="@/assets/logo.svg" alt="Logo" class="logo" />
        <h1 v-if="!isSidebarCollapsed">Food Tracker</h1>
      </div>

      <nav class="sidebar-nav">
        <router-link to="/" class="nav-item" active-class="active">
          <i class="pi pi-home"></i>
          <span v-if="!isSidebarCollapsed">Dashboard</span>
        </router-link>
        <router-link to="/meals" class="nav-item" active-class="active">
          <i class="pi pi-calendar"></i>
          <span v-if="!isSidebarCollapsed">Meal Tracking</span>
        </router-link>
        <router-link to="/food" class="nav-item" active-class="active">
          <i class="pi pi-list"></i>
          <span v-if="!isSidebarCollapsed">Food Database</span>
        </router-link>
        <router-link to="/reports" class="nav-item" active-class="active">
          <i class="pi pi-chart-bar"></i>
          <span v-if="!isSidebarCollapsed">Reports</span>
        </router-link>
        <router-link to="/profile" class="nav-item" active-class="active">
          <i class="pi pi-user"></i>
          <span v-if="!isSidebarCollapsed">Profile</span>
        </router-link>
      </nav>

      <div class="sidebar-footer">
        <Button
          icon="pi pi-cog"
          class="p-button-text"
          @click="router.push('/settings')"
        />
        <Button
          icon="pi pi-sign-out"
          class="p-button-text"
          @click="handleLogout"
        />
      </div>
    </aside>

    <!-- Main Content -->
    <div class="main-content">
      <!-- Header -->
      <header class="header">
        <Button
          icon="pi pi-bars"
          class="p-button-text sidebar-toggle"
          @click="toggleSidebar"
        />
        <div class="header-search">
          <span class="p-input-icon-left">
            <i class="pi pi-search" />
            <InputText
              v-model="searchQuery"
              placeholder="Search..."
              class="p-inputtext-sm"
            />
          </span>
        </div>
        <div class="header-actions">
          <Button
            icon="pi pi-bell"
            class="p-button-text"
            @click="showNotifications"
          />
          <Button
            icon="pi pi-plus"
            label="Add Meal"
            @click="showAddMealDialog"
          />
        </div>
      </header>

      <!-- Page Content -->
      <main class="content">
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </main>
    </div>

    <!-- Toast for notifications -->
    <Toast position="top-right" />
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'
import { useToast } from 'primevue/usetoast'

const router = useRouter()
const toast = useToast()

const isSidebarCollapsed = ref(false)
const searchQuery = ref('')

const toggleSidebar = () => {
  isSidebarCollapsed.value = !isSidebarCollapsed.value
}

const handleLogout = () => {
  // TODO: Implement logout logic
  localStorage.removeItem('token')
  router.push('/auth/login')
  toast.add({
    severity: 'success',
    summary: 'Success',
    detail: 'Logged out successfully',
    life: 3000
  })
}

const showNotifications = () => {
  // TODO: Implement notifications panel
  toast.add({
    severity: 'info',
    summary: 'Notifications',
    detail: 'No new notifications',
    life: 3000
  })
}

const showAddMealDialog = () => {
  router.push('/meals')
}
</script>

<style lang="scss" scoped>
.app-layout {
  @include flex(row, flex-start, stretch);
  min-height: 100vh;
}

.sidebar {
  width: 280px;
  background-color: $surface-ground;
  border-right: 1px solid $surface-border;
  @include flex(column, space-between, stretch);
  transition: width 0.3s ease;
  padding: $spacing-md;

  &.sidebar-collapsed {
    width: 80px;

    .sidebar-header {
      h1 {
        display: none;
      }
    }
  }

  .sidebar-header {
    @include flex(row, flex-start, center);
    gap: $spacing-sm;
    padding: $spacing-sm 0;
    margin-bottom: $spacing-lg;

    .logo {
      width: 32px;
      height: 32px;
    }

    h1 {
      font-size: $font-size-lg;
      margin: 0;
      color: $primary-color;
    }
  }

  .sidebar-nav {
    @include flex(column, flex-start, stretch);
    gap: $spacing-xs;

    .nav-item {
      @include flex(row, flex-start, center);
      gap: $spacing-sm;
      padding: $spacing-sm;
      color: $text-color;
      text-decoration: none;
      border-radius: $border-radius-sm;
      transition: background-color 0.2s ease;

      i {
        font-size: $font-size-lg;
      }

      &:hover {
        background-color: $surface-hover;
      }

      &.active {
        background-color: $primary-color;
        color: white;
      }
    }
  }

  .sidebar-footer {
    @include flex(row, space-between, center);
    padding-top: $spacing-md;
    border-top: 1px solid $surface-border;
  }
}

.main-content {
  flex: 1;
  @include flex(column, flex-start, stretch);
  background-color: $surface-ground;
}

.header {
  height: 64px;
  background-color: $surface-card;
  border-bottom: 1px solid $surface-border;
  @include flex(row, space-between, center);
  padding: 0 $spacing-lg;

  .header-search {
    flex: 1;
    max-width: 400px;
    margin: 0 $spacing-lg;
  }

  .header-actions {
    @include flex(row, flex-end, center);
    gap: $spacing-sm;
  }
}

.content {
  flex: 1;
  padding: $spacing-lg;
  overflow-y: auto;
}

// Transitions
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style> 
<template>
  <div class="auth-layout">
    <div class="auth-container">
      <div class="auth-content">
        <div class="auth-header">
          <h1 class="auth-title">Food Tracking App</h1>
          <p class="auth-subtitle">Track your nutrition, achieve your goals</p>
        </div>
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component" />
          </transition>
        </router-view>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// No additional setup needed for the layout
</script>

<style lang="scss" scoped>
.auth-layout {
  min-height: 100vh;
  @include flex(row, center, center);
  background: linear-gradient(135deg, $primary-color 0%, darken($primary-color, 15%) 100%);
}

.auth-container {
  width: 100%;
  max-width: 400px;
  padding: $spacing-lg;
}

.auth-content {
  background-color: white;
  border-radius: $border-radius-lg;
  padding: $spacing-xl;
  @include shadow(2);
}

.auth-header {
  text-align: center;
  margin-bottom: $spacing-xl;
}

.auth-title {
  color: $primary-color;
  margin-bottom: $spacing-xs;
}

.auth-subtitle {
  color: $secondary-color;
  font-size: $font-size-sm;
}
</style> 
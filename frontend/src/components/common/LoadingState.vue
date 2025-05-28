<template>
  <div class="loading-state" :class="{ overlay: isOverlay }">
    <div class="spinner">
      <i class="pi pi-spin pi-spinner" />
    </div>
    <p v-if="message" class="message">{{ message }}</p>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  message?: string
  isOverlay?: boolean
}>()
</script>

<style lang="scss" scoped>
.loading-state {
  @include flex(column, center, center);
  gap: $spacing-md;
  padding: $spacing-xl;
  min-height: 200px;

  &.overlay {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-color: rgba($surface-ground, 0.8);
    backdrop-filter: blur(4px);
    z-index: 1000;
  }

  .spinner {
    @include flex(row, center, center);
    width: 48px;
    height: 48px;

    i {
      font-size: 32px;
      color: $primary-color;
    }
  }

  .message {
    margin: 0;
    color: $text-color;
    font-size: $font-size-md;
  }
}
</style> 
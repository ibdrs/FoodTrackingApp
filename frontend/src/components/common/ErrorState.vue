<template>
  <div class="error-state">
    <div class="icon">
      <i class="pi pi-exclamation-triangle" />
    </div>
    <h3 class="title">{{ title }}</h3>
    <p class="message">{{ message }}</p>
    <div class="actions" v-if="$slots.actions">
      <slot name="actions" />
    </div>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  title: string
  message: string
}>()
</script>

<style lang="scss" scoped>
.error-state {
  @include flex(column, center, center);
  gap: $spacing-md;
  padding: $spacing-xl;
  text-align: center;
  background-color: $surface-ground;
  border-radius: $border-radius-lg;
  min-height: 300px;

  .icon {
    @include flex(row, center, center);
    width: 80px;
    height: 80px;
    background-color: $surface-card;
    border-radius: 50%;
    margin-bottom: $spacing-sm;

    i {
      font-size: 32px;
      color: $error-color;
    }
  }

  .title {
    margin: 0;
    font-size: $font-size-xl;
    color: $text-color;
  }

  .message {
    margin: 0;
    color: $secondary-color;
    font-size: $font-size-md;
    max-width: 400px;
  }

  .actions {
    @include flex(row, center, center);
    gap: $spacing-sm;
    margin-top: $spacing-md;
  }
}
</style> 
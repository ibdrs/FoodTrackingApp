<template>
  <div class="page-header">
    <div class="header-content">
      <div class="title-section">
        <h1 class="title">{{ title }}</h1>
        <p v-if="subtitle" class="subtitle">{{ subtitle }}</p>
      </div>

      <div class="actions" v-if="$slots.actions">
        <slot name="actions" />
      </div>
    </div>

    <div class="header-extra" v-if="$slots.extra">
      <slot name="extra" />
    </div>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  title: string
  subtitle?: string
}>()
</script>

<style lang="scss" scoped>
.page-header {
  @include flex(column, flex-start, stretch);
  gap: $spacing-lg;
  margin-bottom: $spacing-xl;

  .header-content {
    @include flex(row, space-between, flex-start);
    gap: $spacing-md;

    .title-section {
      .title {
        margin: 0;
        font-size: $font-size-2xl;
        color: $text-color;
        font-weight: 600;
      }

      .subtitle {
        margin: $spacing-xs 0 0;
        color: $secondary-color;
        font-size: $font-size-md;
      }
    }

    .actions {
      @include flex(row, flex-end, center);
      gap: $spacing-sm;
    }
  }

  .header-extra {
    @include flex(row, flex-start, center);
    gap: $spacing-md;
  }
}
</style> 
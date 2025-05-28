<template>
  <div class="card" :class="cardClass">
    <div v-if="$slots.header || title" class="card-header">
      <slot name="header">
        <h3 class="title">{{ title }}</h3>
      </slot>
    </div>

    <div class="card-content">
      <slot />
    </div>

    <div v-if="$slots.footer" class="card-footer">
      <slot name="footer" />
    </div>
  </div>
</template>

<script setup lang="ts">
defineProps<{
  title?: string
  cardClass?: string
}>()
</script>

<style lang="scss" scoped>
.card {
  background-color: $surface-card;
  border-radius: $border-radius-md;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);

  .card-header {
    padding: $spacing-md;
    border-bottom: 1px solid $surface-border;

    .title {
      margin: 0;
      font-size: $font-size-lg;
      color: $text-color;
      font-weight: 600;
    }
  }

  .card-content {
    padding: $spacing-md;
  }

  .card-footer {
    padding: $spacing-md;
    border-top: 1px solid $surface-border;
  }

  &.no-padding {
    .card-content {
      padding: 0;
    }
  }

  &.no-header {
    .card-content {
      border-top-left-radius: $border-radius-md;
      border-top-right-radius: $border-radius-md;
    }
  }

  &.no-footer {
    .card-content {
      border-bottom-left-radius: $border-radius-md;
      border-bottom-right-radius: $border-radius-md;
    }
  }

  &.hoverable {
    transition: transform 0.2s ease, box-shadow 0.2s ease;

    &:hover {
      transform: translateY(-2px);
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
    }
  }

  &.bordered {
    border: 1px solid $surface-border;
  }

  &.flat {
    box-shadow: none;
  }

  &.elevated {
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  }
}
</style> 
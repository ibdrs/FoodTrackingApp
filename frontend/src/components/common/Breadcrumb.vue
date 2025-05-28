<template>
  <div class="breadcrumb">
    <ol class="breadcrumb-list">
      <li
        v-for="(item, index) in items"
        :key="index"
        class="breadcrumb-item"
        :class="{ 'is-active': index === items.length - 1 }"
      >
        <template v-if="index === items.length - 1">
          <span class="item-text">{{ item.label }}</span>
        </template>
        <template v-else>
          <router-link
            v-if="item.to"
            :to="item.to"
            class="item-link"
          >
            {{ item.label }}
          </router-link>
          <span v-else class="item-text">{{ item.label }}</span>
        </template>
      </li>
    </ol>
  </div>
</template>

<script setup lang="ts">
interface BreadcrumbItem {
  label: string
  to?: string
}

defineProps<{
  items: BreadcrumbItem[]
}>()
</script>

<style lang="scss" scoped>
.breadcrumb {
  margin-bottom: $spacing-md;

  .breadcrumb-list {
    @include flex(row, flex-start, center);
    gap: $spacing-xs;
    margin: 0;
    padding: 0;
    list-style: none;

    .breadcrumb-item {
      @include flex(row, flex-start, center);
      color: $secondary-color;
      font-size: $font-size-sm;

      &:not(:last-child)::after {
        content: '/';
        margin-left: $spacing-xs;
        color: $secondary-color;
      }

      &.is-active {
        color: $text-color;
        font-weight: 500;
      }

      .item-link {
        color: $primary-color;
        text-decoration: none;
        transition: color 0.2s ease;

        &:hover {
          color: darken($primary-color, 10%);
        }
      }

      .item-text {
        color: inherit;
      }
    }
  }
}
</style> 
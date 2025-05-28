<template>
  <div class="food-item">
    <div class="food-info">
      <h3 class="food-name">{{ food.name }}</h3>
      <span class="food-category">{{ food.category }}</span>
    </div>

    <div class="food-nutrition">
      <div class="nutrition-item">
        <span class="label">Calories</span>
        <span class="value">{{ food.calories }} kcal</span>
      </div>
      <div class="nutrition-item">
        <span class="label">Protein</span>
        <span class="value">{{ food.protein }}g</span>
      </div>
      <div class="nutrition-item">
        <span class="label">Carbs</span>
        <span class="value">{{ food.carbs }}g</span>
      </div>
      <div class="nutrition-item">
        <span class="label">Fats</span>
        <span class="value">{{ food.fats }}g</span>
      </div>
    </div>

    <div class="food-serving">
      <span class="serving-size">
        {{ food.servingSize }} {{ food.servingUnit }}
      </span>
    </div>

    <div class="food-actions">
      <Button
        icon="pi pi-pencil"
        class="p-button-text"
        @click="$emit('edit', food)"
      />
      <Button
        icon="pi pi-trash"
        class="p-button-text p-button-danger"
        @click="$emit('delete', food)"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import type { Food } from '@/types'

defineProps<{
  food: Food
}>()

defineEmits<{
  (e: 'edit', food: Food): void
  (e: 'delete', food: Food): void
}>()
</script>

<style lang="scss" scoped>
.food-item {
  @include flex(row, space-between, center);
  padding: $spacing-md;
  background-color: $surface-card;
  border-radius: $border-radius-md;
  margin-bottom: $spacing-sm;

  .food-info {
    flex: 1;
    min-width: 0;

    .food-name {
      margin: 0;
      font-size: $font-size-lg;
      color: $text-color;
      @include text-ellipsis;
    }

    .food-category {
      font-size: $font-size-sm;
      color: $secondary-color;
    }
  }

  .food-nutrition {
    @include flex(row, flex-start, center);
    gap: $spacing-lg;
    margin: 0 $spacing-lg;

    .nutrition-item {
      @include flex(column, center, center);
      text-align: center;

      .label {
        font-size: $font-size-sm;
        color: $secondary-color;
      }

      .value {
        font-weight: 600;
        color: $text-color;
      }
    }
  }

  .food-serving {
    margin-right: $spacing-lg;
    color: $secondary-color;
    font-size: $font-size-sm;
  }

  .food-actions {
    @include flex(row, flex-end, center);
    gap: $spacing-xs;
  }
}
</style> 
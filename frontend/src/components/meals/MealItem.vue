<template>
  <div class="meal-item">
    <div class="meal-time">
      {{ formatTime(meal.time) }}
    </div>

    <div class="meal-content">
      <div class="meal-header">
        <h3 class="meal-name">{{ meal.name }}</h3>
        <span class="meal-type">{{ meal.type }}</span>
      </div>

      <div class="meal-nutrition">
        <div class="nutrition-item">
          <span class="label">Calories</span>
          <span class="value">{{ meal.calories }} kcal</span>
        </div>
        <div class="nutrition-item">
          <span class="label">Protein</span>
          <span class="value">{{ meal.protein }}g</span>
        </div>
        <div class="nutrition-item">
          <span class="label">Carbs</span>
          <span class="value">{{ meal.carbs }}g</span>
        </div>
        <div class="nutrition-item">
          <span class="label">Fats</span>
          <span class="value">{{ meal.fats }}g</span>
        </div>
      </div>

      <div class="meal-foods" v-if="meal.foods && meal.foods.length > 0">
        <div
          v-for="food in meal.foods"
          :key="food.id"
          class="food-item"
        >
          <span class="food-name">{{ food.name }}</span>
          <span class="food-quantity">
            {{ food.quantity }} {{ food.unit }}
          </span>
        </div>
      </div>
    </div>

    <div class="meal-actions">
      <Button
        icon="pi pi-pencil"
        class="p-button-text"
        @click="$emit('edit', meal)"
      />
      <Button
        icon="pi pi-trash"
        class="p-button-text p-button-danger"
        @click="$emit('delete', meal)"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'

interface MealFood {
  id: number
  name: string
  quantity: number
  unit: string
}

interface Meal {
  id: number
  name: string
  type: string
  time: Date
  calories: number
  protein: number
  carbs: number
  fats: number
  foods?: MealFood[]
}

const props = defineProps<{
  meal: Meal
}>()

defineEmits<{
  (e: 'edit', meal: Meal): void
  (e: 'delete', meal: Meal): void
}>()

const formatTime = (date: Date) => {
  return new Intl.DateTimeFormat('en-US', {
    hour: '2-digit',
    minute: '2-digit'
  }).format(date)
}
</script>

<style lang="scss" scoped>
.meal-item {
  @include flex(row, flex-start, flex-start);
  gap: $spacing-md;
  padding: $spacing-md;
  background-color: $surface-card;
  border-radius: $border-radius-md;
  margin-bottom: $spacing-sm;

  .meal-time {
    min-width: 80px;
    color: $secondary-color;
    font-size: $font-size-sm;
  }

  .meal-content {
    flex: 1;
    min-width: 0;

    .meal-header {
      @include flex(row, space-between, center);
      margin-bottom: $spacing-xs;

      .meal-name {
        margin: 0;
        font-size: $font-size-lg;
        color: $text-color;
      }

      .meal-type {
        font-size: $font-size-sm;
        color: $secondary-color;
        background-color: $surface-ground;
        padding: $spacing-xs $spacing-sm;
        border-radius: $border-radius-sm;
      }
    }

    .meal-nutrition {
      @include flex(row, flex-start, center);
      gap: $spacing-lg;
      margin-bottom: $spacing-sm;

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

    .meal-foods {
      @include flex(column, flex-start, stretch);
      gap: $spacing-xs;

      .food-item {
        @include flex(row, space-between, center);
        padding: $spacing-xs $spacing-sm;
        background-color: $surface-ground;
        border-radius: $border-radius-sm;

        .food-name {
          color: $text-color;
          font-size: $font-size-sm;
        }

        .food-quantity {
          color: $secondary-color;
          font-size: $font-size-sm;
        }
      }
    }
  }

  .meal-actions {
    @include flex(row, flex-end, center);
    gap: $spacing-xs;
  }
}
</style> 
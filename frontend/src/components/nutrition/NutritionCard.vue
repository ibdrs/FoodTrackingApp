<template>
  <Card class="nutrition-card">
    <template #title>
      <div class="card-title">
        <i :class="icon"></i>
        <span>{{ title }}</span>
      </div>
    </template>
    <template #content>
      <div class="nutrition-content">
        <div class="nutrition-value">
          {{ value }}{{ unit }}
        </div>
        <div class="nutrition-target" v-if="target">
          of {{ target }}{{ unit }}
        </div>
        <ProgressBar
          :value="progress"
          :showValue="false"
          :class="progressClass"
        />
      </div>
    </template>
  </Card>
</template>

<script setup lang="ts">
import { computed } from 'vue'

interface Props {
  title: string
  value: number
  target?: number
  unit: string
  icon: string
  color?: 'primary' | 'success' | 'warning' | 'danger'
}

const props = withDefaults(defineProps<Props>(), {
  color: 'primary'
})

const progress = computed(() => {
  if (!props.target) return 0
  return (props.value / props.target) * 100
})

const progressClass = computed(() => {
  return `p-progressbar-${props.color}`
})
</script>

<style lang="scss" scoped>
.nutrition-card {
  .card-title {
    @include flex(row, flex-start, center);
    gap: $spacing-sm;

    i {
      font-size: $font-size-lg;
      color: $primary-color;
    }
  }

  .nutrition-content {
    text-align: center;

    .nutrition-value {
      font-size: $font-size-xl;
      font-weight: 600;
      color: $primary-color;
    }

    .nutrition-target {
      color: $secondary-color;
      font-size: $font-size-sm;
      margin-bottom: $spacing-sm;
    }
  }
}
</style> 
<template>
  <div class="nutrition-summary">
    <div class="summary-header">
      <h2 class="title">Daily Summary</h2>
      <span class="date">{{ formatDate(date) }}</span>
    </div>

    <div class="summary-cards">
      <div class="summary-card">
        <div class="card-header">
          <i class="pi pi-fire" />
          <span class="label">Calories</span>
        </div>
        <div class="card-content">
          <div class="value">{{ consumed.calories }} / {{ goals.calories }}</div>
          <ProgressBar
            :value="(consumed.calories / goals.calories) * 100"
            :showValue="false"
            class="progress-bar"
          />
        </div>
      </div>

      <div class="summary-card">
        <div class="card-header">
          <i class="pi pi-chart-bar" />
          <span class="label">Protein</span>
        </div>
        <div class="card-content">
          <div class="value">{{ consumed.protein }}g / {{ goals.protein }}g</div>
          <ProgressBar
            :value="(consumed.protein / goals.protein) * 100"
            :showValue="false"
            class="progress-bar"
          />
        </div>
      </div>

      <div class="summary-card">
        <div class="card-header">
          <i class="pi pi-chart-line" />
          <span class="label">Carbs</span>
        </div>
        <div class="card-content">
          <div class="value">{{ consumed.carbs }}g / {{ goals.carbs }}g</div>
          <ProgressBar
            :value="(consumed.carbs / goals.carbs) * 100"
            :showValue="false"
            class="progress-bar"
          />
        </div>
      </div>

      <div class="summary-card">
        <div class="card-header">
          <i class="pi pi-chart-pie" />
          <span class="label">Fats</span>
        </div>
        <div class="card-content">
          <div class="value">{{ consumed.fats }}g / {{ goals.fats }}g</div>
          <ProgressBar
            :value="(consumed.fats / goals.fats) * 100"
            :showValue="false"
            class="progress-bar"
          />
        </div>
      </div>
    </div>

    <div class="macro-distribution">
      <h3>Macro Distribution</h3>
      <Chart
        type="doughnut"
        :data="chartData"
        :options="chartOptions"
        class="chart"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { Chart } from 'primevue/chart'

interface NutritionValues {
  calories: number
  protein: number
  carbs: number
  fats: number
}

const props = defineProps<{
  date: Date
  consumed: NutritionValues
  goals: NutritionValues
}>()

const formatDate = (date: Date) => {
  return new Intl.DateTimeFormat('en-US', {
    weekday: 'long',
    year: 'numeric',
    month: 'long',
    day: 'numeric'
  }).format(date)
}

const chartData = computed(() => ({
  labels: ['Protein', 'Carbs', 'Fats'],
  datasets: [
    {
      data: [
        props.consumed.protein * 4, // 4 calories per gram of protein
        props.consumed.carbs * 4, // 4 calories per gram of carbs
        props.consumed.fats * 9 // 9 calories per gram of fat
      ],
      backgroundColor: ['#4CAF50', '#2196F3', '#FFC107'],
      borderWidth: 0
    }
  ]
}))

const chartOptions = {
  plugins: {
    legend: {
      position: 'bottom' as const,
      labels: {
        usePointStyle: true,
        padding: 20
      }
    }
  },
  cutout: '70%'
}
</script>

<style lang="scss" scoped>
.nutrition-summary {
  @include flex(column, flex-start, stretch);
  gap: $spacing-lg;

  .summary-header {
    @include flex(row, space-between, center);

    .title {
      margin: 0;
      font-size: $font-size-xl;
      color: $text-color;
    }

    .date {
      color: $secondary-color;
      font-size: $font-size-md;
    }
  }

  .summary-cards {
    @include flex(row, space-between, stretch);
    gap: $spacing-md;

    .summary-card {
      flex: 1;
      background-color: $surface-card;
      border-radius: $border-radius-md;
      padding: $spacing-md;

      .card-header {
        @include flex(row, flex-start, center);
        gap: $spacing-sm;
        margin-bottom: $spacing-sm;

        i {
          color: $primary-color;
          font-size: $font-size-lg;
        }

        .label {
          color: $text-color;
          font-weight: 500;
        }
      }

      .card-content {
        @include flex(column, flex-start, stretch);
        gap: $spacing-xs;

        .value {
          color: $text-color;
          font-size: $font-size-lg;
          font-weight: 600;
        }

        .progress-bar {
          height: 8px;
        }
      }
    }
  }

  .macro-distribution {
    @include flex(column, flex-start, center);
    gap: $spacing-md;

    h3 {
      margin: 0;
      color: $text-color;
      font-size: $font-size-lg;
    }

    .chart {
      width: 300px;
      height: 300px;
    }
  }
}
</style> 
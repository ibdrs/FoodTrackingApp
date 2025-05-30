<template>
  <div class="chart-container" :style="{ height }">
    <Chart
      :type="type"
      :data="chartData"
      :options="chartOptions"
      :plugins="plugins"
      :class="chartClass"
    />
  </div>
</template>

<script setup lang="ts">
import { computed } from 'vue'
import { Chart as ChartJS } from 'chart.js'
import { Chart } from 'primevue/chart'

interface ChartProps {
  type: 'line' | 'bar' | 'pie' | 'doughnut' | 'polarArea' | 'radar'
  data: any
  options?: any
  plugins?: any[]
  height?: string
  chartClass?: string
}

const props = withDefaults(defineProps<ChartProps>(), {
  height: '300px',
  chartClass: ''
})

const chartData = computed(() => ({
  ...props.data,
  datasets: props.data.datasets.map((dataset: any) => ({
    ...dataset,
    borderColor: dataset.borderColor || getDefaultColors(props.type),
    backgroundColor: dataset.backgroundColor || getDefaultColors(props.type, true)
  }))
}))

const chartOptions = computed(() => ({
  responsive: true,
  maintainAspectRatio: false,
  plugins: {
    legend: {
      position: 'bottom' as const,
      labels: {
        usePointStyle: true,
        padding: 20
      }
    },
    tooltip: {
      backgroundColor: 'rgba(0, 0, 0, 0.8)',
      padding: 12,
      titleFont: {
        size: 14,
        weight: 'bold'
      },
      bodyFont: {
        size: 13
      },
      displayColors: true,
      boxPadding: 4
    }
  },
  ...props.options
}))

const getDefaultColors = (type: string, isBackground = false) => {
  const colors = {
    line: isBackground ? 'rgba(33, 150, 243, 0.1)' : '#2196F3',
    bar: isBackground ? 'rgba(33, 150, 243, 0.8)' : '#2196F3',
    pie: ['#2196F3', '#4CAF50', '#FFC107', '#F44336', '#9C27B0'],
    doughnut: ['#2196F3', '#4CAF50', '#FFC107', '#F44336', '#9C27B0'],
    polarArea: ['#2196F3', '#4CAF50', '#FFC107', '#F44336', '#9C27B0'],
    radar: isBackground ? 'rgba(33, 150, 243, 0.2)' : '#2196F3'
  }

  return colors[type as keyof typeof colors]
}
</script>

<style lang="scss" scoped>
.chart-container {
  position: relative;
  width: 100%;
  background-color: $surface-card;
  border-radius: $border-radius-md;
  padding: $spacing-md;
}
</style> 
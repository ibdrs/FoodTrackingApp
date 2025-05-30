<template>
  <div class="food-details-view">
    <div class="view-header">
      <Button
        icon="pi pi-arrow-left"
        class="p-button-text"
        @click="router.back()"
      />
      <h1>Food Details</h1>
    </div>

    <div v-if="loading" class="loading-container">
      <i class="pi pi-spin pi-spinner" style="font-size: 2rem"></i>
    </div>

    <div v-else-if="food" class="food-details-content">
      <Card class="food-info-card">
        <template #title>
          <div class="food-header">
            <h2>{{ food.name }}</h2>
            <span class="category-badge">{{ food.category }}</span>
          </div>
        </template>
        <template #content>
          <div class="nutrition-grid">
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

          <div class="serving-info">
            <h3>Serving Information</h3>
            <p>
              Serving Size: {{ food.servingSize }} {{ food.servingUnit }}
            </p>
          </div>

          <div class="macro-distribution">
            <h3>Macro Distribution</h3>
            <Chart type="doughnut" :data="macroChartData" :options="chartOptions" />
          </div>
        </template>
      </Card>

      <div class="action-buttons">
        <Button
          icon="pi pi-pencil"
          label="Edit"
          @click="editFood"
        />
        <Button
          icon="pi pi-trash"
          label="Delete"
          class="p-button-danger"
          @click="confirmDelete"
        />
      </div>
    </div>

    <div v-else class="error-container">
      <i class="pi pi-exclamation-triangle"></i>
      <p>Food item not found</p>
      <Button
        label="Back to Food Database"
        @click="router.push('/food')"
      />
    </div>

    <!-- Confirm Dialog -->
    <ConfirmDialog />
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'
import type { Food } from '@/types'

const route = useRoute()
const router = useRouter()
const toast = useToast()
const confirm = useConfirm()

const loading = ref(true)
const food = ref<Food | null>(null)

// Sample data - replace with actual API call
const fetchFoodDetails = async () => {
  try {
    // TODO: Implement actual API call
    await new Promise(resolve => setTimeout(resolve, 1000))
    
    food.value = {
      id: Number(route.params.id),
      name: 'Chicken Breast',
      category: 'Protein',
      calories: 165,
      protein: 31,
      carbs: 0,
      fats: 3.6,
      servingSize: 100,
      servingUnit: 'g'
    }
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to load food details',
      life: 3000
    })
  } finally {
    loading.value = false
  }
}

// Chart data
const macroChartData = {
  labels: ['Protein', 'Carbs', 'Fats'],
  datasets: [
    {
      data: [31, 0, 3.6],
      backgroundColor: ['#3B82F6', '#10B981', '#F59E0B']
    }
  ]
}

const chartOptions = {
  responsive: true,
  maintainAspectRatio: false
}

const editFood = () => {
  // TODO: Implement edit functionality
  console.log('Edit food:', food.value)
}

const confirmDelete = () => {
  confirm.require({
    message: 'Are you sure you want to delete this food item?',
    header: 'Delete Confirmation',
    icon: 'pi pi-exclamation-triangle',
    accept: () => deleteFood()
  })
}

const deleteFood = async () => {
  try {
    // TODO: Implement actual API call
    await new Promise(resolve => setTimeout(resolve, 1000))
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Food item deleted successfully',
      life: 3000
    })

    router.push('/food')
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to delete food item',
      life: 3000
    })
  }
}

onMounted(() => {
  fetchFoodDetails()
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_variables.scss";
@import "@/assets/styles/_mixins.scss";

.food-details-view {
  .view-header {
    @include flex(row, flex-start, center);
    gap: $spacing-md;
    margin-bottom: $spacing-lg;

    h1 {
      margin: 0;
    }
  }
}

.loading-container {
  @include flex(row, center, center);
  min-height: 400px;
}

.food-details-content {
  .food-info-card {
    margin-bottom: $spacing-lg;

    .food-header {
      @include flex(row, space-between, center);

      h2 {
        margin: 0;
      }

      .category-badge {
        background-color: $primary-color;
        color: white;
        padding: $spacing-xs $spacing-sm;
        border-radius: $border-radius-sm;
        font-size: $font-size-sm;
      }
    }

    .nutrition-grid {
      @include grid(4, $spacing-md);
      margin-bottom: $spacing-lg;

      @include responsive(lg) {
        grid-template-columns: repeat(3, 1fr);
      }

      @include responsive(md) {
        grid-template-columns: repeat(2, 1fr);
      }

      @include responsive(sm) {
        grid-template-columns: 1fr;
      }

      .nutrition-item {
        @include flex(column, center, center);
        text-align: center;
        padding: $spacing-md;
        background-color: $background-color;
        border-radius: $border-radius-md;

        .label {
          color: $secondary-color;
          font-size: $font-size-sm;
          margin-bottom: $spacing-xs;
        }

        .value {
          font-size: $font-size-lg;
          font-weight: 600;
          color: $primary-color;
        }
      }
    }

    .serving-info {
      margin-bottom: $spacing-lg;

      h3 {
        margin-bottom: $spacing-sm;
      }
    }

    .macro-distribution {
      h3 {
        margin-bottom: $spacing-md;
      }

      :deep(.p-chart) {
        height: 300px;
      }
    }
  }

  .action-buttons {
    @include flex(row, flex-start, center);
    gap: $spacing-md;
  }
}

.error-container {
  @include flex(column, center, center);
  min-height: 400px;
  text-align: center;

  i {
    font-size: 3rem;
    color: $danger-color;
    margin-bottom: $spacing-md;
  }

  p {
    color: $secondary-color;
    margin-bottom: $spacing-lg;
  }
}
</style> 
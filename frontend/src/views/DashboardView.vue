<template>
  <div class="dashboard-view">
    <div class="dashboard-header">
      <h1>Dashboard</h1>
      <Button
        icon="pi pi-plus"
        label="Add Meal"
        @click="showAddMealDialog"
      />
    </div>

    <!-- Nutrition Summary Cards -->
    <div class="nutrition-summary">
      <Card class="summary-card">
        <template #title>Daily Calories</template>
        <template #content>
          <div class="summary-content">
            <div class="summary-value">{{ dailyCalories }}</div>
            <div class="summary-target">of {{ calorieGoal }} kcal</div>
            <ProgressBar
              :value="(dailyCalories / calorieGoal) * 100"
              :showValue="false"
            />
          </div>
        </template>
      </Card>

      <Card class="summary-card">
        <template #title>Protein</template>
        <template #content>
          <div class="summary-content">
            <div class="summary-value">{{ dailyProtein }}g</div>
            <div class="summary-target">of {{ proteinGoal }}g</div>
            <ProgressBar
              :value="(dailyProtein / proteinGoal) * 100"
              :showValue="false"
            />
          </div>
        </template>
      </Card>

      <Card class="summary-card">
        <template #title>Carbs</template>
        <template #content>
          <div class="summary-content">
            <div class="summary-value">{{ dailyCarbs }}g</div>
            <div class="summary-target">of {{ carbsGoal }}g</div>
            <ProgressBar
              :value="(dailyCarbs / carbsGoal) * 100"
              :showValue="false"
            />
          </div>
        </template>
      </Card>

      <Card class="summary-card">
        <template #title>Fats</template>
        <template #content>
          <div class="summary-content">
            <div class="summary-value">{{ dailyFats }}g</div>
            <div class="summary-target">of {{ fatsGoal }}g</div>
            <ProgressBar
              :value="(dailyFats / fatsGoal) * 100"
              :showValue="false"
            />
          </div>
        </template>
      </Card>
    </div>

    <!-- Charts Section -->
    <div class="dashboard-charts">
      <Card class="chart-card">
        <template #title>Weekly Nutrition</template>
        <template #content>
          <Chart type="line" :data="weeklyChartData" :options="chartOptions" />
        </template>
      </Card>

      <Card class="chart-card">
        <template #title>Macro Distribution</template>
        <template #content>
          <Chart type="doughnut" :data="macroChartData" :options="chartOptions" />
        </template>
      </Card>
    </div>

    <!-- Recent Meals -->
    <Card class="recent-meals">
      <template #title>Recent Meals</template>
      <template #content>
        <DataTable
          :value="recentMeals"
          :paginator="true"
          :rows="5"
          :rowsPerPageOptions="[5, 10, 20]"
          responsiveLayout="scroll"
        >
          <Column field="time" header="Time" sortable>
            <template #body="{ data }">
              {{ formatTime(data.time) }}
            </template>
          </Column>
          <Column field="name" header="Meal" sortable />
          <Column field="calories" header="Calories" sortable>
            <template #body="{ data }">
              {{ data.calories }} kcal
            </template>
          </Column>
          <Column field="protein" header="Protein" sortable>
            <template #body="{ data }">
              {{ data.protein }}g
            </template>
          </Column>
          <Column field="carbs" header="Carbs" sortable>
            <template #body="{ data }">
              {{ data.carbs }}g
            </template>
          </Column>
          <Column field="fats" header="Fats" sortable>
            <template #body="{ data }">
              {{ data.fats }}g
            </template>
          </Column>
          <Column>
            <template #body="{ data }">
              <Button
                icon="pi pi-pencil"
                class="p-button-text"
                @click="editMeal(data)"
              />
              <Button
                icon="pi pi-trash"
                class="p-button-text p-button-danger"
                @click="confirmDeleteMeal(data)"
              />
            </template>
          </Column>
        </DataTable>
      </template>
    </Card>

    <!-- Add Meal Dialog -->
    <Dialog
      v-model:visible="addMealDialogVisible"
      header="Add Meal"
      :style="{ width: '50vw' }"
      :modal="true"
    >
      <div class="add-meal-form">
        <div class="form-group">
          <label for="mealName">Meal Name</label>
          <InputText
            id="mealName"
            v-model="newMeal.name"
            class="w-full"
            placeholder="Enter meal name"
          />
        </div>

        <div class="form-group">
          <label for="mealTime">Time</label>
          <Calendar
            id="mealTime"
            v-model="newMeal.time"
            showTime
            hourFormat="24"
            class="w-full"
          />
        </div>

        <div class="form-group">
          <label for="mealCalories">Calories</label>
          <InputNumber
            id="mealCalories"
            v-model="newMeal.calories"
            class="w-full"
            placeholder="Enter calories"
          />
        </div>

        <div class="form-group">
          <label for="mealProtein">Protein (g)</label>
          <InputNumber
            id="mealProtein"
            v-model="newMeal.protein"
            class="w-full"
            placeholder="Enter protein"
          />
        </div>

        <div class="form-group">
          <label for="mealCarbs">Carbs (g)</label>
          <InputNumber
            id="mealCarbs"
            v-model="newMeal.carbs"
            class="w-full"
            placeholder="Enter carbs"
          />
        </div>

        <div class="form-group">
          <label for="mealFats">Fats (g)</label>
          <InputNumber
            id="mealFats"
            v-model="newMeal.fats"
            class="w-full"
            placeholder="Enter fats"
          />
        </div>
      </div>

      <template #footer>
        <Button
          label="Cancel"
          icon="pi pi-times"
          class="p-button-text"
          @click="addMealDialogVisible = false"
        />
        <Button
          label="Save"
          icon="pi pi-check"
          @click="saveMeal"
          :loading="savingMeal"
        />
      </template>
    </Dialog>

    <!-- Confirm Dialog -->
    <ConfirmDialog />
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, onMounted } from 'vue'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'

// State
const toast = useToast()
const confirm = useConfirm()

const dailyCalories = ref(1200)
const dailyProtein = ref(60)
const dailyCarbs = ref(150)
const dailyFats = ref(40)

const calorieGoal = ref(2000)
const proteinGoal = ref(100)
const carbsGoal = ref(250)
const fatsGoal = ref(70)

const addMealDialogVisible = ref(false)
const savingMeal = ref(false)

const newMeal = reactive({
  name: '',
  time: new Date(),
  calories: 0,
  protein: 0,
  carbs: 0,
  fats: 0
})

// Sample data - replace with actual API calls
const recentMeals = ref([
  {
    id: 1,
    name: 'Breakfast',
    time: new Date(),
    calories: 450,
    protein: 20,
    carbs: 60,
    fats: 15
  },
  {
    id: 2,
    name: 'Lunch',
    time: new Date(),
    calories: 650,
    protein: 35,
    carbs: 80,
    fats: 20
  }
])

// Chart data
const weeklyChartData = {
  labels: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
  datasets: [
    {
      label: 'Calories',
      data: [1800, 1900, 1700, 2000, 1850, 2100, 1950],
      borderColor: '#3B82F6',
      tension: 0.4
    }
  ]
}

const macroChartData = {
  labels: ['Protein', 'Carbs', 'Fats'],
  datasets: [
    {
      data: [dailyProtein.value, dailyCarbs.value, dailyFats.value],
      backgroundColor: ['#3B82F6', '#10B981', '#F59E0B']
    }
  ]
}

const chartOptions = {
  responsive: true,
  maintainAspectRatio: false
}

// Methods
const showAddMealDialog = () => {
  addMealDialogVisible.value = true
}

const saveMeal = async () => {
  savingMeal.value = true

  try {
    // TODO: Implement actual API call
    await new Promise(resolve => setTimeout(resolve, 1000))

    recentMeals.value.unshift({
      id: Date.now(),
      ...newMeal
    })

    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Meal added successfully',
      life: 3000
    })

    addMealDialogVisible.value = false
    resetNewMeal()
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to add meal',
      life: 3000
    })
  } finally {
    savingMeal.value = false
  }
}

const resetNewMeal = () => {
  newMeal.name = ''
  newMeal.time = new Date()
  newMeal.calories = 0
  newMeal.protein = 0
  newMeal.carbs = 0
  newMeal.fats = 0
}

const editMeal = (meal: any) => {
  // TODO: Implement edit functionality
  console.log('Edit meal:', meal)
}

const confirmDeleteMeal = (meal: any) => {
  confirm.require({
    message: 'Are you sure you want to delete this meal?',
    header: 'Delete Confirmation',
    icon: 'pi pi-exclamation-triangle',
    accept: () => deleteMeal(meal)
  })
}

const deleteMeal = async (meal: any) => {
  try {
    // TODO: Implement actual API call
    await new Promise(resolve => setTimeout(resolve, 1000))

    recentMeals.value = recentMeals.value.filter(m => m.id !== meal.id)

    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Meal deleted successfully',
      life: 3000
    })
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to delete meal',
      life: 3000
    })
  }
}

const formatTime = (date: Date) => {
  return new Intl.DateTimeFormat('en-US', {
    hour: '2-digit',
    minute: '2-digit'
  }).format(date)
}

// Lifecycle hooks
onMounted(() => {
  // TODO: Fetch initial data from API
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_variables.scss";
@import "@/assets/styles/_mixins.scss";

.dashboard-view {
  .dashboard-header {
    @include flex(row, space-between, center);
    margin-bottom: $spacing-lg;
  }
}

.nutrition-summary {
  @include grid(4, $spacing-md);
  margin-bottom: $spacing-xl;

  @include responsive(lg) {
    grid-template-columns: repeat(3, 1fr);
  }

  @include responsive(sm) {
    grid-template-columns: repeat(2, 1fr);
  }

  .summary-card {
    .summary-content {
      text-align: center;

      .summary-value {
        font-size: $font-size-xl;
        font-weight: 600;
        color: $primary-color;
      }

      .summary-target {
        color: $secondary-color;
        font-size: $font-size-sm;
        margin-bottom: $spacing-sm;
      }
    }
  }
}

.dashboard-charts {
  @include grid(2, $spacing-md);
  margin-bottom: $spacing-xl;

  @include responsive(lg) {
    grid-template-columns: 1fr;
  }

  .chart-card {
    height: 400px;

    :deep(.p-card-content) {
      height: calc(100% - 50px);
    }
  }
}

.recent-meals {
  :deep(.p-datatable) {
    .p-datatable-header {
      background: transparent;
      border: none;
      padding: 0;
    }
  }
}

.add-meal-form {
  .form-group {
    margin-bottom: $spacing-md;

    label {
      display: block;
      margin-bottom: $spacing-xs;
      color: $text-color;
      font-weight: 500;
    }
  }
}
</style> 
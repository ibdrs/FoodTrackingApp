<template>
  <div class="meal-tracking-view">
    <div class="view-header">
      <h1>Meal Tracking</h1>
      <Button
        icon="pi pi-plus"
        label="Add Meal"
        @click="showAddMealDialog"
      />
    </div>

    <!-- Date Navigation -->
    <Card class="date-navigation">
      <template #content>
        <div class="date-controls">
          <Button
            icon="pi pi-chevron-left"
            class="p-button-text"
            @click="previousDay"
          />
          <Calendar
            v-model="selectedDate"
            dateFormat="dd/mm/yy"
            :showIcon="true"
            class="date-picker"
          />
          <Button
            icon="pi pi-chevron-right"
            class="p-button-text"
            @click="nextDay"
          />
        </div>
      </template>
    </Card>

    <!-- Daily Summary -->
    <div class="daily-summary">
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

    <!-- Meals List -->
    <Card class="meals-list">
      <template #title>Meals</template>
      <template #content>
        <div v-if="loading" class="loading-container">
          <i class="pi pi-spin pi-spinner" style="font-size: 2rem"></i>
        </div>

        <div v-else-if="meals.length === 0" class="empty-state">
          <i class="pi pi-calendar"></i>
          <p>No meals recorded for this day</p>
          <Button
            label="Add Meal"
            icon="pi pi-plus"
            @click="showAddMealDialog"
          />
        </div>

        <div v-else class="meals-timeline">
          <div
            v-for="meal in meals"
            :key="meal.id"
            class="meal-item"
          >
            <div class="meal-time">
              {{ formatTime(meal.time) }}
            </div>
            <div class="meal-content">
              <div class="meal-header">
                <h3>{{ meal.name }}</h3>
                <div class="meal-actions">
                  <Button
                    icon="pi pi-pencil"
                    class="p-button-text"
                    @click="editMeal(meal)"
                  />
                  <Button
                    icon="pi pi-trash"
                    class="p-button-text p-button-danger"
                    @click="confirmDeleteMeal(meal)"
                  />
                </div>
              </div>
              <div class="meal-nutrition">
                <span>{{ meal.calories }} kcal</span>
                <span>P: {{ meal.protein }}g</span>
                <span>C: {{ meal.carbs }}g</span>
                <span>F: {{ meal.fats }}g</span>
              </div>
            </div>
          </div>
        </div>
      </template>
    </Card>

    <!-- Add/Edit Meal Dialog -->
    <Dialog
      v-model:visible="mealDialogVisible"
      :header="dialogHeader"
      :style="{ width: '50vw' }"
      :modal="true"
    >
      <div class="meal-form">
        <div class="form-group">
          <label for="mealName">Meal Name</label>
          <InputText
            id="mealName"
            v-model="mealForm.name"
            class="w-full"
            :class="{ 'p-invalid': v$.name.$error }"
            placeholder="Enter meal name"
          />
          <small class="p-error" v-if="v$.name.$error">
            {{ v$.name.$errors[0].$message }}
          </small>
        </div>

        <div class="form-group">
          <label for="mealTime">Time</label>
          <Calendar
            id="mealTime"
            v-model="mealForm.time"
            showTime
            hourFormat="24"
            class="w-full"
            :class="{ 'p-invalid': v$.time.$error }"
          />
          <small class="p-error" v-if="v$.time.$error">
            {{ v$.time.$errors[0].$message }}
          </small>
        </div>

        <div class="form-group">
          <label for="mealCalories">Calories</label>
          <InputNumber
            id="mealCalories"
            v-model="mealForm.calories"
            class="w-full"
            :class="{ 'p-invalid': v$.calories.$error }"
            placeholder="Enter calories"
          />
          <small class="p-error" v-if="v$.calories.$error">
            {{ v$.calories.$errors[0].$message }}
          </small>
        </div>

        <div class="form-group">
          <label for="mealProtein">Protein (g)</label>
          <InputNumber
            id="mealProtein"
            v-model="mealForm.protein"
            class="w-full"
            :class="{ 'p-invalid': v$.protein.$error }"
            placeholder="Enter protein"
          />
          <small class="p-error" v-if="v$.protein.$error">
            {{ v$.protein.$errors[0].$message }}
          </small>
        </div>

        <div class="form-group">
          <label for="mealCarbs">Carbs (g)</label>
          <InputNumber
            id="mealCarbs"
            v-model="mealForm.carbs"
            class="w-full"
            :class="{ 'p-invalid': v$.carbs.$error }"
            placeholder="Enter carbs"
          />
          <small class="p-error" v-if="v$.carbs.$error">
            {{ v$.carbs.$errors[0].$message }}
          </small>
        </div>

        <div class="form-group">
          <label for="mealFats">Fats (g)</label>
          <InputNumber
            id="mealFats"
            v-model="mealForm.fats"
            class="w-full"
            :class="{ 'p-invalid': v$.fats.$error }"
            placeholder="Enter fats"
          />
          <small class="p-error" v-if="v$.fats.$error">
            {{ v$.fats.$errors[0].$message }}
          </small>
        </div>
      </div>

      <template #footer>
        <Button
          label="Cancel"
          icon="pi pi-times"
          class="p-button-text"
          @click="mealDialogVisible = false"
        />
        <Button
          label="Save"
          icon="pi pi-check"
          @click="saveMeal"
          :loading="saving"
        />
      </template>
    </Dialog>

    <!-- Confirm Dialog -->
    <ConfirmDialog />
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required, minValue } from '@vuelidate/validators'
import { useToast } from 'primevue/usetoast'
import { useConfirm } from 'primevue/useconfirm'
import type { Meal } from '@/types'

// State
const toast = useToast()
const confirm = useConfirm()

const loading = ref(false)
const saving = ref(false)
const mealDialogVisible = ref(false)
const editingMeal = ref(false)

const selectedDate = ref(new Date())
const dailyCalories = ref(1200)
const dailyProtein = ref(60)
const dailyCarbs = ref(150)
const dailyFats = ref(40)

const calorieGoal = ref(2000)
const proteinGoal = ref(100)
const carbsGoal = ref(250)
const fatsGoal = ref(70)

const mealForm = reactive<Partial<Meal>>({
  id: null,
  name: '',
  time: new Date(),
  calories: 0,
  protein: 0,
  carbs: 0,
  fats: 0
})

// Sample data - replace with actual API calls
const meals = ref<Meal[]>([
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

// Computed
const dialogHeader = computed(() => {
  return editingMeal.value ? 'Edit Meal' : 'Add Meal'
})

// Validation
const rules = {
  name: { required },
  time: { required },
  calories: { required, minValue: minValue(0) },
  protein: { required, minValue: minValue(0) },
  carbs: { required, minValue: minValue(0) },
  fats: { required, minValue: minValue(0) }
}

const v$ = useVuelidate(rules, mealForm)

// Methods
const showAddMealDialog = () => {
  editingMeal.value = false
  resetMealForm()
  mealDialogVisible.value = true
}

const editMeal = (meal: any) => {
  editingMeal.value = true
  Object.assign(mealForm, meal)
  mealDialogVisible.value = true
}

const saveMeal = async () => {
  const isValid = await v$.value.$validate()
  
  if (!isValid) {
    return
  }

  saving.value = true

  try {
    // TODO: Implement actual API call
    await new Promise(resolve => setTimeout(resolve, 1000))

    if (editingMeal.value) {
      const index = meals.value.findIndex(m => m.id === mealForm.id)
      if (index !== -1) {
        meals.value[index] = { ...mealForm }
      }
    } else {
      meals.value.unshift({
        id: Date.now(),
        ...mealForm
      })
    }

    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: `Meal ${editingMeal.value ? 'updated' : 'added'} successfully`,
      life: 3000
    })

    mealDialogVisible.value = false
    resetMealForm()
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: `Failed to ${editingMeal.value ? 'update' : 'add'} meal`,
      life: 3000
    })
  } finally {
    saving.value = false
  }
}

const resetMealForm = () => {
  mealForm.id = null
  mealForm.name = ''
  mealForm.time = new Date()
  mealForm.calories = 0
  mealForm.protein = 0
  mealForm.carbs = 0
  mealForm.fats = 0
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

    meals.value = meals.value.filter(m => m.id !== meal.id)

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

const previousDay = () => {
  const date = new Date(selectedDate.value)
  date.setDate(date.getDate() - 1)
  selectedDate.value = date
  fetchMeals()
}

const nextDay = () => {
  const date = new Date(selectedDate.value)
  date.setDate(date.getDate() + 1)
  selectedDate.value = date
  fetchMeals()
}

const formatTime = (date: Date) => {
  return new Intl.DateTimeFormat('en-US', {
    hour: '2-digit',
    minute: '2-digit'
  }).format(date)
}

const fetchMeals = async () => {
  loading.value = true

  try {
    // TODO: Implement actual API call
    await new Promise(resolve => setTimeout(resolve, 1000))
    
    // Update sample data with new date
    meals.value = meals.value.map(meal => ({
      ...meal,
      time: new Date(selectedDate.value)
    }))
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to load meals',
      life: 3000
    })
  } finally {
    loading.value = false
  }
}

// Lifecycle hooks
onMounted(() => {
  fetchMeals()
})
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_variables.scss";
@import "@/assets/styles/_mixins.scss";

.meal-tracking-view {
  .view-header {
    @include flex(row, space-between, center);
    margin-bottom: $spacing-lg;
  }
}

.date-navigation {
  margin-bottom: $spacing-lg;

  .date-controls {
    @include flex(row, center, center);
    gap: $spacing-md;

    .date-picker {
      width: 200px;
    }
  }
}

.daily-summary {
  @include grid(4, $spacing-md);

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

.meals-list {
  .loading-container {
    @include flex(row, center, center);
    min-height: 200px;
  }

  .empty-state {
    @include flex(column, center, center);
    min-height: 200px;
    text-align: center;

    i {
      font-size: 3rem;
      color: $secondary-color;
      margin-bottom: $spacing-md;
    }

    p {
      color: $secondary-color;
      margin-bottom: $spacing-lg;
    }
  }

  .meals-timeline {
    .meal-item {
      @include flex(row, flex-start, flex-start);
      gap: $spacing-md;
      padding: $spacing-md;
      border-bottom: 1px solid rgba(0, 0, 0, 0.1);

      &:last-child {
        border-bottom: none;
      }

      .meal-time {
        min-width: 80px;
        color: $secondary-color;
        font-size: $font-size-sm;
      }

      .meal-content {
        flex: 1;

        .meal-header {
          @include flex(row, space-between, center);
          margin-bottom: $spacing-xs;

          h3 {
            margin: 0;
          }

          .meal-actions {
            @include flex(row, flex-end, center);
            gap: $spacing-xs;
          }
        }

        .meal-nutrition {
          @include flex(row, flex-start, center);
          gap: $spacing-md;
          color: $secondary-color;
          font-size: $font-size-sm;
        }
      }
    }
  }
}

.meal-form {
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
<template>
    <div class="daily-overview-page">
      <Card>
        <template #title>Daily Overview</template>
        <template #content>
          <div class="grid">
            <div class="col-12 md:col-4">
              <Panel header="Select Date">
                <!-- Changed Calendar to DatePicker -->
                <DatePicker v-model="selectedDate" inline @date-select="loadDataForDate" />
              </Panel>
            </div>
            <!-- ... rest of the template ... -->
            <div class="col-12 md:col-8">
              <Panel :header="`Meals for ${formattedSelectedDate}`">
                <DataTable :value="todaysMeals" responsiveLayout="scroll" :paginator="todaysMeals.length > 5" :rows="5" emptyMessage="No meals added for this day.">
                  <Column field="name" header="Meal Name" sortable></Column>
                  <Column field="time" header="Time" sortable></Column>
                  <Column field="summary.calories" header="Calories (kcal)" sortable>
                    <template #body="slotProps">
                      {{ slotProps.data.summary.calories || 'N/A' }}
                    </template>
                  </Column>
                  <Column header="Actions">
                     <template #body="slotProps">
                       <Button icon="pi pi-eye" class="p-button-rounded p-button-info p-button-text" @click="viewMealDetails(slotProps.data)" />
                       <Button icon="pi pi-trash" class="p-button-rounded p-button-danger p-button-text" @click="removeMeal(slotProps.data.id)" />
                     </template>
                  </Column>
                </DataTable>
              </Panel>
  
              <Panel header="Nutritional Totals (Dummy)" class="mt-4" :toggleable="true">
                <div v-if="dailyTotals" class="grid text-center">
                  <div class="col-6 md:col-3">
                    <div class="font-bold text-xl">{{ dailyTotals.calories }}</div>
                    <div>Calories (kcal)</div>
                  </div>
                  <div class="col-6 md:col-3">
                    <div class="font-bold text-xl">{{ dailyTotals.protein }} g</div>
                    <div>Protein</div>
                  </div>
                  <div class="col-6 md:col-3">
                    <div class="font-bold text-xl">{{ dailyTotals.carbs }} g</div>
                    <div>Carbohydrates</div>
                  </div>
                  <div class="col-6 md:col-3">
                    <div class="font-bold text-xl">{{ dailyTotals.fat }} g</div>
                    <div>Fat</div>
                  </div>
                </div>
                <div v-else class="text-center">No data for this day.</div>
                <Chart type="doughnut" :data="macroChartData" :options="chartOptions" class="mt-4" v-if="dailyTotals && dailyTotals.calories > 0" />
              </Panel>
            </div>
          </div>
        </template>
      </Card>
  
      <Dialog v-model:visible="mealDetailDialogVisible" header="Meal Details" :modal="true" :style="{width: '50vw'}">
          <div v-if="selectedMealDetail">
              <h3>{{ selectedMealDetail.name }}</h3>
              <p v-if="selectedMealDetail.time"><strong>Time:</strong> {{ selectedMealDetail.time }}</p>
              <p v-if="selectedMealDetail.notes"><strong>Notes:</strong> {{ selectedMealDetail.notes }}</p>
  
              <h4>Ingredients:</h4>
              <ul v-if="selectedMealDetail.ingredients && selectedMealDetail.ingredients.length">
                  <li v-for="ing in selectedMealDetail.ingredients" :key="ing.id">
                      {{ ing.quantity }} {{ getUnitLabel(ing.unit) }} - {{ ing.name }}
                  </li>
              </ul>
              <p v-else>No ingredients specified.</p>
  
              <h4>Nutritional Summary (Dummy):</h4>
              <div class="grid">
                  <div class="col-6"><strong>Calories:</strong> {{ selectedMealDetail.summary?.calories || 'N/A' }} kcal</div>
                  <div class="col-6"><strong>Protein:</strong> {{ selectedMealDetail.summary?.protein || 'N/A' }} g</div>
                  <div class="col-6"><strong>Carbs:</strong> {{ selectedMealDetail.summary?.carbs || 'N/A' }} g</div>
                  <div class="col-6"><strong>Fat:</strong> {{ selectedMealDetail.summary?.fat || 'N/A' }} g</div>
              </div>
          </div>
          <template #footer>
              <Button label="Close" icon="pi pi-times" @click="mealDetailDialogVisible = false" class="p-button-text"/>
          </template>
      </Dialog>
    </div>
  </template>
  
  <script setup>
  // No script changes needed for DatePicker if props are compatible
  import { ref, computed, onMounted, watch } from 'vue';
  import { dailyLogs, unitOptions } from '@/services/dummyData.js';
  import { useToast } from 'primevue/usetoast';
  
  const toast = useToast();
  const selectedDate = ref(new Date());
  const todaysMeals = ref([]);
  const dailyTotals = ref(null);
  
  const mealDetailDialogVisible = ref(false);
  const selectedMealDetail = ref(null);
  
  
  const getUnitLabel = (value) => {
    const option = unitOptions.find(opt => opt.value === value);
    return option ? option.label : value;
  };
  
  const viewMealDetails = (meal) => {
    selectedMealDetail.value = meal;
    mealDetailDialogVisible.value = true;
  };
  
  const formatDateToKey = (date) => {
    if (!date) return '';
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  };
  
  const formattedSelectedDate = computed(() => {
    return selectedDate.value ? selectedDate.value.toLocaleDateString() : 'N/A';
  });
  
  const loadDataForDate = () => {
    const dateKey = formatDateToKey(selectedDate.value);
    const log = dailyLogs[dateKey];
    if (log) {
      todaysMeals.value = [...log.meals];
      dailyTotals.value = { ...log.totals };
    } else {
      todaysMeals.value = [];
      dailyTotals.value = { calories: 0, protein: 0, carbs: 0, fat: 0 };
    }
  };
  
  const removeMeal = (mealId) => {
    const dateKey = formatDateToKey(selectedDate.value);
    if (dailyLogs[dateKey]) {
      const mealToRemove = dailyLogs[dateKey].meals.find(m => m.id === mealId);
      if (mealToRemove && mealToRemove.summary) {
        dailyLogs[dateKey].totals.calories -= mealToRemove.summary.calories || 0;
        dailyLogs[dateKey].totals.protein -= mealToRemove.summary.protein || 0;
        dailyLogs[dateKey].totals.carbs -= mealToRemove.summary.carbs || 0;
        dailyLogs[dateKez].totals.fat -= mealToRemove.summary.fat || 0;
      }
      dailyLogs[dateKey].meals = dailyLogs[dateKey].meals.filter(meal => meal.id !== mealId);
      loadDataForDate(); // Refresh the view
      toast.add({ severity: 'info', summary: 'Meal Removed', detail: 'The meal has been removed from this day.', life: 3000 });
    }
  };
  
  const macroChartData = computed(() => {
    if (!dailyTotals.value) return {};
    return {
      labels: ['Protein (g)', 'Carbs (g)', 'Fat (g)'],
      datasets: [
        {
          data: [dailyTotals.value.protein, dailyTotals.value.carbs, dailyTotals.value.fat],
          backgroundColor: ["#42A5F5", "#66BB6A", "#FFA726"],
          hoverBackgroundColor: ["#64B5F6", "#81C784", "#FFB74D"]
        }
      ]
    };
  });
  
  const chartOptions = ref({
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        position: 'bottom',
        labels: {
          color: '#495057'
        }
      }
    }
  });
  
  onMounted(loadDataForDate);
  watch(selectedDate, loadDataForDate);
  </script>
  
  <style scoped>
  .daily-overview-page {}
  .p-panel .p-panel-content { min-height: 150px; }
  .p-chart { height: 250px; }
  </style>
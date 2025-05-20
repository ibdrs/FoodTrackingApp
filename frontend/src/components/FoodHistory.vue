<template>
    <div class="food-history-page">
      <Card>
        <template #title>Food History</template>
        <template #content>
          <div class="mb-4">
            <label for="dateRange" class="block mb-2">Select Date or Range (Optional Filter):</label>
            <!-- Changed Calendar to DatePicker -->
            <DatePicker v-model="dateFilter" selectionMode="range" :manualInput="false" showIcon dateFormat="yy-mm-dd" class="w-full md:w-auto" />
            <Button label="Clear Filter" icon="pi pi-times" class="p-button-text ml-2" @click="clearFilter" v-if="dateFilter && dateFilter[0]"/>
          </div>
          <!-- ... rest of the template ... -->
          <DataTable :value="filteredHistory" responsiveLayout="scroll" paginator :rows="10"
                     sortMode="multiple" :multiSortMeta="multiSortMeta" @sort="onSort"
                     emptyMessage="No meal history found for the selected criteria.">
            <Column field="date" header="Date" sortable>
               <template #body="slotProps">
                  {{ formatDate(slotProps.data.date) }}
               </template>
            </Column>
            <Column field="name" header="Meal Name" sortable></Column>
            <Column field="time" header="Time" sortable></Column>
            <Column field="summary.calories" header="Calories (kcal)" sortable>
              <template #body="slotProps">
                {{ slotProps.data.summary.calories || 'N/A' }}
              </template>
            </Column>
            <Column field="summary.protein" header="Protein (g)" sortable>
              <template #body="slotProps">
                {{ slotProps.data.summary.protein || 'N/A' }} g
              </template>
            </Column>
             <Column header="Actions">
                 <template #body="slotProps">
                   <Button icon="pi pi-eye" class="p-button-rounded p-button-info p-button-text" @click="viewMealDetails(slotProps.data)" />
                 </template>
             </Column>
          </DataTable>
        </template>
      </Card>
  
      <Dialog v-model:visible="mealDetailDialogVisible" header="Meal Details" :modal="true" :style="{width: '50vw'}">
          <div v-if="selectedMealDetail">
              <h3>{{ selectedMealDetail.name }}</h3>
              <p><strong>Date:</strong> {{ formatDate(selectedMealDetail.date) }}</p>
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
  import { ref, computed } from 'vue';
  import { dailyLogs, unitOptions } from '@/services/dummyData.js';
  
  const dateFilter = ref(null);
  const mealDetailDialogVisible = ref(false);
  const selectedMealDetail = ref(null);
  const multiSortMeta = ref([{ field: 'date', order: -1 }]); 
  
  const getUnitLabel = (value) => {
    const option = unitOptions.find(opt => opt.value === value);
    return option ? option.label : value;
  };
  
  const viewMealDetails = (meal) => {
    selectedMealDetail.value = meal;
    mealDetailDialogVisible.value = true;
  };
  
  const formatDate = (dateString) => {
      if (!dateString) return 'N/A';
      const [year, month, day] = dateString.split('-');
      return new Date(year, month - 1, day).toLocaleDateString();
  };
  
  const allMealsHistory = computed(() => {
    const history = [];
    for (const dateKey in dailyLogs) {
      dailyLogs[dateKey].meals.forEach(meal => {
        history.push({
          ...meal,
          date: dateKey
        });
      });
    }
    return history;
  });
  
  const filteredHistory = computed(() => {
    if (!dateFilter.value || !dateFilter.value[0]) {
      return allMealsHistory.value;
    }
    const startDate = dateFilter.value[0];
    const endDate = dateFilter.value[1] || dateFilter.value[0]; 
    startDate.setHours(0,0,0,0);
    endDate.setHours(23,59,59,999);
    return allMealsHistory.value.filter(meal => {
      const mealDate = new Date(meal.date);
      mealDate.setHours(0,0,0,0);
      return mealDate >= startDate && mealDate <= endDate;
    });
  });
  
  const clearFilter = () => {
    dateFilter.value = null;
  };
  
  const onSort = (event) => {
      multiSortMeta.value = event.multiSortMeta;
  };
  </script>
  
  <style scoped>
  .food-history-page {}
  </style>
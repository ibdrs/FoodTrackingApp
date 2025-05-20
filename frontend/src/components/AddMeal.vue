<template>
    <div class="add-meal-page">
      <Card>
        <template #title>
          <div class="flex justify-content-between align-items-center">
            <span>Add New Meal</span>
            <Button icon="pi pi-save" label="Save Meal" @click="saveMeal" />
          </div>
        </template>
        <template #content>
          <Fieldset legend="Meal Details" :toggleable="true" class="mb-4">
            <!-- Removed p-fluid from here -->
            <div class="grid">
              <div class="field col-12 md:col-6">
                <label for="mealName">Meal Name</label>
                <InputText id="mealName" v-model="meal.name" placeholder="e.g., Morning Smoothie" fluid />
              </div>
              <div class="field col-12 md:col-6">
                <label for="mealTime">Meal Time (Optional)</label>
                <!-- Changed Calendar to DatePicker -->
                <DatePicker id="mealTime" v-model="meal.time" :showTime="true" :timeOnly="true" hourFormat="12" fluid />
              </div>
              <div class="field col-12">
                <label for="mealNotes">Notes (Optional)</label>
                <Textarea id="mealNotes" v-model="meal.notes" rows="3" fluid />
              </div>
            </div>
          </Fieldset>
  
          <Fieldset legend="Ingredients" :toggleable="true">
            <DataTable :value="meal.ingredients" editMode="row" dataKey="id" @row-edit-save="onRowEditSave" responsiveLayout="scroll">
              <template #header>
                <div class="flex justify-content-end">
                  <Button icon="pi pi-plus" label="Add Ingredient" @click="addNewIngredient" class="p-button-sm" />
                </div>
              </template>
              <Column field="name" header="Name">
                <template #editor="{ data, field }">
                  <InputText v-model="data[field]" autofocus fluid />
                </template>
              </Column>
              <Column field="quantity" header="Quantity">
                <template #editor="{ data, field }">
                  <InputNumber v-model="data[field]" mode="decimal" :minFractionDigits="0" :maxFractionDigits="2" fluid />
                </template>
              </Column>
              <Column field="unit" header="Unit">
                <template #editor="{ data, field }">
                  <!-- Changed Dropdown to Select -->
                  <Select v-model="data[field]" :options="unitOptions" optionLabel="label" optionValue="value" placeholder="Select Unit" fluid />
                </template>
                <template #body="slotProps">
                   <Tag :value="getUnitLabel(slotProps.data.unit) || slotProps.data.unit" />
                </template>
              </Column>
              <Column :rowEditor="true" style="width:10%; min-width:8rem" bodyStyle="text-align:center" header="Edit"></Column>
              <Column header="Actions" style="width:10%; min-width:8rem" bodyStyle="text-align:center">
                <template #body="slotProps">
                  <Button icon="pi pi-trash" class="p-button-rounded p-button-danger p-button-text" @click="removeIngredient(slotProps.data.id)" />
                </template>
              </Column>
               <template #empty>
                  No ingredients added yet. Click "Add Ingredient" to start.
              </template>
            </DataTable>
          </Fieldset>
  
          <Panel header="Nutritional Summary (Dummy Data)" class="mt-4" :toggleable="true">
            <div class="grid">
              <div class="col-6 md:col-3"><strong>Calories:</strong> 550 kcal</div>
              <div class="col-6 md:col-3"><strong>Protein:</strong> 30 g</div>
              <div class="col-6 md:col-3"><strong>Carbs:</strong> 70 g</div>
              <div class="col-6 md:col-3"><strong>Fat:</strong> 20 g</div>
              <div class="col-6 md:col-3"><strong>Vitamin C:</strong> 25 mg</div>
              <div class="col-6 md:col-3"><strong>Iron:</strong> 5 mg</div>
            </div>
          </Panel>
        </template>
      </Card>
    </div>
  </template>
  
  <script setup>
  // No script changes needed for these specific component renames if props are compatible
  import { ref, computed } from 'vue';
  import { useToast } from 'primevue/usetoast';
  import { unitOptions, addMealToLog, getTodaysDateKey } from '@/services/dummyData.js';
  import { useRouter } from 'vue-router';
  
  const toast = useToast();
  const router = useRouter();
  
  const meal = ref({
    id: `meal_${Date.now()}`,
    name: '',
    time: null,
    notes: '',
    ingredients: [],
    summary: { calories: 550, protein: 30, carbs: 70, fat: 20, vitaminC: 25, iron: 5 }
  });
  
  let ingredientIdCounter = 0;
  
  const addNewIngredient = () => {
    meal.value.ingredients.push({
      id: `ing_${ingredientIdCounter++}`,
      name: '',
      quantity: null,
      unit: unitOptions[0].value
    });
  };
  
  const removeIngredient = (id) => {
    meal.value.ingredients = meal.value.ingredients.filter(ing => ing.id !== id);
  };
  
  const onRowEditSave = (event) => {
    let { newData, index } = event;
    meal.value.ingredients[index] = newData;
  };
  
  const getUnitLabel = (value) => {
    const option = unitOptions.find(opt => opt.value === value);
    return option ? option.label : value;
  };
  
  const saveMeal = () => {
    if (!meal.value.name.trim()) {
      toast.add({ severity: 'warn', summary: 'Validation Error', detail: 'Meal name is required.', life: 3000 });
      return;
    }
    if (!meal.value.ingredients.length) {
      toast.add({ severity: 'warn', summary: 'Validation Error', detail: 'Please add at least one ingredient.', life: 3000 });
      return;
    }
  
    const todayKey = getTodaysDateKey();
    const mealToLog = {
      ...meal.value,
      time: meal.value.time ? meal.value.time.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) : 'N/A'
    };
    addMealToLog(todayKey, mealToLog);
  
    toast.add({ severity: 'success', summary: 'Meal Saved', detail: `${meal.value.name} has been saved.`, life: 3000 });
    
    meal.value = {
      id: `meal_${Date.now()}`,
      name: '',
      time: null,
      notes: '',
      ingredients: [],
      summary: { calories: 550, protein: 30, carbs: 70, fat: 20, vitaminC: 25, iron: 5 }
    };
    ingredientIdCounter = 0;
  };
  </script>
  
  <style scoped>
  .add-meal-page {
    max-width: 900px;
    margin: 0 auto;
  }
  .p-fieldset .p-fieldset-legend {
    padding: 0.5rem 1rem !important;
  }
  </style>
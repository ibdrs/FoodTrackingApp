<template>
  <div class="preset-meals-page">
    <Card>
      <template #title>
        <div class="flex justify-content-between align-items-center">
          <span>Preset Meals</span>
          <!-- Replaced DataViewLayoutOptions with SelectButton -->
          <SelectButton v-model="layout" :options="layoutOptions" optionLabel="icon" dataKey="value" aria-labelledby="layout-mode">
            <template #option="slotProps">
              <i :class="slotProps.option.icon" style="font-size: 1.2rem; margin: 0.2rem;"></i>
            </template>
          </SelectButton>
        </div>
      </template>
      <template #content>
        <DataView :value="presets" :layout="layout" :paginator="presets.length > 6" :rows="6">
          <!-- ... rest of the DataView template (list and grid item templates) ... -->
          <template #header>
            <div class="text-center text-xl mb-2">Quickly add your common meals</div>
          </template>

          <template #list="slotProps">
            <div class="col-12">
              <div class="flex flex-column xl:flex-row xl:align-items-start p-4 gap-4">
                <i class="pi pi-star-fill text-yellow-500 text-4xl xl:mb-0"></i>
                <div class="flex-1">
                  <div class="text-xl font-bold">{{ slotProps.data.name }}</div>
                  <div class="text-sm text-gray-600 mb-2">{{ slotProps.data.description }}</div>
                  <div class="text-sm">
                    Calories: {{ slotProps.data.summary.calories }},
                    Protein: {{ slotProps.data.summary.protein }}g,
                    Carbs: {{ slotProps.data.summary.carbs }}g,
                    Fat: {{ slotProps.data.summary.fat }}g
                  </div>
                </div>
                <div class="flex flex-column align-items-end gap-2">
                  <Button icon="pi pi-plus" label="Add to Today" class="p-button-sm" @click="addPresetToToday(slotProps.data)"></Button>
                  <Button icon="pi pi-eye" label="View" class="p-button-sm p-button-outlined" @click="viewPresetDetails(slotProps.data)"></Button>
                </div>
              </div>
            </div>
          </template>

          <template #grid="slotProps">
            <div class="col-12 md:col-6 lg:col-4 p-2">
              <Card class="h-full flex flex-column">
                <template #header>
                    <div class="p-3 bg-primary-50 text-primary flex align-items-center justify-content-center" style="height: 60px;">
                        <i class="pi pi-star-fill text-yellow-500 text-3xl"></i>
                    </div>
                </template>
                <template #title>
                  {{ slotProps.data.name }}
                </template>
                <template #subtitle>
                  {{ slotProps.data.description }}
                </template>
                <template #content>
                  <div class="text-sm mb-2">
                    <p><strong>Calories:</strong> {{ slotProps.data.summary.calories }}</p>
                    <p><strong>Protein:</strong> {{ slotProps.data.summary.protein }}g</p>
                    <p><strong>Carbs:</strong> {{ slotProps.data.summary.carbs }}g</p>
                    <p><strong>Fat:</strong> {{ slotProps.data.summary.fat }}g</p>
                  </div>
                </template>
                <template #footer>
                    <div class="flex gap-2 mt-auto">
                        <Button icon="pi pi-plus" label="Add to Today" class="p-button-sm w-full" @click="addPresetToToday(slotProps.data)"></Button>
                        <Button icon="pi pi-eye" label="View" class="p-button-sm p-button-outlined w-full" @click="viewPresetDetails(slotProps.data)"></Button>
                    </div>
                </template>
              </Card>
            </div>
          </template>
          <template #empty>
            No preset meals available.
          </template>
        </DataView>
      </template>
    </Card>

    <Dialog v-model:visible="presetDetailDialogVisible" header="Preset Meal Details" :modal="true" :style="{width: '50vw'}">
        <div v-if="selectedPresetDetail">
            <h3>{{ selectedPresetDetail.name }}</h3>
            <p><strong>Description:</strong> {{ selectedPresetDetail.description }}</p>

            <h4>Ingredients:</h4>
            <ul v-if="selectedPresetDetail.ingredients && selectedPresetDetail.ingredients.length">
                <li v-for="ing in selectedPresetDetail.ingredients" :key="ing.id">
                    {{ ing.quantity }} {{ getUnitLabel(ing.unit) }} - {{ ing.name }}
                </li>
            </ul>
            <p v-else>No ingredients specified for this preset.</p>

            <h4>Nutritional Summary:</h4>
             <div class="grid">
                <div class="col-6"><strong>Calories:</strong> {{ selectedPresetDetail.summary?.calories || 'N/A' }} kcal</div>
                <div class="col-6"><strong>Protein:</strong> {{ selectedPresetDetail.summary?.protein || 'N/A' }} g</div>
                <div class="col-6"><strong>Carbs:</strong> {{ selectedPresetDetail.summary?.carbs || 'N/A' }} g</div>
                <div class="col-6"><strong>Fat:</strong> {{ selectedPresetDetail.summary?.fat || 'N/A' }} g</div>
            </div>
        </div>
        <template #footer>
            <Button label="Close" icon="pi pi-times" @click="presetDetailDialogVisible = false" class="p-button-text"/>
        </template>
    </Dialog>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import { dummyPresetMeals, addMealToLog, getTodaysDateKey, unitOptions } from '@/services/dummyData.js';
import { useToast } from 'primevue/usetoast';
import { useRouter } from 'vue-router';

const toast = useToast();
const router = useRouter();
const presets = ref(dummyPresetMeals);
const layout = ref('grid'); // 'list' or 'grid'

// Options for SelectButton
const layoutOptions = ref([
    { icon: 'pi pi-bars', value: 'list' },
    { icon: 'pi pi-th-large', value: 'grid' }
]);

const presetDetailDialogVisible = ref(false);
const selectedPresetDetail = ref(null);

const getUnitLabel = (value) => {
  const option = unitOptions.find(opt => opt.value === value);
  return option ? option.label : value;
};

const addPresetToToday = (preset) => {
  const todayKey = getTodaysDateKey();
  const mealToAdd = {
    ...preset,
    id: `log_preset_${preset.id}_${Date.now()}`,
    time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' })
  };
  addMealToLog(todayKey, mealToAdd);
  toast.add({ severity: 'success', summary: 'Meal Added', detail: `${preset.name} added to today's log.`, life: 3000 });
  router.push('/overview');
};

const viewPresetDetails = (preset) => {
  selectedPresetDetail.value = preset;
  presetDetailDialogVisible.value = true;
};
</script>

<style scoped>
.preset-meals-page {}
.p-dataview .p-dataview-header {
    background: transparent;
    border: none;
    padding-bottom: 1rem;
}
.p-card.p-component { height: 100%; }
.p-card-footer { margin-top: auto; }
</style>
<template>
  <form @submit.prevent="handleSubmit" class="food-form">
    <div class="form-group">
      <label for="name">Food Name</label>
      <InputText
        id="name"
        v-model="form.name"
        :class="{ 'p-invalid': v$.name.$error }"
        placeholder="Enter food name"
      />
      <small class="p-error" v-if="v$.name.$error">
        {{ v$.name.$errors[0].$message }}
      </small>
    </div>

    <div class="form-group">
      <label for="category">Category</label>
      <Dropdown
        id="category"
        v-model="form.category"
        :options="categories"
        optionLabel="label"
        optionValue="value"
        placeholder="Select category"
        :class="{ 'p-invalid': v$.category.$error }"
      />
      <small class="p-error" v-if="v$.category.$error">
        {{ v$.category.$errors[0].$message }}
      </small>
    </div>

    <div class="form-row">
      <div class="form-group">
        <label for="servingSize">Serving Size</label>
        <InputNumber
          id="servingSize"
          v-model="form.servingSize"
          :minFractionDigits="1"
          :maxFractionDigits="1"
          :class="{ 'p-invalid': v$.servingSize.$error }"
        />
        <small class="p-error" v-if="v$.servingSize.$error">
          {{ v$.servingSize.$errors[0].$message }}
        </small>
      </div>

      <div class="form-group">
        <label for="servingUnit">Unit</label>
        <Dropdown
          id="servingUnit"
          v-model="form.servingUnit"
          :options="units"
          optionLabel="label"
          optionValue="value"
          placeholder="Select unit"
          :class="{ 'p-invalid': v$.servingUnit.$error }"
        />
        <small class="p-error" v-if="v$.servingUnit.$error">
          {{ v$.servingUnit.$errors[0].$message }}
        </small>
      </div>
    </div>

    <div class="form-row">
      <div class="form-group">
        <label for="calories">Calories</label>
        <InputNumber
          id="calories"
          v-model="form.calories"
          :minFractionDigits="0"
          :maxFractionDigits="0"
          :class="{ 'p-invalid': v$.calories.$error }"
        />
        <small class="p-error" v-if="v$.calories.$error">
          {{ v$.calories.$errors[0].$message }}
        </small>
      </div>

      <div class="form-group">
        <label for="protein">Protein (g)</label>
        <InputNumber
          id="protein"
          v-model="form.protein"
          :minFractionDigits="1"
          :maxFractionDigits="1"
          :class="{ 'p-invalid': v$.protein.$error }"
        />
        <small class="p-error" v-if="v$.protein.$error">
          {{ v$.protein.$errors[0].$message }}
        </small>
      </div>
    </div>

    <div class="form-row">
      <div class="form-group">
        <label for="carbs">Carbs (g)</label>
        <InputNumber
          id="carbs"
          v-model="form.carbs"
          :minFractionDigits="1"
          :maxFractionDigits="1"
          :class="{ 'p-invalid': v$.carbs.$error }"
        />
        <small class="p-error" v-if="v$.carbs.$error">
          {{ v$.carbs.$errors[0].$message }}
        </small>
      </div>

      <div class="form-group">
        <label for="fats">Fats (g)</label>
        <InputNumber
          id="fats"
          v-model="form.fats"
          :minFractionDigits="1"
          :maxFractionDigits="1"
          :class="{ 'p-invalid': v$.fats.$error }"
        />
        <small class="p-error" v-if="v$.fats.$error">
          {{ v$.fats.$errors[0].$message }}
        </small>
      </div>
    </div>

    <div class="form-actions">
      <Button
        type="button"
        label="Cancel"
        class="p-button-text"
        @click="$emit('cancel')"
      />
      <Button
        type="submit"
        :label="isEditing ? 'Update Food' : 'Add Food'"
        :loading="loading"
      />
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required, minValue } from '@vuelidate/validators'

interface Food {
  id?: number
  name: string
  category: string
  servingSize: number
  servingUnit: string
  calories: number
  protein: number
  carbs: number
  fats: number
}

const props = defineProps<{
  food?: Food
  loading?: boolean
}>()

const emit = defineEmits<{
  (e: 'submit', food: Food): void
  (e: 'cancel'): void
}>()

const categories = [
  { label: 'Vegetables', value: 'vegetables' },
  { label: 'Fruits', value: 'fruits' },
  { label: 'Grains', value: 'grains' },
  { label: 'Protein Foods', value: 'protein' },
  { label: 'Dairy', value: 'dairy' },
  { label: 'Fats & Oils', value: 'fats' },
  { label: 'Sweets', value: 'sweets' },
  { label: 'Beverages', value: 'beverages' }
]

const units = [
  { label: 'Grams (g)', value: 'g' },
  { label: 'Milliliters (ml)', value: 'ml' },
  { label: 'Ounces (oz)', value: 'oz' },
  { label: 'Cups', value: 'cup' },
  { label: 'Tablespoons (tbsp)', value: 'tbsp' },
  { label: 'Teaspoons (tsp)', value: 'tsp' },
  { label: 'Pieces', value: 'piece' }
]

const form = ref<Food>({
  name: props.food?.name || '',
  category: props.food?.category || '',
  servingSize: props.food?.servingSize || 0,
  servingUnit: props.food?.servingUnit || '',
  calories: props.food?.calories || 0,
  protein: props.food?.protein || 0,
  carbs: props.food?.carbs || 0,
  fats: props.food?.fats || 0
})

const rules = {
  name: { required },
  category: { required },
  servingSize: { required, minValue: minValue(0) },
  servingUnit: { required },
  calories: { required, minValue: minValue(0) },
  protein: { required, minValue: minValue(0) },
  carbs: { required, minValue: minValue(0) },
  fats: { required, minValue: minValue(0) }
}

const v$ = useVuelidate(rules, form)

const isEditing = computed(() => !!props.food)

const handleSubmit = async () => {
  const isValid = await v$.value.$validate()
  if (isValid) {
    emit('submit', {
      ...form.value,
      id: props.food?.id
    })
  }
}
</script>

<style lang="scss" scoped>
.food-form {
  @include flex(column, flex-start, stretch);
  gap: $spacing-md;

  .form-group {
    @include flex(column, flex-start, stretch);
    gap: $spacing-xs;

    label {
      color: $text-color;
      font-weight: 500;
    }

    .p-inputtext,
    .p-dropdown,
    .p-inputnumber {
      width: 100%;
    }
  }

  .form-row {
    @include flex(row, space-between, flex-start);
    gap: $spacing-md;

    .form-group {
      flex: 1;
    }
  }

  .form-actions {
    @include flex(row, flex-end, center);
    gap: $spacing-sm;
    margin-top: $spacing-md;
  }
}
</style> 
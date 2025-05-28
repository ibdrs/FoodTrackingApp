<template>
  <form @submit.prevent="handleSubmit" class="meal-form">
    <div class="form-group">
      <label for="name">Meal Name</label>
      <InputText
        id="name"
        v-model="form.name"
        :class="{ 'p-invalid': v$.name.$error }"
        placeholder="Enter meal name"
      />
      <small class="p-error" v-if="v$.name.$error">
        {{ v$.name.$errors[0].$message }}
      </small>
    </div>

    <div class="form-group">
      <label for="type">Meal Type</label>
      <Dropdown
        id="type"
        v-model="form.type"
        :options="mealTypes"
        optionLabel="label"
        optionValue="value"
        placeholder="Select meal type"
        :class="{ 'p-invalid': v$.type.$error }"
      />
      <small class="p-error" v-if="v$.type.$error">
        {{ v$.type.$errors[0].$message }}
      </small>
    </div>

    <div class="form-group">
      <label for="time">Time</label>
      <Calendar
        id="time"
        v-model="form.time"
        :showTime="true"
        :showSeconds="false"
        :class="{ 'p-invalid': v$.time.$error }"
      />
      <small class="p-error" v-if="v$.time.$error">
        {{ v$.time.$errors[0].$message }}
      </small>
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
        :label="isEditing ? 'Update Meal' : 'Add Meal'"
        :loading="loading"
      />
    </div>
  </form>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue'
import { useVuelidate } from '@vuelidate/core'
import { required, minValue } from '@vuelidate/validators'

interface Meal {
  id?: number
  name: string
  type: string
  time: Date
  calories: number
  protein: number
  carbs: number
  fats: number
}

const props = defineProps<{
  meal?: Meal
  loading?: boolean
}>()

const emit = defineEmits<{
  (e: 'submit', meal: Meal): void
  (e: 'cancel'): void
}>()

const mealTypes = [
  { label: 'Breakfast', value: 'breakfast' },
  { label: 'Lunch', value: 'lunch' },
  { label: 'Dinner', value: 'dinner' },
  { label: 'Snack', value: 'snack' }
]

const form = ref<Meal>({
  name: props.meal?.name || '',
  type: props.meal?.type || '',
  time: props.meal?.time || new Date(),
  calories: props.meal?.calories || 0,
  protein: props.meal?.protein || 0,
  carbs: props.meal?.carbs || 0,
  fats: props.meal?.fats || 0
})

const rules = {
  name: { required },
  type: { required },
  time: { required },
  calories: { required, minValue: minValue(0) },
  protein: { required, minValue: minValue(0) },
  carbs: { required, minValue: minValue(0) },
  fats: { required, minValue: minValue(0) }
}

const v$ = useVuelidate(rules, form)

const isEditing = computed(() => !!props.meal)

const handleSubmit = async () => {
  const isValid = await v$.value.$validate()
  if (isValid) {
    emit('submit', {
      ...form.value,
      id: props.meal?.id
    })
  }
}
</script>

<style lang="scss" scoped>
.meal-form {
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
    .p-calendar,
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
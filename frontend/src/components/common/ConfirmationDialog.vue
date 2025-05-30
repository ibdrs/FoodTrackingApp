<template>
  <Dialog
    v-model:visible="visible"
    :modal="true"
    :closable="false"
    :style="{ width: '450px' }"
    :header="title"
    class="confirmation-dialog"
  >
    <div class="dialog-content">
      <div class="icon">
        <i :class="icon" />
      </div>
      <p class="message">{{ message }}</p>
    </div>

    <template #footer>
      <div class="dialog-footer">
        <Button
          :label="cancelLabel"
          class="p-button-text"
          @click="handleCancel"
        />
        <Button
          :label="confirmLabel"
          :class="confirmButtonClass"
          :loading="loading"
          @click="handleConfirm"
        />
      </div>
    </template>
  </Dialog>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'

const props = defineProps<{
  modelValue: boolean
  title: string
  message: string
  icon?: string
  confirmLabel?: string
  cancelLabel?: string
  confirmButtonClass?: string
  loading?: boolean
}>()

const emit = defineEmits<{
  (e: 'update:modelValue', value: boolean): void
  (e: 'confirm'): void
  (e: 'cancel'): void
}>()

const visible = ref(props.modelValue)

watch(() => props.modelValue, (newValue) => {
  visible.value = newValue
})

watch(visible, (newValue) => {
  emit('update:modelValue', newValue)
})

const handleConfirm = () => {
  emit('confirm')
}

const handleCancel = () => {
  emit('cancel')
  visible.value = false
}
</script>

<style lang="scss" scoped>
.confirmation-dialog {
  :deep(.p-dialog-content) {
    padding: $spacing-lg;
  }

  .dialog-content {
    @include flex(column, center, center);
    gap: $spacing-md;
    text-align: center;

    .icon {
      @include flex(row, center, center);
      width: 64px;
      height: 64px;
      background-color: $surface-ground;
      border-radius: 50%;
      margin-bottom: $spacing-sm;

      i {
        font-size: 24px;
        color: $primary-color;
      }
    }

    .message {
      margin: 0;
      color: $text-color;
      font-size: $font-size-md;
    }
  }

  .dialog-footer {
    @include flex(row, flex-end, center);
    gap: $spacing-sm;
  }
}
</style> 
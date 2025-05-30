<template>
  <Toast
    ref="toast"
    position="top-right"
    :baseZIndex="1000"
    :breakpoints="{ '960px': { width: '100%', right: '0', left: '0' } }"
  />
</template>

<script setup lang="ts">
import { ref } from 'vue'

interface NotificationOptions {
  severity?: 'success' | 'info' | 'warn' | 'error'
  summary?: string
  detail?: string
  life?: number
}

const toast = ref()

const show = (options: NotificationOptions) => {
  toast.value?.add({
    severity: options.severity || 'info',
    summary: options.summary || '',
    detail: options.detail || '',
    life: options.life || 3000
  })
}

const showSuccess = (message: string, summary?: string) => {
  show({
    severity: 'success',
    summary: summary || 'Success',
    detail: message
  })
}

const showError = (message: string, summary?: string) => {
  show({
    severity: 'error',
    summary: summary || 'Error',
    detail: message
  })
}

const showInfo = (message: string, summary?: string) => {
  show({
    severity: 'info',
    summary: summary || 'Information',
    detail: message
  })
}

const showWarning = (message: string, summary?: string) => {
  show({
    severity: 'warn',
    summary: summary || 'Warning',
    detail: message
  })
}

defineExpose({
  show,
  showSuccess,
  showError,
  showInfo,
  showWarning
})
</script>

<style lang="scss" scoped>
:deep(.p-toast) {
  .p-toast-message {
    &.p-toast-message-success {
      background-color: $success-color;
      border: none;
      color: white;

      .p-toast-message-icon,
      .p-toast-icon-close {
        color: white;
      }
    }

    &.p-toast-message-error {
      background-color: $error-color;
      border: none;
      color: white;

      .p-toast-message-icon,
      .p-toast-icon-close {
        color: white;
      }
    }

    &.p-toast-message-info {
      background-color: $info-color;
      border: none;
      color: white;

      .p-toast-message-icon,
      .p-toast-icon-close {
        color: white;
      }
    }

    &.p-toast-message-warn {
      background-color: $warning-color;
      border: none;
      color: white;

      .p-toast-message-icon,
      .p-toast-icon-close {
        color: white;
      }
    }

    .p-toast-message-content {
      padding: $spacing-md;
    }

    .p-toast-message-icon {
      font-size: $font-size-lg;
    }

    .p-toast-message-text {
      margin: 0 $spacing-md;
    }

    .p-toast-summary {
      font-weight: 600;
      font-size: $font-size-md;
    }

    .p-toast-detail {
      margin-top: $spacing-xs;
      font-size: $font-size-sm;
    }
  }
}
</style> 
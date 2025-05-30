<template>
  <div class="reset-password-view">
    <h2 class="form-title">Set New Password</h2>
    <p class="form-description">
      Please enter your new password below.
    </p>

    <form @submit.prevent="handleSubmit" class="reset-password-form">
      <div class="form-group">
        <label for="password">New Password</label>
        <Password
          id="password"
          v-model="form.password"
          class="w-full"
          :class="{ 'p-invalid': v$.password.$error }"
          toggleMask
          placeholder="Enter new password"
        />
        <small class="p-error" v-if="v$.password.$error">
          {{ v$.password.$errors[0].$message }}
        </small>
      </div>

      <div class="form-group">
        <label for="confirmPassword">Confirm New Password</label>
        <Password
          id="confirmPassword"
          v-model="form.confirmPassword"
          class="w-full"
          :class="{ 'p-invalid': v$.confirmPassword.$error }"
          :feedback="false"
          toggleMask
          placeholder="Confirm new password"
        />
        <small class="p-error" v-if="v$.confirmPassword.$error">
          {{ v$.confirmPassword.$errors[0].$message }}
        </small>
      </div>

      <Button
        type="submit"
        label="Reset Password"
        class="w-full"
        :loading="loading"
      />

      <div class="form-footer">
        <p>
          Remember your password?
          <router-link to="/auth/login">
            Back to login
          </router-link>
        </p>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { required, minLength, sameAs } from '@vuelidate/validators'
import { useToast } from 'primevue/usetoast'

const router = useRouter()
const route = useRoute()
const toast = useToast()

const loading = ref(false)

const form = reactive({
  password: '',
  confirmPassword: ''
})

const rules = {
  password: { required, minLength: minLength(6) },
  confirmPassword: { required, sameAs: sameAs(form.password) }
}

const v$ = useVuelidate(rules, form)

const handleSubmit = async () => {
  const isValid = await v$.value.$validate()
  
  if (!isValid) {
    return
  }

  loading.value = true

  try {
    // TODO: Implement actual password reset logic
    const token = route.query.token
    if (!token) {
      throw new Error('Invalid reset token')
    }

    await new Promise(resolve => setTimeout(resolve, 1000)) // Simulated API call
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Your password has been reset successfully.',
      life: 3000
    })

    router.push('/auth/login')
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to reset password. Please try again.',
      life: 3000
    })
  } finally {
    loading.value = false
  }
}
</script>

<style lang="scss" scoped>
.reset-password-view {
  .form-title {
    text-align: center;
    margin-bottom: $spacing-md;
    color: $text-color;
  }

  .form-description {
    text-align: center;
    color: $secondary-color;
    margin-bottom: $spacing-xl;
  }
}

.reset-password-form {
  .form-group {
    margin-bottom: $spacing-md;

    label {
      display: block;
      margin-bottom: $spacing-xs;
      color: $text-color;
      font-weight: 500;
    }
  }

  .form-footer {
    margin-top: $spacing-lg;
    text-align: center;
    color: $secondary-color;
    font-size: $font-size-sm;

    a {
      color: $primary-color;
      text-decoration: none;
      font-weight: 500;

      &:hover {
        text-decoration: underline;
      }
    }
  }
}

:deep(.p-password) {
  width: 100%;
}

:deep(.p-password-input) {
  width: 100%;
}
</style> 
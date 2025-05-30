<template>
  <div class="forgot-password-view">
    <h2 class="form-title">Reset Password</h2>
    <p class="form-description">
      Enter your email address and we'll send you instructions to reset your password.
    </p>

    <form @submit.prevent="handleSubmit" class="forgot-password-form">
      <div class="form-group">
        <label for="email">Email</label>
        <InputText
          id="email"
          v-model="form.email"
          type="email"
          class="w-full"
          :class="{ 'p-invalid': v$.email.$error }"
          placeholder="Enter your email"
        />
        <small class="p-error" v-if="v$.email.$error">
          {{ v$.email.$errors[0].$message }}
        </small>
      </div>

      <Button
        type="submit"
        label="Send Reset Link"
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
import { useRouter } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { required, email } from '@vuelidate/validators'
import { useToast } from 'primevue/usetoast'

const router = useRouter()
const toast = useToast()

const loading = ref(false)

const form = reactive({
  email: ''
})

const rules = {
  email: { required, email }
}

const v$ = useVuelidate(rules, form)

const handleSubmit = async () => {
  const isValid = await v$.value.$validate()
  
  if (!isValid) {
    return
  }

  loading.value = true

  try {
    // TODO: Implement actual password reset request logic
    await new Promise(resolve => setTimeout(resolve, 1000)) // Simulated API call
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Password reset instructions have been sent to your email.',
      life: 3000
    })

    router.push('/auth/login')
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to send reset instructions. Please try again.',
      life: 3000
    })
  } finally {
    loading.value = false
  }
}
</script>

<style lang="scss" scoped>
.forgot-password-view {
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

.forgot-password-form {
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
</style> 
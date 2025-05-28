<template>
  <div class="login-view">
    <h2 class="form-title">Welcome Back</h2>
    <form @submit.prevent="handleSubmit" class="login-form">
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

      <div class="form-group">
        <label for="password">Password</label>
        <Password
          id="password"
          v-model="form.password"
          class="w-full"
          :class="{ 'p-invalid': v$.password.$error }"
          :feedback="false"
          toggleMask
          placeholder="Enter your password"
        />
        <small class="p-error" v-if="v$.password.$error">
          {{ v$.password.$errors[0].$message }}
        </small>
      </div>

      <div class="form-options">
        <div class="remember-me">
          <Checkbox
            id="remember"
            v-model="form.remember"
            :binary="true"
          />
          <label for="remember">Remember me</label>
        </div>
        <router-link
          to="/auth/forgot-password"
          class="forgot-password"
        >
          Forgot password?
        </router-link>
      </div>

      <Button
        type="submit"
        label="Sign In"
        class="w-full"
        :loading="loading"
      />

      <div class="form-footer">
        <p>
          Don't have an account?
          <router-link to="/auth/register">
            Sign up
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
import { required, email, minLength } from '@vuelidate/validators'
import { useToast } from 'primevue/usetoast'
import Password from 'primevue/password'

const router = useRouter()
const route = useRoute()
const toast = useToast()

const loading = ref(false)

const form = reactive({
  email: '',
  password: '',
  remember: false
})

const rules = {
  email: { required, email },
  password: { required, minLength: minLength(6) }
}

const v$ = useVuelidate(rules, form)

const handleSubmit = async () => {
  const isValid = await v$.value.$validate()
  
  if (!isValid) {
    return
  }

  loading.value = true

  try {
    // TODO: Implement actual login logic
    await new Promise(resolve => setTimeout(resolve, 1000)) // Simulated API call
    
    // Store token and user data
    localStorage.setItem('token', 'dummy-token')
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Welcome back!',
      life: 3000
    })

    // Redirect to the intended page or dashboard
    const redirectPath = route.query.redirect as string || '/'
    router.push(redirectPath)
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Invalid email or password',
      life: 3000
    })
  } finally {
    loading.value = false
  }
}
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_variables.scss";
@import "@/assets/styles/_mixins.scss";

.login-view {
  @include flex(column, center, center);
  min-height: calc(100vh - 64px);
  padding: $spacing-xl;

  .form-title {
    text-align: center;
    margin-bottom: $spacing-xl;
    color: $text-color;
  }
}

.login-form {
  width: 100%;
  max-width: 400px;
  padding: $spacing-xl;
  background: white;
  border-radius: $border-radius-lg;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);

  .form-group {
    margin-bottom: $spacing-md;

    label {
      display: block;
      margin-bottom: $spacing-xs;
      color: $text-color;
      font-weight: 500;
    }
  }

  :deep(.p-inputtext),
  :deep(.p-password) {
    width: 100%;

    .p-password-input {
      width: 100%;
    }
  }

  .form-options {
    @include flex(row, space-between, center);
    margin-bottom: $spacing-lg;

    .remember-me {
      @include flex(row, flex-start, center);
      gap: $spacing-xs;

      label {
        color: $secondary-color;
        font-size: $font-size-sm;
      }
    }

    .forgot-password {
      color: $primary-color;
      font-size: $font-size-sm;
      text-decoration: none;

      &:hover {
        text-decoration: underline;
      }
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
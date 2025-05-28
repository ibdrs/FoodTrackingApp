<template>
  <div class="register-view">
    <h2 class="form-title">Create Account</h2>
    <form @submit.prevent="handleSubmit" class="register-form">
      <div class="form-group">
        <label for="name">Full Name</label>
        <InputText
          id="name"
          v-model="form.name"
          class="w-full"
          :class="{ 'p-invalid': v$.name.$error }"
          placeholder="Enter your full name"
        />
        <small class="p-error" v-if="v$.name.$error">
          {{ v$.name.$errors[0].$message }}
        </small>
      </div>

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
          toggleMask
          placeholder="Create a password"
        />
        <small class="p-error" v-if="v$.password.$error">
          {{ v$.password.$errors[0].$message }}
        </small>
      </div>

      <div class="form-group">
        <label for="confirmPassword">Confirm Password</label>
        <Password
          id="confirmPassword"
          v-model="form.confirmPassword"
          class="w-full"
          :class="{ 'p-invalid': v$.confirmPassword.$error }"
          :feedback="false"
          toggleMask
          placeholder="Confirm your password"
        />
        <small class="p-error" v-if="v$.confirmPassword.$error">
          {{ v$.confirmPassword.$errors[0].$message }}
        </small>
      </div>

      <div class="form-group">
        <div class="terms-checkbox">
          <Checkbox
            id="terms"
            v-model="form.acceptTerms"
            :binary="true"
            :class="{ 'p-invalid': v$.acceptTerms.$error }"
          />
          <label for="terms">
            I agree to the
            <a href="#" @click.prevent="showTerms">Terms of Service</a>
            and
            <a href="#" @click.prevent="showPrivacy">Privacy Policy</a>
          </label>
        </div>
        <small class="p-error" v-if="v$.acceptTerms.$error">
          {{ v$.acceptTerms.$errors[0].$message }}
        </small>
      </div>

      <Button
        type="submit"
        label="Create Account"
        class="w-full"
        :loading="loading"
      />

      <div class="form-footer">
        <p>
          Already have an account?
          <router-link to="/auth/login">
            Sign in
          </router-link>
        </p>
      </div>
    </form>

    <!-- Terms Dialog -->
    <Dialog
      v-model:visible="termsVisible"
      modal
      header="Terms of Service"
      :style="{ width: '50vw' }"
    >
      <p>Terms of service content goes here...</p>
    </Dialog>

    <!-- Privacy Dialog -->
    <Dialog
      v-model:visible="privacyVisible"
      modal
      header="Privacy Policy"
      :style="{ width: '50vw' }"
    >
      <p>Privacy policy content goes here...</p>
    </Dialog>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { useVuelidate } from '@vuelidate/core'
import { required, email, minLength, sameAs } from '@vuelidate/validators'
import { useToast } from 'primevue/usetoast'

const router = useRouter()
const toast = useToast()

const loading = ref(false)
const termsVisible = ref(false)
const privacyVisible = ref(false)

const form = reactive({
  name: '',
  email: '',
  password: '',
  confirmPassword: '',
  acceptTerms: false
})

const rules = {
  name: { required },
  email: { required, email },
  password: { required, minLength: minLength(6) },
  confirmPassword: { required, sameAs: sameAs(form.password) },
  acceptTerms: { required }
}

const v$ = useVuelidate(rules, form)

const handleSubmit = async () => {
  const isValid = await v$.value.$validate()
  
  if (!isValid) {
    return
  }

  loading.value = true

  try {
    // TODO: Implement actual registration logic
    await new Promise(resolve => setTimeout(resolve, 1000)) // Simulated API call
    
    toast.add({
      severity: 'success',
      summary: 'Success',
      detail: 'Account created successfully!',
      life: 3000
    })

    router.push('/auth/login')
  } catch (error) {
    toast.add({
      severity: 'error',
      summary: 'Error',
      detail: 'Failed to create account. Please try again.',
      life: 3000
    })
  } finally {
    loading.value = false
  }
}

const showTerms = () => {
  termsVisible.value = true
}

const showPrivacy = () => {
  privacyVisible.value = true
}
</script>

<style lang="scss" scoped>
.register-view {
  .form-title {
    text-align: center;
    margin-bottom: $spacing-xl;
    color: $text-color;
  }
}

.register-form {
  .form-group {
    margin-bottom: $spacing-md;

    label {
      display: block;
      margin-bottom: $spacing-xs;
      color: $text-color;
      font-weight: 500;
    }
  }

  .terms-checkbox {
    @include flex(row, flex-start, center);
    gap: $spacing-xs;

    label {
      color: $secondary-color;
      font-size: $font-size-sm;
    }

    a {
      color: $primary-color;
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

:deep(.p-password) {
  width: 100%;
}

:deep(.p-password-input) {
  width: 100%;
}
</style> 
<template>
  <div class="not-found-view">
    <div class="not-found-content">
      <i class="pi pi-exclamation-circle"></i>
      <h1>404</h1>
      <h2>Page Not Found</h2>
      <p>The page you are looking for doesn't exist or has been moved.</p>
      <Button
        label="Go to Dashboard"
        icon="pi pi-home"
        @click="router.push('/')"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
</script>

<style lang="scss" scoped>
@import "@/assets/styles/_variables.scss";
@import "@/assets/styles/_mixins.scss";

.not-found-view {
  min-height: calc(100vh - 64px);
  @include flex(row, center, center);
  padding: $spacing-xl;

  .not-found-content {
    text-align: center;
    max-width: 500px;

    i {
      font-size: 4rem;
      color: $primary-color;
      margin-bottom: $spacing-md;
    }

    h1 {
      font-size: 6rem;
      color: $primary-color;
      margin: 0;
      line-height: 1;
    }

    h2 {
      font-size: 2rem;
      color: $text-color;
      margin: $spacing-sm 0;
    }

    p {
      color: $secondary-color;
      margin-bottom: $spacing-lg;
    }
  }
}
</style> 
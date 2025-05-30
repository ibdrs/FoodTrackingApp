<template>
  <div class="search-and-filter">
    <div class="search-box">
      <span class="p-input-icon-left">
        <i class="pi pi-search" />
        <InputText
          v-model="searchQuery"
          placeholder="Search..."
          class="search-input"
          @input="handleSearch"
        />
      </span>
    </div>

    <div class="filters">
      <div class="filter-group" v-if="showCategoryFilter">
        <Dropdown
          v-model="selectedCategory"
          :options="categories"
          optionLabel="label"
          optionValue="value"
          placeholder="Category"
          class="filter-dropdown"
          @change="handleFilterChange"
        />
      </div>

      <div class="filter-group" v-if="showSortFilter">
        <Dropdown
          v-model="selectedSort"
          :options="sortOptions"
          optionLabel="label"
          optionValue="value"
          placeholder="Sort by"
          class="filter-dropdown"
          @change="handleFilterChange"
        />
      </div>

      <div class="filter-group" v-if="showDateFilter">
        <Calendar
          v-model="selectedDate"
          :showIcon="true"
          dateFormat="yy-mm-dd"
          class="filter-calendar"
          @date-select="handleFilterChange"
        />
      </div>

      <div class="filter-group" v-if="showRangeFilter">
        <div class="range-filter">
          <span class="range-label">Range:</span>
          <InputNumber
            v-model="rangeStart"
            placeholder="Min"
            class="range-input"
            @input="handleFilterChange"
          />
          <span class="range-separator">-</span>
          <InputNumber
            v-model="rangeEnd"
            placeholder="Max"
            class="range-input"
            @input="handleFilterChange"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'

interface Category {
  label: string
  value: string
}

interface SortOption {
  label: string
  value: string
}

const props = defineProps<{
  showCategoryFilter?: boolean
  showSortFilter?: boolean
  showDateFilter?: boolean
  showRangeFilter?: boolean
  categories?: Category[]
  sortOptions?: SortOption[]
  initialSearch?: string
  initialCategory?: string
  initialSort?: string
  initialDate?: Date
  initialRangeStart?: number
  initialRangeEnd?: number
}>()

const emit = defineEmits<{
  (e: 'filter', filters: {
    search: string
    category?: string
    sort?: string
    date?: Date
    rangeStart?: number
    rangeEnd?: number
  }): void
}>()

const searchQuery = ref(props.initialSearch || '')
const selectedCategory = ref(props.initialCategory || '')
const selectedSort = ref(props.initialSort || '')
const selectedDate = ref(props.initialDate || null)
const rangeStart = ref(props.initialRangeStart || null)
const rangeEnd = ref(props.initialRangeEnd || null)

const handleSearch = () => {
  emitFilters()
}

const handleFilterChange = () => {
  emitFilters()
}

const emitFilters = () => {
  emit('filter', {
    search: searchQuery.value,
    category: selectedCategory.value,
    sort: selectedSort.value,
    date: selectedDate.value,
    rangeStart: rangeStart.value,
    rangeEnd: rangeEnd.value
  })
}

// Watch for prop changes to update local state
watch(() => props.initialSearch, (newValue) => {
  searchQuery.value = newValue || ''
})

watch(() => props.initialCategory, (newValue) => {
  selectedCategory.value = newValue || ''
})

watch(() => props.initialSort, (newValue) => {
  selectedSort.value = newValue || ''
})

watch(() => props.initialDate, (newValue) => {
  selectedDate.value = newValue || null
})

watch(() => props.initialRangeStart, (newValue) => {
  rangeStart.value = newValue || null
})

watch(() => props.initialRangeEnd, (newValue) => {
  rangeEnd.value = newValue || null
})
</script>

<style lang="scss" scoped>
.search-and-filter {
  @include flex(column, flex-start, stretch);
  gap: $spacing-md;

  .search-box {
    .search-input {
      width: 100%;
    }
  }

  .filters {
    @include flex(row, flex-start, center);
    gap: $spacing-md;
    flex-wrap: wrap;

    .filter-group {
      .filter-dropdown,
      .filter-calendar {
        min-width: 200px;
      }

      .range-filter {
        @include flex(row, flex-start, center);
        gap: $spacing-sm;

        .range-label {
          color: $text-color;
          font-weight: 500;
        }

        .range-input {
          width: 100px;
        }

        .range-separator {
          color: $secondary-color;
        }
      }
    }
  }
}
</style> 
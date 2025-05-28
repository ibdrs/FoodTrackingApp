<template>
  <div class="data-table">
    <DataTable
      :value="items"
      :paginator="paginator"
      :rows="rows"
      :rowsPerPageOptions="rowsPerPageOptions"
      :totalRecords="totalRecords"
      :loading="loading"
      :sortField="sortField"
      :sortOrder="sortOrder"
      :filters="filters"
      :globalFilterFields="globalFilterFields"
      :selection="selection"
      :selectionMode="selectionMode"
      :metaKeySelection="metaKeySelection"
      :scrollable="scrollable"
      :scrollHeight="scrollHeight"
      :resizableColumns="resizableColumns"
      :showGridlines="showGridlines"
      :stripedRows="stripedRows"
      :class="tableClass"
      @page="onPage"
      @sort="onSort"
      @filter="onFilter"
      @row-select="onRowSelect"
      @row-unselect="onRowUnselect"
      @row-select-all="onRowSelectAll"
      @row-unselect-all="onRowUnselectAll"
    >
      <template #empty>
        <EmptyState
          icon="pi pi-table"
          title="No Data"
          :description="emptyMessage"
        >
          <template #actions>
            <slot name="empty-actions" />
          </template>
        </EmptyState>
      </template>

      <template #loading>
        <LoadingState :message="loadingMessage" />
      </template>

      <template v-for="(_, slot) in $slots" #[slot]="scope">
        <slot :name="slot" v-bind="scope" />
      </template>
    </DataTable>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'

interface DataTableProps {
  items: any[]
  paginator?: boolean
  rows?: number
  rowsPerPageOptions?: number[]
  totalRecords?: number
  loading?: boolean
  sortField?: string
  sortOrder?: number
  filters?: any
  globalFilterFields?: string[]
  selection?: any
  selectionMode?: 'single' | 'multiple'
  metaKeySelection?: boolean
  scrollable?: boolean
  scrollHeight?: string
  resizableColumns?: boolean
  showGridlines?: boolean
  stripedRows?: boolean
  tableClass?: string
  emptyMessage?: string
  loadingMessage?: string
}

const props = withDefaults(defineProps<DataTableProps>(), {
  paginator: true,
  rows: 10,
  rowsPerPageOptions: () => [10, 20, 50],
  loading: false,
  selectionMode: 'single',
  metaKeySelection: true,
  scrollable: false,
  resizableColumns: true,
  showGridlines: false,
  stripedRows: true,
  emptyMessage: 'No records found',
  loadingMessage: 'Loading...'
})

const emit = defineEmits<{
  (e: 'page', event: any): void
  (e: 'sort', event: any): void
  (e: 'filter', event: any): void
  (e: 'row-select', event: any): void
  (e: 'row-unselect', event: any): void
  (e: 'row-select-all', event: any): void
  (e: 'row-unselect-all', event: any): void
}>()

const onPage = (event: any) => {
  emit('page', event)
}

const onSort = (event: any) => {
  emit('sort', event)
}

const onFilter = (event: any) => {
  emit('filter', event)
}

const onRowSelect = (event: any) => {
  emit('row-select', event)
}

const onRowUnselect = (event: any) => {
  emit('row-unselect', event)
}

const onRowSelectAll = (event: any) => {
  emit('row-select-all', event)
}

const onRowUnselectAll = (event: any) => {
  emit('row-unselect-all', event)
}
</script>

<style lang="scss" scoped>
.data-table {
  :deep(.p-datatable) {
    .p-datatable-header {
      background-color: $surface-ground;
      border: none;
      padding: $spacing-md;
    }

    .p-datatable-thead > tr > th {
      background-color: $surface-ground;
      color: $text-color;
      font-weight: 600;
      padding: $spacing-md;
      border: none;
      border-bottom: 1px solid $surface-border;

      &.p-sortable-column {
        &:hover {
          background-color: $surface-hover;
        }

        &.p-highlight {
          background-color: $surface-hover;
          color: $primary-color;

          .p-sortable-column-icon {
            color: $primary-color;
          }
        }
      }
    }

    .p-datatable-tbody > tr {
      > td {
        padding: $spacing-md;
        border: none;
        border-bottom: 1px solid $surface-border;
      }

      &:hover {
        background-color: $surface-hover;
      }

      &.p-highlight {
        background-color: $surface-hover;
      }
    }

    .p-datatable-footer {
      background-color: $surface-ground;
      border: none;
      padding: $spacing-md;
    }

    .p-paginator {
      background-color: transparent;
      border: none;
      padding: 0;

      .p-paginator-first,
      .p-paginator-prev,
      .p-paginator-next,
      .p-paginator-last {
        background-color: $surface-card;
        border: 1px solid $surface-border;
        color: $text-color;
        min-width: 2.5rem;
        height: 2.5rem;
        margin: 0 0.2rem;
        border-radius: $border-radius-sm;

        &:hover {
          background-color: $surface-hover;
          border-color: $surface-border;
          color: $text-color;
        }

        &.p-disabled {
          background-color: $surface-ground;
          border-color: $surface-border;
          color: $secondary-color;
        }
      }

      .p-paginator-pages {
        .p-paginator-page {
          background-color: $surface-card;
          border: 1px solid $surface-border;
          color: $text-color;
          min-width: 2.5rem;
          height: 2.5rem;
          margin: 0 0.2rem;
          border-radius: $border-radius-sm;

          &:hover {
            background-color: $surface-hover;
            border-color: $surface-border;
            color: $text-color;
          }

          &.p-highlight {
            background-color: $primary-color;
            border-color: $primary-color;
            color: white;
          }
        }
      }
    }
  }
}
</style> 
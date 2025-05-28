import { createApp } from 'vue'
import { createPinia } from 'pinia'
import PrimeVue from 'primevue/config'
import ToastService from 'primevue/toastservice'
import ConfirmationService from 'primevue/confirmationservice'

// PrimeVue components
import Button from 'primevue/button'
import InputText from 'primevue/inputtext'
import InputNumber from 'primevue/inputnumber'
import Dropdown from 'primevue/dropdown'
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Dialog from 'primevue/dialog'
import Toast from 'primevue/toast'
import ConfirmDialog from 'primevue/confirmdialog'
import Card from 'primevue/card'
import Chart from 'primevue/chart'
import ProgressBar from 'primevue/progressbar'
import Menu from 'primevue/menu'
import Menubar from 'primevue/menubar'
import Sidebar from 'primevue/sidebar'
import Breadcrumb from 'primevue/breadcrumb'
import Calendar from 'primevue/calendar'
import MultiSelect from 'primevue/multiselect'
import TabView from 'primevue/tabview'
import TabPanel from 'primevue/tabpanel'
import Divider from 'primevue/divider'
import Badge from 'primevue/badge'
import Avatar from 'primevue/avatar'
import Tooltip from 'primevue/tooltip'

// PrimeVue styles
import 'primevue/resources/themes/lara-light-blue/theme.css'
import 'primevue/resources/primevue.min.css'
import 'primeicons/primeicons.css'

// Our styles
import './assets/styles/main.scss'

import App from './App.vue'
import router from './router'

const app = createApp(App)

// Register PrimeVue components
app.component('Button', Button)
app.component('InputText', InputText)
app.component('InputNumber', InputNumber)
app.component('Dropdown', Dropdown)
app.component('DataTable', DataTable)
app.component('Column', Column)
app.component('Dialog', Dialog)
app.component('Toast', Toast)
app.component('ConfirmDialog', ConfirmDialog)
app.component('Card', Card)
app.component('Chart', Chart)
app.component('ProgressBar', ProgressBar)
app.component('Menu', Menu)
app.component('Menubar', Menubar)
app.component('Sidebar', Sidebar)
app.component('Breadcrumb', Breadcrumb)
app.component('Calendar', Calendar)
app.component('MultiSelect', MultiSelect)
app.component('TabView', TabView)
app.component('TabPanel', TabPanel)
app.component('Divider', Divider)
app.component('Badge', Badge)
app.component('Avatar', Avatar)

// Register PrimeVue directives
app.directive('tooltip', Tooltip)

// Use plugins
app.use(createPinia())
app.use(router)
app.use(PrimeVue, { ripple: true })
app.use(ToastService)
app.use(ConfirmationService)

app.mount('#app') 
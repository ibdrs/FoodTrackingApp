import { createApp } from 'vue';
import App from './App.vue';
import router from './router';

import PrimeVue from 'primevue/config';

// Import the Lara preset
import Lara from '@primevue/themes/lara';
import 'primeicons/primeicons.css'; // PrimeIcons

// Updated and new component imports
import Button from 'primevue/button';
import InputText from 'primevue/inputtext';
import DatePicker from 'primevue/datepicker'; // Formerly Calendar
import Select from 'primevue/select';       // Formerly Dropdown
import DataTable from 'primevue/datatable';
import Column from 'primevue/column';
import Card from 'primevue/card';
import Panel from 'primevue/panel';
import Chart from 'primevue/chart';
import Dialog from 'primevue/dialog';
import Menubar from 'primevue/menubar';
import Textarea from 'primevue/textarea';
import InputNumber from 'primevue/inputnumber';
import Fieldset from 'primevue/fieldset';
import Toast from 'primevue/toast';
import ToastService from 'primevue/toastservice';
import DataView from 'primevue/dataview';
import SelectButton from 'primevue/selectbutton'; // Replaces DataViewLayoutOptions
import Tag from 'primevue/tag';

// Custom global styles (if any)
import './assets/main.css';

const app = createApp(App);

app.use(router);

// Configure PrimeVue with the new theme system
app.use(PrimeVue, {
    ripple: true,
    theme: {
        preset: Lara, // Use the imported preset
        options: {
            prefix: 'p', // Optional: CSS prefix, default is 'p'
            darkModeSelector: '.my-app-dark', // Optional: Your dark mode selector if you implement dark mode
            cssLayer: { // Optional: Configure CSS Layering
                name: 'primevue',
                order: 'primevue' // Or 'tailwind-base, primevue, tailwind-utilities' if using Tailwind
            }
        }
    }
});
app.use(ToastService);

// Register PrimeVue components globally
app.component('Button', Button);
app.component('InputText', InputText);
app.component('DatePicker', DatePicker); // Updated
app.component('Select', Select);         // Updated
app.component('DataTable', DataTable);
app.component('Column', Column);
app.component('Card', Card);
app.component('Panel', Panel);
app.component('Chart', Chart);
app.component('Dialog', Dialog);
app.component('Menubar', Menubar);
app.component('Textarea', Textarea);
app.component('InputNumber', InputNumber);
app.component('Fieldset', Fieldset);
app.component('Toast', Toast);
app.component('DataView', DataView);
app.component('SelectButton', SelectButton); // Updated
app.component('Tag', Tag);

app.mount('#app');
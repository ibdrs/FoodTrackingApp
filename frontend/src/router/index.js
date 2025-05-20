import { createRouter, createWebHistory } from 'vue-router';
import AddMeal from '../components/AddMeal.Vue';
import DailyOverview from '../components/DailyOverview.vue';
import PresetMeals from '../components/PresetMeals.vue';
import FoodHistory from '../components/FoodHistory.vue';

const routes = [
  {
    path: '/',
    redirect: '/overview', // Default to overview page
  },
  {
    path: '/overview',
    name: 'DailyOverview',
    component: DailyOverview,
  },
  {
    path: '/add-meal',
    name: 'AddMeal',
    component: AddMeal,
  },
  {
    path: '/preset-meals',
    name: 'PresetMeals',
    component: PresetMeals,
  },
  {
    path: '/history',
    name: 'FoodHistory',
    component: FoodHistory,
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
});

export default router;
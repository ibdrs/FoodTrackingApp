export interface Food {
  id: number
  name: string
  category: string
  calories: number
  protein: number
  carbs: number
  fats: number
  servingSize: number
  servingUnit: string
}

export interface Meal {
  id: number
  name: string
  time: Date
  calories: number
  protein: number
  carbs: number
  fats: number
}

export interface User {
  id: number
  fullName: string
  email: string
}

export interface NutritionGoals {
  calories: number
  protein: number
  carbs: number
  fats: number
} 
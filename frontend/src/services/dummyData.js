export const unitOptions = [
    { label: 'grams (g)', value: 'g' },
    { label: 'milliliters (ml)', value: 'ml' },
    { label: 'ounces (oz)', value: 'oz' },
    { label: 'pounds (lb)', value: 'lb' },
    { label: 'piece(s)', value: 'piece' },
    { label: 'slice(s)', value: 'slice' },
    { label: 'cup(s)', value: 'cup' },
    { label: 'tbsp', value: 'tbsp' },
    { label: 'tsp', value: 'tsp' },
];

export const dummyPresetMeals = [
    {
        id: 'preset1',
        name: 'Chicken Salad Lunch',
        description: 'A light and healthy chicken salad.',
        summary: { calories: 450, protein: 35, carbs: 20, fat: 20 },
        ingredients: [
            { id: 'i1', name: 'Grilled Chicken Breast', quantity: 150, unit: 'g' },
            { id: 'i2', name: 'Mixed Greens', quantity: 100, unit: 'g' },
            { id: 'i3', name: 'Cherry Tomatoes', quantity: 50, unit: 'g' },
            { id: 'i4', name: 'Light Vinaigrette', quantity: 2, unit: 'tbsp' },
        ]
    },
    {
        id: 'preset2',
        name: 'Oatmeal Breakfast',
        description: 'Quick and nutritious start to the day.',
        summary: { calories: 300, protein: 10, carbs: 55, fat: 5 },
        ingredients: [
            { id: 'i5', name: 'Rolled Oats', quantity: 50, unit: 'g' },
            { id: 'i6', name: 'Almond Milk', quantity: 1, unit: 'cup' },
            { id: 'i7', name: 'Berries', quantity: 75, unit: 'g' },
            { id: 'i8', name: 'Almonds', quantity: 10, unit: 'g' },
        ]
    },
    {
        id: 'preset3',
        name: 'Spaghetti Bolognese',
        description: 'Classic comfort food.',
        summary: { calories: 600, protein: 30, carbs: 70, fat: 25 },
        ingredients: [
            { id: 'i9', name: 'Spaghetti', quantity: 100, unit: 'g' },
            { id: 'i10', name: 'Beef Mince', quantity: 150, unit: 'g' },
            { id: 'i11', name: 'Tomato Sauce', quantity: 1, unit: 'cup' },
            { id: 'i12', name: 'Parmesan Cheese', quantity: 1, unit: 'tbsp' },
        ]
    }
];

// Dummy data for daily logs - in a real app this would come from a store or API
export const dailyLogs = {
    '2024-07-28': {
        meals: [
            { ...dummyPresetMeals[1], id: 'log1_meal1', time: '08:00' }, // Oatmeal
            { ...dummyPresetMeals[0], id: 'log1_meal2', name: "Office Chicken Salad", time: '12:30' }, // Chicken Salad
        ],
        totals: { calories: 750, protein: 45, carbs: 75, fat: 25 }
    },
    '2024-07-29': {
        meals: [
            { id: 'log2_meal1', name: 'Scrambled Eggs & Toast', time: '09:00', summary: { calories: 350, protein: 20, carbs: 30, fat: 15 }, ingredients: [] },
            { ...dummyPresetMeals[2], id: 'log2_meal2', time: '19:00' }, // Spaghetti
        ],
        totals: { calories: 950, protein: 50, carbs: 100, fat: 40 }
    }
};

// Function to get today's date in YYYY-MM-DD format for default selection
export const getTodaysDateKey = () => {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
};

// Initialize with today's data if it doesn't exist
if (!dailyLogs[getTodaysDateKey()]) {
    dailyLogs[getTodaysDateKey()] = {
        meals: [],
        totals: { calories: 0, protein: 0, carbs: 0, fat: 0 }
    };
}

// Very basic function to add a meal (doesn't persist beyond session, no real backend)
export function addMealToLog(dateKey, meal) {
    if (!dailyLogs[dateKey]) {
        dailyLogs[dateKey] = { meals: [], totals: { calories: 0, protein: 0, carbs: 0, fat: 0 } };
    }
    const newMeal = { ...meal, id: `log_meal_${Date.now()}`, time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) };
    dailyLogs[dateKey].meals.push(newMeal);
    
    // Update totals (rudimentary)
    dailyLogs[dateKey].totals.calories += meal.summary.calories || 0;
    dailyLogs[dateKey].totals.protein += meal.summary.protein || 0;
    dailyLogs[dateKey].totals.carbs += meal.summary.carbs || 0;
    dailyLogs[dateKey].totals.fat += meal.summary.fat || 0;
}
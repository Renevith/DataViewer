using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSimulator {
    public class Data {
        public class ExerciseData {
            public int Id;
            public string Name;
            public double ExerciseIndex;
            public override string ToString() {
                return Name;
            }
        }

        public class FoodData {
            public int Id;
            public string Name;
            public double GlycemicIndex;
            public override string ToString() {
                return Name;
            }
        }

        //fake database table:
        public static readonly HashSet<ExerciseData> ExerciseDatabase = new HashSet<ExerciseData> {
            new ExerciseData{Id = 1, Name = "Crunches", ExerciseIndex = 20},
            new ExerciseData{Id = 2, Name = "Walking", ExerciseIndex = 15},
            new ExerciseData{Id = 3, Name = "Running", ExerciseIndex = 40},
            new ExerciseData{Id = 4, Name = "Sprinting", ExerciseIndex = 60},
            new ExerciseData{Id = 5, Name = "Squats", ExerciseIndex = 60},
            new ExerciseData{Id = 6, Name = "Bench press", ExerciseIndex = 45},
        };

        //fake database table:
        public static readonly HashSet<FoodData> FoodDatabase = new HashSet<FoodData> {
            new FoodData{Id = 1, Name = "Banana cake, made with sugar", GlycemicIndex = 47},
            new FoodData{Id = 2, Name = "Banana cake, made without sugar", GlycemicIndex = 55},
            new FoodData{Id = 3, Name = "Sponge cake, plain", GlycemicIndex = 46},
            new FoodData{Id = 4, Name = "Vanilla cake made from packet mix with vanilla frosting (Betty Crocker)", GlycemicIndex = 42},
            new FoodData{Id = 5, Name = "Apple, made with sugar", GlycemicIndex = 44},
            new FoodData{Id = 6, Name = "Apple, made without sugar", GlycemicIndex = 48},
            new FoodData{Id = 7, Name = "Waffles, Aunt Jemima (Quaker Oats)", GlycemicIndex = 76},
            new FoodData{Id = 8, Name = "Bagel, white, frozen", GlycemicIndex = 72},
            new FoodData{Id = 9, Name = "Baguette, white, plain", GlycemicIndex = 95},
            new FoodData{Id = 10, Name = "Coarse barley bread, 75-80% kernels, average", GlycemicIndex = 34},
            new FoodData{Id = 11, Name = "Hamburger bun", GlycemicIndex = 61},
            new FoodData{Id = 12, Name = "Kaiser roll", GlycemicIndex = 73},
            new FoodData{Id = 13, Name = "Pumpernickel bread", GlycemicIndex = 56},
            new FoodData{Id = 14, Name = "50% cracked wheat kernel bread", GlycemicIndex = 58},
            new FoodData{Id = 15, Name = "White wheat flour bread", GlycemicIndex = 71},
            new FoodData{Id = 16, Name = "Wonder™ bread, average", GlycemicIndex = 73},
            new FoodData{Id = 17, Name = "Whole wheat bread, average", GlycemicIndex = 71},
            new FoodData{Id = 18, Name = "100% Whole Grain™ bread (Natural Ovens)", GlycemicIndex = 51},
            new FoodData{Id = 19, Name = "Pita bread, white", GlycemicIndex = 68},
            new FoodData{Id = 20, Name = "Corn tortilla", GlycemicIndex = 52},
            new FoodData{Id = 21, Name = "Wheat tortilla", GlycemicIndex = 30},
            new FoodData{Id = 23, Name = "Coca Cola®, average", GlycemicIndex = 63},
            new FoodData{Id = 24, Name = "Fanta®, orange soft drink", GlycemicIndex = 68},
            new FoodData{Id = 25, Name = "Lucozade®, original (sparkling glucose drink)", GlycemicIndex = 95},
            new FoodData{Id = 26, Name = "Apple juice, unsweetened, average", GlycemicIndex = 44},
            new FoodData{Id = 27, Name = "Cranberry juice cocktail (Ocean Spray®)", GlycemicIndex = 68},
            new FoodData{Id = 28, Name = "Gatorade", GlycemicIndex = 78},
            new FoodData{Id = 29, Name = "Orange juice, unsweetened", GlycemicIndex = 50},
            new FoodData{Id = 30, Name = "Tomato juice, canned", GlycemicIndex = 38},
            new FoodData{Id = 32, Name = "All-Bran™, average", GlycemicIndex = 55},
            new FoodData{Id = 33, Name = "Coco Pops™, average", GlycemicIndex = 77},
            new FoodData{Id = 34, Name = "Cornflakes™, average", GlycemicIndex = 93},
            new FoodData{Id = 35, Name = "Cream of Wheat™ (Nabisco)", GlycemicIndex = 66},
            new FoodData{Id = 36, Name = "Cream of Wheat™, Instant (Nabisco)", GlycemicIndex = 74},
            new FoodData{Id = 37, Name = "Grapenuts™, average", GlycemicIndex = 75},
            new FoodData{Id = 38, Name = "Muesli, average", GlycemicIndex = 66},
            new FoodData{Id = 39, Name = "Oatmeal, average", GlycemicIndex = 55},
            new FoodData{Id = 40, Name = "Instant oatmeal, average", GlycemicIndex = 83},
            new FoodData{Id = 41, Name = "Puffed wheat, average", GlycemicIndex = 80},
            new FoodData{Id = 42, Name = "Raisin Bran™ (Kellogg's)", GlycemicIndex = 61},
            new FoodData{Id = 43, Name = "Special K™ (Kellogg's)", GlycemicIndex = 69},
            new FoodData{Id = 45, Name = "Pearled barley, average", GlycemicIndex = 28},
            new FoodData{Id = 46, Name = "Sweet corn on the cob, average", GlycemicIndex = 60},
            new FoodData{Id = 47, Name = "Couscous, average", GlycemicIndex = 65},
            new FoodData{Id = 48, Name = "Quinoa", GlycemicIndex = 53},
            new FoodData{Id = 49, Name = "White rice, average", GlycemicIndex = 89},
            new FoodData{Id = 50, Name = "Quick cooking white basmati", GlycemicIndex = 67},
            new FoodData{Id = 51, Name = "Brown rice, average", GlycemicIndex = 50},
            new FoodData{Id = 52, Name = "Converted, white rice (Uncle Ben's®)", GlycemicIndex = 38},
            new FoodData{Id = 53, Name = "Whole wheat kernels, average", GlycemicIndex = 30},
            new FoodData{Id = 54, Name = "Bulgur, average", GlycemicIndex = 48},
            new FoodData{Id = 56, Name = "Graham crackers", GlycemicIndex = 74},
            new FoodData{Id = 57, Name = "Vanilla wafers", GlycemicIndex = 77},
            new FoodData{Id = 58, Name = "Shortbread", GlycemicIndex = 64},
            new FoodData{Id = 59, Name = "Rice cakes, average", GlycemicIndex = 82},
            new FoodData{Id = 60, Name = "Rye crisps, average", GlycemicIndex = 64},
            new FoodData{Id = 61, Name = "Soda crackers", GlycemicIndex = 74},
            new FoodData{Id = 63, Name = "Ice cream, regular", GlycemicIndex = 57},
            new FoodData{Id = 64, Name = "Ice cream, premium", GlycemicIndex = 38},
            new FoodData{Id = 65, Name = "Milk, full fat", GlycemicIndex = 41},
            new FoodData{Id = 66, Name = "Milk, skim", GlycemicIndex = 32},
            new FoodData{Id = 67, Name = "Reduced-fat yogurt with fruit, average", GlycemicIndex = 33},
            new FoodData{Id = 69, Name = "Apple, average", GlycemicIndex = 39},
            new FoodData{Id = 70, Name = "Banana, ripe", GlycemicIndex = 62},
            new FoodData{Id = 71, Name = "Dates, dried", GlycemicIndex = 42},
            new FoodData{Id = 72, Name = "Grapefruit", GlycemicIndex = 25},
            new FoodData{Id = 73, Name = "Grapes, average", GlycemicIndex = 59},
            new FoodData{Id = 74, Name = "Orange, average", GlycemicIndex = 40},
            new FoodData{Id = 75, Name = "Peach, average", GlycemicIndex = 42},
            new FoodData{Id = 76, Name = "Peach, canned in light syrup", GlycemicIndex = 40},
            new FoodData{Id = 77, Name = "Pear, average", GlycemicIndex = 38},
            new FoodData{Id = 78, Name = "Pear, canned in pear juice", GlycemicIndex = 43},
            new FoodData{Id = 79, Name = "Prunes, pitted", GlycemicIndex = 29},
            new FoodData{Id = 80, Name = "Raisins", GlycemicIndex = 64},
            new FoodData{Id = 81, Name = "Watermelon", GlycemicIndex = 72},
            new FoodData{Id = 83, Name = "Baked beans, average", GlycemicIndex = 40},
            new FoodData{Id = 84, Name = "Blackeye peas, average", GlycemicIndex = 33},
            new FoodData{Id = 85, Name = "Black beans", GlycemicIndex = 30},
            new FoodData{Id = 86, Name = "Chickpeas, average", GlycemicIndex = 10},
            new FoodData{Id = 87, Name = "Chickpeas, canned in brine", GlycemicIndex = 38},
            new FoodData{Id = 88, Name = "Navy beans, average", GlycemicIndex = 31},
            new FoodData{Id = 89, Name = "Kidney beans, average", GlycemicIndex = 29},
            new FoodData{Id = 90, Name = "Lentils, average", GlycemicIndex = 29},
            new FoodData{Id = 91, Name = "Soy beans, average", GlycemicIndex = 15},
            new FoodData{Id = 92, Name = "Cashews, salted", GlycemicIndex = 27},
            new FoodData{Id = 93, Name = "Peanuts, average", GlycemicIndex = 7},
            new FoodData{Id = 95, Name = "Fettucini, average", GlycemicIndex = 32},
            new FoodData{Id = 96, Name = "Macaroni, average", GlycemicIndex = 47},
            new FoodData{Id = 97, Name = "Macaroni and Cheese (Kraft)", GlycemicIndex = 64},
            new FoodData{Id = 98, Name = "Spaghetti, white, boiled, average", GlycemicIndex = 46},
            new FoodData{Id = 99, Name = "Spaghetti, white, boiled 20 min, average", GlycemicIndex = 58},
            new FoodData{Id = 100, Name = "Spaghetti, wholemeal, boiled, average", GlycemicIndex = 42},
            new FoodData{Id = 102, Name = "Corn chips, plain, salted, average", GlycemicIndex = 42},
            new FoodData{Id = 103, Name = "Fruit Roll-Ups®", GlycemicIndex = 99},
            new FoodData{Id = 104, Name = "M & M's®, peanut", GlycemicIndex = 33},
            new FoodData{Id = 105, Name = "Microwave popcorn, plain, average", GlycemicIndex = 55},
            new FoodData{Id = 106, Name = "Potato chips, average", GlycemicIndex = 51},
            new FoodData{Id = 107, Name = "Pretzels, oven-baked", GlycemicIndex = 83},
            new FoodData{Id = 108, Name = "Snickers Bar®", GlycemicIndex = 51},
            new FoodData{Id = 111, Name = "Carrots, average", GlycemicIndex = 35},
            new FoodData{Id = 112, Name = "Parsnips", GlycemicIndex = 52},
            new FoodData{Id = 113, Name = "Baked russet potato, average", GlycemicIndex = 111},
            new FoodData{Id = 114, Name = "Boiled white potato, average", GlycemicIndex = 82},
            new FoodData{Id = 115, Name = "Instant mashed potato, average", GlycemicIndex = 87},
            new FoodData{Id = 116, Name = "Sweet potato, average", GlycemicIndex = 70},
            new FoodData{Id = 117, Name = "Yam, average", GlycemicIndex = 54},
            new FoodData{Id = 110, Name = "Green peas, average", GlycemicIndex = 51},
            new FoodData{Id = 119, Name = "Hummus (chickpea salad dip)", GlycemicIndex = 6},
            new FoodData{Id = 120, Name = "Chicken nuggets, frozen, reheated in microwave oven 5 min", GlycemicIndex = 46},
            new FoodData{Id = 121, Name = "Pizza, plain baked dough, served with parmesan cheese and tomato sauce", GlycemicIndex = 80},
            new FoodData{Id = 122, Name = "Pizza, Super Supreme (Pizza Hut)", GlycemicIndex = 36},
            new FoodData{Id = 123, Name = "Honey, average", GlycemicIndex = 61},
        };
    }
}

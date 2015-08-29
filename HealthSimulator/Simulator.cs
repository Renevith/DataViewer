using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSimulator {
    public class Simulator {
        private List<Activity> Activities;

        public Simulator() {
            Activities = new List<Activity>();
        }

        public void AddActivities(IEnumerable<Activity> activities) {
            Activities.AddRange(activities);
        }

        public double GetBloodSugar(TimeSpan time) {
            return 0;
        }

        public double GetCumulativeGlycation(TimeSpan time) {
            return 0;
        }
    }

    public abstract class Activity {
        public TimeSpan ActivityTime { get; protected set; }
        public string Description { get; protected set; }
        public string ActivityType { get; protected set; }
        protected TimeSpan Onset { get; set; }

        public abstract double GetEffect(TimeSpan asOfTime);
    }

    public class FoodActivity : Activity {
        Data.FoodData Food;

        public FoodActivity(Data.FoodData food, TimeSpan time) {
            ActivityType = "Food";
            ActivityTime = time;
            Onset = new TimeSpan(2, 0, 0); //2 hours

            Food = food;
            Description = food.Name;
        }

        public override double GetEffect(TimeSpan asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime.Add(Onset))
                return Food.GlycemicIndex;
            return Food.GlycemicIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }

    public class ExerciseActivity : Activity {
        private Data.ExerciseData Exercise;

        public ExerciseActivity(Data.ExerciseData exercise, TimeSpan time) {
            ActivityType = "Exercise";
            ActivityTime = time;
            Onset = new TimeSpan(1, 0, 0); //1 hour

            Exercise = exercise;
            Description = exercise.Name;
        }

        public override double GetEffect(TimeSpan asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime.Add(Onset))
                return Exercise.ExerciseIndex;
            return Exercise.ExerciseIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }
}

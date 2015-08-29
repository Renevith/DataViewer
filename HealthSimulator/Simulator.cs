using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSimulator {
    public class Simulator {
        private DateTime? Today; //simulator is only capable of simulating one day, so keep track which one
        private List<Activity> Activities;

        public Simulator() {
            Today = null;
            Activities = new List<Activity>();
        }

        public void AddActivities(IEnumerable<Activity> activities) {
            if (!activities.Any())
                return;

            var activityDates = activities.Select(x => x.ActivityTime.Date).Distinct();
            if (activityDates.Count() > 1)
                throw new ArgumentException("Simulator only supports activities on a single day.", "activities");
            if (Today == null)
                Today = activityDates.Single();
            else if (Today != activityDates.Single())
                throw new Exception("Simulator only supports activities on a single day.");

            Activities.AddRange(activities);
        }

        public double GetBloodSugar(DateTime time) {
            return 0;
        }

        public double GetCumulativeGlycation(DateTime time) {
            return 0;
        }
    }

    public abstract class Activity {
        public DateTime ActivityTime { get; protected set; }
        public string Description { get; protected set; }
        public string ActivityType { get; protected set; }
        protected TimeSpan Onset { get; set; }

        public abstract double GetEffect(DateTime asOfTime);
    }

    public class FoodActivity : Activity {
        Data.FoodData Food;

        public FoodActivity(Data.FoodData food, DateTime time) {
            ActivityType = "Food";
            ActivityTime = time;
            Onset = new TimeSpan(2, 0, 0); //2 hours

            Food = food;
            Description = food.Name;
        }

        public override double GetEffect(DateTime asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime.Add(Onset))
                return Food.GlycemicIndex;
            return Food.GlycemicIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }

    public class ExerciseActivity : Activity {
        private Data.ExerciseData Exercise;

        public ExerciseActivity(Data.ExerciseData exercise, DateTime time) {
            ActivityType = "Exercise";
            ActivityTime = time;
            Onset = new TimeSpan(1, 0, 0); //1 hour

            Exercise = exercise;
            Description = exercise.Name;
        }

        public override double GetEffect(DateTime asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime.Add(Onset))
                return Exercise.ExerciseIndex;
            return Exercise.ExerciseIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }
}

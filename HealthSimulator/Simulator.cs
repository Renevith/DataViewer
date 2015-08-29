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
            Activities.RemoveAll(x => x is NormalizationActivity);
            Activities.AddRange(GetNormalization());
        }

        public double GetBloodSugar(TimeSpan time) {
            return Activities.Sum(x => x.GetEffect(time));
        }

        public double GetCumulativeGlycation(TimeSpan time) {
            int glycation = 0;
            for (int minute = 0; minute < time.TotalMinutes; minute++) {
                if (GetBloodSugar(TimeSpan.FromMinutes(minute)) > 150)
                    glycation++;
            }
            return glycation;
        }

        private IEnumerable<Activity> GetNormalization() {
            var normalizations = new List<Activity>();
            var sorted = Activities.Where(x => !(x is NormalizationActivity)).OrderBy(x => x.ActivityTime).ToList();
            for (int i = 0; i < sorted.Count(); i++) {
                var normalizationStart = sorted[i].ActivityTime + sorted[i].Onset;
                var normalizationEnd = sorted[i + 1].ActivityTime;
                if (normalizationStart < normalizationEnd)
                    normalizations.Add(new NormalizationActivity(normalizationStart, normalizationEnd, GetBloodSugar(normalizationStart)));
            }
            return normalizations;
        }
    }

    public abstract class Activity {
        public TimeSpan ActivityTime { get; protected set; }
        public string Description { get; protected set; }
        public string ActivityType { get; protected set; }
        public TimeSpan Onset { get; protected set; }

        public abstract double GetEffect(TimeSpan asOfTime);
    }

    public class FoodActivity : Activity {
        Data.FoodData Food;

        public FoodActivity(Data.FoodData food, TimeSpan time) {
            ActivityType = "Food";
            ActivityTime = time;
            Onset = TimeSpan.FromHours(2);

            Food = food;
            Description = food.Name;
        }

        public override double GetEffect(TimeSpan asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime + Onset)
                return Food.GlycemicIndex;
            return Food.GlycemicIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }

    public class ExerciseActivity : Activity {
        private Data.ExerciseData Exercise;

        public ExerciseActivity(Data.ExerciseData exercise, TimeSpan time) {
            ActivityType = "Exercise";
            ActivityTime = time;
            Onset = TimeSpan.FromHours(1);

            Exercise = exercise;
            Description = exercise.Name;
        }

        public override double GetEffect(TimeSpan asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime + Onset)
                return Exercise.ExerciseIndex;
            return Exercise.ExerciseIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }

    public class NormalizationActivity : Activity {
        private const double RATE_PER_MINUTE = 1;
        private const double TARGET_BLOOD_SUGAR = 80;

        public NormalizationActivity(TimeSpan startTime, TimeSpan endTime, double currentBloodSugar) {
            ActivityType = "Normalization";
            ActivityTime = startTime;
            var rate = RATE_PER_MINUTE * (currentBloodSugar > TARGET_BLOOD_SUGAR ? -1 : 1);
            var timeToNeutral = TimeSpan.FromMinutes((TARGET_BLOOD_SUGAR - currentBloodSugar) / rate);
            if (endTime > timeToNeutral)
                endTime = timeToNeutral;
            Onset = endTime - startTime;
            Description = "";
        }

        //normalization effect is 
        public override double GetEffect(TimeSpan asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime + Onset)
                return RATE_PER_MINUTE * Onset.TotalMinutes;
            return RATE_PER_MINUTE * (asOfTime - ActivityTime).TotalMinutes;
        }
    }
}

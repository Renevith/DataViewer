using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthSimulator {
    public class Simulator {
        const double STARTING_BLOOD_SUGAR = 80;
        private List<Activity> Activities;

        public Simulator(IEnumerable<Activity> activities) {
            Activities = activities.ToList();
            AddNormalization();
        }

        public IEnumerable<Activity> GetActivitiesAndNormalizations() {
            return Activities.ToList();
        }

        public double GetBloodSugar(TimeSpan time) {
            return STARTING_BLOOD_SUGAR + Activities.Sum(x => x.GetEffect(time));
        }

        public double GetCumulativeGlycation(TimeSpan time) {
            int glycation = 0;
            for (int minute = 0; minute < time.TotalMinutes; minute++) {
                if (GetBloodSugar(TimeSpan.FromMinutes(minute)) > 150)
                    glycation++;
            }
            return glycation;
        }

        private void AddNormalization() {
            var sorted = Activities.Where(x => !(x is NormalizationActivity)).OrderBy(x => x.ActivityTime).ToList();
            for (int i = 0; i < sorted.Count(); i++) {
                var normalizationStart = sorted[i].ActivityTime + sorted[i].Onset;
                var normalizationEnd = (i + 1 < sorted.Count()) ? sorted[i + 1].ActivityTime : TimeSpan.MaxValue;
                if (normalizationStart < normalizationEnd)
                    Activities.Add(new NormalizationActivity(normalizationStart, normalizationEnd, GetBloodSugar(normalizationStart)));
            }
        }
    }

    public abstract class Activity {
        public TimeSpan ActivityTime { get; protected set; }
        public string Description { get; protected set; }
        public string ActivityType { get; protected set; }
        public TimeSpan Onset;

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
                return -Exercise.ExerciseIndex;
            return -Exercise.ExerciseIndex * ((asOfTime - ActivityTime).TotalMinutes / Onset.TotalMinutes);
        }
    }

    internal class NormalizationActivity : Activity {
        private const double RATE_PER_MINUTE = 1;
        private const double TARGET_BLOOD_SUGAR = 80;
        private double Rate;

        public NormalizationActivity(TimeSpan startTime, TimeSpan endTime, double currentBloodSugar) {
            ActivityType = "Normalization";
            ActivityTime = startTime;
            Rate = RATE_PER_MINUTE * (currentBloodSugar > TARGET_BLOOD_SUGAR ? -1 : 1);
            var timeToNeutral = TimeSpan.FromMinutes((TARGET_BLOOD_SUGAR - currentBloodSugar) / Rate);
            if (endTime < startTime + timeToNeutral)
                Onset = endTime - startTime;
            else
                Onset = timeToNeutral;
            Description = "";
        }

        public override double GetEffect(TimeSpan asOfTime) {
            if (asOfTime <= ActivityTime)
                return 0;
            if (asOfTime >= ActivityTime + Onset)
                return Rate * Onset.TotalMinutes;
            return Rate * (asOfTime - ActivityTime).TotalMinutes;
        }
    }
}

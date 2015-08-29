using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using HealthSimulator;

namespace Tests {
    [TestClass]
    public class HealthSimulatorTests {
        [TestMethod]
        public void TestOneFood() {
            var sim1 = new Simulator(new Activity[] {
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 10 }, TimeSpan.FromHours(1)),
            });
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(-1)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(0)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(1)), 0.00001);
            Assert.AreEqual(85, sim1.GetBloodSugar(TimeSpan.FromHours(2)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromHours(3)), 0.00001);
            Assert.AreEqual(89, sim1.GetBloodSugar(TimeSpan.FromMinutes(181)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(99)), 0.00001);
            Assert.AreEqual(0, sim1.GetCumulativeGlycation(TimeSpan.FromHours(99)), 0.00001);
        }

        [TestMethod]
        public void TestOneExercise() {
            var sim1 = new Simulator(new Activity[] { 
                new ExerciseActivity(new Data.ExerciseData() { ExerciseIndex = 10 }, TimeSpan.FromHours(1)),
            });
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(-1)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(0)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(1)), 0.00001);
            Assert.AreEqual(75, sim1.GetBloodSugar(TimeSpan.FromHours(1.5)), 0.00001);
            Assert.AreEqual(70, sim1.GetBloodSugar(TimeSpan.FromHours(2)), 0.00001);
            Assert.AreEqual(71, sim1.GetBloodSugar(TimeSpan.FromMinutes(121)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(99)), 0.00001);
            Assert.AreEqual(00, sim1.GetCumulativeGlycation(TimeSpan.FromHours(99)), 0.00001);
        }

        [TestMethod]
        public void TestBasicGlycation() {
            var sim1 = new Simulator(new Activity[] { 
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 140 }, TimeSpan.FromHours(1)),
            });
            Assert.AreEqual(0, sim1.GetCumulativeGlycation(TimeSpan.FromHours(0)), 0.00001);
            Assert.AreEqual(0, sim1.GetCumulativeGlycation(TimeSpan.FromHours(1)), 0.00001);
            Assert.AreEqual(0, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(120)), 0.00001); //blood sugar now exactly equal to 150, but not "above" so no glycation yet
            Assert.AreEqual(1, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(121)), 0.00001);
            Assert.AreEqual(2, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(122)), 0.00001);
            Assert.AreEqual(128, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(248)), 0.00001);
            Assert.AreEqual(129, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(249)), 0.00001);
            Assert.AreEqual(129, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(250)), 0.00001); //blood sugar now exactly equal to 150 again, but not "above" so no additional glycation
            Assert.AreEqual(129, sim1.GetCumulativeGlycation(TimeSpan.FromMinutes(999)), 0.00001);
        }

        [TestMethod]
        public void TestFoodExerciseOffset() {
            var sim1 = new Simulator(new Activity[] {
                new ExerciseActivity(new Data.ExerciseData() { ExerciseIndex = 10 }, TimeSpan.FromHours(1)),
                new ExerciseActivity(new Data.ExerciseData() { ExerciseIndex = 10 }, TimeSpan.FromHours(2)),
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 20 }, TimeSpan.FromHours(1)),
            });
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(-1)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(0)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(1)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(1.5)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(2)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromMinutes(121)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(99)), 0.00001);
            Assert.AreEqual(00, sim1.GetCumulativeGlycation(TimeSpan.FromHours(99)), 0.00001);
        }

        [TestMethod]
        public void TestNoNormalization() {
            //a bunch of zero-index activities just to stave off normalization
            var sim1 = new Simulator(new Activity[] {
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 10 }, TimeSpan.FromHours(1)),
                new ExerciseActivity(new Data.ExerciseData() { ExerciseIndex = 0 }, TimeSpan.FromHours(2)),
                new ExerciseActivity(new Data.ExerciseData() { ExerciseIndex = 0 }, TimeSpan.FromHours(3)),
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 0 }, TimeSpan.FromHours(4)),
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 0 }, TimeSpan.FromHours(5)),
                new FoodActivity(new Data.FoodData() { GlycemicIndex = 0 }, TimeSpan.FromHours(7) + TimeSpan.FromMinutes(5)),
            });
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(-1)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(0)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(1)), 0.00001);
            Assert.AreEqual(85, sim1.GetBloodSugar(TimeSpan.FromHours(2)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromHours(3)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromMinutes(181)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromHours(4)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromHours(5)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromHours(6)), 0.00001);
            Assert.AreEqual(90, sim1.GetBloodSugar(TimeSpan.FromHours(7)), 0.00001);
            Assert.AreEqual(85, sim1.GetBloodSugar(TimeSpan.FromHours(8)), 0.00001);
            Assert.AreEqual(80, sim1.GetBloodSugar(TimeSpan.FromHours(99)), 0.00001);
            Assert.AreEqual(0, sim1.GetCumulativeGlycation(TimeSpan.FromHours(99)), 0.00001);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using HealthSimulator;

namespace DataViewer {
    public partial class MainViewerForm : Form {
        private readonly BindingList<Activity> Activities;

        public MainViewerForm() {
            InitializeComponent();
            Activities = new BindingList<Activity>();
            dataGridView.DataSource = Activities;
            zedGraphControl.GraphPane.Title.Text = "Blood sugar simulation";
            zedGraphControl.GraphPane.XAxis.Title.Text = "Hours into day";
            zedGraphControl.GraphPane.YAxis.Title.IsVisible = false;
            GenerateGraph();
        }

        private void GenerateGraph() {
            zedGraphControl.GraphPane.CurveList.Clear();
            var sim = new Simulator(Activities);
            //figure out any points where the graph potentially changes slope:
            var xValues = sim.GetActivitiesAndNormalizations()
                .SelectMany(x => new[] {x.ActivityTime, x.ActivityTime + x.Onset})
                .Union(new[] {TimeSpan.FromHours(0), TimeSpan.FromHours(24)})
                .Distinct()
                .OrderBy(x => x)
                .ToList();

            //Add any starting or ending points for glycation, when blood sugar crosses 150:
            var crossovers = new List<TimeSpan>();
            for (int i = 0; i + 1 < xValues.Count; i++) {
                if (Math.Sign(sim.GetBloodSugar(xValues[i]) - 150) != Math.Sign(sim.GetBloodSugar(xValues[i + 1]) - 150)) {
                    var fractionOfTime = (150 - sim.GetBloodSugar(xValues[i])) / (sim.GetBloodSugar(xValues[i + 1]) - sim.GetBloodSugar(xValues[i]));
                    crossovers.Add(xValues[i] + TimeSpan.FromMinutes(fractionOfTime * (xValues[i + 1] - xValues[i]).TotalMinutes));
                }
            }
            xValues = xValues.Union(crossovers).Distinct().OrderBy(x => x).ToList();

            var bloodSugar = new ZedGraph.PointPairList();
            foreach (var time in xValues) {
                double sugar = sim.GetBloodSugar(time);
                bloodSugar.Add(new ZedGraph.PointPair(time.TotalHours, sugar));
            }

            var glycation = new ZedGraph.PointPairList();
            foreach (var time in xValues) {
                double gly = sim.GetCumulativeGlycation(time);
                glycation.Add(new ZedGraph.PointPair(time.TotalHours, gly));
            }

            var foodEvents = new ZedGraph.PointPairList();
            foreach (var time in Activities.OfType<FoodActivity>().Select(x => x.ActivityTime)) {
                double point = sim.GetBloodSugar(time);
                foodEvents.Add(new ZedGraph.PointPair(time.TotalHours, point));
            }

            var exerciseEvents = new ZedGraph.PointPairList();
            foreach (var time in Activities.OfType<ExerciseActivity>().Select(x => x.ActivityTime)) {
                double point = sim.GetBloodSugar(time);
                exerciseEvents.Add(new ZedGraph.PointPair(time.TotalHours, point));
            }

            var threshold = new ZedGraph.PointPairList(new[] {0.0, 24.0}, new[] {150.0, 150.0});

            zedGraphControl.GraphPane.AddCurve("Blood Sugar", bloodSugar, Color.Green, ZedGraph.SymbolType.None);
            zedGraphControl.GraphPane.AddCurve("Cumulative Glycation", glycation, Color.Red, ZedGraph.SymbolType.None);
            var line = zedGraphControl.GraphPane.AddCurve("Glycation threshold", threshold, Color.Red,
                ZedGraph.SymbolType.HDash);
            line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            var food = zedGraphControl.GraphPane.AddCurve("", foodEvents, Color.Red, ZedGraph.SymbolType.Triangle);
            food.Line.IsVisible = false;
            food.Symbol.Fill.Type = ZedGraph.FillType.Solid;
            var exercise = zedGraphControl.GraphPane.AddCurve("", exerciseEvents, Color.Green,
                ZedGraph.SymbolType.TriangleDown);
            exercise.Line.IsVisible = false;
            exercise.Symbol.Fill.Type = ZedGraph.FillType.Solid;
            zedGraphControl.RestoreScale(zedGraphControl.GraphPane);
        }

        private void foodRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (!foodRadioButton.Checked)
                return;
            foodRadioButton.BackColor = DefaultBackColor;
            exerciseRadioButton.BackColor = DefaultBackColor;
            foodExerciseComboBox.Items.Clear();
            foodExerciseComboBox.Items.AddRange(Data.FoodDatabase.ToArray<object>());
        }

        private void exerciseRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (!exerciseRadioButton.Checked)
                return;
            foodRadioButton.BackColor = DefaultBackColor;
            exerciseRadioButton.BackColor = DefaultBackColor;
            foodExerciseComboBox.Items.Clear();
            foodExerciseComboBox.Items.AddRange(Data.ExerciseDatabase.ToArray<object>());
        }

        private void addButton_Click(object sender, EventArgs e) {
            //validate inputs:
            if (!foodRadioButton.Checked && !exerciseRadioButton.Checked) {
                foodRadioButton.BackColor = Color.Red;
                exerciseRadioButton.BackColor = Color.Red;
                return;
            }
            if (foodExerciseComboBox.SelectedItem == null) {
                foodExerciseComboBox.BackColor = Color.Red;
                foodExerciseComboBox.DroppedDown = true;
                return;
            }
            Double d;
            if (!Double.TryParse(timeTextBox.Text, out d)) {
                timeTextBox.BackColor = Color.Red;
                return;
            }
            var time = TimeSpan.FromHours(d);

            //add new entry to data grid:
            if (foodRadioButton.Checked) {
                var foodData = (Data.FoodData)foodExerciseComboBox.SelectedItem;
                AddActivity(new FoodActivity(foodData, time));
            }
            else if (exerciseRadioButton.Checked) {
                var exerciseData = (Data.ExerciseData)foodExerciseComboBox.SelectedItem;
                AddActivity(new ExerciseActivity(exerciseData, time));
            }

            foodExerciseComboBox.SelectedItem = null;
            timeTextBox.Clear();
        }

        private void foodExerciseComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            foodExerciseComboBox.BackColor = Color.White;
        }

        private void timeTextBox_TextChanged(object sender, EventArgs e) {
            DateTime d;
            if (DateTime.TryParse(timeTextBox.Text, out d))
                timeTextBox.BackColor = Color.White;
        }

        private void deleteButton_Click(object sender, EventArgs e) {
            foreach (DataGridViewRow row in dataGridView.SelectedRows) {
                Activities.Remove((Activity)row.DataBoundItem);
            }
            Activities.ResetBindings();
            GenerateGraph();
        }

        private void timeTextBox_KeyUp(object sender, KeyEventArgs e) {
            if (e.KeyData == Keys.Enter)
                addButton_Click(sender, e);
        }

        private void addRandomButton_Click(object sender, EventArgs e) {
            Activity randomActivity;
            var r = new Random();
            if (r.NextDouble() < 0.6) {
                var data = Data.FoodDatabase.OrderBy(x => r.NextDouble()).First();
                var time = TimeSpan.FromMinutes(r.Next(60, 1000));
                randomActivity = new FoodActivity(data, time);
            }
            else {
                var data = Data.ExerciseDatabase.OrderBy(x => r.NextDouble()).First();
                var time = TimeSpan.FromMinutes(r.Next(180, 1000));
                randomActivity = new ExerciseActivity(data, time);
            }
            AddActivity(randomActivity);
        }

        private void AddActivity(Activity a) {
            var insertAfter = Activities.Where(x => x.ActivityTime <= a.ActivityTime).OrderByDescending(x => x.ActivityTime).FirstOrDefault();
            var insertIndex = (insertAfter == null ? 0 : Activities.IndexOf(insertAfter) + 1);
            Activities.Insert(insertIndex, a);
            Activities.ResetBindings();
            GenerateGraph();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthSimulator;

namespace DataViewer
{
    public partial class MainViewerForm : Form
    {
        private readonly BindingList<Activity> Activities;

        public MainViewerForm()
        {
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
                                 .SelectMany(x => new[] { x.ActivityTime, x.ActivityTime + x.Onset })
                                 .Union(new[] { TimeSpan.FromHours(0), TimeSpan.FromHours(24) })
                                 .Distinct()
                                 .OrderBy(x => x);

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

                var threshold = new ZedGraph.PointPairList(new[] { 0.0, 24.0 }, new[] { 150.0, 150.0 });

                zedGraphControl.GraphPane.AddCurve("Blood Sugar", bloodSugar, Color.Green, ZedGraph.SymbolType.None);
                zedGraphControl.GraphPane.AddCurve("Cumulative Glycation", glycation, Color.Red, ZedGraph.SymbolType.None);
                var line = zedGraphControl.GraphPane.AddCurve("Glycation threshold", threshold, Color.Red, ZedGraph.SymbolType.HDash);
                line.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            zedGraphControl.RestoreScale(zedGraphControl.GraphPane);
        }

        private void foodRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (!foodRadioButton.Checked)
                return;
            foodRadioButton.BackColor = RadioButton.DefaultBackColor;
            exerciseRadioButton.BackColor = RadioButton.DefaultBackColor;
            foodExerciseComboBox.Items.Clear();
            foodExerciseComboBox.Items.AddRange(Data.FoodDatabase.ToArray<object>());
        }

        private void exerciseRadioButton_CheckedChanged(object sender, EventArgs e) {
            if (!exerciseRadioButton.Checked)
                return;
            foodRadioButton.BackColor = RadioButton.DefaultBackColor;
            exerciseRadioButton.BackColor = RadioButton.DefaultBackColor;
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
                Activities.Add(new FoodActivity(foodData, time));
            }
            else if (exerciseRadioButton.Checked) {
                var exerciseData = (Data.ExerciseData)foodExerciseComboBox.SelectedItem;
                Activities.Add(new ExerciseActivity(exerciseData, time));
            }

            foodExerciseComboBox.SelectedItem = null;
            timeTextBox.Clear();

            Activities.ResetBindings();
            GenerateGraph();
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
            Activities.Add(randomActivity);
            Activities.ResetBindings();
            GenerateGraph();
        }
    }
}

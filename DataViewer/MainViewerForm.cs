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
        }

        private void GenerateGraph() {
            zedGraphControl.GraphPane.CurveList.Clear();
            if (Activities.Any()) {
                var sim = new Simulator(Activities);
                const int pointsToDraw = 100;
                TimeSpan chartEndTime = Activities.Max(x => x.ActivityTime + x.Onset);
                if (chartEndTime < TimeSpan.FromHours(24))
                    chartEndTime = TimeSpan.FromHours(24);
                var bloodSugar = new ZedGraph.PointPairList();
                for (int i = 0; i < pointsToDraw; i++) {
                    TimeSpan time = TimeSpan.FromMinutes((i * chartEndTime.TotalMinutes) / (pointsToDraw - 1));
                    double sugar = sim.GetBloodSugar(time);
                    bloodSugar.Add(new ZedGraph.PointPair(time.TotalHours, sugar));
                }
                var glycation = new ZedGraph.PointPairList();
                for (int i = 0; i < pointsToDraw; i++) {
                    TimeSpan time = TimeSpan.FromMinutes((i * chartEndTime.TotalMinutes) / (pointsToDraw - 1));
                    double gly = sim.GetCumulativeGlycation(time);
                    glycation.Add(new ZedGraph.PointPair(time.TotalHours, gly));
                }

                zedGraphControl.GraphPane.AddCurve("Blood Sugar", bloodSugar, Color.Green, ZedGraph.SymbolType.None);
                zedGraphControl.GraphPane.AddCurve("Cumulative Glycation", glycation, Color.Red, ZedGraph.SymbolType.None);
            }
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
            TimeSpan t;
            if (!TimeSpan.TryParse(timeTextBox.Text, out t)) {
                timeTextBox.BackColor = Color.Red;
                return;
            }

            //add new entry to data grid:
            if (foodRadioButton.Checked) {
                var foodData = (Data.FoodData)foodExerciseComboBox.SelectedItem;
                Activities.Add(new FoodActivity(foodData, t));
            }
            else if (exerciseRadioButton.Checked) {
                var exerciseData = (Data.ExerciseData)foodExerciseComboBox.SelectedItem;
                Activities.Add(new ExerciseActivity(exerciseData, t));
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
    }
}

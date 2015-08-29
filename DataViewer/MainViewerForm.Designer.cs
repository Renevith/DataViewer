namespace DataViewer
{
    partial class MainViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.zedGraphControl = new ZedGraph.ZedGraphControl();
            this.foodExerciseComboBox = new System.Windows.Forms.ComboBox();
            this.timeTextBox = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.foodRadioButton = new System.Windows.Forms.RadioButton();
            this.exerciseRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView.Location = new System.Drawing.Point(12, 121);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(268, 294);
            this.dataGridView.TabIndex = 0;
            // 
            // zedGraphControl
            // 
            this.zedGraphControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl.Location = new System.Drawing.Point(286, 12);
            this.zedGraphControl.Name = "zedGraphControl";
            this.zedGraphControl.ScrollGrace = 0D;
            this.zedGraphControl.ScrollMaxX = 0D;
            this.zedGraphControl.ScrollMaxY = 0D;
            this.zedGraphControl.ScrollMaxY2 = 0D;
            this.zedGraphControl.ScrollMinX = 0D;
            this.zedGraphControl.ScrollMinY = 0D;
            this.zedGraphControl.ScrollMinY2 = 0D;
            this.zedGraphControl.Size = new System.Drawing.Size(512, 432);
            this.zedGraphControl.TabIndex = 1;
            // 
            // foodExerciseComboBox
            // 
            this.foodExerciseComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.foodExerciseComboBox.FormattingEnabled = true;
            this.foodExerciseComboBox.Location = new System.Drawing.Point(6, 42);
            this.foodExerciseComboBox.Name = "foodExerciseComboBox";
            this.foodExerciseComboBox.Size = new System.Drawing.Size(159, 21);
            this.foodExerciseComboBox.TabIndex = 2;
            this.foodExerciseComboBox.SelectedIndexChanged += new System.EventHandler(this.foodExerciseComboBox_SelectedIndexChanged);
            // 
            // timeTextBox
            // 
            this.timeTextBox.Location = new System.Drawing.Point(171, 42);
            this.timeTextBox.Name = "timeTextBox";
            this.timeTextBox.Size = new System.Drawing.Size(91, 20);
            this.timeTextBox.TabIndex = 4;
            this.timeTextBox.TextChanged += new System.EventHandler(this.timeTextBox_TextChanged);
            this.timeTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.timeTextBox_KeyUp);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(41, 68);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(192, 23);
            this.addButton.TabIndex = 5;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.deleteButton.Location = new System.Drawing.Point(12, 421);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(96, 23);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Delete Selected";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // foodRadioButton
            // 
            this.foodRadioButton.AutoSize = true;
            this.foodRadioButton.Location = new System.Drawing.Point(6, 19);
            this.foodRadioButton.Name = "foodRadioButton";
            this.foodRadioButton.Size = new System.Drawing.Size(49, 17);
            this.foodRadioButton.TabIndex = 7;
            this.foodRadioButton.TabStop = true;
            this.foodRadioButton.Text = "Food";
            this.foodRadioButton.UseVisualStyleBackColor = true;
            this.foodRadioButton.CheckedChanged += new System.EventHandler(this.foodRadioButton_CheckedChanged);
            // 
            // exerciseRadioButton
            // 
            this.exerciseRadioButton.AutoSize = true;
            this.exerciseRadioButton.Location = new System.Drawing.Point(74, 19);
            this.exerciseRadioButton.Name = "exerciseRadioButton";
            this.exerciseRadioButton.Size = new System.Drawing.Size(65, 17);
            this.exerciseRadioButton.TabIndex = 8;
            this.exerciseRadioButton.TabStop = true;
            this.exerciseRadioButton.Text = "Exercise";
            this.exerciseRadioButton.UseVisualStyleBackColor = true;
            this.exerciseRadioButton.CheckedChanged += new System.EventHandler(this.exerciseRadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.exerciseRadioButton);
            this.groupBox1.Controls.Add(this.foodExerciseComboBox);
            this.groupBox1.Controls.Add(this.foodRadioButton);
            this.groupBox1.Controls.Add(this.timeTextBox);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 103);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add New Activity";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(168, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Hours into day:";
            // 
            // MainViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 456);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.zedGraphControl);
            this.Controls.Add(this.dataGridView);
            this.Name = "MainViewerForm";
            this.Text = "MainViewerForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private ZedGraph.ZedGraphControl zedGraphControl;
        private System.Windows.Forms.ComboBox foodExerciseComboBox;
        private System.Windows.Forms.TextBox timeTextBox;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.RadioButton foodRadioButton;
        private System.Windows.Forms.RadioButton exerciseRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
    }
}
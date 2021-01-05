using System.Windows.Forms;

namespace ImageFilters
{
    partial class Form1
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
            this.filtersListBox = new System.Windows.Forms.ListBox();
            this.applyFilterButton = new System.Windows.Forms.Button();
            this.grayScaleButton = new System.Windows.Forms.Button();
            this.filtersPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.undoButton = new System.Windows.Forms.Button();
            this.filtersLabel = new System.Windows.Forms.Label();
            this.histogramLabel = new System.Windows.Forms.Label();
            this.editingLabel = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.interpolationLabel = new System.Windows.Forms.Label();
            this.interpolationPanel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.interpolationTrackBar = new System.Windows.Forms.TrackBar();
            this.bicubicLabel = new System.Windows.Forms.Label();
            this.nearestNeighborLabel = new System.Windows.Forms.Label();
            this.bilinearLabel = new System.Windows.Forms.Label();
            this.interpolationButton = new System.Windows.Forms.Button();
            this.histogramButton = new System.Windows.Forms.Button();
            this.histogramPanel = new System.Windows.Forms.Panel();
            this.imageEditingPanel = new System.Windows.Forms.Panel();
            this.applyEditButton = new System.Windows.Forms.Button();
            this.brightnessValue = new System.Windows.Forms.Label();
            this.contrastValue = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.brightnessTrackBar = new System.Windows.Forms.TrackBar();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.colorInvertingButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saturationValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.saturationTrackBar = new System.Windows.Forms.TrackBar();
            this.filtersPanel.SuspendLayout();
            this.interpolationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.interpolationTrackBar)).BeginInit();
            this.histogramPanel.SuspendLayout();
            this.imageEditingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // filtersListBox
            // 
            this.filtersListBox.FormattingEnabled = true;
            this.filtersListBox.Location = new System.Drawing.Point(6, 22);
            this.filtersListBox.Name = "filtersListBox";
            this.filtersListBox.Size = new System.Drawing.Size(120, 316);
            this.filtersListBox.TabIndex = 3;
            this.filtersListBox.SelectedIndexChanged += new System.EventHandler(this.FiltersListBox_SelectedIndexChanged);
            // 
            // applyFilterButton
            // 
            this.applyFilterButton.Location = new System.Drawing.Point(142, 58);
            this.applyFilterButton.Name = "applyFilterButton";
            this.applyFilterButton.Size = new System.Drawing.Size(118, 23);
            this.applyFilterButton.TabIndex = 4;
            this.applyFilterButton.Text = "Apply";
            this.applyFilterButton.UseVisualStyleBackColor = true;
            this.applyFilterButton.Click += new System.EventHandler(this.applyFilterButton_Click);
            // 
            // grayScaleButton
            // 
            this.grayScaleButton.Location = new System.Drawing.Point(3, 265);
            this.grayScaleButton.Name = "grayScaleButton";
            this.grayScaleButton.Size = new System.Drawing.Size(139, 29);
            this.grayScaleButton.TabIndex = 5;
            this.grayScaleButton.Text = "ConvertGrayScale";
            this.grayScaleButton.UseVisualStyleBackColor = true;
            this.grayScaleButton.Click += new System.EventHandler(this.grayScaleButton_Click);
            // 
            // filtersPanel
            // 
            this.filtersPanel.Controls.Add(this.label1);
            this.filtersPanel.Controls.Add(this.applyFilterButton);
            this.filtersPanel.Controls.Add(this.filtersListBox);
            this.filtersPanel.Location = new System.Drawing.Point(167, 16);
            this.filtersPanel.Name = "filtersPanel";
            this.filtersPanel.Size = new System.Drawing.Size(284, 355);
            this.filtersPanel.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Selected Filter";
            // 
            // undoButton
            // 
            this.undoButton.Location = new System.Drawing.Point(3, 182);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(139, 26);
            this.undoButton.TabIndex = 9;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = true;
            this.undoButton.Click += new System.EventHandler(this.undoButton_Click);
            // 
            // filtersLabel
            // 
            this.filtersLabel.AutoSize = true;
            this.filtersLabel.Location = new System.Drawing.Point(12, 42);
            this.filtersLabel.Name = "filtersLabel";
            this.filtersLabel.Size = new System.Drawing.Size(34, 13);
            this.filtersLabel.TabIndex = 7;
            this.filtersLabel.Text = "Filters";
            this.filtersLabel.Click += new System.EventHandler(this.filtersLabel_Click);
            // 
            // histogramLabel
            // 
            this.histogramLabel.AutoSize = true;
            this.histogramLabel.Location = new System.Drawing.Point(12, 74);
            this.histogramLabel.Name = "histogramLabel";
            this.histogramLabel.Size = new System.Drawing.Size(54, 13);
            this.histogramLabel.TabIndex = 8;
            this.histogramLabel.Text = "Histogram";
            this.histogramLabel.Click += new System.EventHandler(this.histogramLabel_Click);
            // 
            // editingLabel
            // 
            this.editingLabel.AutoSize = true;
            this.editingLabel.Location = new System.Drawing.Point(12, 104);
            this.editingLabel.Name = "editingLabel";
            this.editingLabel.Size = new System.Drawing.Size(39, 13);
            this.editingLabel.TabIndex = 9;
            this.editingLabel.Text = "Editing";
            this.editingLabel.Click += new System.EventHandler(this.editingLabel_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(3, 222);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(139, 29);
            this.button3.TabIndex = 10;
            this.button3.Text = "Browse Image";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // interpolationLabel
            // 
            this.interpolationLabel.AutoSize = true;
            this.interpolationLabel.Location = new System.Drawing.Point(12, 133);
            this.interpolationLabel.Name = "interpolationLabel";
            this.interpolationLabel.Size = new System.Drawing.Size(65, 13);
            this.interpolationLabel.TabIndex = 11;
            this.interpolationLabel.Text = "Interpolation";
            this.interpolationLabel.Click += new System.EventHandler(this.interpolationLabel_Click);
            // 
            // interpolationPanel
            // 
            this.interpolationPanel.Controls.Add(this.label9);
            this.interpolationPanel.Controls.Add(this.interpolationTrackBar);
            this.interpolationPanel.Controls.Add(this.bicubicLabel);
            this.interpolationPanel.Controls.Add(this.nearestNeighborLabel);
            this.interpolationPanel.Controls.Add(this.bilinearLabel);
            this.interpolationPanel.Controls.Add(this.interpolationButton);
            this.interpolationPanel.Location = new System.Drawing.Point(158, 13);
            this.interpolationPanel.Name = "interpolationPanel";
            this.interpolationPanel.Size = new System.Drawing.Size(290, 351);
            this.interpolationPanel.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 130);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Multiplyer";
            // 
            // interpolationTrackBar
            // 
            this.interpolationTrackBar.Location = new System.Drawing.Point(14, 162);
            this.interpolationTrackBar.Maximum = 5;
            this.interpolationTrackBar.Name = "interpolationTrackBar";
            this.interpolationTrackBar.Size = new System.Drawing.Size(267, 45);
            this.interpolationTrackBar.TabIndex = 5;
            this.interpolationTrackBar.Scroll += new System.EventHandler(this.interpolationTrackBar_Scroll);
            // 
            // bicubicLabel
            // 
            this.bicubicLabel.AutoSize = true;
            this.bicubicLabel.Location = new System.Drawing.Point(20, 76);
            this.bicubicLabel.Name = "bicubicLabel";
            this.bicubicLabel.Size = new System.Drawing.Size(103, 13);
            this.bicubicLabel.TabIndex = 11;
            this.bicubicLabel.Text = "Bicubic Interpolation";
            this.bicubicLabel.Click += new System.EventHandler(this.bicubicLabel_Click);
            // 
            // nearestNeighborLabel
            // 
            this.nearestNeighborLabel.AutoSize = true;
            this.nearestNeighborLabel.Location = new System.Drawing.Point(21, 25);
            this.nearestNeighborLabel.Name = "nearestNeighborLabel";
            this.nearestNeighborLabel.Size = new System.Drawing.Size(151, 13);
            this.nearestNeighborLabel.TabIndex = 10;
            this.nearestNeighborLabel.Text = "Nearest Neighbor Interpolation";
            this.nearestNeighborLabel.Click += new System.EventHandler(this.nearestNeighborLabel_Click);
            // 
            // bilinearLabel
            // 
            this.bilinearLabel.AutoSize = true;
            this.bilinearLabel.ForeColor = System.Drawing.Color.Blue;
            this.bilinearLabel.Location = new System.Drawing.Point(21, 50);
            this.bilinearLabel.Name = "bilinearLabel";
            this.bilinearLabel.Size = new System.Drawing.Size(102, 13);
            this.bilinearLabel.TabIndex = 8;
            this.bilinearLabel.Text = "Bilinear Interpolation";
            this.bilinearLabel.Click += new System.EventHandler(this.bilinearLabel_Click);
            // 
            // interpolationButton
            // 
            this.interpolationButton.Location = new System.Drawing.Point(87, 213);
            this.interpolationButton.Name = "interpolationButton";
            this.interpolationButton.Size = new System.Drawing.Size(112, 23);
            this.interpolationButton.TabIndex = 4;
            this.interpolationButton.Text = "Interpolate Image";
            this.interpolationButton.UseVisualStyleBackColor = true;
            this.interpolationButton.Click += new System.EventHandler(this.interpolationButton_Click);
            // 
            // histogramButton
            // 
            this.histogramButton.Location = new System.Drawing.Point(87, 56);
            this.histogramButton.Name = "histogramButton";
            this.histogramButton.Size = new System.Drawing.Size(112, 23);
            this.histogramButton.TabIndex = 12;
            this.histogramButton.Text = "Apply Histogram";
            this.histogramButton.UseVisualStyleBackColor = true;
            this.histogramButton.Click += new System.EventHandler(this.histogramButton_Click);
            // 
            // histogramPanel
            // 
            this.histogramPanel.Controls.Add(this.histogramButton);
            this.histogramPanel.Location = new System.Drawing.Point(170, 6);
            this.histogramPanel.Name = "histogramPanel";
            this.histogramPanel.Size = new System.Drawing.Size(284, 374);
            this.histogramPanel.TabIndex = 13;
            // 
            // imageEditingPanel
            // 
            this.imageEditingPanel.Controls.Add(this.saturationValue);
            this.imageEditingPanel.Controls.Add(this.label3);
            this.imageEditingPanel.Controls.Add(this.saturationTrackBar);
            this.imageEditingPanel.Controls.Add(this.applyEditButton);
            this.imageEditingPanel.Controls.Add(this.brightnessValue);
            this.imageEditingPanel.Controls.Add(this.contrastValue);
            this.imageEditingPanel.Controls.Add(this.label11);
            this.imageEditingPanel.Controls.Add(this.label10);
            this.imageEditingPanel.Controls.Add(this.brightnessTrackBar);
            this.imageEditingPanel.Controls.Add(this.contrastTrackBar);
            this.imageEditingPanel.Controls.Add(this.colorInvertingButton);
            this.imageEditingPanel.Location = new System.Drawing.Point(170, 12);
            this.imageEditingPanel.Name = "imageEditingPanel";
            this.imageEditingPanel.Size = new System.Drawing.Size(284, 355);
            this.imageEditingPanel.TabIndex = 14;
            // 
            // applyEditButton
            // 
            this.applyEditButton.Location = new System.Drawing.Point(82, 323);
            this.applyEditButton.Name = "applyEditButton";
            this.applyEditButton.Size = new System.Drawing.Size(112, 23);
            this.applyEditButton.TabIndex = 19;
            this.applyEditButton.Text = "Edit Image";
            this.applyEditButton.UseVisualStyleBackColor = true;
            this.applyEditButton.Click += new System.EventHandler(this.applyEditButton_Click);
            // 
            // brightnessValue
            // 
            this.brightnessValue.AutoSize = true;
            this.brightnessValue.Location = new System.Drawing.Point(228, 146);
            this.brightnessValue.Name = "brightnessValue";
            this.brightnessValue.Size = new System.Drawing.Size(13, 13);
            this.brightnessValue.TabIndex = 18;
            this.brightnessValue.Text = "0";
            // 
            // contrastValue
            // 
            this.contrastValue.AutoSize = true;
            this.contrastValue.Location = new System.Drawing.Point(228, 71);
            this.contrastValue.Name = "contrastValue";
            this.contrastValue.Size = new System.Drawing.Size(13, 13);
            this.contrastValue.TabIndex = 17;
            this.contrastValue.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(24, 71);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(46, 13);
            this.label11.TabIndex = 16;
            this.label11.Text = "Contrast";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(24, 146);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(56, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Brightness";
            // 
            // brightnessTrackBar
            // 
            this.brightnessTrackBar.Location = new System.Drawing.Point(18, 172);
            this.brightnessTrackBar.Maximum = 100;
            this.brightnessTrackBar.Minimum = -100;
            this.brightnessTrackBar.Name = "brightnessTrackBar";
            this.brightnessTrackBar.Size = new System.Drawing.Size(247, 45);
            this.brightnessTrackBar.TabIndex = 14;
            this.brightnessTrackBar.Scroll += new System.EventHandler(this.BrightnessTrackBar_Scroll);
            // 
            // contrastTrackBar
            // 
            this.contrastTrackBar.Location = new System.Drawing.Point(18, 102);
            this.contrastTrackBar.Maximum = 255;
            this.contrastTrackBar.Minimum = -255;
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.Size = new System.Drawing.Size(247, 45);
            this.contrastTrackBar.TabIndex = 13;
            this.contrastTrackBar.Scroll += new System.EventHandler(this.contrastTrackBar_Scroll);
            // 
            // colorInvertingButton
            // 
            this.colorInvertingButton.Location = new System.Drawing.Point(82, 25);
            this.colorInvertingButton.Name = "colorInvertingButton";
            this.colorInvertingButton.Size = new System.Drawing.Size(112, 23);
            this.colorInvertingButton.TabIndex = 12;
            this.colorInvertingButton.Text = "Invert Colors";
            this.colorInvertingButton.UseVisualStyleBackColor = true;
            this.colorInvertingButton.Click += new System.EventHandler(this.colorInvertingButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // saturationValue
            // 
            this.saturationValue.AutoSize = true;
            this.saturationValue.Location = new System.Drawing.Point(228, 220);
            this.saturationValue.Name = "saturationValue";
            this.saturationValue.Size = new System.Drawing.Size(13, 13);
            this.saturationValue.TabIndex = 22;
            this.saturationValue.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Saturation";
            // 
            // saturationTrackBar
            // 
            this.saturationTrackBar.Location = new System.Drawing.Point(18, 246);
            this.saturationTrackBar.Maximum = 100;
            this.saturationTrackBar.Minimum = -100;
            this.saturationTrackBar.Name = "saturationTrackBar";
            this.saturationTrackBar.Size = new System.Drawing.Size(247, 45);
            this.saturationTrackBar.TabIndex = 20;
            this.saturationTrackBar.Scroll += new System.EventHandler(this.saturationTrackBar_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 392);
            this.Controls.Add(this.grayScaleButton);
            this.Controls.Add(this.undoButton);
            this.Controls.Add(this.histogramPanel);
            this.Controls.Add(this.interpolationPanel);
            this.Controls.Add(this.imageEditingPanel);
            this.Controls.Add(this.interpolationLabel);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.editingLabel);
            this.Controls.Add(this.histogramLabel);
            this.Controls.Add(this.filtersLabel);
            this.Controls.Add(this.filtersPanel);
            this.Name = "Form1";
            this.Text = "ImageFilter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.filtersPanel.ResumeLayout(false);
            this.filtersPanel.PerformLayout();
            this.interpolationPanel.ResumeLayout(false);
            this.interpolationPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.interpolationTrackBar)).EndInit();
            this.histogramPanel.ResumeLayout(false);
            this.imageEditingPanel.ResumeLayout(false);
            this.imageEditingPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saturationTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox filtersListBox;
        private System.Windows.Forms.Button applyFilterButton;
        private System.Windows.Forms.Button grayScaleButton;
        private System.Windows.Forms.Panel filtersPanel;
        private System.Windows.Forms.Label label1;
        private Label filtersLabel;
        private Label histogramLabel;
        private Label editingLabel;
        private Button undoButton;
        private Button button3;
        private Label interpolationLabel;
        private Panel interpolationPanel;
        private Label bilinearLabel;
        private Button interpolationButton;
        private TrackBar interpolationTrackBar;
        private Label bicubicLabel;
        private Label nearestNeighborLabel;
        private Label label9;
        private Panel histogramPanel;
        private Button histogramButton;
        private Panel imageEditingPanel;
        private Label label11;
        private Label label10;
        private TrackBar brightnessTrackBar;
        private TrackBar contrastTrackBar;
        private Button colorInvertingButton;
        private Button applyEditButton;
        private Label brightnessValue;
        private Label contrastValue;
        private OpenFileDialog openFileDialog1;
        private Label saturationValue;
        private Label label3;
        private TrackBar saturationTrackBar;
    }
}


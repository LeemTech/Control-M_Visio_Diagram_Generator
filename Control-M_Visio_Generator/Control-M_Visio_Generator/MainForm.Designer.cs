using System.Diagnostics;
namespace Control_M_Visio_Generator
{
    partial class MainForm
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

            //MainForm.processThread.Interrupt();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.folderLocationTextBox = new System.Windows.Forms.TextBox();
            this.folderLocationLabel = new System.Windows.Forms.Label();
            this.folderLocationButton = new System.Windows.Forms.Button();
            this.outputLocationTextbox = new System.Windows.Forms.TextBox();
            this.outputLocationLabel = new System.Windows.Forms.Label();
            this.outputLocationButton = new System.Windows.Forms.Button();
            this.generateVisioButton = new System.Windows.Forms.Button();
            this.xmlComboBox = new System.Windows.Forms.ComboBox();
            this.outputTextBox = new System.Windows.Forms.RichTextBox();
            this.colour_dialog = new System.Windows.Forms.ColorDialog();
            this.extJobCheckBox = new System.Windows.Forms.CheckBox();
            this.AdvancedButton = new System.Windows.Forms.Button();
            this.LocalJobsLabel = new System.Windows.Forms.Label();
            this.ExternalJobsLabel = new System.Windows.Forms.Label();
            this.Section1Label = new System.Windows.Forms.Label();
            this.Section2Label = new System.Windows.Forms.Label();
            this.Section3Label = new System.Windows.Forms.Label();
            this.Section1ColourLabel = new System.Windows.Forms.Label();
            this.Section2ColourLabel = new System.Windows.Forms.Label();
            this.Section3Colour = new System.Windows.Forms.Label();
            this.Section1ComboBox = new System.Windows.Forms.ComboBox();
            this.Section2ComboBox = new System.Windows.Forms.ComboBox();
            this.Section3ComboBox = new System.Windows.Forms.ComboBox();
            this.Section1ColourButton = new System.Windows.Forms.Button();
            this.Section2ColourButton = new System.Windows.Forms.Button();
            this.Section3ColourButton = new System.Windows.Forms.Button();
            this.ExternalJobColourButton = new System.Windows.Forms.Button();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.Section3ColourShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Section2ColourShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.Section1ColourShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.ExternalColourShape = new Microsoft.VisualBasic.PowerPacks.RectangleShape();
            this.FlowLabel = new System.Windows.Forms.Label();
            this.FlowComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // folderLocationTextBox
            // 
            this.folderLocationTextBox.Cursor = System.Windows.Forms.Cursors.Default;
            this.folderLocationTextBox.Enabled = false;
            this.folderLocationTextBox.Location = new System.Drawing.Point(49, 28);
            this.folderLocationTextBox.Name = "folderLocationTextBox";
            this.folderLocationTextBox.ReadOnly = true;
            this.folderLocationTextBox.Size = new System.Drawing.Size(370, 20);
            this.folderLocationTextBox.TabIndex = 0;
            this.folderLocationTextBox.TextChanged += new System.EventHandler(this.folderLocationTextBox_TextChanged);
            // 
            // folderLocationLabel
            // 
            this.folderLocationLabel.AutoSize = true;
            this.folderLocationLabel.Location = new System.Drawing.Point(204, 12);
            this.folderLocationLabel.Name = "folderLocationLabel";
            this.folderLocationLabel.Size = new System.Drawing.Size(80, 13);
            this.folderLocationLabel.TabIndex = 1;
            this.folderLocationLabel.Text = "Folder Location";
            // 
            // folderLocationButton
            // 
            this.folderLocationButton.Location = new System.Drawing.Point(465, 28);
            this.folderLocationButton.Name = "folderLocationButton";
            this.folderLocationButton.Size = new System.Drawing.Size(75, 23);
            this.folderLocationButton.TabIndex = 2;
            this.folderLocationButton.Text = "Choose...";
            this.folderLocationButton.UseVisualStyleBackColor = true;
            this.folderLocationButton.Click += new System.EventHandler(this.folderLocationButton_Click);
            // 
            // outputLocationTextbox
            // 
            this.outputLocationTextbox.Location = new System.Drawing.Point(50, 113);
            this.outputLocationTextbox.Name = "outputLocationTextbox";
            this.outputLocationTextbox.Size = new System.Drawing.Size(370, 20);
            this.outputLocationTextbox.TabIndex = 3;
            // 
            // outputLocationLabel
            // 
            this.outputLocationLabel.AutoSize = true;
            this.outputLocationLabel.Location = new System.Drawing.Point(204, 98);
            this.outputLocationLabel.Name = "outputLocationLabel";
            this.outputLocationLabel.Size = new System.Drawing.Size(83, 13);
            this.outputLocationLabel.TabIndex = 4;
            this.outputLocationLabel.Text = "Output Location";
            // 
            // outputLocationButton
            // 
            this.outputLocationButton.Location = new System.Drawing.Point(465, 109);
            this.outputLocationButton.Name = "outputLocationButton";
            this.outputLocationButton.Size = new System.Drawing.Size(75, 23);
            this.outputLocationButton.TabIndex = 5;
            this.outputLocationButton.Text = "Choose...";
            this.outputLocationButton.UseVisualStyleBackColor = true;
            this.outputLocationButton.Click += new System.EventHandler(this.outputLocationButton_Click);
            // 
            // generateVisioButton
            // 
            this.generateVisioButton.Location = new System.Drawing.Point(198, 158);
            this.generateVisioButton.Name = "generateVisioButton";
            this.generateVisioButton.Size = new System.Drawing.Size(98, 23);
            this.generateVisioButton.TabIndex = 6;
            this.generateVisioButton.Text = "Generate";
            this.generateVisioButton.UseVisualStyleBackColor = true;
            this.generateVisioButton.Click += new System.EventHandler(this.generateVisioButton_Click);
            // 
            // xmlComboBox
            // 
            this.xmlComboBox.FormattingEnabled = true;
            this.xmlComboBox.Location = new System.Drawing.Point(112, 54);
            this.xmlComboBox.Name = "xmlComboBox";
            this.xmlComboBox.Size = new System.Drawing.Size(244, 21);
            this.xmlComboBox.TabIndex = 7;
            this.xmlComboBox.SelectedIndexChanged += new System.EventHandler(this.xmlComboBox_SelectedIndexChanged);
            // 
            // outputTextBox
            // 
            this.outputTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.outputTextBox.Enabled = false;
            this.outputTextBox.Location = new System.Drawing.Point(12, 208);
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(528, 126);
            this.outputTextBox.TabIndex = 13;
            this.outputTextBox.Text = "";
            this.outputTextBox.TextChanged += new System.EventHandler(this.diagTextBox_TextChanged);
            // 
            // extJobCheckBox
            // 
            this.extJobCheckBox.AutoSize = true;
            this.extJobCheckBox.Checked = true;
            this.extJobCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.extJobCheckBox.Location = new System.Drawing.Point(195, 369);
            this.extJobCheckBox.Name = "extJobCheckBox";
            this.extJobCheckBox.Size = new System.Drawing.Size(133, 17);
            this.extJobCheckBox.TabIndex = 14;
            this.extJobCheckBox.Text = "Include External Jobs?";
            this.extJobCheckBox.UseVisualStyleBackColor = true;
            this.extJobCheckBox.CheckedChanged += new System.EventHandler(this.extJobCheckBox_CheckedChanged);
            // 
            // AdvancedButton
            // 
            this.AdvancedButton.Location = new System.Drawing.Point(465, 158);
            this.AdvancedButton.Name = "AdvancedButton";
            this.AdvancedButton.Size = new System.Drawing.Size(75, 23);
            this.AdvancedButton.TabIndex = 15;
            this.AdvancedButton.Text = "Advanced";
            this.AdvancedButton.UseVisualStyleBackColor = true;
            this.AdvancedButton.Click += new System.EventHandler(this.AdvancedButton_Click);
            // 
            // LocalJobsLabel
            // 
            this.LocalJobsLabel.AutoSize = true;
            this.LocalJobsLabel.Location = new System.Drawing.Point(16, 371);
            this.LocalJobsLabel.Name = "LocalJobsLabel";
            this.LocalJobsLabel.Size = new System.Drawing.Size(91, 13);
            this.LocalJobsLabel.TabIndex = 16;
            this.LocalJobsLabel.Text = "Local Jobs Config";
            // 
            // ExternalJobsLabel
            // 
            this.ExternalJobsLabel.AutoSize = true;
            this.ExternalJobsLabel.Location = new System.Drawing.Point(396, 371);
            this.ExternalJobsLabel.Name = "ExternalJobsLabel";
            this.ExternalJobsLabel.Size = new System.Drawing.Size(103, 13);
            this.ExternalJobsLabel.TabIndex = 17;
            this.ExternalJobsLabel.Text = "External Jobs Config";
            // 
            // Section1Label
            // 
            this.Section1Label.AutoSize = true;
            this.Section1Label.Location = new System.Drawing.Point(16, 397);
            this.Section1Label.Name = "Section1Label";
            this.Section1Label.Size = new System.Drawing.Size(52, 13);
            this.Section1Label.TabIndex = 18;
            this.Section1Label.Text = "Section 1";
            // 
            // Section2Label
            // 
            this.Section2Label.AutoSize = true;
            this.Section2Label.Location = new System.Drawing.Point(16, 463);
            this.Section2Label.Name = "Section2Label";
            this.Section2Label.Size = new System.Drawing.Size(52, 13);
            this.Section2Label.TabIndex = 19;
            this.Section2Label.Text = "Section 2";
            // 
            // Section3Label
            // 
            this.Section3Label.AutoSize = true;
            this.Section3Label.Location = new System.Drawing.Point(16, 529);
            this.Section3Label.Name = "Section3Label";
            this.Section3Label.Size = new System.Drawing.Size(52, 13);
            this.Section3Label.TabIndex = 20;
            this.Section3Label.Text = "Section 3";
            // 
            // Section1ColourLabel
            // 
            this.Section1ColourLabel.AutoSize = true;
            this.Section1ColourLabel.Location = new System.Drawing.Point(195, 397);
            this.Section1ColourLabel.Name = "Section1ColourLabel";
            this.Section1ColourLabel.Size = new System.Drawing.Size(85, 13);
            this.Section1ColourLabel.TabIndex = 21;
            this.Section1ColourLabel.Text = "Section 1 Colour";
            // 
            // Section2ColourLabel
            // 
            this.Section2ColourLabel.AutoSize = true;
            this.Section2ColourLabel.Location = new System.Drawing.Point(195, 463);
            this.Section2ColourLabel.Name = "Section2ColourLabel";
            this.Section2ColourLabel.Size = new System.Drawing.Size(85, 13);
            this.Section2ColourLabel.TabIndex = 22;
            this.Section2ColourLabel.Text = "Section 2 Colour";
            // 
            // Section3Colour
            // 
            this.Section3Colour.AutoSize = true;
            this.Section3Colour.Location = new System.Drawing.Point(195, 529);
            this.Section3Colour.Name = "Section3Colour";
            this.Section3Colour.Size = new System.Drawing.Size(85, 13);
            this.Section3Colour.TabIndex = 23;
            this.Section3Colour.Text = "Section 3 Colour";
            // 
            // Section1ComboBox
            // 
            this.Section1ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Section1ComboBox.FormattingEnabled = true;
            this.Section1ComboBox.Location = new System.Drawing.Point(16, 426);
            this.Section1ComboBox.Name = "Section1ComboBox";
            this.Section1ComboBox.Size = new System.Drawing.Size(121, 21);
            this.Section1ComboBox.TabIndex = 24;
            this.Section1ComboBox.SelectedIndexChanged += new System.EventHandler(this.Section1ComboBox_SelectedIndexChanged);
            // 
            // Section2ComboBox
            // 
            this.Section2ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Section2ComboBox.FormattingEnabled = true;
            this.Section2ComboBox.Location = new System.Drawing.Point(16, 492);
            this.Section2ComboBox.Name = "Section2ComboBox";
            this.Section2ComboBox.Size = new System.Drawing.Size(121, 21);
            this.Section2ComboBox.TabIndex = 25;
            this.Section2ComboBox.SelectedIndexChanged += new System.EventHandler(this.Section2ComboBox_SelectedIndexChanged);
            // 
            // Section3ComboBox
            // 
            this.Section3ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Section3ComboBox.FormattingEnabled = true;
            this.Section3ComboBox.Location = new System.Drawing.Point(16, 558);
            this.Section3ComboBox.Name = "Section3ComboBox";
            this.Section3ComboBox.Size = new System.Drawing.Size(121, 21);
            this.Section3ComboBox.TabIndex = 26;
            this.Section3ComboBox.SelectedIndexChanged += new System.EventHandler(this.Section3ComboBox_SelectedIndexChanged);
            // 
            // Section1ColourButton
            // 
            this.Section1ColourButton.Location = new System.Drawing.Point(195, 425);
            this.Section1ColourButton.Name = "Section1ColourButton";
            this.Section1ColourButton.Size = new System.Drawing.Size(75, 23);
            this.Section1ColourButton.TabIndex = 27;
            this.Section1ColourButton.Text = "Pick Colour";
            this.Section1ColourButton.UseVisualStyleBackColor = true;
            this.Section1ColourButton.Click += new System.EventHandler(this.Section1ColourButton_Click);
            // 
            // Section2ColourButton
            // 
            this.Section2ColourButton.Location = new System.Drawing.Point(195, 491);
            this.Section2ColourButton.Name = "Section2ColourButton";
            this.Section2ColourButton.Size = new System.Drawing.Size(75, 23);
            this.Section2ColourButton.TabIndex = 28;
            this.Section2ColourButton.Text = "Pick Colour";
            this.Section2ColourButton.UseVisualStyleBackColor = true;
            this.Section2ColourButton.Click += new System.EventHandler(this.Section2ColourButton_Click);
            // 
            // Section3ColourButton
            // 
            this.Section3ColourButton.Location = new System.Drawing.Point(195, 557);
            this.Section3ColourButton.Name = "Section3ColourButton";
            this.Section3ColourButton.Size = new System.Drawing.Size(75, 23);
            this.Section3ColourButton.TabIndex = 29;
            this.Section3ColourButton.Text = "Pick Colour";
            this.Section3ColourButton.UseVisualStyleBackColor = true;
            this.Section3ColourButton.Click += new System.EventHandler(this.Section3ColourButton_Click);
            // 
            // ExternalJobColourButton
            // 
            this.ExternalJobColourButton.Location = new System.Drawing.Point(396, 424);
            this.ExternalJobColourButton.Name = "ExternalJobColourButton";
            this.ExternalJobColourButton.Size = new System.Drawing.Size(75, 23);
            this.ExternalJobColourButton.TabIndex = 30;
            this.ExternalJobColourButton.Text = "Pick Colour";
            this.ExternalJobColourButton.UseVisualStyleBackColor = true;
            this.ExternalJobColourButton.Click += new System.EventHandler(this.ExternalJobColourButton_Click);
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.Section3ColourShape,
            this.Section2ColourShape,
            this.Section1ColourShape,
            this.ExternalColourShape});
            this.shapeContainer1.Size = new System.Drawing.Size(552, 622);
            this.shapeContainer1.TabIndex = 31;
            this.shapeContainer1.TabStop = false;
            // 
            // Section3ColourShape
            // 
            this.Section3ColourShape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section3ColourShape.Enabled = false;
            this.Section3ColourShape.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section3ColourShape.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section3ColourShape.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.Section3ColourShape.Location = new System.Drawing.Point(295, 557);
            this.Section3ColourShape.Name = "Section3ColourShape";
            this.Section3ColourShape.Size = new System.Drawing.Size(24, 23);
            // 
            // Section2ColourShape
            // 
            this.Section2ColourShape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section2ColourShape.Enabled = false;
            this.Section2ColourShape.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section2ColourShape.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section2ColourShape.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.Section2ColourShape.Location = new System.Drawing.Point(295, 491);
            this.Section2ColourShape.Name = "Section2ColourShape";
            this.Section2ColourShape.Size = new System.Drawing.Size(24, 23);
            // 
            // Section1ColourShape
            // 
            this.Section1ColourShape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section1ColourShape.Enabled = false;
            this.Section1ColourShape.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section1ColourShape.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.Section1ColourShape.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.Section1ColourShape.Location = new System.Drawing.Point(295, 424);
            this.Section1ColourShape.Name = "Section1ColourShape";
            this.Section1ColourShape.Size = new System.Drawing.Size(24, 23);
            // 
            // ExternalColourShape
            // 
            this.ExternalColourShape.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(127)))), ((int)(((byte)(54)))));
            this.ExternalColourShape.Enabled = false;
            this.ExternalColourShape.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(127)))), ((int)(((byte)(54)))));
            this.ExternalColourShape.FillGradientColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(127)))), ((int)(((byte)(54)))));
            this.ExternalColourShape.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid;
            this.ExternalColourShape.Location = new System.Drawing.Point(493, 423);
            this.ExternalColourShape.Name = "ExternalColourShape";
            this.ExternalColourShape.Size = new System.Drawing.Size(24, 23);
            // 
            // FlowLabel
            // 
            this.FlowLabel.AutoSize = true;
            this.FlowLabel.Location = new System.Drawing.Point(396, 491);
            this.FlowLabel.Name = "FlowLabel";
            this.FlowLabel.Size = new System.Drawing.Size(56, 13);
            this.FlowLabel.TabIndex = 32;
            this.FlowLabel.Text = "Flow Type";
            // 
            // FlowComboBox
            // 
            this.FlowComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FlowComboBox.FormattingEnabled = true;
            this.FlowComboBox.Items.AddRange(new object[] {
            "Flowchart",
            "Hierarchy"});
            this.FlowComboBox.Location = new System.Drawing.Point(396, 529);
            this.FlowComboBox.Name = "FlowComboBox";
            this.FlowComboBox.Size = new System.Drawing.Size(121, 21);
            this.FlowComboBox.TabIndex = 33;
            this.FlowComboBox.SelectedIndexChanged += new System.EventHandler(this.FlowComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(552, 622);
            this.Controls.Add(this.FlowComboBox);
            this.Controls.Add(this.FlowLabel);
            this.Controls.Add(this.ExternalJobColourButton);
            this.Controls.Add(this.Section3ColourButton);
            this.Controls.Add(this.Section2ColourButton);
            this.Controls.Add(this.Section1ColourButton);
            this.Controls.Add(this.Section3ComboBox);
            this.Controls.Add(this.Section2ComboBox);
            this.Controls.Add(this.Section1ComboBox);
            this.Controls.Add(this.Section3Colour);
            this.Controls.Add(this.Section2ColourLabel);
            this.Controls.Add(this.Section1ColourLabel);
            this.Controls.Add(this.Section3Label);
            this.Controls.Add(this.Section2Label);
            this.Controls.Add(this.Section1Label);
            this.Controls.Add(this.ExternalJobsLabel);
            this.Controls.Add(this.LocalJobsLabel);
            this.Controls.Add(this.AdvancedButton);
            this.Controls.Add(this.extJobCheckBox);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.xmlComboBox);
            this.Controls.Add(this.generateVisioButton);
            this.Controls.Add(this.outputLocationButton);
            this.Controls.Add(this.outputLocationLabel);
            this.Controls.Add(this.outputLocationTextbox);
            this.Controls.Add(this.folderLocationButton);
            this.Controls.Add(this.folderLocationLabel);
            this.Controls.Add(this.folderLocationTextBox);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Control-M Visio Generator V2.2 Beta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox folderLocationTextBox;
        private System.Windows.Forms.Label folderLocationLabel;
        private System.Windows.Forms.Button folderLocationButton;
        private System.Windows.Forms.TextBox outputLocationTextbox;
        private System.Windows.Forms.Label outputLocationLabel;
        private System.Windows.Forms.Button outputLocationButton;
        private System.Windows.Forms.Button generateVisioButton;
        private System.Windows.Forms.ComboBox xmlComboBox;
        private System.Windows.Forms.ColorDialog colour_dialog;
        public System.Windows.Forms.RichTextBox outputTextBox;
        private System.Windows.Forms.CheckBox extJobCheckBox;
        private System.Windows.Forms.Button AdvancedButton;
        private System.Windows.Forms.Label LocalJobsLabel;
        private System.Windows.Forms.Label ExternalJobsLabel;
        private System.Windows.Forms.Label Section1Label;
        private System.Windows.Forms.Label Section2Label;
        private System.Windows.Forms.Label Section3Label;
        private System.Windows.Forms.Label Section1ColourLabel;
        private System.Windows.Forms.Label Section2ColourLabel;
        private System.Windows.Forms.Label Section3Colour;
        private System.Windows.Forms.ComboBox Section1ComboBox;
        private System.Windows.Forms.ComboBox Section2ComboBox;
        private System.Windows.Forms.ComboBox Section3ComboBox;
        private System.Windows.Forms.Button Section1ColourButton;
        private System.Windows.Forms.Button Section2ColourButton;
        private System.Windows.Forms.Button Section3ColourButton;
        private System.Windows.Forms.Button ExternalJobColourButton;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape ExternalColourShape;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Section1ColourShape;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Section3ColourShape;
        private Microsoft.VisualBasic.PowerPacks.RectangleShape Section2ColourShape;
        private System.Windows.Forms.Label FlowLabel;
        private System.Windows.Forms.ComboBox FlowComboBox;
    }
}


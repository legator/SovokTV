namespace WindowsFormsApplication11
{
    partial class ChSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.ratio_box = new System.Windows.Forms.ComboBox();
            this.dein_box = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buffer_box = new System.Windows.Forms.TextBox();
            this.Save_button = new System.Windows.Forms.Button();
            this.clear_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ratio";
            // 
            // ratio_box
            // 
            this.ratio_box.FormattingEnabled = true;
            this.ratio_box.Items.AddRange(new object[] {
            "4:3",
            "16:9",
            "221:100"});
            this.ratio_box.Location = new System.Drawing.Point(118, 29);
            this.ratio_box.Name = "ratio_box";
            this.ratio_box.Size = new System.Drawing.Size(165, 27);
            this.ratio_box.TabIndex = 1;
            // 
            // dein_box
            // 
            this.dein_box.FormattingEnabled = true;
            this.dein_box.Items.AddRange(new object[] {
            "0",
            "blend",
            "bob",
            "discard",
            "linear",
            "mean",
            "x",
            "yadif",
            "yadif2x"});
            this.dein_box.Location = new System.Drawing.Point(118, 68);
            this.dein_box.Name = "dein_box";
            this.dein_box.Size = new System.Drawing.Size(165, 27);
            this.dein_box.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Deinterlace ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Buffer";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buffer_box
            // 
            this.buffer_box.Location = new System.Drawing.Point(118, 103);
            this.buffer_box.Name = "buffer_box";
            this.buffer_box.Size = new System.Drawing.Size(165, 26);
            this.buffer_box.TabIndex = 7;
            this.buffer_box.Text = "3000";
            // 
            // Save_button
            // 
            this.Save_button.Location = new System.Drawing.Point(118, 145);
            this.Save_button.Name = "Save_button";
            this.Save_button.Size = new System.Drawing.Size(75, 28);
            this.Save_button.TabIndex = 8;
            this.Save_button.Text = "Save";
            this.Save_button.UseVisualStyleBackColor = true;
            this.Save_button.Click += new System.EventHandler(this.Save_button_Click);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(208, 145);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 28);
            this.clear_button.TabIndex = 9;
            this.clear_button.Text = "Clear";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // ChSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(323, 211);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.Save_button);
            this.Controls.Add(this.buffer_box);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dein_box);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ratio_box);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(330, 250);
            this.Name = "ChSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Channel settings";
            this.Load += new System.EventHandler(this.ChSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ratio_box;
        private System.Windows.Forms.ComboBox dein_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox buffer_box;
        private System.Windows.Forms.Button Save_button;
        private System.Windows.Forms.Button clear_button;


    }
}
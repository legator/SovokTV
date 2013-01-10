namespace WindowsFormsApplication11
{
    partial class Settings
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
            this.settingstab = new System.Windows.Forms.TabControl();
            this.LogintabPage = new System.Windows.Forms.TabPage();
            this.stream_box1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.time_box1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.clear_button = new System.Windows.Forms.Button();
            this.code_box = new System.Windows.Forms.TextBox();
            this.pass_box = new System.Windows.Forms.TextBox();
            this.user_box = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.save_button = new System.Windows.Forms.Button();
            this.AccounttabPage = new System.Windows.Forms.TabPage();
            this.acc_name = new System.Windows.Forms.Label();
            this.stream_box = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.time_box = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.save_set_acc = new System.Windows.Forms.Button();
            this.PintabPage = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.pin2_box = new System.Windows.Forms.TextBox();
            this.pin1_box = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.chasnge_pin = new System.Windows.Forms.Button();
            this.packeglist = new System.Windows.Forms.ListView();
            this.NameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ExpireHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label9 = new System.Windows.Forms.Label();
            this.balance = new System.Windows.Forms.Label();
            this.settingstab.SuspendLayout();
            this.LogintabPage.SuspendLayout();
            this.AccounttabPage.SuspendLayout();
            this.PintabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // settingstab
            // 
            this.settingstab.Controls.Add(this.LogintabPage);
            this.settingstab.Controls.Add(this.AccounttabPage);
            this.settingstab.Controls.Add(this.PintabPage);
            this.settingstab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingstab.Location = new System.Drawing.Point(0, 0);
            this.settingstab.Name = "settingstab";
            this.settingstab.SelectedIndex = 0;
            this.settingstab.Size = new System.Drawing.Size(324, 312);
            this.settingstab.TabIndex = 0;
            // 
            // LogintabPage
            // 
            this.LogintabPage.Controls.Add(this.stream_box1);
            this.LogintabPage.Controls.Add(this.label5);
            this.LogintabPage.Controls.Add(this.time_box1);
            this.LogintabPage.Controls.Add(this.label4);
            this.LogintabPage.Controls.Add(this.clear_button);
            this.LogintabPage.Controls.Add(this.code_box);
            this.LogintabPage.Controls.Add(this.pass_box);
            this.LogintabPage.Controls.Add(this.user_box);
            this.LogintabPage.Controls.Add(this.label3);
            this.LogintabPage.Controls.Add(this.label2);
            this.LogintabPage.Controls.Add(this.label1);
            this.LogintabPage.Controls.Add(this.save_button);
            this.LogintabPage.Location = new System.Drawing.Point(4, 28);
            this.LogintabPage.Name = "LogintabPage";
            this.LogintabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogintabPage.Size = new System.Drawing.Size(316, 280);
            this.LogintabPage.TabIndex = 0;
            this.LogintabPage.Text = "Login";
            this.LogintabPage.UseVisualStyleBackColor = true;
            // 
            // stream_box1
            // 
            this.stream_box1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stream_box1.FormattingEnabled = true;
            this.stream_box1.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.stream_box1.Location = new System.Drawing.Point(122, 152);
            this.stream_box1.Name = "stream_box1";
            this.stream_box1.Size = new System.Drawing.Size(165, 27);
            this.stream_box1.TabIndex = 23;
            this.stream_box1.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 19);
            this.label5.TabIndex = 22;
            this.label5.Text = "Streamers";
            this.label5.Visible = false;
            // 
            // time_box1
            // 
            this.time_box1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.time_box1.FormattingEnabled = true;
            this.time_box1.Items.AddRange(new object[] {
            "-06:00:00",
            "-05:00:00",
            "-04:00:00",
            "-03:00:00",
            "-02:00:00",
            "-01:00:00",
            "+00:00:00",
            "+01:00:00",
            "+02:00:00",
            "+03:00:00",
            "+04:00:00"});
            this.time_box1.Location = new System.Drawing.Point(122, 117);
            this.time_box1.Name = "time_box1";
            this.time_box1.Size = new System.Drawing.Size(165, 27);
            this.time_box1.TabIndex = 21;
            this.time_box1.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 19);
            this.label4.TabIndex = 20;
            this.label4.Text = "Time zone";
            this.label4.Visible = false;
            // 
            // clear_button
            // 
            this.clear_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clear_button.Location = new System.Drawing.Point(212, 240);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(75, 28);
            this.clear_button.TabIndex = 19;
            this.clear_button.Text = "Clear";
            this.clear_button.UseVisualStyleBackColor = true;
            // 
            // code_box
            // 
            this.code_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.code_box.Location = new System.Drawing.Point(122, 82);
            this.code_box.Name = "code_box";
            this.code_box.Size = new System.Drawing.Size(165, 26);
            this.code_box.TabIndex = 18;
            // 
            // pass_box
            // 
            this.pass_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pass_box.Location = new System.Drawing.Point(122, 47);
            this.pass_box.Name = "pass_box";
            this.pass_box.Size = new System.Drawing.Size(165, 26);
            this.pass_box.TabIndex = 17;
            // 
            // user_box
            // 
            this.user_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.user_box.Location = new System.Drawing.Point(122, 12);
            this.user_box.Name = "user_box";
            this.user_box.Size = new System.Drawing.Size(165, 26);
            this.user_box.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(87, 19);
            this.label3.TabIndex = 15;
            this.label3.Text = "Protect code";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 14;
            this.label2.Text = "Password";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 19);
            this.label1.TabIndex = 13;
            this.label1.Text = "Username";
            // 
            // save_button
            // 
            this.save_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.save_button.Location = new System.Drawing.Point(122, 240);
            this.save_button.Name = "save_button";
            this.save_button.Size = new System.Drawing.Size(75, 28);
            this.save_button.TabIndex = 12;
            this.save_button.Text = "Save";
            this.save_button.UseVisualStyleBackColor = true;
            this.save_button.Click += new System.EventHandler(this.save_button_Click);
            // 
            // AccounttabPage
            // 
            this.AccounttabPage.Controls.Add(this.balance);
            this.AccounttabPage.Controls.Add(this.label9);
            this.AccounttabPage.Controls.Add(this.packeglist);
            this.AccounttabPage.Controls.Add(this.acc_name);
            this.AccounttabPage.Controls.Add(this.stream_box);
            this.AccounttabPage.Controls.Add(this.label6);
            this.AccounttabPage.Controls.Add(this.time_box);
            this.AccounttabPage.Controls.Add(this.label7);
            this.AccounttabPage.Controls.Add(this.button1);
            this.AccounttabPage.Controls.Add(this.label10);
            this.AccounttabPage.Controls.Add(this.save_set_acc);
            this.AccounttabPage.Location = new System.Drawing.Point(4, 28);
            this.AccounttabPage.Name = "AccounttabPage";
            this.AccounttabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AccounttabPage.Size = new System.Drawing.Size(316, 280);
            this.AccounttabPage.TabIndex = 1;
            this.AccounttabPage.Text = "Account";
            this.AccounttabPage.UseVisualStyleBackColor = true;
            // 
            // acc_name
            // 
            this.acc_name.AutoSize = true;
            this.acc_name.Location = new System.Drawing.Point(118, 12);
            this.acc_name.Name = "acc_name";
            this.acc_name.Size = new System.Drawing.Size(70, 19);
            this.acc_name.TabIndex = 24;
            this.acc_name.Text = "acc_name";
            // 
            // stream_box
            // 
            this.stream_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.stream_box.FormattingEnabled = true;
            this.stream_box.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.stream_box.Location = new System.Drawing.Point(122, 117);
            this.stream_box.Name = "stream_box";
            this.stream_box.Size = new System.Drawing.Size(165, 27);
            this.stream_box.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 120);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 19);
            this.label6.TabIndex = 22;
            this.label6.Text = "Streamers";
            // 
            // time_box
            // 
            this.time_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.time_box.FormattingEnabled = true;
            this.time_box.Items.AddRange(new object[] {
            "-06:00:00",
            "-05:00:00",
            "-04:00:00",
            "-03:00:00",
            "-02:00:00",
            "-01:00:00",
            "+00:00:00",
            "+01:00:00",
            "+02:00:00",
            "+03:00:00",
            "+04:00:00"});
            this.time_box.Location = new System.Drawing.Point(122, 82);
            this.time_box.Name = "time_box";
            this.time_box.Size = new System.Drawing.Size(165, 27);
            this.time_box.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 19);
            this.label7.TabIndex = 20;
            this.label7.Text = "Time zone";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(212, 240);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 28);
            this.button1.TabIndex = 19;
            this.button1.Text = "Clear";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 15);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 19);
            this.label10.TabIndex = 13;
            this.label10.Text = "Username";
            // 
            // save_set_acc
            // 
            this.save_set_acc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.save_set_acc.Location = new System.Drawing.Point(122, 240);
            this.save_set_acc.Name = "save_set_acc";
            this.save_set_acc.Size = new System.Drawing.Size(75, 28);
            this.save_set_acc.TabIndex = 12;
            this.save_set_acc.Text = "Save";
            this.save_set_acc.UseVisualStyleBackColor = true;
            this.save_set_acc.Click += new System.EventHandler(this.save_set_acc_Click);
            // 
            // PintabPage
            // 
            this.PintabPage.Controls.Add(this.button3);
            this.PintabPage.Controls.Add(this.pin2_box);
            this.PintabPage.Controls.Add(this.pin1_box);
            this.PintabPage.Controls.Add(this.label14);
            this.PintabPage.Controls.Add(this.label15);
            this.PintabPage.Controls.Add(this.chasnge_pin);
            this.PintabPage.Location = new System.Drawing.Point(4, 28);
            this.PintabPage.Name = "PintabPage";
            this.PintabPage.Padding = new System.Windows.Forms.Padding(3);
            this.PintabPage.Size = new System.Drawing.Size(316, 280);
            this.PintabPage.TabIndex = 2;
            this.PintabPage.Text = "Pin";
            this.PintabPage.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button3.Location = new System.Drawing.Point(211, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 28);
            this.button3.TabIndex = 31;
            this.button3.Text = "Clear";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.clear_button_Click);
            // 
            // pin2_box
            // 
            this.pin2_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pin2_box.Location = new System.Drawing.Point(122, 47);
            this.pin2_box.Name = "pin2_box";
            this.pin2_box.Size = new System.Drawing.Size(165, 26);
            this.pin2_box.TabIndex = 29;
            // 
            // pin1_box
            // 
            this.pin1_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pin1_box.Location = new System.Drawing.Point(122, 12);
            this.pin1_box.Name = "pin1_box";
            this.pin1_box.Size = new System.Drawing.Size(165, 26);
            this.pin1_box.TabIndex = 28;
            // 
            // label14
            // 
            this.label14.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(15, 50);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 19);
            this.label14.TabIndex = 26;
            this.label14.Text = "PIN2";
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 15);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(43, 19);
            this.label15.TabIndex = 25;
            this.label15.Text = "PIN1";
            // 
            // chasnge_pin
            // 
            this.chasnge_pin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chasnge_pin.Location = new System.Drawing.Point(122, 240);
            this.chasnge_pin.Name = "chasnge_pin";
            this.chasnge_pin.Size = new System.Drawing.Size(75, 28);
            this.chasnge_pin.TabIndex = 24;
            this.chasnge_pin.Text = "Save";
            this.chasnge_pin.UseVisualStyleBackColor = true;
            this.chasnge_pin.Click += new System.EventHandler(this.chasnge_pin_Click);
            // 
            // packeglist
            // 
            this.packeglist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHeader,
            this.ExpireHeader});
            this.packeglist.Location = new System.Drawing.Point(15, 155);
            this.packeglist.Name = "packeglist";
            this.packeglist.Size = new System.Drawing.Size(272, 78);
            this.packeglist.TabIndex = 25;
            this.packeglist.UseCompatibleStateImageBehavior = false;
            this.packeglist.View = System.Windows.Forms.View.Details;
            // 
            // NameHeader
            // 
            this.NameHeader.Text = "Name";
            this.NameHeader.Width = 100;
            // 
            // ExpireHeader
            // 
            this.ExpireHeader.Text = "Expire";
            this.ExpireHeader.Width = 150;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 50);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 19);
            this.label9.TabIndex = 26;
            this.label9.Text = "Balance";
            // 
            // balance
            // 
            this.balance.AutoSize = true;
            this.balance.Location = new System.Drawing.Point(118, 50);
            this.balance.Name = "balance";
            this.balance.Size = new System.Drawing.Size(55, 19);
            this.balance.TabIndex = 27;
            this.balance.Text = "balance";
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 312);
            this.Controls.Add(this.settingstab);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(340, 350);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 295);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.settingstab.ResumeLayout(false);
            this.LogintabPage.ResumeLayout(false);
            this.LogintabPage.PerformLayout();
            this.AccounttabPage.ResumeLayout(false);
            this.AccounttabPage.PerformLayout();
            this.PintabPage.ResumeLayout(false);
            this.PintabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl settingstab;
        private System.Windows.Forms.TabPage LogintabPage;
        private System.Windows.Forms.ComboBox stream_box1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox time_box1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.TextBox code_box;
        private System.Windows.Forms.TextBox pass_box;
        private System.Windows.Forms.TextBox user_box;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button save_button;
        private System.Windows.Forms.TabPage AccounttabPage;
        private System.Windows.Forms.ComboBox stream_box;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox time_box;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button save_set_acc;
        private System.Windows.Forms.Label acc_name;
        private System.Windows.Forms.TabPage PintabPage;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox pin2_box;
        private System.Windows.Forms.TextBox pin1_box;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button chasnge_pin;
        private System.Windows.Forms.ListView packeglist;
        private System.Windows.Forms.ColumnHeader NameHeader;
        private System.Windows.Forms.ColumnHeader ExpireHeader;
        private System.Windows.Forms.Label balance;
        private System.Windows.Forms.Label label9;

    }
}
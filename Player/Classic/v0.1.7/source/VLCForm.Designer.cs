namespace WindowsFormsApplication11
{
    partial class VLCForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VLCForm));
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.vlcform_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.volume10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.volume10ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.playSttopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.topMostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            this.vlcform_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.ContextMenuStrip = this.vlcform_menu;
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(0, 0);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(584, 311);
            this.axVLCPlugin21.TabIndex = 1;
            // 
            // vlcform_menu
            // 
            this.vlcform_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.volume10ToolStripMenuItem,
            this.volume10ToolStripMenuItem1,
            this.playSttopToolStripMenuItem,
            this.toolStripMenuItem2,
            this.topMostToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.vlcform_menu.Name = "vlcform_menu";
            this.vlcform_menu.ShowImageMargin = false;
            this.vlcform_menu.Size = new System.Drawing.Size(151, 136);
            // 
            // volume10ToolStripMenuItem
            // 
            this.volume10ToolStripMenuItem.Name = "volume10ToolStripMenuItem";
            this.volume10ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Z)));
            this.volume10ToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.volume10ToolStripMenuItem.Text = "Volume +10";
            this.volume10ToolStripMenuItem.Click += new System.EventHandler(this.volume10ToolStripMenuItem_Click);
            // 
            // volume10ToolStripMenuItem1
            // 
            this.volume10ToolStripMenuItem1.Name = "volume10ToolStripMenuItem1";
            this.volume10ToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.X)));
            this.volume10ToolStripMenuItem1.Size = new System.Drawing.Size(150, 22);
            this.volume10ToolStripMenuItem1.Text = "Volume -10";
            this.volume10ToolStripMenuItem1.Click += new System.EventHandler(this.volume10ToolStripMenuItem1_Click);
            // 
            // playSttopToolStripMenuItem
            // 
            this.playSttopToolStripMenuItem.Name = "playSttopToolStripMenuItem";
            this.playSttopToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.playSttopToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.playSttopToolStripMenuItem.Text = "Play/Sttop";
            this.playSttopToolStripMenuItem.Click += new System.EventHandler(this.playSttopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItem2.Text = "4:3/16:9";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // topMostToolStripMenuItem
            // 
            this.topMostToolStripMenuItem.Checked = true;
            this.topMostToolStripMenuItem.CheckOnClick = true;
            this.topMostToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.topMostToolStripMenuItem.Name = "topMostToolStripMenuItem";
            this.topMostToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
            this.topMostToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.topMostToolStripMenuItem.Text = "TopMost";
            this.topMostToolStripMenuItem.Click += new System.EventHandler(this.topMostToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // VLCForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.ContextMenuStrip = this.vlcform_menu;
            this.Controls.Add(this.axVLCPlugin21);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "VLCForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VLC windows";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VLCForm_FormClosing);
            this.Load += new System.EventHandler(this.VLCForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            this.vlcform_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private System.Windows.Forms.ContextMenuStrip vlcform_menu;
        private System.Windows.Forms.ToolStripMenuItem volume10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem volume10ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem playSttopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem topMostToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}
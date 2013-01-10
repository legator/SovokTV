using System.Windows.Forms;
namespace WindowsFormsApplication11
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listview_panel = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.channel_list = new DevComponents.AdvTree.AdvTree();
            this.contextMenuChlist = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setFavoriteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.epg_list = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.favorite_list = new DevComponents.AdvTree.AdvTree();
            this.contextMenuFavorite = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nodeConnector3 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.vlc_panel = new System.Windows.Forms.Panel();
            this.vlcwindows_panel = new System.Windows.Forms.Panel();
            this.axVLCPlugin21 = new AxAXVLC.AxVLCPlugin2();
            this.vlc2win_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toWindowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vlccontrols_panel = new System.Windows.Forms.Panel();
            this.about_button = new System.Windows.Forms.Button();
            this.ontop_box = new System.Windows.Forms.CheckBox();
            this.settings_button = new System.Windows.Forms.Button();
            this.volume_bar = new System.Windows.Forms.TrackBar();
            this.ratio_button = new System.Windows.Forms.Button();
            this.play_button = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIco = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.s = new System.Windows.Forms.Splitter();
            this.load_channel = new System.ComponentModel.BackgroundWorker();
            this.listview_panel.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.channel_list)).BeginInit();
            this.contextMenuChlist.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.favorite_list)).BeginInit();
            this.contextMenuFavorite.SuspendLayout();
            this.vlc_panel.SuspendLayout();
            this.vlcwindows_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).BeginInit();
            this.vlc2win_menu.SuspendLayout();
            this.vlccontrols_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume_bar)).BeginInit();
            this.contextMenuNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // listview_panel
            // 
            this.listview_panel.AutoScroll = true;
            this.listview_panel.AutoScrollMinSize = new System.Drawing.Size(200, 0);
            this.listview_panel.Controls.Add(this.tabControl1);
            this.listview_panel.Dock = System.Windows.Forms.DockStyle.Left;
            this.listview_panel.Location = new System.Drawing.Point(0, 0);
            this.listview_panel.MaximumSize = new System.Drawing.Size(400, 0);
            this.listview_panel.MinimumSize = new System.Drawing.Size(200, 400);
            this.listview_panel.Name = "listview_panel";
            this.listview_panel.Size = new System.Drawing.Size(300, 461);
            this.listview_panel.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.MinimumSize = new System.Drawing.Size(300, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(300, 461);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.channel_list);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(292, 429);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Channel list";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // channel_list
            // 
            this.channel_list.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.channel_list.AllowDrop = true;
            this.channel_list.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.channel_list.BackgroundStyle.Class = "TreeBorderKey";
            this.channel_list.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.channel_list.ContextMenuStrip = this.contextMenuChlist;
            this.channel_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channel_list.GridRowLines = true;
            this.channel_list.Location = new System.Drawing.Point(3, 3);
            this.channel_list.Name = "channel_list";
            this.channel_list.NodesConnector = this.nodeConnector1;
            this.channel_list.NodeStyle = this.elementStyle1;
            this.channel_list.PathSeparator = ";";
            this.channel_list.Size = new System.Drawing.Size(286, 423);
            this.channel_list.Styles.Add(this.elementStyle1);
            this.channel_list.TabIndex = 1;
            this.channel_list.Text = "advTree1";
            this.channel_list.View = DevComponents.AdvTree.eView.Tile;
            this.channel_list.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.channel_list_NodeClick);
            // 
            // contextMenuChlist
            // 
            this.contextMenuChlist.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFavoriteToolStripMenuItem,
            this.settingsToolStripMenuItem1});
            this.contextMenuChlist.Name = "contextMenuChlist";
            this.contextMenuChlist.ShowImageMargin = false;
            this.contextMenuChlist.Size = new System.Drawing.Size(109, 48);
            // 
            // setFavoriteToolStripMenuItem
            // 
            this.setFavoriteToolStripMenuItem.Name = "setFavoriteToolStripMenuItem";
            this.setFavoriteToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.setFavoriteToolStripMenuItem.Text = "Set favorite";
            this.setFavoriteToolStripMenuItem.Click += new System.EventHandler(this.setFavoriteToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem1
            // 
            this.settingsToolStripMenuItem1.Name = "settingsToolStripMenuItem1";
            this.settingsToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.settingsToolStripMenuItem1.Text = "Settings";
            this.settingsToolStripMenuItem1.Click += new System.EventHandler(this.settingsToolStripMenuItem1_Click);
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.Class = "";
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.epg_list);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(292, 435);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "TV programm";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // epg_list
            // 
            this.epg_list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.epg_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.epg_list.FullRowSelect = true;
            this.epg_list.GridLines = true;
            this.epg_list.Location = new System.Drawing.Point(3, 3);
            this.epg_list.Name = "epg_list";
            this.epg_list.Size = new System.Drawing.Size(286, 429);
            this.epg_list.TabIndex = 0;
            this.epg_list.UseCompatibleStateImageBehavior = false;
            this.epg_list.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 1;
            this.columnHeader1.Text = "Праграмма";
            this.columnHeader1.Width = 240;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DisplayIndex = 0;
            this.columnHeader2.Text = "Время";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.favorite_list);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(292, 435);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Favorite";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // favorite_list
            // 
            this.favorite_list.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.favorite_list.AllowDrop = true;
            this.favorite_list.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.favorite_list.BackgroundStyle.Class = "TreeBorderKey";
            this.favorite_list.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.favorite_list.ContextMenuStrip = this.contextMenuFavorite;
            this.favorite_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favorite_list.GridRowLines = true;
            this.favorite_list.Location = new System.Drawing.Point(3, 3);
            this.favorite_list.Name = "favorite_list";
            this.favorite_list.NodesConnector = this.nodeConnector3;
            this.favorite_list.NodeStyle = this.elementStyle3;
            this.favorite_list.PathSeparator = ";";
            this.favorite_list.Size = new System.Drawing.Size(286, 429);
            this.favorite_list.Styles.Add(this.elementStyle3);
            this.favorite_list.TabIndex = 3;
            this.favorite_list.Text = "Favorite";
            this.favorite_list.View = DevComponents.AdvTree.eView.Tile;
            this.favorite_list.NodeClick += new DevComponents.AdvTree.TreeNodeMouseEventHandler(this.channel_list_NodeClick);
            // 
            // contextMenuFavorite
            // 
            this.contextMenuFavorite.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.contextMenuFavorite.Name = "contextMenuFavorite";
            this.contextMenuFavorite.ShowImageMargin = false;
            this.contextMenuFavorite.Size = new System.Drawing.Size(92, 48);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(91, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // nodeConnector3
            // 
            this.nodeConnector3.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle3
            // 
            this.elementStyle3.Class = "";
            this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // vlc_panel
            // 
            this.vlc_panel.BackColor = System.Drawing.Color.White;
            this.vlc_panel.Controls.Add(this.vlcwindows_panel);
            this.vlc_panel.Controls.Add(this.vlccontrols_panel);
            this.vlc_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vlc_panel.Location = new System.Drawing.Point(300, 0);
            this.vlc_panel.MinimumSize = new System.Drawing.Size(500, 400);
            this.vlc_panel.Name = "vlc_panel";
            this.vlc_panel.Size = new System.Drawing.Size(500, 461);
            this.vlc_panel.TabIndex = 1;
            // 
            // vlcwindows_panel
            // 
            this.vlcwindows_panel.Controls.Add(this.axVLCPlugin21);
            this.vlcwindows_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vlcwindows_panel.Location = new System.Drawing.Point(0, 0);
            this.vlcwindows_panel.MinimumSize = new System.Drawing.Size(500, 400);
            this.vlcwindows_panel.Name = "vlcwindows_panel";
            this.vlcwindows_panel.Size = new System.Drawing.Size(500, 401);
            this.vlcwindows_panel.TabIndex = 1;
            // 
            // axVLCPlugin21
            // 
            this.axVLCPlugin21.ContextMenuStrip = this.vlc2win_menu;
            this.axVLCPlugin21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axVLCPlugin21.Enabled = true;
            this.axVLCPlugin21.Location = new System.Drawing.Point(0, 0);
            this.axVLCPlugin21.Name = "axVLCPlugin21";
            this.axVLCPlugin21.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axVLCPlugin21.OcxState")));
            this.axVLCPlugin21.Size = new System.Drawing.Size(500, 401);
            this.axVLCPlugin21.TabIndex = 0;
            this.axVLCPlugin21.MediaPlayerOpening += new System.EventHandler(this.axVLCPlugin21_MediaPlayerOpening);
            this.axVLCPlugin21.MediaPlayerPlaying += new System.EventHandler(this.axVLCPlugin21_MediaPlayerPlaying);
            this.axVLCPlugin21.MediaPlayerStopped += new System.EventHandler(this.axVLCPlugin21_MediaPlayerStopped);
            // 
            // vlc2win_menu
            // 
            this.vlc2win_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toWindowsToolStripMenuItem});
            this.vlc2win_menu.Name = "vlc2win_menu";
            this.vlc2win_menu.ShowImageMargin = false;
            this.vlc2win_menu.Size = new System.Drawing.Size(155, 26);
            // 
            // toWindowsToolStripMenuItem
            // 
            this.toWindowsToolStripMenuItem.Name = "toWindowsToolStripMenuItem";
            this.toWindowsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.W)));
            this.toWindowsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.toWindowsToolStripMenuItem.Text = "To windows";
            this.toWindowsToolStripMenuItem.Click += new System.EventHandler(this.toWindowsToolStripMenuItem_Click);
            // 
            // vlccontrols_panel
            // 
            this.vlccontrols_panel.ContextMenuStrip = this.vlc2win_menu;
            this.vlccontrols_panel.Controls.Add(this.about_button);
            this.vlccontrols_panel.Controls.Add(this.ontop_box);
            this.vlccontrols_panel.Controls.Add(this.settings_button);
            this.vlccontrols_panel.Controls.Add(this.volume_bar);
            this.vlccontrols_panel.Controls.Add(this.ratio_button);
            this.vlccontrols_panel.Controls.Add(this.play_button);
            this.vlccontrols_panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.vlccontrols_panel.Location = new System.Drawing.Point(0, 401);
            this.vlccontrols_panel.MinimumSize = new System.Drawing.Size(500, 60);
            this.vlccontrols_panel.Name = "vlccontrols_panel";
            this.vlccontrols_panel.Size = new System.Drawing.Size(500, 60);
            this.vlccontrols_panel.TabIndex = 0;
            // 
            // about_button
            // 
            this.about_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.about_button.Location = new System.Drawing.Point(412, 13);
            this.about_button.Name = "about_button";
            this.about_button.Size = new System.Drawing.Size(67, 28);
            this.about_button.TabIndex = 5;
            this.about_button.Text = "About";
            this.about_button.UseVisualStyleBackColor = true;
            this.about_button.Click += new System.EventHandler(this.about_button_Click);
            // 
            // ontop_box
            // 
            this.ontop_box.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ontop_box.AutoSize = true;
            this.ontop_box.Location = new System.Drawing.Point(260, 18);
            this.ontop_box.Name = "ontop_box";
            this.ontop_box.Size = new System.Drawing.Size(75, 23);
            this.ontop_box.TabIndex = 4;
            this.ontop_box.Text = "On Top";
            this.ontop_box.UseVisualStyleBackColor = true;
            this.ontop_box.CheckedChanged += new System.EventHandler(this.ontop_box_CheckedChanged);
            // 
            // settings_button
            // 
            this.settings_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.settings_button.Location = new System.Drawing.Point(341, 13);
            this.settings_button.Name = "settings_button";
            this.settings_button.Size = new System.Drawing.Size(65, 28);
            this.settings_button.TabIndex = 3;
            this.settings_button.Text = "Settings";
            this.settings_button.UseVisualStyleBackColor = true;
            this.settings_button.Click += new System.EventHandler(this.settings_button_Click);
            // 
            // volume_bar
            // 
            this.volume_bar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.volume_bar.Location = new System.Drawing.Point(134, 8);
            this.volume_bar.Maximum = 100;
            this.volume_bar.Name = "volume_bar";
            this.volume_bar.Size = new System.Drawing.Size(130, 45);
            this.volume_bar.TabIndex = 2;
            this.volume_bar.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.volume_bar.ValueChanged += new System.EventHandler(this.volume_bar_ValueChanged);
            // 
            // ratio_button
            // 
            this.ratio_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ratio_button.Location = new System.Drawing.Point(70, 13);
            this.ratio_button.Name = "ratio_button";
            this.ratio_button.Size = new System.Drawing.Size(58, 28);
            this.ratio_button.TabIndex = 1;
            this.ratio_button.Text = "16:9";
            this.ratio_button.UseVisualStyleBackColor = true;
            this.ratio_button.Click += new System.EventHandler(this.ratio_button_Click);
            // 
            // play_button
            // 
            this.play_button.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.play_button.Location = new System.Drawing.Point(6, 13);
            this.play_button.Name = "play_button";
            this.play_button.Size = new System.Drawing.Size(58, 30);
            this.play_button.TabIndex = 0;
            this.play_button.Text = "Play";
            this.play_button.UseVisualStyleBackColor = true;
            this.play_button.Click += new System.EventHandler(this.play_button_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 300000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIco
            // 
            this.notifyIco.ContextMenuStrip = this.contextMenuNotify;
            this.notifyIco.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIco.Icon")));
            this.notifyIco.Text = "Sovok.tv player by legAToR";
            this.notifyIco.Visible = true;
            this.notifyIco.DoubleClick += new System.EventHandler(this.notifyIco_DoubleClick);
            this.notifyIco.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIco_MouseDoubleClick);
            // 
            // contextMenuNotify
            // 
            this.contextMenuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.showHideToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuNotify.Name = "contextMenuNotify";
            this.contextMenuNotify.ShowImageMargin = false;
            this.contextMenuNotify.Size = new System.Drawing.Size(109, 70);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // showHideToolStripMenuItem
            // 
            this.showHideToolStripMenuItem.Name = "showHideToolStripMenuItem";
            this.showHideToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.showHideToolStripMenuItem.Text = "Show/Hide";
            this.showHideToolStripMenuItem.Click += new System.EventHandler(this.sowHideToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // s
            // 
            this.s.Location = new System.Drawing.Point(300, 0);
            this.s.Name = "s";
            this.s.Size = new System.Drawing.Size(3, 461);
            this.s.TabIndex = 0;
            this.s.TabStop = false;
            // 
            // load_channel
            // 
            this.load_channel.DoWork += new System.ComponentModel.DoWorkEventHandler(this.load_channel_DoWork);
            this.load_channel.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.load_channel_RunWorkerCompleted);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.vlc_panel);
            this.Controls.Add(this.listview_panel);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sovok.tv player";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.listview_panel.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.channel_list)).EndInit();
            this.contextMenuChlist.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.favorite_list)).EndInit();
            this.contextMenuFavorite.ResumeLayout(false);
            this.vlc_panel.ResumeLayout(false);
            this.vlcwindows_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axVLCPlugin21)).EndInit();
            this.vlc2win_menu.ResumeLayout(false);
            this.vlccontrols_panel.ResumeLayout(false);
            this.vlccontrols_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.volume_bar)).EndInit();
            this.contextMenuNotify.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel listview_panel;
        private System.Windows.Forms.Panel vlc_panel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Panel vlcwindows_panel;
        private System.Windows.Forms.Panel vlccontrols_panel;
        private System.Windows.Forms.Button about_button;
        private System.Windows.Forms.CheckBox ontop_box;
        private System.Windows.Forms.Button settings_button;
        private System.Windows.Forms.TrackBar volume_bar;
        private System.Windows.Forms.Button ratio_button;
        private System.Windows.Forms.Button play_button;
        private System.Windows.Forms.NotifyIcon notifyIco;
        private Splitter s;
        private TabControl tabControl1;
        private TabPage tabPage2;
        private ListView epg_list;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private AxAXVLC.AxVLCPlugin2 axVLCPlugin21;
        private ContextMenuStrip contextMenuChlist;
        private TabPage tabPage3;
        private ContextMenuStrip contextMenuFavorite;
        private ContextMenuStrip contextMenuNotify;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem showHideToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem setFavoriteToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem1;
        private TabPage tabPage1;
        private DevComponents.AdvTree.AdvTree channel_list;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
        private System.ComponentModel.BackgroundWorker load_channel;
        private DevComponents.AdvTree.AdvTree favorite_list;
        private DevComponents.AdvTree.NodeConnector nodeConnector3;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private ContextMenuStrip vlc2win_menu;
        private ToolStripMenuItem toWindowsToolStripMenuItem;

    }
}


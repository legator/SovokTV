using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using SovoktvAPI;
using WindowsFormsApplication11.Properties;
using System.IO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.AdvTree;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication11
{
    public partial class MainForm : Form
    {
        #region init var
        private string user { get; set; }
        private string pass { get; set; }
        private string code { get; set; }
        private string time_zone { get; set; }
        private int streamer { get; set; }
        private string active_ch { get; set; }
        public string chratio { get; set; }
        private Boolean isepg { get; set; }
        private Boolean isshow { get; set; }
        private string vlc_url { get; set; }

        SovokAPI api = new SovokAPI();
        Account acc = new Account();
        Setting sch = new Setting();
        EpgNext epgn = new EpgNext();
        List<Programs> lp = new List<Programs>();
        List<EpgNext> le = new List<EpgNext>();
        List<Service> ls = new List<Service>();
        #endregion

        public MainForm()
        {
            InitializeComponent();

            //InitChannelList();
            this.channel_list.TileSize = new Size(240, 48);
            this.favorite_list.TileSize = new Size(240, 48);
        }

        #region inittree
        private void InitChannelList()
        {
            ElementStyle elementStyle = new ElementStyle();
            elementStyle.TextColor = Color.SlateGray;
            elementStyle.Font = new Font(this.channel_list.Font.FontFamily, 9f, FontStyle.Bold);
            elementStyle.Name = "groupstyle";
            this.favorite_list.Styles.Add(elementStyle);
            this.channel_list.Styles.Add(elementStyle);
            ElementStyle gray = new ElementStyle();
            gray.TextColor = Color.SlateGray;
            gray.Name = "subitemstyle";
            this.favorite_list.Styles.Add(gray);
            this.channel_list.Styles.Add(gray);
            gray = new ElementStyle();
            gray.TextColor = Color.Maroon;
            gray.Name = "subitemphone";
            this.channel_list.Styles.Add(gray);
            this.favorite_list.Styles.Add(gray);

            this.channel_list.GroupNodeCreated += AdvTree7GroupNodeCreated;
            this.channel_list.DataNodeCreated += AdvTree7DataNodeCreated;
            //this.channel_list.TileSize = new Size(250, 60);
            this.channel_list.TileSize = new Size(250, 40);
            this.channel_list.GroupingMembers = "Group";
            this.channel_list.DisplayMembers = "Name,EPGnow,EPGnext,Url";

            this.favorite_list.GroupNodeCreated += AdvTreeGroupNodeCreated;
            this.favorite_list.DataNodeCreated += AdvTreeDataNodeCreated;
            //this.favorite_list.TileSize = new Size(250, 60);
            this.favorite_list.TileSize = new Size(250, 40);
            this.favorite_list.GroupingMembers = "Group";
            this.favorite_list.DisplayMembers = "Name,EPGnow,EPGnext,Url";
        }

        private void AdvTree7GroupNodeCreated(object sender, DataNodeEventArgs e)
        {
            e.Node.Style = this.channel_list.Styles["groupstyle"];
        }

        private void AdvTree7DataNodeCreated(object sender, DataNodeEventArgs e)
        {
            e.Node.Cells[1].StyleNormal = this.channel_list.Styles["subitemstyle"];
            e.Node.Cells[2].StyleNormal = this.channel_list.Styles["subitemphone"];
            e.Node.Cells[3].StyleNormal = this.channel_list.Styles["subitemphone"];
        }

        private void AdvTreeGroupNodeCreated(object sender, DataNodeEventArgs e)
        {
            e.Node.Style = this.favorite_list.Styles["groupstyle"];
        }

        private void AdvTreeDataNodeCreated(object sender, DataNodeEventArgs e)
        {
            e.Node.Cells[1].StyleNormal = this.favorite_list.Styles["subitemstyle"];
            e.Node.Cells[2].StyleNormal = this.favorite_list.Styles["subitemphone"];
            e.Node.Cells[3].StyleNormal = this.favorite_list.Styles["subitemphone"];
        }

        public Image ResizeImage(Image img, int percentage)
        {
            //get the height and width of the image
            int originalW = img.Width;
            int originalH = img.Height;

            //get the new size based on the percentage change
            int resizedW = (int)((originalW * percentage)/100);
            int resizedH = (int)((originalH * percentage)/100);

            //create a new Bitmap the size of the new image
            Bitmap bmp = new Bitmap(resizedW, resizedH);
            //create a new graphic from the Bitmap
            Graphics graphic = Graphics.FromImage((Image)bmp);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //draw the newly resized image
            graphic.DrawImage(img, 0, 0, resizedW, resizedH);
            //dispose and free up the resources
            graphic.Dispose();
            //return the image
            return (Image)bmp;
        }

        private Node CreateChildNode(string nodeText, string subText, Image image, string id, ElementStyle subItemStyle)
        {
            Node node = new Node(nodeText);
            node.Image = image;
            node.Tag = id;
            node.Cells.Add(new Cell(subText, subItemStyle));
            /*string epgnext = "";
            try
            {
                List<EpgNext> epgn = api.epg_next(id);
                epgnext = epgn[1].progname;
            }catch (Exception){}
            node.Cells.Add(new Cell(epgnext, subItemStyle));*/
            return node;
        }
        #endregion

        #region form action
        /// <summary>
        /// Get user login data and login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            isshow = true;
            get_data();
            if (user != "" || pass != "")
            {
                login(user, pass, code);
            }
            axVLCPlugin21.Volume = volume_bar.Value = 50;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                notifyIco.BalloonTipTitle = "Work..";
                notifyIco.BalloonTipText = "App stil working";
                notifyIco.ShowBalloonTip(100);
            }
        }

        private void notifyIco_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void notifyIco_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Close app?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                timer1.Stop();
                timer2.Stop();
                try
                {
                    if (axVLCPlugin21.playlist.isPlaying)
                    {
                        axVLCPlugin21.playlist.stop();
                    }
                }
                catch (Exception er) { error_log(er); }
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region sovok.tv api
        private void get_data()
        {
            try
            {
                user = ConfigurationSettings.AppSettings["user"].ToString();
                pass = ConfigurationSettings.AppSettings["pass"].ToString();
                code = ConfigurationSettings.AppSettings["code"].ToString();
                time_zone = ConfigurationSettings.AppSettings["time"].ToString();
            }catch(Exception er)
            {
                error_log(er);
            }
        }
        /// <summary>
        /// Login to sovok.tv
        /// </summary>
        /// <param name="luser">user login</param>
        /// <param name="lpass">user pass</param>
        /// <param name="lcode">user code for xxx channels</param>
        public void login(string luser, string lpass, string lcode)
        {
            try
            {
                api.auth(luser, lpass, lcode);
                api.channel_list();
                load_channel.RunWorkerAsync();
                timer2.Start();
            }
            catch (Exception ex) 
            { 
                error_log(ex);
                MessageBox.Show("Error to login", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Get information about account
        /// </summary>
        /// <returns></returns>
        public Account get_acc()
        {
            try
            {
                acc = api.acc;
                return acc;
            }catch(Exception){return null;}
        }

        public Setting get_set()
        {
            try
            {
                return api.setting();
            }
            catch (Exception) { return null; }
        }

        private void insert_ch()
        {
            /*List<Channel> ch = new List<Channel>();
            foreach (Group gitem in api.acc.channel_group)
            {
                for (int i = 0; i < gitem.Channels.Count; i++)
                {
                    string epgnext = "";//hide from here
                    try
                    {
                        List<EpgNext> epgn = api.epg_next(gitem.Channels[i].id);
                        epgnext = epgn[1].progname;
                        Thread.Sleep(350);
                    }
                    catch (Exception)
                    {   }//to here
                    Channel c = new Channel(gitem.name, gitem.Channels[i].name, gitem.Channels[i].epg_progname, epgnext, gitem.Channels[i].id);
                    ch.Add(c);
                }
            }
            try
            {
                if (channel_list.InvokeRequired == true)
                {
                    channel_list.Invoke((MethodInvoker)delegate { channel_list.Nodes.Clear(); channel_list.DataSource = ch; });
                }
            }
            catch (Exception e) { }*/
            channel_list.Nodes.Clear();
            ElementStyle elementStyle = new ElementStyle();
            elementStyle.TextColor = Color.Navy;
            elementStyle.Font = new Font(this.channel_list.Font.FontFamily, 8.0f);
            elementStyle.Name = "groupstyle";
            this.channel_list.Styles.Add(elementStyle);
            ElementStyle gray = new ElementStyle();
            gray.TextColor = Color.Gray;
            gray.Name = "subitemstyle";
            this.channel_list.Styles.Add(gray);

            foreach (Group gitem in api.acc.channel_group)
            {
                Node node = new Node(gitem.name, elementStyle);
                node.Expanded = true;
                this.channel_list.Nodes.Add(node);
                for (int i = 0; i < gitem.Channels.Count; i++)
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    System.IO.Stream stream = webClient.OpenRead("http://sovok.tv"+gitem.Channels[i].icon);
                    //bitmap = new Bitmap(stream);
                    Image im = System.Drawing.Image.FromStream(stream);
                    im = ResizeImage(im, 35);

                    node.Nodes.Add(this.CreateChildNode(gitem.Channels[i].name, gitem.Channels[i].epg_progname, im, gitem.Channels[i].id, gray));
                }
            }
        }

        private void channel_list_ItemActivate(object sender, EventArgs e)
        {
            ListView bv = (ListView)sender;
            active_ch = bv.SelectedItems[0].Tag.ToString();
            ac_ch(active_ch);
        }

        private void ac_ch(string id)
        {
            if (id != "")
            {
                if (axVLCPlugin21.playlist.items.count > 0)
                {
                    try
                    {
                        play_button.Text = "Stop";
                        ratio_button.Text = "16:9";
                        chratio = "4:3";
                        axVLCPlugin21.playlist.stop();
                        axVLCPlugin21.playlist.items.clear();
                    }
                    catch (Exception ex)
                    {
                        error_log(ex);
                    }
                }
                open_ch(id);
            }
        }

        private void channel_list_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            DevComponents.AdvTree.AdvTree tn = (DevComponents.AdvTree.AdvTree)sender;
            bool isbreak = false;
            foreach (DevComponents.AdvTree.Node item in tn.Nodes)
            {
                if (item.HasChildNodes)
                {
                    foreach (DevComponents.AdvTree.Node n in item.Nodes)
                    {
                        if (n.IsSelected)
                        {
                            //active_ch = n.Cells[n.Cells.Count-1].Text;
                            active_ch = n.Tag.ToString();
                            isbreak = true;
                            break;
                        }
                    }
                    if (isbreak)
                    {
                        isbreak = false;
                        break;
                    }
                }
            }
            ac_ch(active_ch);
        }

        private void open_ch(string id)
        {
            string url = "";
            try
            {
                string u = api.get_url(id);
                url = u.Split(' ')[0];
                var option = new string[u.Split(' ').Length-1];
                for (int i = 1; i < u.Split(' ').Length; i++)
                {
                    option[i-1] = u.Split(' ')[i];
                    if (u.Split(' ')[i].Split('=')[0] == ":aspect-ratio")
                    {
                        chratio = u.Split(' ')[i].Split('=')[1];
                    }
                }
                if (chratio=="16:9")
                {
                    ratio_button.Text = "4:3";
                }
                else
                    if (chratio=="4:3")
                    {
                        ratio_button.Text = "16:9";
                    }
                vlc_url = url;
                axVLCPlugin21.playlist.add(url);
                axVLCPlugin21.playlist.playItem(0);
                axVLCPlugin21.playlist.play();
                play_button.Text = "Stop";
            }
            catch (Exception er)
            {
                //MessageBox.Show("Невозможно открыть канал\n" + vlc_url, "Ошыбка");
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error_log(er);
            }
            open_epg(id);
        }

        private void open_epg(string id)
        {
            //formating datetime
            string y = (DateTime.Now.Year - 2000).ToString();
            string d = DateTime.Now.Day.ToString();
            if (d.Length == 1) d = "0" + d;
            string m = DateTime.Now.Month.ToString();
            if (m.Length == 1) m = "0" + m;
            string dt = d + m + y;
            //get epg
            epg_list.Items.Clear();
            try
            {
                lp = api.epg(active_ch, dt);
                foreach (Programs p in lp)
                {
                    ListViewItem lv = new ListViewItem(p.progname);
                    lv.Name = p.progname;
                    lv.SubItems.Add(p.t_start);
                    lv.Tag = p.progname;
                    epg_list.Items.Add(lv);
                }
                epg(id);
                isepg = true;
            }
            catch (Exception ex) 
            {
                isepg = false;
                MessageBox.Show("Ошыбка одержание программы\n" + ex.Message,"Ошыбка",MessageBoxButtons.OK,MessageBoxIcon.Error);
                error_log(ex);
            }
        }

        private void epg(string id)
        {
            try
            {
                le = api.epg_next(id);
                bool p = false;
                for (int i = 0; i < epg_list.Items.Count - 2; i++)
                {
                    if (epg_list.Items[i].Name == le[0].progname)
                    {
                        if (epg_list.Items[i + 1].Name == le[1].progname)
                        {
                            if (epg_list.Items[i + 2].Name == le[2].progname)
                            {
                                epg_list.Items[i].BackColor = Color.White;
                                p = true;
                            }
                            else
                                if (!p)
                                {
                                    epg_list.Items[i].BackColor = Color.Gray;
                                }
                                else
                                {
                                    epg_list.Items[i].BackColor = Color.White;
                                }
                        }
                        else
                            if (!p)
                            {
                                epg_list.Items[i].BackColor = Color.Gray;
                            }
                            else
                            {
                                epg_list.Items[i].BackColor = Color.White;
                            }
                    }
                    else
                        if (!p)
                        {
                            epg_list.Items[i].BackColor = Color.Gray;
                        }
                        else
                        {
                            epg_list.Items[i].BackColor = Color.White;
                        }
                }
            }
             catch(Exception){}   
        }

        private void favorite_ch()
        {
            favorite_list.Nodes.Clear();


            ElementStyle elementStyle = new ElementStyle();
            elementStyle.TextColor = Color.Navy;
            elementStyle.Font = new Font(this.favorite_list.Font.FontFamily, 8.0f);
            elementStyle.Name = "groupstyle";
            this.favorite_list.Styles.Add(elementStyle);
            ElementStyle gray = new ElementStyle();
            gray.TextColor = Color.Gray;
            gray.Name = "subitemstyle";
            this.favorite_list.Styles.Add(gray);
            Node node = new Node("Favorite", elementStyle);
            node.Expanded = true;
            this.favorite_list.Nodes.Add(node);
            try
            {
                api.favorites();
                List<Favorite> lf = api.acc.favorite_channel;
                foreach (Favorite f in lf)
                { 
                    Boolean isf = false;
                    foreach (DevComponents.AdvTree.Node nd in channel_list.Nodes)
                    {
                        if (nd.HasChildNodes)
                        {
                            foreach (DevComponents.AdvTree.Node n in nd.Nodes)
                            {
                                if (n.Tag.ToString() == f.id_channel)
                                {
                                    node.Nodes.Add(this.CreateChildNode(n.Cells[0].Text, n.Cells[1].Text, n.Image, f.id_channel, gray));
                                    //MessageBox.Show(n.Cells[0].Text);
                                    isf = true;
                                    break;
                                }
                            }
                        }
                        if (isf)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            /*
            List<Channel> ch = new List<Channel>();
            try
            {
                api.favorites();
                List<Favorite> lf = api.acc.favorite_channel;
                foreach (Favorite f in lf)
                {
                    Boolean isf = false;
                    foreach (DevComponents.AdvTree.Node nd in channel_list.Nodes)
                    {
                        if (nd.HasChildNodes)
                        {
                            foreach (DevComponents.AdvTree.Node n in nd.Nodes)
                            {
                                if (n.Cells[n.Cells.Count - 1].Text == f.id_channel)
                                {
                                    Channel c = new Channel(n.Name, n.Cells[n.Cells.Count - 4].Text, n.Cells[n.Cells.Count - 3].Text, n.Cells[n.Cells.Count - 2].Text, n.Cells[n.Cells.Count - 1].Text);
                                    ch.Add(c);
                                    isf = true;
                                    break;
                                }
                            }
                        }
                        if (isf)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                
            }
            favorite_list.DataSource = ch;*/
        }
        #endregion

        #region timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            load_channel.RunWorkerAsync();
            open_epg(active_ch);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute%5==0)
            {
                timer1.Start();
                timer2.Stop();
            }
        }
        #endregion

        #region control event
        private void play_button_Click(object sender, EventArgs e)
        {
            if (play_button.Text == "Play")
            {
                play_button.Text = "Stop";
                try
                {
                    axVLCPlugin21.playlist.play();
                }
                catch (Exception ex)
                {
                    error_log(ex);
                }
            }
            else
            {
                play_button.Text = "Play";
                try
                {
                    axVLCPlugin21.playlist.stop();
                }
                catch (Exception ex)
                {
                    error_log(ex); 
                }
            }
        }

        private void ratio_button_Click(object sender, EventArgs e)
        {
            if (ratio_button.Text == "16:9")
            {
                ratio_button.Text = "4:3";
                axVLCPlugin21.video.aspectRatio = "16:9";
            }
            else
            {
                ratio_button.Text = "16:9";
                axVLCPlugin21.video.aspectRatio = "4:3";
            }
        }

        private void settings_button_Click(object sender, EventArgs e)
        {
            Settings s = new Settings(this);
            s.ShowDialog();
        }

        private void about_button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program - sovok.tv vlc player v0.1.7.0\nAuthor - legAToR\nEmail - oleg.legator@gmail.com", "About",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void ontop_box_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = ontop_box.Checked;
        }

        private void volume_bar_ValueChanged(object sender, EventArgs e)
        {
            TrackBar tr = (TrackBar)sender;
            axVLCPlugin21.Volume = tr.Value;
        }
        #endregion

        #region mediaplayer
        private void axVLCPlugin21_MediaPlayerOpening(object sender, EventArgs e)
        {
            this.Text = "Sovok.tv player - Opening";
        }

        private void axVLCPlugin21_MediaPlayerPlaying(object sender, EventArgs e)
        {
            this.Text = "Sovok.tv player - Playing";
        }

        private void axVLCPlugin21_MediaPlayerStopped(object sender, EventArgs e)
        {
            this.Text = "Sovok.tv player";
        }
        #endregion

        public void error_log(Exception er)
        {
            TextWriter tw;
            string fn = "error_log.txt";
            if (File.Exists(fn))
            {
                tw = new StreamWriter(fn, true);
            }
            else tw = new StreamWriter(fn);
            tw.WriteLine(DateTime.Now.ToString() + "-" + er.Message);
            tw.Close();
        }

        #region contexmenu
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Program - sovok.tv vlc player v0.1.5.0\nAuthor - legAToR\nEmail - oleg.legator@gmail.com", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void sowHideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isshow)
            {
                Hide();
                notifyIco.BalloonTipTitle = "Work..";
                notifyIco.BalloonTipText = "App stil working";
                notifyIco.ShowBalloonTip(100);
                isshow = false;
            }
            else
            {
                isshow = true;
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fav(active_ch);
            favorite_ch();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            channel_settings(active_ch);
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            channel_settings(active_ch);
        }

        private void setFavoriteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fav(active_ch);
            favorite_ch();
        }
        #endregion

        #region channel action
        public void chset(string chratio, string chbuffer, string chdein)
        {
            try
            {
                sch = api.set_settings(active_ch, chratio, chbuffer, chdein);
                ac_ch(active_ch);
            }
            catch (Exception er) 
            { 
                error_log(er);
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Set time zone
        /// </summary>
        /// <param name="timez">value of time zone</param>
        public void set_timezone(string timez)
        {
            try
            {
                time_zone = api.set_settings(timez);
            }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void set_streamers(int stream)
        {
            try
            {
                streamer = api.set_settings(stream);
            }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        public void set_pins(string pin1, string pin2)
        {
            try
            {
                api.set_settings(pin1, pin2);
            }
            catch (Exception er) { MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void channel_settings(string id)
        {
            ChSettings chs = new ChSettings(this);
            chs.ShowDialog();
        }

        private void fav(string id)
        {
            try
            {
                api.favorites_set(id);
            }
            catch (Exception er) 
            {
                error_log(er);
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region load channel thread
        private void load_channel_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (this.InvokeRequired == true)
                {
                    this.Invoke((MethodInvoker)delegate { this.Text = "Load channels - please wait"; });
                }
            }
            catch (Exception er) {}
            insert_ch();
        }
        
        private void load_channel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                this.Text = "Sovok.tv player";
            }
            catch (Exception er) { }
            Thread.Sleep(400);
            favorite_ch();
        }
        #endregion

        private void toWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (vlc_url != null)
            {
                try
                {
                    axVLCPlugin21.playlist.stop();
                }
                catch (Exception ex)
                {
                    error_log(ex);
                }
                isshow = false;
                VLCForm vform = new VLCForm(this,vlc_url);
                this.Hide();
                vform.Show();
            }
            
        }
        
    }

    internal class Channel
    {
        private string _Group;
        private string _Name;
        private string _EPGnow;
        private string _EPGnext;
        private string _url;

        public string Group
        {
            get
            {
                return this._Group;
            }
            set
            {
                this._Group = value;
            }
        }

        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }

        public string EPGnow
        {
            get
            {
                return this._EPGnow;
            }
            set
            {
                this._EPGnow = value;
            }
        }

        public string EPGnext
        {
            get
            {
                return this._EPGnext;
            }
            set
            {
                this._EPGnext = value;
            }
        }

        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        public Channel(string group, string name, string epgnow, string epgnext, string url)
        {
            this.Group = group;
            this.Name = name;
            this.EPGnow = epgnow;
            this.EPGnext = epgnext;
            this.Url = url;
        }
    }
}

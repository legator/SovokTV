using AxAXVLC;
using MahApps.Metro.Controls;
using SovoktvAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SovokTV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public AxVLCPlugin2 vlc;
        private SovokAPI api = new SovokAPI();
        private Account acc = new Account();
        private Setting sch = new Setting();
        private EpgNext epgn = new EpgNext();
        private List<Programs> lp = new List<Programs>();
        private List<EpgNext> le = new List<EpgNext>();
        public List<Channel> lchannel = new List<Channel>();

        private string user { get; set; }
        private string pass { get; set; }
        private string code { get; set; }
        private string time_zone { get; set; }
        private int streamer { get; set; }
        private string active_ch { get; set; }
        public string chratio { get; set; }
        private Boolean isepg { get; set; }
        private Boolean isshow { get; set; }
        private Boolean islogin = false;
        private string vlc_url { get; set; }
        Timer timer1 = new Timer();
        Timer timer2 = new Timer();
        public int k = 0;

        BackgroundWorker login_thread;
        BackgroundWorker loadchannel_thread;
        BackgroundWorker loadfav_thread;
        BackgroundWorker updatelist_thread;

        public MainWindow()
        {
            vlc = new AxVLCPlugin2();
            InitializeComponent();
            WinFormsHost.Child = vlc; 
            login_thread = new BackgroundWorker();
            login_thread.RunWorkerCompleted += login_thread_RunWorkerCompleted;
            login_thread.DoWork += login_thread_DoWork;

            loadchannel_thread = new BackgroundWorker();
            loadchannel_thread.RunWorkerCompleted += loadchannel_RunWorkerCompleted;
            loadchannel_thread.DoWork += loadchannel_DoWork;

            loadfav_thread = new BackgroundWorker();
            loadfav_thread.RunWorkerCompleted += loadfav_RunWorkerCompleted;
            loadfav_thread.DoWork += loadfav_DoWork;

            updatelist_thread = new BackgroundWorker();
            updatelist_thread.RunWorkerCompleted += updatelist_thread_RunWorkerCompleted;
            updatelist_thread.DoWork += updatelist_thread_DoWork;

            login_thread.RunWorkerAsync();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            /*timer1.Interval = 300000;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer2.Tick += new System.EventHandler(this.timer2_Tick);
            timer1.Start();*/
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            player_version.Text = fvi.FileVersion;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Minute % 5 == 0)
            {
                timer1.Start();
                timer2.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            updatelist_thread.RunWorkerAsync();
        }

        private void loadfav_DoWork(object sender, DoWorkEventArgs e)
        {
            load_favorite();
        }

        private void loadfav_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (k==0)
            {
                ChList.ItemsSource = lchannel;
                //ListCollectionView lc = (ListCollectionView)CollectionViewSource.GetDefaultView(ChList.ItemsSource);
                //lc.GroupDescriptions.Add(new PropertyGroupDescription { PropertyName = "CategoryName" });
            }
            
        }

        void login_thread_DoWork(object sender, DoWorkEventArgs e)
        {
            if (SovokTV.Properties.Settings.Default.UserName != "")
            {
                if (SovokTV.Properties.Settings.Default.Password != "")
                {
                    login(SovokTV.Properties.Settings.Default.UserName, SovokTV.Properties.Settings.Default.Password, SovokTV.Properties.Settings.Default.Code);
                }
            }
        }

        void login_thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (islogin)
            {
                account_user.Text = SovokTV.Properties.Settings.Default.UserName;
                account_pass.Text = SovokTV.Properties.Settings.Default.Password;
                account_code.Text = SovokTV.Properties.Settings.Default.Code;
                set_acc_data();
                //insert_ch();
                loadchannel_thread.RunWorkerAsync();
            }
        }

        void updatelist_thread_DoWork(object sender, DoWorkEventArgs e)
        {
            UpdateChList();
        }

        void updatelist_thread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void loadchannel_DoWork(object sender, DoWorkEventArgs e)
        {
            insert_ch();
        }

        private void loadchannel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            loadfav_thread.RunWorkerAsync();
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("Close app?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
                e.Cancel = true;
        }

        #region open tab
        private void BackToList(object sender, RoutedEventArgs e)
        {
            chlist_tab.IsSelected = true;
        }

        private void author_open_tab(object sender, RoutedEventArgs e)
        {
            author_tab.IsSelected = true;
        }

        private void account_open_tab(object sender, RoutedEventArgs e)
        {
            account_tab.IsSelected = true;
        }

        private void progtv_open_tab(object sender, RoutedEventArgs e)
        {
            tvprog_tab.IsSelected = true;
        }
        #endregion

        #region vlc actions
        private void stop_stream(object sender, RoutedEventArgs e)
        {
            vlc.playlist.stop();
        }

        private void pay_stream(object sender, RoutedEventArgs e)
        {
            if (!islogin || active_ch!=null)
            {
                return;
            }
            try
            {
                ac_ch(active_ch);
            }
            catch (Exception ex)
            {
                error_log(ex);
            }
        }

        private void dec_vol_action(object sender, RoutedEventArgs e)
        {
            vol_slider.Value = vlc.Volume = vlc.Volume - 10;
        }

        private void inc_vol_action(object sender, RoutedEventArgs e)
        {
            vol_slider.Value = vlc.Volume = vlc.Volume + 10;
        }

        private void change_ration(object sender, RoutedEventArgs e)
        {
            if (but_rat.Text=="16:9")
            {
                but_rat.Text = "4:3";
                vlc.video.aspectRatio = "16:9";
            }
            else
            {
                but_rat.Text = "16:9";
                vlc.video.aspectRatio = "4:3";
                //axVLCPlugin21.video.aspectRatio = "4:3";
            }
        }
        #endregion

        #region open contact
        private void open_twitter(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/_legator");
        }

        private void open_github(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/legator");
        }

        private void open_controls(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://mahapps.com/MahApps.Metro");
        }

        private void open_sovoktv(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sovok.tv");
        }
        #endregion

        #region help methods
        private void set_acc_data()
        {
            if((acc=get_acc())!=null)
            {
                acc_name.Text = acc.login;
                acc_balance.Text = acc.balance;
                packagelist.Items.Clear();
                for (int i = 0; i < acc.services.Count; i++)
                {
                    Package p = new Package();
                    p.package_name = acc.services[i].name;
                    p.package_expire = ConvertFromUnixTimestamp(Convert.ToDouble(acc.services[i].expire)).ToShortDateString();
                    packagelist.Items.Add(p);
                }
                //acc_time.Text = ConvertFromUnixTimestamp(Convert.ToDouble(acc.packet_expire)).ToShortDateString();
            }
            if ((sch = get_set()) != null)
            {
                SovokTV.Properties.Settings.Default.TimeZ = account_zone.Text = get_set().timezone;
                SovokTV.Properties.Settings.Default.Streamer = account_streamer.Text = sch.streamer;
                SovokTV.Properties.Settings.Default.Save();
            }
            timer1.Interval = 300000;
            timer1.Tick += new System.EventHandler(this.timer1_Tick);
            timer2.Tick += new System.EventHandler(this.timer2_Tick);
            timer1.Start();
        }

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

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
        #endregion

        #region sovok api
        private void login(string luser, string lpass, string lcode)
        {
            try
            {
                api.auth(luser, lpass, lcode);
                api.channel_list();
                islogin = true;
            }
            catch (Exception ex)
            {
                error_log(ex);
                System.Windows.Forms.MessageBox.Show("Error to login\n"+ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Account get_acc()
        {
            try
            {
                acc = api.acc;
                return acc;
            }
            catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); return null; }
        }

        private Setting get_set()
        {
            try
            {
                return api.setting();
            }
            catch (Exception) { return null; }
        }

        private void set_timezone(string timez)
        {
            try
            {
                time_zone = api.set_settings(timez);
            }
            catch (Exception er) { System.Windows.Forms.MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void set_streamers(int stream)
        {
            try
            {
                streamer = api.set_settings(stream);
            }
            catch (Exception er) { System.Windows.Forms.MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void set_ch_set(string chratio, string chbuffer, string chdein)
        {
            try
            {
                sch = api.set_settings(active_ch, chratio, chbuffer, chdein);
                ac_ch(active_ch);
            }
            catch (Exception er)
            {
                error_log(er);
                System.Windows.Forms.MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ac_ch(string id)
        {
            if (id != "")
            {
                try
                    {
                        vlc.playlist.stop();
                        vlc.playlist.items.clear();
                    }
                    catch (Exception ex)
                    {
                        error_log(ex);
                    }
                open_ch(id);
            }
        }

        private void open_ch(string id)
        {
            string url = "";
            try
            {
                string u = api.get_url(id);
                url = u.Split(' ')[0];
                var option = new string[u.Split(' ').Length - 1];
                for (int i = 1; i < u.Split(' ').Length; i++)
                {
                    option[i - 1] = u.Split(' ')[i];
                    if (u.Split(' ')[i].Split('=')[0] == ":aspect-ratio")
                    {
                        chratio = u.Split(' ')[i].Split('=')[1];
                    }
                }
                if (chratio == "16:9")
                {
                    but_rat.Text = "4:3";
                }
                else
                    if (chratio == "4:3")
                    {
                        but_rat.Text = "16:9";
                    }
                vlc.playlist.add(url);
                vlc.playlist.playItem(0);
                vlc.playlist.play();
            }
            catch (Exception er)
            {
                System.Windows.Forms.MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ProgramEPG item = new ProgramEPG();
                    item.epg_program = p.progname;
                    item.epg_time = p.t_start;
                    epg_list.Items.Add(item);//epg_list.ItemContainerStyle.
                }
                epg(id);
                isepg = true;
            }
            catch (Exception ex)
            {
                isepg = false;
                System.Windows.Forms.MessageBox.Show("Ошыбка одержание программы\n" + ex.Message, "Ошыбка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ProgramEPG item = (ProgramEPG)epg_list.Items[i];
                    ProgramEPG item1 = (ProgramEPG)epg_list.Items[i + 1];
                    ProgramEPG item2 = (ProgramEPG)epg_list.Items[i + 2];
                    if (item.epg_program == le[0].progname)
                    {
                        if (item1.epg_program == le[1].progname)
                        {
                            if (item2.epg_program == le[2].progname)
                            {
                                //epg_list.Items[i].BackColor = Color.White;
                                System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                                l.Background = Brushes.White;
                                p = true;
                            }
                            else
                                if (!p)
                                {
                                    //epg_list.Items[i].BackColor = Color.Gray;
                                    System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                                    l.Background = Brushes.Gray;
                                }
                                else
                                {
                                   //epg_list.Items[i].BackColor = Color.White;
                                    System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                                    l.Background = Brushes.White;
                                }
                        }
                        else
                            if (!p)
                            {
                                //epg_list.Items[i].BackColor = Color.Gray;
                                System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                                l.Background = Brushes.Gray;
                            }
                            else
                            {
                                //epg_list.Items[i].BackColor = Color.White;
                                System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                                l.Background = Brushes.White;
                            }
                    }
                    else
                        if (!p)
                        {
                            //epg_list.Items[i].BackColor = Color.Gray;
                            System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                            l.Background = Brushes.Gray;
                        }
                        else
                        {
                            //epg_list.Items[i].BackColor = Color.White;
                            System.Windows.Controls.ListViewItem l = (System.Windows.Controls.ListViewItem)epg_list.Items[i];
                            l.Background = Brushes.White;
                        }
                }
            }
            catch (Exception) { }
        }

        private void insert_ch()
        {
            foreach (Group gitem in api.acc.channel_group)
            {
                this.Dispatcher.Invoke((Action)(() =>
                {
                    ComboBoxItem cbi = new ComboBoxItem();
                    cbi.Content = cbi.Name = gitem.name;
                    CategoryComboBox.Items.Add(cbi);
                }));
                
                for (int i = 0; i < gitem.Channels.Count; i++)
                {
                    Channel ch = new Channel();
                    ch.CategoryName = gitem.name;
                    string address = "http://sovok.tv" + gitem.Channels[i].icon;
                    ch.Image_Source = new Uri(address);
                    ch.ChannelName = gitem.Channels[i].name;
                    ch.Epg = gitem.Channels[i].epg_progname;
                    ch.ChannelId = gitem.Channels[i].id;

                    DateTime start_dt = new DateTime();
                    DateTime end_dt = new DateTime();
                    int proc = 0;
                    try
                    {
                        start_dt = ConvertFromUnixTimestamp(Convert.ToDouble(gitem.Channels[i].epg_start));
                        end_dt = ConvertFromUnixTimestamp(Convert.ToDouble(gitem.Channels[i].epg_end));
                        DateTime dt = new DateTime();
                        dt = DateTime.Now;
                        int z = -3;
                        if (SovokTV.Properties.Settings.Default.TimeZ!=null)
                        {
                            z = Convert.ToInt32(SovokTV.Properties.Settings.Default.TimeZ);
                        }
                        dt = dt.AddHours(z);
                        TimeSpan plength = end_dt.Subtract(start_dt);
                        TimeSpan pduration = dt.Subtract(start_dt);
                        proc = Convert.ToInt32((pduration.TotalMinutes / plength.TotalMinutes) * 100);
                    }
                    catch (System.Exception ex)
                    {
                    }

                    ch.Progress = proc;
                    
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        lchannel.Add(ch);
                    }));
                }
            }
        }

        private void load_favorite()
        {
            try
            {
                api.favorites();
                List<Favorite> lf = api.acc.favorite_channel;
                List<Channel> lc = lchannel;
                foreach (Favorite f in lf)
                {
                    foreach (Channel ch in lc)
                    {
                        if (ch.ChannelId == f.id_channel)
                        {
                            Channel c = new Channel();
                            c = ch;
                            c.CategoryName = "Favorite";
                            this.Dispatcher.Invoke((Action)(() =>
                            {
                                bool isf = false;
                                foreach (Channel citem in lchannel)
                                {
                                    if (c.CategoryName==citem.CategoryName)
                                    {
                                        if (c.ChannelId==citem.ChannelId)
                                        {
                                            isf = true;
                                        }
                                    }
                                }
                                if(!isf) lchannel.Add(c);
                            }));
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

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
                System.Windows.Forms.MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateChList()
        {
            List<Channel> chs = new List<Channel>();
            chs = lchannel;
            for (int j = 0; j < chs.Count; j++)
            {
                foreach (Group gitem in api.acc.channel_group)
                {
                    for (int i = 0; i < gitem.Channels.Count; i++)
                    {
                        if (chs[j].ChannelId == gitem.Channels[i].id)
                        {
                            DateTime start_dt = new DateTime();
                            DateTime end_dt = new DateTime();
                            int proc = 0;
                            try
                            {
                                start_dt = ConvertFromUnixTimestamp(Convert.ToDouble(gitem.Channels[i].epg_start));
                                end_dt = ConvertFromUnixTimestamp(Convert.ToDouble(gitem.Channels[i].epg_end));
                                DateTime dt = new DateTime();
                                dt = DateTime.Now;
                                int z = -3;
                                if (SovokTV.Properties.Settings.Default.TimeZ != null)
                                {
                                    z = Convert.ToInt32(SovokTV.Properties.Settings.Default.TimeZ);
                                }
                                dt = dt.AddHours(z);
                                TimeSpan plength = end_dt.Subtract(start_dt);
                                TimeSpan pduration = dt.Subtract(start_dt);
                                proc = Convert.ToInt32((pduration.TotalMinutes / plength.TotalMinutes) * 100);
                            }
                            catch (System.Exception ex)
                            {       }
                            if (proc>=100)
                            {
                                chs[j].Progress = 0;
                                List<SovoktvAPI.Channel> lsh = new List<SovoktvAPI.Channel>();
                                lsh.Add(gitem.Channels[i]);
                                List<Epg2> le = api.epg2(lsh);
                                chs[j].Epg = le[0].progname;
                                this.Dispatcher.Invoke((Action)(() =>
                                {
                                    gitem.Channels[i].epg_start = le[0].start;
                                    gitem.Channels[i].epg_end = le[0].end;
                                }));
                            }
                            else chs[j].Progress = proc;
                        }
                    }
                }
            }
            this.Dispatcher.Invoke((Action)(() =>
            {
                lchannel = chs;
                ChList.Items.Refresh();
            }));
        }
        #endregion

        #region buttons click
        private void clear_acc_click(object sender, RoutedEventArgs e)
        {
            account_streamer.Text = account_zone.Text = null;
        }

        private void save_acc_click(object sender, RoutedEventArgs e)
        {
            if (account_zone.Text!="" || account_streamer.Text!="")
            {
                set_streamers(Convert.ToInt32(account_streamer.Text));
                set_timezone((Convert.ToInt32(account_zone.Text.Split(':')[0])).ToString());
                SovokTV.Properties.Settings.Default.TimeZ = account_zone.Text;
                SovokTV.Properties.Settings.Default.Streamer = account_streamer.Text;
                SovokTV.Properties.Settings.Default.Save();
            }
        }

        private void clear_click(object sender, RoutedEventArgs e)
        {
            account_code.Text = account_pass.Text = account_user.Text = null;
        }

        private void save_click(object sender, RoutedEventArgs e)
        {
            if (account_user.Text != "" || account_pass.Text != "" || account_code.Text != "")
            {
                SovokTV.Properties.Settings.Default.UserName = account_user.Text;
                SovokTV.Properties.Settings.Default.Password = account_pass.Text;
                SovokTV.Properties.Settings.Default.Code = account_code.Text;
                SovokTV.Properties.Settings.Default.Save();
                login_thread.RunWorkerAsync();
                //login(account_user.Text, account_pass.Text, account_code.Text);
            }
        }
        
        private void clear_pin_click(object sender, RoutedEventArgs e)
        {
            account_pin1.Text = account_pin2.Text = null;
        }

        private void save_pin_click(object sender, RoutedEventArgs e)
        {
            if (account_pin1.Text !="" || account_pin2.Text !="")
            {
                try
                {
                    api.set_settings(account_pin1.Text, account_pin2.Text);
                }
                catch (Exception er) { System.Windows.Forms.MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }
        
        private void clear_set_click(object sender, RoutedEventArgs e)
        {
            ch_buffer.Text = ch_dein.Text = ch_ratio.Text = null;
        }

        private void save_set_click(object sender, RoutedEventArgs e)
        {
            if (ch_buffer.Text != "" || ch_dein.Text != "" || ch_ratio.Text != "")
            {
                set_ch_set(ch_ratio.Text, ch_buffer.Text, ch_dein.Text);
            }
        }

        private void Ch_fav_Click(object sender, RoutedEventArgs e)
        {
            Channel c = new Channel();
            c = (Channel)ChList.SelectedItems[0];
            if (c.CategoryName=="Favorite")
            {
                if (lchannel.Remove(c))
                {
                    fav(c.ChannelId);
                    System.Windows.Forms.MessageBox.Show("Channel delete form favorite","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            else
            {
                Channel fc = c;
                fc.CategoryName = "Favorite";
                lchannel.Add(fc);
                fav(fc.ChannelId);
            }
            BackgroundWorker listrefresh = new BackgroundWorker();
            listrefresh.DoWork += listrefresh_DoWork;
            listrefresh.RunWorkerAsync();
        }

        void listrefresh_DoWork(object sender, DoWorkEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                ChList.Items.Refresh();
            }));
        }

        private void Ch_set_Click(object sender, RoutedEventArgs e)
        {
            settings_tab.IsSelected = true;
        }
        #endregion

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (k == 0)
            {
                k++;
            }
            else
            {
                Channel c = new Channel();
                c = (Channel)ChList.SelectedItem;
                active_ch = c.ChannelId;
                ac_ch(active_ch);
            }
        }

        private void vol_slider_change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                vlc.Volume = Convert.ToInt32(vol_slider.Value);
            }
            catch (System.Exception ex)
            {            }
        }

        private void UpdateCategory(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox cmb = (System.Windows.Controls.ComboBox)sender;
            ComboBoxItem val = (ComboBoxItem)cmb.SelectedItem;
            lchannel.Clear();
            foreach (Group gitem in api.acc.channel_group)
            {
                if (gitem.name!=(string)val.Content)
                {
                    continue;
                }
                System.Windows.MessageBox.Show(gitem.name);
                for (int i = 0; i < gitem.Channels.Count; i++)
                {
                    Channel ch = new Channel();
                    ch.CategoryName = gitem.name;
                    string address = "http://sovok.tv" + gitem.Channels[i].icon;
                    ch.Image_Source = new Uri(address);
                    ch.ChannelName = gitem.Channels[i].name;
                    ch.Epg = gitem.Channels[i].epg_progname;
                    ch.ChannelId = gitem.Channels[i].id;

                    DateTime start_dt = new DateTime();
                    DateTime end_dt = new DateTime();
                    int proc = 0;
                    try
                    {
                        start_dt = ConvertFromUnixTimestamp(Convert.ToDouble(gitem.Channels[i].epg_start));
                        end_dt = ConvertFromUnixTimestamp(Convert.ToDouble(gitem.Channels[i].epg_end));
                        DateTime dt = new DateTime();
                        dt = DateTime.Now;
                        int z = -3;
                        if (SovokTV.Properties.Settings.Default.TimeZ != null)
                        {
                            z = Convert.ToInt32(SovokTV.Properties.Settings.Default.TimeZ);
                        }
                        dt = dt.AddHours(z);
                        TimeSpan plength = end_dt.Subtract(start_dt);
                        TimeSpan pduration = dt.Subtract(start_dt);
                        proc = Convert.ToInt32((pduration.TotalMinutes / plength.TotalMinutes) * 100);
                    }
                    catch (System.Exception ex)
                    {
                    }

                    ch.Progress = proc;
                    lchannel.Add(ch);
                }
            }
            ChList.Items.Refresh();
            //ChList.ItemsSource = lchannel;
        }
    }

    public class ProgramEPG
    {
        public string epg_time { get; set; }
        public string epg_program { get; set; }
    }

    public class Package
    {
        public string package_name { get; set; }
        public string package_expire { get; set; }
    }

    public class Channel
    {
        public string CategoryName { get; set; }
        public Uri Image_Source { get; set; }
        public string ChannelName { get; set; }
        public int Progress { get; set; }
        public string Epg { get; set; }
        public string ChannelId { get; set; }
    }
}

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

namespace WindowsFormsApplication11
{
    public partial class Settings : Form
    {
        private MainForm mf;
        private string login { get; set; }
        private string pass  { get; set; }
        private Account acc { get; set; }
        private Setting set { get; set; }

        public Settings(MainForm m)
        {
            InitializeComponent();
            mf = m;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            try
            {
                login = user_box.Text = ConfigurationSettings.AppSettings["user"].ToString();
                pass  = pass_box.Text = ConfigurationSettings.AppSettings["pass"].ToString();
                code_box.Text = ConfigurationSettings.AppSettings["code"].ToString();
                time_box1.Text = ConfigurationSettings.AppSettings["time"].ToString();
                stream_box1.Text = ConfigurationSettings.AppSettings["stream"].ToString();
                if ((acc=mf.get_acc())!=null)
                {
                    acc_name.Text = acc.login;
                    balance.Text = acc.balance;
                    /*name_expire.Text = acc.packet_name;
                    date_expire.Text = ConvertFromUnixTimestamp(Convert.ToDouble(acc.packet_expire)).ToShortDateString();*/
                    for (int i = 0; i < acc.services.Count; i++)
                    {
                        string expire = ConvertFromUnixTimestamp(Convert.ToDouble(acc.services[i].expire)).ToShortDateString();
                        ListViewItem lvi = new ListViewItem(acc.services[i].name);
                        lvi.SubItems.Add(expire);
                        packeglist.Items.Add(lvi);
                    }
                }
                if ((set = mf.get_set()) != null)
                {
                    time_box.Text = set.timezone;
                    stream_box.Text = set.streamer;
                    // Open App.Config of executable
                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    // Add an Application Setting.
                    config.AppSettings.Settings.Remove("time");
                    config.AppSettings.Settings.Add("time", time_box.Text);
                    config.AppSettings.Settings.Remove("stream");
                    config.AppSettings.Settings.Add("stream", stream_box.Text);
                    // Save the configuration file.
                    config.Save(ConfigurationSaveMode.Modified);
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            catch (Exception er)
            {
                mf.error_log(er);
            }
        }

        static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            user_box.Text = pass_box.Text = code_box.Text = "";
        }

        private void save_button_Click(object sender, EventArgs e)
        {
            if (user_box.Text != null || pass_box.Text != null)
            {
                // Open App.Config of executable
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // Add an Application Setting.
                config.AppSettings.Settings.Remove("user");
                config.AppSettings.Settings.Add("user", user_box.Text);
                config.AppSettings.Settings.Remove("pass");
                config.AppSettings.Settings.Add("pass", pass_box.Text);
                config.AppSettings.Settings.Remove("code");
                config.AppSettings.Settings.Add("code", code_box.Text);
                // Save the configuration file.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                if (login!=user_box.Text || pass!=pass_box.Text)
                {
                    mf.login(user_box.Text, pass_box.Text, code_box.Text);
                }
                this.Close();
            }
            else MessageBox.Show("Put all data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void chasnge_pin_Click(object sender, EventArgs e)
        {
            if (pin1_box.Text!=null || pin2_box.Text!=null)
            {
                mf.set_pins(pin1_box.Text, pin2_box.Text);
            }
            else MessageBox.Show("Put all pins", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void save_set_acc_Click(object sender, EventArgs e)
        {
            if (time_box.Text != null || stream_box.Text != null)
            {
                // Open App.Config of executable
                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                // Add an Application Setting.
                config.AppSettings.Settings.Remove("time");
                config.AppSettings.Settings.Add("time", time_box.Text);
                config.AppSettings.Settings.Remove("stream");
                config.AppSettings.Settings.Add("stream", stream_box.Text);
                // Save the configuration file.
                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
                mf.set_timezone(time_box.Text);
                mf.set_streamers(Convert.ToInt32(stream_box.Text));
            }
            else MessageBox.Show("Put all data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

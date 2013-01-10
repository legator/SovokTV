using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace WindowsFormsApplication11
{
    public partial class ChSettings : Form
    {
        private MainForm mf;

        public ChSettings(MainForm m)
        {
            InitializeComponent();
            mf = m;
        }

        private void ChSettings_Load(object sender, EventArgs e)
        {
            ratio_box.Text = mf.chratio;
        }

        private void Save_button_Click(object sender, EventArgs e)
        {
            if (ratio_box.Text != null || buffer_box.Text != null || dein_box.Text != null)
            {
                mf.chset(ratio_box.Text, buffer_box.Text, dein_box.Text);
                ChSettings.ActiveForm.Close();
            }
            else MessageBox.Show("Put all data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void clear_button_Click(object sender, EventArgs e)
        {
            ratio_box.Text = buffer_box.Text = dein_box.Text =  "";
        }
    }
}

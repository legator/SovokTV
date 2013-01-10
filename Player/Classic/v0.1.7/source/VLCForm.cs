using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication11
{
    public partial class VLCForm : Form
    {
        private bool mouseIsDown = false;
        private Point firstPoint;

        private Boolean isratio = false;
        private MainForm mf;
        private string url { get; set; }

        public VLCForm(MainForm m,string urls)
        {
            m = mf;
            InitializeComponent();
            url = urls;
        }

        private void volume10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axVLCPlugin21.Volume != 100)
            {
                axVLCPlugin21.Volume += 10;
            }
        }

        private void volume10ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (axVLCPlugin21.Volume != 0)
            {
                axVLCPlugin21.Volume -= 10;
            }
        }

        private void playSttopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (axVLCPlugin21.playlist.isPlaying)
            {
                axVLCPlugin21.playlist.stop();
            }
            else axVLCPlugin21.playlist.play();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (isratio)
            {
                axVLCPlugin21.video.aspectRatio = "16:9";
            }
            else axVLCPlugin21.video.aspectRatio = "4:3";
        }

        private void topMostToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ti = (ToolStripMenuItem)sender;
            if (ti.Checked)
            {
                this.TopMost = true;
            }
            else this.TopMost = false;
        }

        private void VLCForm_Load(object sender, EventArgs e)
        {
            if (url != "")
            {
                if (axVLCPlugin21.playlist.items.count > 0)
                {
                    try
                    {
                        axVLCPlugin21.playlist.stop();
                        axVLCPlugin21.playlist.items.clear();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                open_ch();
            }
        }

        private void open_ch()
        {
            try
            {
                axVLCPlugin21.playlist.add(url);
                axVLCPlugin21.playlist.playItem(0);
                axVLCPlugin21.playlist.play();
            }
            catch (Exception er)
            {
                MessageBox.Show("Невозможно открыть канал\n" + er.Message, "Ошыбка");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //mf.Show();
            this.Close();
        }

        private void VLCForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (axVLCPlugin21.playlist.isPlaying)
                {
                    axVLCPlugin21.playlist.stop();
                }
            }
            catch (Exception) { }
            mf.Show();
            e.Cancel = false;
        }
    }
}

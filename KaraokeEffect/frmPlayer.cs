﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KaraokeEffect
{
    public partial class frmPlayer : Form
    {
        public frmPlayer()
        {
            InitializeComponent();           
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            karaokeEffect1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using lyrics;

namespace TestKaraokeEffect
{
    public partial class frmPlayer : Form
    {
        List<SyncText> SyncLine = new List<SyncText>();
        List<List<SyncText>> SyncLyrics = new List<List<SyncText>>();

        DateTime start;
        DateTime pausestart;
        DateTime pauselength;

        int pausestartms;
        int pauselengthms;

        int Duration;
        bool paused = false;

        public frmPlayer()
        {
            InitializeComponent();
            Init();

            timer1.Interval = 10;

            karaokeEffect1.nbLyricsLines = 3;
            //karaokeEffect1.StepPercent = 0.01F;
            karaokeEffect1.SyncLyrics = SyncLyrics;
        }


        private void Init()
        {
            SyncLine = new List<SyncText> { new SyncText(0, "Oh"), new SyncText(500, " la"), new SyncText(1000, " la"), new SyncText(1500, " la"), new SyncText(2000, " vie"), new SyncText(2500, " en"), new SyncText(3000, " rose") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(4000, "Le"), new SyncText(4500, " rose"), new SyncText(5000, " qu'on"), new SyncText(5500, " nous"), new SyncText(6000, " pro"), new SyncText(6500, "pose") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(7500, "D'a"), new SyncText(8000, "voir"), new SyncText(8500, " les"), new SyncText(9000, " quan"), new SyncText(9500, "ti"), new SyncText(10000, "tés"), new SyncText(10500, " d'choses") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(11500, "Qui"), new SyncText(12000, " donnent"), new SyncText(12500, " en"), new SyncText(13000, "vie"), new SyncText(13500, " d'au"), new SyncText(14000, "tres"), new SyncText(14500, " choses") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(13500, "Aïe"), new SyncText(15500, " on"), new SyncText(16000, " nous"), new SyncText(16500, " fait"), new SyncText(17500, " croire") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(18500, "Que"), new SyncText(19000, " le"), new SyncText(19500, " bon"), new SyncText(20000, "heur"), new SyncText(20500, " c'est"), new SyncText(21000, " d'a"), new SyncText(21500, "voir") };
            SyncLyrics.Add(SyncLine);

            // Duration = last item of lyrics
            Duration = (int)SyncLyrics[SyncLyrics.Count - 1][SyncLine.Count - 1].time;

            trackBar1.Minimum = 0;
            trackBar1.Maximum = Duration;

            trackBar1.SmallChange = 1000;
            trackBar1.LargeChange = 1000;
            trackBar1.TickFrequency = 500;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            paused = false;
            start = DateTime.Now;
            trackBar1.Value = trackBar1.Minimum;
            karaokeEffect1.Start();
            timer1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            paused = !paused;

            if (paused)
                pausestart = DateTime.Now;
            else
            {
                pauselengthms = DateTime.Now.Subtract(pausestart).Milliseconds;
                start = start.AddMilliseconds(pauselengthms);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (paused) return;

            int ms = (int)DateTime.Now.Subtract(start).TotalMilliseconds;

            if (ms <= trackBar1.Maximum)
                trackBar1.Value = ms;

            
            if (ms > Duration)
            {
                timer1.Stop();
                trackBar1.Value = trackBar1.Minimum;
                karaokeEffect1.Stop();
                return;
            }

            karaokeEffect1.SetPos(ms);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            paused = false;
            timer1.Stop();
            trackBar1.Value = trackBar1.Minimum;
            karaokeEffect1.Stop();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            karaokeEffect1.SetPos(trackBar1.Value);
        }
    }
}


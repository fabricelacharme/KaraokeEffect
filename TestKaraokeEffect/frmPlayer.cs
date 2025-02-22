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

            karaokeEffect1.nbLyricsLines = 3;
            karaokeEffect1.StepPercent = 0.01F;
            karaokeEffect1.SyncLyrics = SyncLyrics;
        }


        private void Init()
        {
            SyncLine = new List<SyncText> { new SyncText(0, "Oh"), new SyncText(500, " la"), new SyncText(1000, " la"), new SyncText(1500, " la"), new SyncText(2000, " vie"), new SyncText(2500, " en"), new SyncText(3000, " rose") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(3500, "Le"), new SyncText(4000, " rose"), new SyncText(4500, " qu'on"), new SyncText(5000, " nous"), new SyncText(5500, " pro"), new SyncText(6000, "pose") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(6500, "D'a"), new SyncText(7000, "voir"), new SyncText(7500, " les"), new SyncText(8000, " quan"), new SyncText(8500, "ti"), new SyncText(9000, "tés"), new SyncText(9500, " d'choses") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(10000, "Qui"), new SyncText(10500, " donnent"), new SyncText(11000, " en"), new SyncText(11500, "vie"), new SyncText(12000, " d'au"), new SyncText(12500, "tres"), new SyncText(13000, " choses") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(13500, "Aïe"), new SyncText(14000, " on"), new SyncText(14500, " nous"), new SyncText(15000, " fait"), new SyncText(15500, " croire") };
            SyncLyrics.Add(SyncLine);
            SyncLine = new List<SyncText> { new SyncText(16000, "Que"), new SyncText(16500, " le"), new SyncText(17000, " bon"), new SyncText(17500, "heur"), new SyncText(18000, " c'est"), new SyncText(18500, " d'a"), new SyncText(19000, "voir") };
            SyncLyrics.Add(SyncLine);

            // Duration = last item of lyrics
            Duration = (int)SyncLyrics[SyncLyrics.Count - 1][SyncLine.Count - 1].time;

            //List<SyncText> l = SyncLyrics[SyncLyrics.Count - 1];
            //SyncText st = l[l.Count - 1];
            //Duration = (int)st.time;

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

            List<SyncText> l = SyncLyrics[SyncLyrics.Count - 1];
            SyncText st = l[l.Count - 1];
            if (ms > st.time)
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


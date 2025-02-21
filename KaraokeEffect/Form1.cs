using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace KaraokeEffect
{
    /*
     *  Add Application.SetCompatibleTextRenderingDefault(false); in Program.Cs
     * 
     * 
     * 
     * 
     * */


    public partial class Form1 : Form
    {
        float percent = 0;
        float lastpercent = 0;        

        List<string[]> Lines = new List<string[]>();
        List<long[]> Times = new List<long[]>();        

        string[] Texts;
        float[] LinesLengths;

        DateTime start;
        int index = 0;
        int lastindex = -1;
        
        float pas;

        float CurLength;
        float lastCurLength;
        
        Font _karaokeFont;
        Font m_font;   // used to measure strings without changing _karaokeFont
        float emSize;
        private StringFormat sf;

        private int _line = 0;
        private int _nbLyricsLines;
        private int _linedeb;
        private int _linefin;
        private int _lines;
        private int _lineHeight;
        private int _linesHeight;
        private string _biggestLine;

        public Form1()
        {           
            InitializeComponent();

            this.SetStyle(
                  System.Windows.Forms.ControlStyles.UserPaint |
                  System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
                  System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
                  true);

            timer1.Enabled = false;
            timer1.Interval = 10;  // 10 milliseconds
            // pas = 0.005F;
            pas = 0.01F;

            _lines = 5;
            _nbLyricsLines = 3;
            _linedeb = 0;
            SetLineFin(_linedeb);

            sf = new StringFormat(StringFormat.GenericTypographic) { FormatFlags = StringFormatFlags.MeasureTrailingSpaces };
            emSize = 40;
            _karaokeFont = new Font( "Comic Sans MS", 60, FontStyle.Regular, GraphicsUnit.Pixel);
            //_karaokeFont = new Font("Comic Sans MS", emSize, FontStyle.Regular, GraphicsUnit.Point);

            // Vertical distance between lines
            _lineHeight = (int)(1.5 * emSize);
            // Height of the full song
            _linesHeight = _nbLyricsLines * _lineHeight;


            // MeasureStr("Galappagos") = 306
            // 306 / 0.01 = 30600
            // Temps = 30600 * 10 = 306 000 millisecondes


            Init();
            
            // Biggest line
            _biggestLine = GetBiggestLine();
            AjustText(_biggestLine);
        }

        private void Init()
        {            
            string[] Line0 = { "Oh", " la", " la", " la", " vie", " en", " rose" };
            string[] Line1 = { "Le", " rose", " qu'on", " nous", " pro", "pose" };
            string[] Line2 = { "D'a", "voir", " les", " quan", "ti", "tés", " d'choses" };
            string[] Line3 = { "Qui", " donnent", " en", "vie", " d'au", "tres", " choses" };
            string[] Line4 = { "Aie", " on", " nous", " fait", " croire" };

            Lines.Add(Line0);
            Lines.Add(Line1);            
            Lines.Add(Line2);
            Lines.Add(Line3);
            Lines.Add(Line4);
            
            Lines.Add(new string[] { " " }); // Add a false line at the end

            Times = new List<long[]> (Lines.Count);            
            Texts = new string[Lines.Count];
            LinesLengths = new float[Lines.Count];

            long[] time;            
            string[] line;
            string Tx;
            int ticks = 0;
            

            for (int i = 0; i < Lines.Count; i++)
            {
                line = Lines[i];
                Tx = string.Empty;
                time = new long[line.Length];                                

                for (int j = 0; j < line.Length; j++) 
                { 
                    Tx += line[j];
                    ticks += 700;
                    time[j] = ticks;                                        
                }
                Texts[i] = Tx;
                LinesLengths[i] = MeasureLine(i);
                                               
                Times.Add(time);                        
            }                       
        }

        /// <summary>
        /// Search for biggest line
        /// </summary>
        /// <returns></returns>
        private string GetBiggestLine()
        {
            float max = 0;
            string maxline = string.Empty;
            float L = 0;
            for (int i = 0; i < Lines.Count; i++)
            {
                L = MeasureLine(i);

                // Search line having max characteres
                if (LinesLengths[i] > max)
                {
                    max = LinesLengths[i];
                    maxline = Texts[i];
                }              
            }
            return maxline;
        }

        /*
        private float MeasureStr(string s)
        {            
            Graphics g = pictureBox1.CreateGraphics();
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            g.PageUnit = GraphicsUnit.Pixel;            
            StringFormat sf = new StringFormat(StringFormat.GenericTypographic);            
            
            float res = 0;
            res = g.MeasureString(s, _karaokeFont, pictureBox1.Width, sf).Width;           

            g.Dispose();            
            return res;
        }
        */

        /// <summary>
        /// Measure the length of a string with a specific size
        /// </summary>
        /// <param name="line"></param>
        /// <param name="fSize"></param>
        /// <returns></returns>
        private float MeasureString(string line, float femSize)
        {
            float ret = 0;
            if (line != "")
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    g.TextRenderingHint = TextRenderingHint.AntiAlias;
                    g.PageUnit = GraphicsUnit.Pixel;

                    m_font = new Font(_karaokeFont.FontFamily, femSize, FontStyle.Regular, GraphicsUnit.Pixel);
                    SizeF sz = g.MeasureString(line, m_font, new Point(0, 0), sf);
                    ret = sz.Width;
                    g.Dispose();
                }
            }
            return ret;
        }

        /// <summary>
        /// Measure the height of a string
        /// </summary>
        /// <param name="line"></param>
        /// <param name="femSize"></param>
        /// <returns></returns>
        private float MeasureStringHeight(string line, float femSize)
        {
            float ret = 0;

            if (line != "")
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {

                    if (femSize > 0)
                        m_font = new Font(_karaokeFont.FontFamily, femSize, FontStyle.Regular, GraphicsUnit.Pixel);

                    SizeF sz = g.MeasureString(line, m_font, new Point(0, 0), sf);
                    ret = sz.Height;

                    g.Dispose();
                }
            }
            return ret;
        }

        private float MeasureLine(int curline)
        {                        
            float Sum = 0;
            for (int i = 0; i < Lines[curline].Length; i++)
            {
                //Sum += MeasureStr(Lines[curline][i]);
                Sum += MeasureString(Lines[curline][i], _karaokeFont.Size);
            }

            return Sum;
        }
       

        /// <summary>
        /// Calculate last line to display according to number of lines to display
        /// and number of lines remaining
        /// </summary>
        /// <param name="deb"></param>
        private void SetLineFin(int deb)
        {
            if (deb + _nbLyricsLines <= _lines)
                _linefin = deb + _nbLyricsLines - 1;
            else
                _linefin = _lines - 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _line = 0;
            percent = 0;
            lastpercent = 0;
            index = 0;
            lastindex = -1;
            lastCurLength = 0;
            CurLength = 0;            

            start = DateTime.Now;
            timer1.Start();
        }
       
        /// <summary>
        /// Retrieve which line for now
        /// </summary>
        /// <returns></returns>
        private int GetLine()
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                if (DateTime.Now < start.AddMilliseconds(Times[i][Times[i].Count() - 1]))
                {
                    return i;
                }
            }
            return 0;
        }


        /// <summary>
        /// Retrive index of current syllabe in the cuurent line
        /// </summary>
        /// <returns></returns>
        private int GetIndex()
        {
            // For each line of lyrics
            for (int j = 0; j < Lines.Count; j++) {
                                
                // Search for which timespamp is greater than now
                for (int i = 0; i < Times[_line].Length; i++)
                {                    
                    if (DateTime.Now < start.AddMilliseconds(Times[_line][i]))
                    {
                        return i + 1;                        
                    }
                }
            }
            return Lines.Count - 1;
        }

        /// <summary>
        /// Mesure length of a portion of line
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        private float GetCurLength(int idx)
        {
            float res = 0;
            for (int i = 0; i < idx; i++)
            {
                //res += MeasureStr(Lines[_line][i]);
                res += MeasureString(Lines[_line][i], _karaokeFont.Size);
            }
            return res;
        }

        /// <summary>
        /// Center text vertically
        /// </summary>
        /// <returns></returns>
        private int VCenterText()
        {            
            // Height of control minus height of lines to show
            int res = (pictureBox1.ClientSize.Height - (_nbLyricsLines + 1) * _lineHeight) / 2;            
            return res > 0 ? res : 0;
        }

        /// <summary>
        /// Center text horizontally
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private int HCenterText(string s)
        {
            //int res = (pictureBox1.ClientSize.Width - (int)MeasureStr(s)) / 2;
            int res = - (int)_karaokeFont.Size/2 + (pictureBox1.ClientSize.Width - (int)MeasureString(s, _karaokeFont.Size)) / 2;
            return res > 0 ? res : 0;
        }


        /// <summary>
        /// Things to do at eatch tick of timer1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // is it a new line ?
            if (DateTime.Now > start.AddMilliseconds(Times[_line][Times[_line].Count() - 1]))
            {
                if (_line < Lines.Count - 1)
                {
                    //Console.WriteLine("*** New line");
                    timer1.Stop();
                    percent = 0;
                    lastpercent = 0;
                    index = 0;
                    lastindex = -1;
                    lastCurLength = 0;
                    CurLength = 0;                    
                    _line = GetLine();

                    _linedeb = _line;
                    SetLineFin(_linedeb);
                    timer1.Start();

                }
                else
                {
                    // Is it the end of the text to display?
                    //Console.WriteLine("*** END");
                    timer1.Stop();

                    _line = 0;
                    _linedeb = _line;
                    SetLineFin(_linedeb);

                    percent = 0;
                    lastpercent = 0;
                    index = 0;
                    lastindex = -1;
                    lastCurLength = 0;
                    CurLength = 0;
                    pictureBox1.Invalidate();
                    return;
                }
            }

            // Search index of lyric to play
            index = GetIndex();
            
            // Length of partial line
            CurLength = GetCurLength(index);
           
            // New word to highlight
            if (index != lastindex)
            {
                // Save last value of percent
                lastpercent = percent;

                // Set new value of percent to the end of the previous word
                // And after that, add a small progressive increment in order to increase the percentage

                // |--- last word ---|--- new word --------------------------|
                //                   | percent => percent+pas => percent+pas
                
                percent = (lastCurLength / LinesLengths[_line]);
                
                lastCurLength = CurLength;
                lastindex = index;
                pictureBox1.Invalidate();
            }
            else
            {
                // if same index: progressive increase of percent
                percent += pas;

                if (percent > (CurLength / LinesLengths[_line]))
                {
                    percent = (CurLength / LinesLengths[_line]);                                        
                }
                pictureBox1.Invalidate();
            }            
        }
     
        /// <summary>
        /// Draw lines in the paint event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Antialiasing
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            e.Graphics.PageUnit = GraphicsUnit.Pixel;
            

            int y0 = VCenterText();
            int x0;

            // ======================================================================================================
            // 1. Draw and color all lines from _linedeb to _linefin in white
            // We want to display only a few number of lines (variable _nbLyricsLines = number of lines to display)  
            // ======================================================================================================
            var otherpath = new GraphicsPath();
            
            for (int i = _linedeb; i <= _linefin; i++)
            {
                x0 = HCenterText(Texts[i]);     // Center horizontally
                otherpath.AddString(Texts[i], _karaokeFont.FontFamily, (int)_karaokeFont.Style, _karaokeFont.Size, new Point(x0, y0 + (i - _linedeb) * _lineHeight), StringFormat.GenericDefault);   
            }
            e.Graphics.FillPath(new SolidBrush(Color.White), otherpath);

            // Borders of text
            e.Graphics.DrawPath(new Pen(Color.Black, 1), otherpath);

            otherpath.Dispose();


            // =============================================
            // 2. Color in green/Red the current line
            // =============================================
            // Create a graphical path
            var path = new GraphicsPath();

            // Add the full text line to the graphical path            
            x0 = HCenterText(Texts[_linedeb]);      // Center horizontally
            path.AddString(Texts[_linedeb], _karaokeFont.FontFamily, (int)_karaokeFont.Style, _karaokeFont.Size, new Point(x0, y0), StringFormat.GenericDefault);

            // Fill graphical path in white => full text is white
            e.Graphics.FillPath(new SolidBrush(Color.White), path);

            // ===================
            // Color in green syllabes before current syllabe
            // ===================
            // Create a region from the graphical path
            Region r = new Region(path);
            // Create a retangle of the graphical path
            RectangleF rect = r.GetBounds(e.Graphics);
            
            RectangleF intersectRectBefore = new RectangleF(rect.X, rect.Y, rect.Width * lastpercent, rect.Height);            

            // update region on the intersection between region and 2nd rectangle
            r.Intersect(intersectRectBefore);
            e.Graphics.FillRegion(Brushes.Green, r);


            // =======================
            // Color in green current syllabe
            // =======================
            r = new Region(path);
                                    
            // Create another rectangle shorter than the 1st one (percent of the first)                       
            RectangleF intersectRect = new RectangleF(rect.X + rect.Width * lastpercent, rect.Y, rect.Width * (percent - lastpercent), rect.Height);
            

            // update region on the intersection between region and 2nd rectangle
            r.Intersect(intersectRect);

            // Fill updated region in red => percent portion of text is red
            e.Graphics.FillRegion(Brushes.Red, r);

            // Borders of text
            e.Graphics.DrawPath(new Pen(Color.Black, 1), path);

            r.Dispose();
            path.Dispose();
        }

   
        /// <summary>
        /// Resize form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Resize(object sender, EventArgs e)
        {
            AjustText(_biggestLine);
            
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }


        /// <summary>
        /// Ajuste la taille de la fonte en fonction de la taille de pictureBox1
        /// </summary>
        /// <param name="S"></param>
        private void AjustText(string S)
        {
            if (S != "" && pictureBox1 != null)
            {
                Graphics g = pictureBox1.CreateGraphics();
                float femsize;

                //long inisize = (long)pictureBox1.Font.Size;
                long inisize = (long)_karaokeFont.Size;
                
                femsize = g.DpiX * inisize / 72;

                float textSize = MeasureString(S, femsize);
                
                // Try to fit inside 95% of client size
                long comp = (long)(0.95 * pictureBox1.ClientSize.Width);

                // Texte trop large
                if (textSize > comp)
                {
                    do
                    {
                        inisize--; 
                        if (inisize > 0)
                        {
                            femsize = g.DpiX * inisize / 72;
                            textSize = MeasureString(S, femsize);
                        }
                    } while (textSize > comp && inisize > 0);
                }
                else
                {
                    do
                    {
                        inisize++; //= inisize + 1;                        
                        femsize = g.DpiX * inisize / 72;
                        textSize = MeasureString(S, femsize);
                    } while (textSize < comp);
                }

                // ------------------------------
                // Ajustement in height 
                // ------------------------------
                float textHeight = MeasureStringHeight(S, inisize);
                float totaltextHeight;
                totaltextHeight = _nbLyricsLines * (textHeight + 10);
               
                long compHeight = (long)(0.95 * pictureBox1.ClientSize.Height);

                if (totaltextHeight > compHeight)
                {
                    do
                    {
                        inisize--; 
                        if (inisize > 0)
                        {
                            femsize = g.DpiY * inisize / 72;
                            textHeight = MeasureStringHeight(S, femsize);

                            totaltextHeight = _nbLyricsLines * (textHeight + 10);                            
                        }
                    } while (totaltextHeight > compHeight && inisize > 0);
                }


                if (inisize > 0)
                {
                    emSize = g.DpiY * inisize / 72;                    
                    _karaokeFont = new Font(_karaokeFont.FontFamily, emSize, FontStyle.Regular, GraphicsUnit.Pixel);

                    // Vertical distance between lines
                    _lineHeight = (int)emSize + 10;
                    // Height of the full song
                    _linesHeight = _nbLyricsLines * _lineHeight;


                    // Update horizontal measure of lines
                    for (int i = 0; i < Lines.Count; i++)
                    {
                        LinesLengths[i] = MeasureLine(i);
                    }

                }
                g.Dispose();
            }
        }

    }
}

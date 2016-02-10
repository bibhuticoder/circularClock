using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShutDownTimer
{
    public partial class animated : Form
    {
        public animated()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            timer.Start();
            setup();
        }

        Graphics g;
        Brush bM;
        Brush bH;
        Brush bS;
        int rH, rM, rS,lH,lM,lS,cH,cM,cS,sH,sM,sS = 0;
        DateTime currentTime;
        void setup()
        {

            setColor(Clock.Properties.Settings.Default.clockColor);

            //radius
            rH = 250;
            rM = 210;
            rS = 160;

            //location
            lH = 5;
            lM = 25;
            lS = 50;

            //circumference
            cH =Convert.ToInt32(2 * Math.PI * rH);
            cM = Convert.ToInt32(2 * Math.PI * rM);
            cS = Convert.ToInt32(2 * Math.PI * rS);

            //sweep angle
            sH = cH / 60;
            sM = cM / 60;
            sS = cS / 60;

            //fix time location
            lblTime.Location = new Point(this.Width / 2 - lblTime.Width / 2 +5, this.Height / 2 - lblTime.Height / 2 +5);

        }

        void setColor(string color)
        {

            if (color == "pink")
            {
                bH = Brushes.Crimson;
                bM = Brushes.HotPink;
                bS = Brushes.LightPink;
            }

            else if (color == "blue")
            {
                bH = Brushes.RoyalBlue;
                bM = Brushes.DodgerBlue;
                bS = Brushes.DeepSkyBlue;
            }

            else if (color == "tomato")
            {
                bH = Brushes.Tomato;
                bM = Brushes.Salmon;
                bS = Brushes.LightSalmon;
            }

            else if (color == "brown")
            {
                bH = Brushes.SaddleBrown;
                bM = Brushes.Chocolate;
                bS = Brushes.SandyBrown;
            }

            else if (color == "green")
            {
                bH = Brushes.ForestGreen;
                bM = Brushes.LimeGreen;
                bS = Brushes.LightGreen;
            }

            else if (color == "purple")
            {
                bH = Brushes.DarkOrchid;
                bM = Brushes.MediumOrchid;
                bS = Brushes.Plum;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            currentTime = DateTime.Now;
            sS = int.Parse(DateTime.Now.ToString("ss")) *6;
            sM = int.Parse(DateTime.Now.ToString("mm")) * 6;

            if (currentTime.ToString("tt") == "PM")
            {
                sH = int.Parse(DateTime.Now.ToString("hh")) * 50;
            }

            else 
            {
                sH = int.Parse(DateTime.Now.ToString("hh")) * 6;
            }



            lblTime.Text = currentTime.ToShortTimeString();

            this.Refresh();

        }

        private void animated_Paint(object sender, PaintEventArgs e)
        {
            //white borderrrrrrr
            e.Graphics.FillPie(Brushes.WhiteSmoke, new Rectangle(lH-5, lH-5, rH+10, rH+10), 0, 360);
           
            //hour
            e.Graphics.FillPie(Brushes.Silver, new Rectangle(lH, lH, rH, rH), 0, 360);//back
            e.Graphics.FillPie(bH, new Rectangle(lH,lH,rH,rH),0,sH);

            //minute
            e.Graphics.FillPie(Brushes.LightGray, new Rectangle(lM, lM, rM, rM), 0, 360);//back
            e.Graphics.FillPie(bM, new Rectangle(lM, lM, rM, rM), 0, sM);

            //second
            e.Graphics.FillPie(Brushes.Gainsboro, new Rectangle(lS, lS, rS, rS), 0, 360);//back
            e.Graphics.FillPie(bS, new Rectangle(lS, lS, rS, rS), 0, sS);


            //at centre
            e.Graphics.FillPie(Brushes.White, new Rectangle(lS + 10, lS + 10, rS - 20, rS - 20), 0, 360);
           
           
          
        }

        int x, y = 0;
        bool down = false;
        private void animated_MouseDown(object sender, MouseEventArgs e)
        {
            down = true;
            x = e.X;
            y = e.Y;
        }

        private void animated_MouseUp(object sender, MouseEventArgs e)
        {
            down = false;
        }

        private void animated_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
                this.Location = new Point(Cursor.Position.X - x, Cursor.Position.Y - y);
            }
        }

        private void pinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tt = (ToolStripMenuItem)sender;
            setColor(tt.Text.ToLower());
            Clock.Properties.Settings.Default.clockColor = tt.Text.ToLower();
            Clock.Properties.Settings.Default.Save();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showInDesktopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.TopMost = false;
        }
    }
}

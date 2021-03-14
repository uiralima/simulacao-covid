using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COVID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            int MAX_X = 200;//1000;
            int MAX_Y = 200;//750;
            List<Subject> subjects = new List<Subject>();
            using (var graph = this.CreateGraphics())
            {
                Random rand = new Random();
                for (int i = 0; i < 10000; i++)
                {
                    Subject p1 = new Subject(subjects, rand, graph, rand.Next(MAX_X), rand.Next(MAX_Y));
                    p1.Draw();
                    subjects.Add(p1);
                }
                for(int i = 0; i<20000; i++)
                {
                    foreach(var sub in subjects)
                    {
                        sub.WalkToDest();
                    }
                    textBox1.AppendText("\nCovid: " + subjects.Count(f => f.Covid).ToString());
                    this.Update();
                    //System.Threading.Thread.Sleep(3);
                }
            }
        }
    }
}

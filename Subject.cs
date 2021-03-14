using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COVID
{
    public class Subject
    {
        const int MAX_X = 1000;
        const int MAX_Y = 750;
        int _factor;
        System.Drawing.Graphics _formGraphics;
        Random _rand;
        bool _covid;
        List<Subject> _allSubjects;
        public Subject(List<Subject> allSubjects, Random rand, System.Drawing.Graphics formGraphics, int px, int py)
        {
            _rand = rand;
            _formGraphics = formGraphics;
            PX = px;
            PY = py;
            _factor = _rand.Next(8) + 1;
            _covid = _rand.Next(30) <= 3;
            _allSubjects = allSubjects;
        }
        public int PX { get; set; }
        public int PY { get; set; }
        public void Draw()
        {
            using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(_covid ? System.Drawing.Color.Red : System.Drawing.Color.Purple))
            {
                //Pen pen = new Pen(myBrush);
                //_formGraphics.DrawEllipse(pen, new RectangleF(PX, PY, 5, 5));
                _formGraphics.FillEllipse(myBrush, new RectangleF(PX, PY, 5, 5));
                //formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 10, 10));
            }
        }

        public bool Covid
        {
            get
            {
                return _covid;
            }
        }

        public void Clear()
        {
            using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                //Pen pen = new Pen(myBrush);
                //_formGraphics.DrawEllipse(pen, new RectangleF(PX, PY, 5, 5));
                _formGraphics.FillEllipse(myBrush, new RectangleF(PX, PY, 5, 5));
                //formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 10, 10));
            }
        }

        public void Walk()
        {
            int newPX = PX;
            int newPY = PY;
            int direction = (((_rand.Next(8)) + _factor) % 9) +1;
            switch(direction)
            {
                case 1: //NORTE
                    newPY -= 1;
                    _factor += 1;
                    break;
                case 2: //SUL
                    newPY += 1;
                    _factor += 8;
                    break;
                case 3: //OESTE
                    newPX -= 1;
                    _factor += 7;
                    break;
                case 4: //LESTE
                    newPX += 1;
                    _factor += 6;
                    break;
                case 5: //NOROESTE
                    newPY -= 1;
                    newPX -= 1;
                    _factor += 5;
                    break;
                case 6: //NORDESTE
                    newPY -= 1;
                    newPX += 1;
                    _factor += 4;
                    break;
                case 7: //SUDOESTE
                    newPY += 1;
                    newPX -= 1;
                    _factor += 3;
                    break;
                case 8: //SUDESTE
                    newPY += 1;
                    newPX += 1;
                    _factor += 2;
                    break;
            }
            Clear();
            if ((newPY >= 0) && (newPY <= MAX_Y))
            {
                PY = newPY;
            }
            if ((newPX >= 0) && (newPX <= MAX_X))
            {
                PX = newPX;
            }
            _covid = _covid || (_allSubjects.Any(f => ((f.PX >= PX-10) && (f.PX <= PX + 10)) && ((f.PY >= PY - 10) && (f.PY <= PY + 10)) && f.Covid));
            Draw();
        }
    }
}

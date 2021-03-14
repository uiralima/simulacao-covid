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
        const int MAX_X = 200;//1000;
        const int MAX_Y = 200;//750;
        int _factor;
        System.Drawing.Graphics _formGraphics;
        Random _rand;
        bool _covid;
        List<Subject> _allSubjects;
        const int mySize = 3;
        const int transmition = 2;

        public Subject(List<Subject> allSubjects, Random rand, System.Drawing.Graphics formGraphics, int px, int py)
        {
            _rand = rand;
            _formGraphics = formGraphics;
            PX = px;
            PY = py;
            _factor = _rand.Next(8) + 1;
            _covid = allSubjects.Count < 2;//_rand.Next(30) <= 3;
            _allSubjects = allSubjects;
            GetNewDest();
        }

        public int DestX { get; set; }
        public int DestY { get; set; }
        public int PX { get; set; }
        public int PY { get; set; }
        public void Draw()
        {
            using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(_covid ? System.Drawing.Color.Red : System.Drawing.Color.Purple))
            {
                //Pen pen = new Pen(myBrush);
                //_formGraphics.DrawEllipse(pen, new RectangleF(PX, PY, 5, 5));
                _formGraphics.FillEllipse(myBrush, new RectangleF(PX, PY, mySize, mySize));
                //formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 10, 10));
            }
        }

        public bool Covid
        {
            get
            {
                return _covid;
            }
            set
            {
                _covid = true;
            }
        }

        public void Clear()
        {
            using (System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White))
            {
                //Pen pen = new Pen(myBrush);
                //_formGraphics.DrawEllipse(pen, new RectangleF(PX, PY, 5, 5));
                _formGraphics.FillEllipse(myBrush, new RectangleF(PX, PY, mySize, mySize));
                //formGraphics.FillRectangle(myBrush, new Rectangle(0, 0, 10, 10));
            }
        }

        private void GetNewDest()
        {
            DestX = _rand.Next(MAX_X);
            DestY = _rand.Next(MAX_Y);
        }

        public void WalkToDest()
        {
            bool walk = false;
            int newPX = PX;
            int newPY = PY;
            if ((PX == DestX) && (PY == DestY))
            {
                GetNewDest();
            }
            if (DestX > PX)
            {
                newPX++;
                walk = true;
            }
            else if (DestX < PX)
            {
                newPX--;
                walk = true;
            }
            if (DestY > PY)
            {
                newPY++;
                walk = true;
            }
            else if (DestY < PY)
            {
                newPY--;
                walk = true;
            }
            if (!_covid)
            {
                _covid = _covid || (_allSubjects.Any(f => ((f.PX >= PX - transmition) && (f.PX <= PX + transmition)) && ((f.PY >= PY - transmition) && (f.PY <= PY + transmition)) && f.Covid));
            }
            else
            {
                _allSubjects.Where(f => ((f.PX >= PX - transmition) && (f.PX <= PX + transmition)) && ((f.PY >= PY - transmition) && (f.PY <= PY + transmition)) && !Covid).ToList().ForEach(f => f.Covid = true);
            }
            if (walk)
            {
                Clear();
                PX = newPX;
                PY = newPY;
                Draw();
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

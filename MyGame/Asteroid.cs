using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Asteroid:BaseObject
    {
        public int Power { get; set; }
        public Asteroid(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            Power = 1;
        }
        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Properties.Resources.astr1, pos.X, pos.Y, size.Width, size.Height);
        }
        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y + dir.Y;
            if (pos.X < 0) pos.X = dir.X = -dir.X;
            if (pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > Game.Height) dir.Y = -dir.Y;
        }
    }
}

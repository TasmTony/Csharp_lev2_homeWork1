using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class DrString: BaseObject
    {
        public DrString(Point pos, Point dir) : base(pos, dir)
        {
        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawString("*", SystemFonts.DefaultFont, Brushes.Azure, pos.X, pos.Y);
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            if (pos.X < 0) pos.X = Game.Width + size.Width;
        }
    }
}

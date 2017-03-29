using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    class Medical : BaseObject 
    {
        
        
        public Medical(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            
        }
        public override void Draw()
        {
            try
            {
                Game.buffer.Graphics.DrawImage(Properties.Resources.medic, pos.X, pos.Y, size.Width, size.Height);
            }
            catch(ArgumentNullException ex)
            {
                Game.buffer.Graphics.FillEllipse(Brushes.Wheat, pos.X, pos.Y, size.Width, size.Height);
            }
            catch(Exception ex)
            {
                Game.buffer.Graphics.FillEllipse(Brushes.Wheat, pos.X, pos.Y, size.Width, size.Height);
            }
        }

        public override void Update()
        {
            pos.X = pos.X + dir.X;
            pos.Y = pos.Y +dir.Y;
            if (pos.X < 0) pos.X = dir.X = -dir.X;
            if (pos.X > Game.Width) dir.X = -dir.X;
            if (pos.Y < 0) dir.Y = -dir.Y;
            if (pos.Y > Game.Height) dir.Y = -dir.Y;
        }
              

    }
    }

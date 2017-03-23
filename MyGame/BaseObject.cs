using System;
using System.Drawing;

namespace MyGame
{
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
    abstract class BaseObject:ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        public BaseObject(Point pos, Point dir, Size size)
        {
            this.pos = pos;
            this.dir = dir;
            this.size = size;
        }
        public BaseObject(Point pos, Point dir)
        {
            this.pos = pos;
            this.dir = dir;            
        }
        abstract public void Draw();
        //        {
        //            //Game.buffer.Graphics.DrawEllipse(Pens.White, pos.X, pos.Y,size.Width,size.Height);
        //            /*
        //2            *Заменить кружочки картинками, используя метод DrawImage.
        //            */            
        //            //Game.buffer.Graphics.DrawImage(Image.FromFile("astr.jpg"),pos.X,pos.Y,size.Width,size.Height);
        //            Game.buffer.Graphics.DrawImage(Properties.Resources.astr1, pos.X, pos.Y, size.Width, size.Height);

        //        }
        abstract public void Update();
        //{
        //    pos.X =pos.X+ dir.X;
        //    //pos.Y = pos.Y + dir.Y;
        //    if (pos.X < 0) pos.X =Game.Width + size.Width;
        //    //if (pos.X > Game.Width) dir.X = -dir.X;
        //    //if (pos.Y < 0) dir.Y = -dir.Y;
        //    //if (pos.Y > Game.Height) dir.Y = -dir.Y;
        //}


        public bool Collision(ICollision o)
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        public Rectangle Rect
        {
            get { return new Rectangle(pos, size); }
        }
    }
}

using System;
using System.Drawing;


namespace MyGame
{
    /// <summary>
    /// Класс описывающий прорисовку и поведение астероидов
    /// </summary>
    class Asteroid:BaseObject
    {
      
        public Asteroid(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            
        }
        public override void Draw()
        {
            Game.buffer.Graphics.DrawImage(Properties.Resources.astr1, pos.X, pos.Y, size.Width, size.Height); //Астероид будет картинкой занесенной в ресурсы
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

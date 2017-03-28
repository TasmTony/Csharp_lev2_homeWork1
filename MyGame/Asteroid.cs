using System;
using System.Drawing;


namespace MyGame
{
    public interface IComparable<T>
    {
        int CompareTo(T obj);
    }
    /// <summary>
    /// Класс описывающий прорисовку и поведение астероидов
    /// </summary>
    class Asteroid:BaseObject,IComparable<Asteroid>
    {
        public int Power { get; set; } = 3;
      
        public Asteroid(Point pos, Point dir, Size size): base(pos,dir,size)
        {
            Power = Game.rnd.Next(1, 4);
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

        int IComparable<Asteroid>.CompareTo(Asteroid obj)
        {
            if (this.Power > obj.Power)
                return 1;
            if (this.Power < obj.Power)
                return -1;
            else
                return 0;
        }
    }
}

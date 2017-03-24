using System;
using System.Drawing;


namespace MyGame
{
    /// <summary>
    /// Класс для описания снарядов
    /// </summary>
    class Bullet: BaseObject
    {
        public Bullet(Point pos,Point dir,Size size): base(pos,dir,size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X, pos.Y, size.Width, size.Height);
        }
        public override void Update()
        {
            pos.X += 3;
            if (pos.X > Game.Width)
                throw new GameObjectException("Снаряд покинул игровое поле((");
        }
    }
}

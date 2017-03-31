using System;
using System.Drawing;


namespace MyGame
{
    /// <summary>
    /// Класс для описания снарядов
    /// </summary>
    class Bullet: BaseObject
    {
        /// <summary>
        /// Событие - вылет снаряда за пределы поля
        /// </summary>
        public static event Action<Bullet> DieBullet;
        public Bullet(Point pos,Point dir,Size size): base(pos,dir,size)
        {

        }

        public override void Draw()
        {
            Game.buffer.Graphics.DrawRectangle(Pens.OrangeRed, pos.X, pos.Y, size.Width, size.Height);
        }
        public override void Update()
        {
            pos.X += 10;                        
        }
        /// <summary>
        /// Метод проверки вылета снаряда за пределы поля возвращает true если снаряд вылетел
        /// за пределы игрового поля и был уничтожен.
        /// </summary>
        /// <returns></returns>
        public bool Die()
        {
            if (pos.X > Game.Width && DieBullet != null)                             
            {
                DieBullet(this);
                return  true;                
            }
            else
                return false;
        }
    }
}

using System;
using System.Drawing;

namespace MyGame
{
    /// <summary>
    /// Интерфейс для обработки столкновений
    /// </summary>
    interface ICollision 
    {
        bool Collision(ICollision obj);
        Rectangle Rect { get; }
    }
    /// <summary>
    /// Абстрактный класс описания всех игровых объектов
    /// </summary>
    abstract class BaseObject:ICollision
    {
        protected Point pos;
        protected Point dir;
        protected Size size;
        public BaseObject(Point pos, Point dir, Size size)
        {
            if (pos.X < 0 || pos.Y < 0)
                throw new GameObjectException("Неверные координаты"); //При отрицательных координатай вылетит исключение.
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
        abstract public void Update();            
                
        public bool Collision(ICollision o)//метод определения столкновения объектов
        {
            return o.Rect.IntersectsWith(this.Rect);
        }

        public Rectangle Rect
        {
            get { return new Rectangle(pos, size); }
        }
    }
}

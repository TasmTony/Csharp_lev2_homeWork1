using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    /// <summary>
    /// Класс описывающий объект - корабль
    /// </summary>
    class Ship:BaseObject
    {
        public static event Message MessageDie; //Событие уничтожение корабля
        public static event LogOut ShipDemagLog;//Событие - повреждение корабля

        int energy = 100; 

        public int Energy
        {
            get { return energy; }
        }

        public void EnergyLow(int n)
        {
            energy -= n;
        }

        public Ship(Point pos,Point dir,Size size):base(pos,dir,size)
        {

        }

        public override void Draw()
        {
           // Game.buffer.Graphics.FillEllipse(Brushes.Wheat, pos.X, pos.Y, size.Width, size.Height);
            Game.buffer.Graphics.DrawImage(Properties.Resources.ship1, pos.X, pos.Y, size.Width, size.Height); //С картинкой интересней))
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (pos.Y > 0) pos.Y = pos.Y - dir.Y;
        }

        public void Down()
        {
            if (pos.Y < Game.Height) pos.Y = pos.Y + dir.Y;
        }
        /// <summary>
        /// Метод обработки попадания по кораблю
        /// </summary>        
        public void Demag(int dem) 
        {
            if (ShipDemagLog != null) ShipDemagLog($"Урон по кораблю {dem}%;");
        }
        /// <summary>
        /// Метод обработки уничтожения корабля
        /// </summary>
        public void Die()
        {
            if (ShipDemagLog != null) ShipDemagLog($"Корабль уничтожен! Game Over!!!");
            if (MessageDie != null) MessageDie();

        }

    }
}

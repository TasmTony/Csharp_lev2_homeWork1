/*
Голиков Антон павлович

ДЗ 2го урока

2. Переделать виртуальный метод Update в BaseObject в абстрактный и реализовать его в
наследниках.
3. Сделать так, чтобы при столкновениях пули с астероидом пуля и астероид регенерировались в
разных концах экрана;
4. Сделать проверку на задание размера экрана в классе Game. Если высота или ширина больше
1000 или принимает отрицательное значение, то выбросить исключение
ArgumentOutOfRangeException().
5. *Создать собственное исключение GameObjectException, которое появляется при попытке
создать объект с неправильными характеристиками (например, отрицательные размеры,
слишком большая скорость или позиция).


*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace MyGame
{
    class Game//Не совсем понял, что нужно сделать в 3м задании, поэтому оставил имя класса неизмененным.
    {
        static System.Drawing.BufferedGraphicsContext context;
        static public BufferedGraphics buffer;
        static BaseObject[] objs;
        static Bullet bullet;
        //static List<Bullet> bullets = new List<Bullet>();
        static Asteroid[] asteroids;
        static private Timer timer;
        static Score score;
        static public Random rnd = new Random();
        static Ship ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(25, 15));
        static LogForm log = new LogForm(); //Создадим форму для ведения журнала
        // Свойства
        // Ширина и высота игрового поля
        static public int Width { get; set; }
        static public int Height { get; set; }
        static public int ScoreGame { get; set; }
        static Game()
        {
        }
        static public void Init(Form form)
        {

            //Проверим размеры формы 
            if (form.Width > 1000 || form.Width <= 0 || form.Height <= 0 || form.Height > 1000) 
                throw new ArgumentOutOfRangeException("Не верный размер игрового поля"); //Если размеры не верны, сгенерируем ошибку
            // Графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            ScoreGame = 0; //Счетчик очков в 0
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            string s = $"Урон по кораблю %;";
            Load();
            form.FormClosing += Form_Closing;
            form.KeyDown += Form_KeyDown;
            timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;
            Ship.MessageDie += Finish; //Подписываемся на событие уничтожение корабля
            Ship.ShipDemagLog += LogEvent;//Подписываемся на событие запись в журнал о попадании по кораблю
            Asteroid.AsterDemagLog += LogEvent;//Подписываемся на событие запись в журнал о попадании по астероиду

            log.Show(); //Отображаем форму для журнала
            log.Location = new Point(form.Location.X + form.Width, form.Location.Y); //Сдвигаем журнал к правому краю игровой формы
            log.StrLog =DateTime.Now.ToLongTimeString() + " Старт!!!"; //Запишем в Журнал 1ю запись.

        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) bullet = new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
            if (e.KeyCode == Keys.Escape) //Добавим проверку на нажатие Esc.
            {
                timer.Stop();
                if (MessageBox.Show("Close?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Form f = Application.OpenForms[1]; //переходим к основной форме и закрываем ее
                    f.Close();
                }
                else
                    timer.Start();
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        private static void Form_Closing(object sender, FormClosingEventArgs e) //Обработка закрытия формы
        {
            timer.Stop();
            if (MessageBox.Show("Close?", "Exit", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Form f1 = Application.OpenForms[0]; //переходим к основной форме и закрываем ее
                f1.Close();
            }
            else
            {
                e.Cancel = true;
                timer.Start();
            }
            
        }
        static public void Draw()
        {
            //buffer.Graphics.Clear(Color.Black);
            //bullet.Draw();
            //foreach (BaseObject obj in objs)
            //    obj.Draw();
            //foreach (Asteroid obj in asteroids)
            //    obj.Draw();
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid a in asteroids)
                if (a != null) a.Draw();
            if (bullet != null) bullet.Draw();
            ship.Draw();
            buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            buffer.Graphics.DrawString($"Score: " + ScoreGame, SystemFonts.CaptionFont, Brushes.Red, 0, 10);
            buffer.Render();
            //score.Draw();
            //buffer.Render();
            //ship.Draw();


        }

        static public void Load()
        {
            Random r = new Random(); //Добавил рандомности первоначальным объектам.
            objs = new BaseObject[30];

           // score = new Score(new Point(3, 3)); //инициализируем счетчик очков  
            bullet = new Bullet(new Point(0, r.Next(1, Height)), new Point(5, 0), new Size(4, 1));
            asteroids = new Asteroid[4];
            for (int i = 0; i < objs.Length; i += 2) //Заполняем массив звездами с учетом размеров формы
            {
                int j = r.Next(1, 30);
                objs[i] = new Star(new Point(r.Next(1, Width), i * Height / 30), new Point(-j, 0), new Size(3, 3));
                j = r.Next(1, 30);
                objs[i + 1] = new DrString(new Point(r.Next(1, Width), (i + 1) * Height / 30), new Point(-j, -j));
            }
            for (int i = 0; i < asteroids.Length; i++)  //Заполняем массив астероидов с учетом размеров формы
            {
                int j = r.Next(1, 15);
                asteroids[i] = new Asteroid(new Point(r.Next(0, Width), i * 20), new Point(-j, -j), new Size(20, 20));
            }
        }
         /// <summary>
         /// Обработка записи в журнал
         /// </summary>
         /// <param name="strEvent"> сообщение, записываемое в журнал</param>
        static public void LogEvent(string strEvent) 
        {
            log.StrLog = DateTime.Now.ToLongTimeString() + strEvent;
        }             
 
        static public void Update()
        {
            
            foreach (BaseObject obj in objs)
                obj.Update();
            if (bullet != null) bullet.Update();
            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] != null)
                {
                    asteroids[i].Update();
                    if (bullet != null && bullet.Collision(asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();

                        if (--asteroids[i].Power == 0)
                        {
                            asteroids[i].Die();
                            asteroids[i] = null;
                            ScoreGame++;
                        }
                        else
                            asteroids[i].Demag(asteroids[i].Power);
                                                
                        bullet = null;
                        continue;
                    }
                    if (ship.Collision(asteroids[i]))
                    {
                        int d = rnd.Next(1, 10);
                        ship.EnergyLow(d);
                        ship.Demag(d);
                        System.Media.SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0) ship.Die();
                    }
                }
            }
        
        //int i = 0;
        //foreach (Asteroid obj in asteroids) //Пробегаем по астероидам и проверяем их на столкновение со снарядом
        //{
        //    obj.Update();
        //    if (obj.Collision(bullet))
        //    {
        //        ScoreGame++; //увеличим счетчик очков
        //        Random r = new Random();
        //        System.Media.SystemSounds.Hand.Play(); //Воспроизводим звук при столкновении
        //        int j = r.Next(1, 30);
        //        asteroids[i]=new Asteroid(new Point(r.Next(0, Width), r.Next(1,Height)), new Point(-j, -j), new Size(20, 20)); //генерируем новый астероид
        //        bullet = new Bullet(new Point(0, r.Next(1,Width)), new Point(5, 0), new Size(4, 1)); //генерируем новый снаряд
        //    }
        //    i++;

        //}
        //try
        //{
        //    bullet.Update();
        //}
        //catch(GameObjectException ex) //Если снаряд улетит за пределы поля, сгенерируется исключение
        //{
        //    timer.Stop(); //Остановим таймер
        //    MessageBox.Show(ex.Message,"Pause!!!"); //Выдадим сообщение об исключении
        //    Random r = new Random();
        //    bullet = new Bullet(new Point(0, r.Next(1, Width)), new Point(5, 0), new Size(4, 1)); //генерируем новый снаряд
        //    timer.Start(); //Продолжаем игру
        //} 
        }
        static public void Finish()
        {
            timer.Stop();
            log.StrLog = "Конец";    
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
           Brushes.White, 200, 100);
            buffer.Render();
        }
    }
}

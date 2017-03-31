﻿/*
Голиков Антон павлович

ДЗ 3го урока
1. а) Добавить в игру “Астероиды” ведение журнала в консоль
б)*и в файл.
2. Добавьте аптечки, которые добавляют энергии.
3. Добавить подсчет очков за сбитые астероиды.

*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;



namespace MyGame
{
    class Game
    {
        static BufferedGraphicsContext context;
        static public BufferedGraphics buffer;
        static BaseObject[] objs;
        static Bullet bullet;
        static Medical med;
        static List<Bullet> bullets = new List<Bullet>();
        static int Level = 1;
        static List<Asteroid> asteroids = new List<Asteroid>();
        static private Timer timer;       
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
            LogEvent(" Старт!!!"); //Запишем в Журнал 1ю запись.         

        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) bullets.Add(new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1)));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
            if (e.KeyCode == Keys.Escape) //Добавим проверку на нажатие Esc.
            {
                Form f = Application.OpenForms[1]; //переходим к основной форме и закрываем ее
                f.Close();
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
                LogEvent(" Выход из игры. Конечный счет: "+ScoreGame); //Запишем в журнал 
                Form f = Application.OpenForms[0]; //переходим к основной форме и закрываем ее
                f.Close();
            }
            else
            {
                e.Cancel = true;
                timer.Start();
            }
            
        }

        static public void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)  obj.Draw();
            foreach (Asteroid a in asteroids) a.Draw();
            foreach (Bullet b in bullets) b.Draw();
            ship.Draw();
            med.Draw();
            buffer.Graphics.DrawString($"LEVEL: " + Level, new Font(FontFamily.GenericSansSerif, 10), Brushes.Red, Width-100, 0);
            buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);
            //Отображение счета
            buffer.Graphics.DrawString($"Score: " + ScoreGame, SystemFonts.CaptionFont, Brushes.Red, 0, 10);
            buffer.Render();
        }

        static public void Load()
        {            
            objs = new BaseObject[30];
            med = new Medical(new Point(rnd.Next(0, Width), rnd.Next(1, Height)), new Point(rnd.Next(-10, 1), rnd.Next(-15, 5)), new Size(20, 20));
            //Заполняем массив звездами с учетом размеров формы
            for (int i = 0; i < objs.Length; i += 2) 
            {
                int j = rnd.Next(1, 30);
                objs[i] = new Star(new Point(rnd.Next(1, Width), i * Height / 30), new Point(-j, 0), new Size(3, 3));
                j = rnd.Next(1, 30);
                objs[i + 1] = new DrString(new Point(rnd.Next(1, Width), (i + 1) * Height / 30), new Point(-j, -j));
            }
            //Заполняем коллекцию астероидов новыми объектами
            NewAsteroids();
            
        }
        /// <summary>
        /// Метод заполнения коллекции новыми астероидами
        /// </summary>
        static public void NewAsteroids()
        {
            for (int i = 0; i < Level + 3; i++)  //Заполняем массив астероидов с учетом размеров формы
            {
                int j = rnd.Next(1, 15);
                asteroids.Add(new Asteroid(new Point(rnd.Next(0, Width), i * 20), new Point(-j, -j), new Size(20, 20)));
            }
        }
         /// <summary>
         /// Обработка записи в журнал
         /// </summary>
         /// <param name="strEvent"> сообщение, записываемое в журнал</param>
        static public void LogEvent(string strEvent) 
        {
            //Запись в файл
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter("log.txt", true))
                {
                    file.WriteLine(DateTime.Now.ToString() + strEvent); 
                }
            }
            catch (Exception ex)
            {
                log.StrLog = DateTime.Now.ToLongTimeString() + "Ошибка записи в файл!";
                log.StrLog = ex.Message;
            }
            //вывод в форму журнала
            log.StrLog = DateTime.Now.ToLongTimeString() + strEvent;
        }             
 
        static public void Update()
        {
            
            foreach (BaseObject obj in objs) obj.Update();
            foreach (Bullet b in bullets) b.Update();
            if (med != null) med.Update();
            //Если поймаем аптечку и энергия корабля меньше 100%, то полечим корабль и создадим новую аптечку
            if (ship.Collision(med)&&ship.Energy<100)
            {
                int hill = rnd.Next(1, 10);
                ship.Hill(hill);
                med = new Medical(new Point(rnd.Next(0, Width), rnd.Next(1, Height)), new Point(rnd.Next(-10, 1), rnd.Next(-15, 5)), new Size(20, 20));
            }
            for (int i = 0; i < asteroids.Count; i++)
            {
                    asteroids[i].Update();
                    for (int j=0; j<bullets.Count; j++)
                    if (i>=0 && bullets[j].Collision(asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        bullets.RemoveAt(j);
                        j--;
                        //Если мощность астероида после попадания равна 0, то уничтожим его и добавим очко
                        if (--asteroids[i].Power == 0)
                        {
                            asteroids[i].Die();
                            asteroids.RemoveAt(i); //Удаляем объект из коллекции
                            i--;
                            ScoreGame++;
                            if (asteroids.Count==0)
                            {
                                Level++;
                                NewAsteroids();
                                //i = 0;
                                //j = -1;
                            }
                        }
                        else
                            asteroids[i].Demag(asteroids[i].Power); //иначе только занесем запись в журнал.                                                
                    }
                    if ( i>=0 && ship.Collision(asteroids[i]))
                    {
                        int d = rnd.Next(1, 10);                        
                        ship.Demag(d);
                        System.Media.SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0) ship.Die();
                    }
                }
            
        }
        static public void Finish()
        {
            timer.Stop();                
            buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline),
           Brushes.White, 200, 100);
            buffer.Render();
        }
    }
}

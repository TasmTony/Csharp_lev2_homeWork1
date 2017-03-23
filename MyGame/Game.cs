﻿/*
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
        static Asteroid[] asteroids;
        // Свойства
        // Ширина и высота игрового поля
        static public int Width { get; set; }
        static public int Height { get; set; }
        static Game()
        {
        }
        static public void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // предоставляет доступ к главному буферу графического контекста для текущего приложения
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();// Создаём объект - поверхность рисования и связываем его с формой
                                      // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            // Связываем буфер в памяти с графическим объектом.
            // для того, чтобы рисовать в буфере
            buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;

        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in objs)
                obj.Draw();
            buffer.Render();


        }

        static public void Load()
        {
            Random r = new Random(); //Добавил рандомности первоначальным объектам.
            objs = new BaseObject[30];
            bullet = new Bullet(new Point(0, 200), new Point(5, 0), new Size(4, 1));
            asteroids = new Asteroid[4];
            for (int i = 0; i < objs.Length; i += 2)
            {                
                int j = r.Next(1, 30);
                objs[i+1] = new Star(new Point(r.Next(300, 600), (i+1) * 20), new Point(-j, 0), new Size(3, 3));
                j = r.Next(1, 30);
                objs[i + 2] = new DrString(new Point(r.Next(1,300), (i + 2) * 20), new Point(-j ,-j ));
            }
            for (int i = 0; i<asteroids.Length;i++)
            {
                int j = r.Next(1, 15);
                asteroids[i] = new Asteroid(new Point(r.Next(0, 600), i * 20), new Point(-j, -j), new Size(20, 20));
            }
                
        }
        static public void Update()
        {
            foreach (BaseObject obj in objs)
                obj.Update();
        }

    }
}

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
        static private Timer timer;
        static Score score;
        static public Random rnd = new Random();
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

            Load();
            form.FormClosed += Form_Closed;
            timer = new Timer();
            timer.Interval = 100;
            timer.Start();
            timer.Tick += Timer_Tick;
            
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        private static void Form_Closed(object sender, EventArgs e) //Обработка закрытия формы
        {
            
            Form f = Application.OpenForms[0]; //переходим к основной форме и закрываем ее
            f.Close();
        }
        static public void Draw()
        {
            buffer.Graphics.Clear(Color.Black);
            bullet.Draw();
            foreach (BaseObject obj in objs)
                obj.Draw();
            foreach (Asteroid obj in asteroids)
                obj.Draw();
            score.Draw();
            buffer.Render();


        }

        static public void Load()
        {
            Random r = new Random(); //Добавил рандомности первоначальным объектам.
            objs = new BaseObject[30];

            score = new Score(new Point(3, 3)); //инициализируем счетчик очков 
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
                      
 
        static public void Update()
        {
            
            foreach (BaseObject obj in objs)
                obj.Update();
            int i = 0;
            foreach (Asteroid obj in asteroids) //Пробегаем по астероидам и проверяем их на столкновение со снарядом
            {
                obj.Update();
                if (obj.Collision(bullet))
                {
                    ScoreGame++; //увеличим счетчик очков
                    Random r = new Random();
                    System.Media.SystemSounds.Hand.Play(); //Воспроизводим звук при столкновении
                    int j = r.Next(1, 30);
                    asteroids[i]=new Asteroid(new Point(r.Next(0, Width), r.Next(1,Height)), new Point(-j, -j), new Size(20, 20)); //генерируем новый астероид
                    bullet = new Bullet(new Point(0, r.Next(1,Width)), new Point(5, 0), new Size(4, 1)); //генерируем новый снаряд
                }
                i++;
                    
            }
            try
            {
                bullet.Update();
            }
            catch(GameObjectException ex) //Если снаряд улетит за пределы поля, сгенерируется исключение
            {
                timer.Stop(); //Остановим таймер
                MessageBox.Show(ex.Message,"Pause!!!"); //Выдадим сообщение об исключении
                Random r = new Random();
                bullet = new Bullet(new Point(0, r.Next(1, Width)), new Point(5, 0), new Size(4, 1)); //генерируем новый снаряд
                timer.Start(); //Продолжаем игру
            } 
        }

    }
}

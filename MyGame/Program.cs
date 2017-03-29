
/*
Голиков Антон павлович

ДЗ 3го урока
1. а) Добавить в игру “Астероиды” ведение журнала в консоль
б)*и в файл.
2. Добавьте аптечки, которые добавляют энергии.
3. Добавить подсчет очков за сбитые астероиды.

*/
using System;
using System.Windows.Forms;

namespace MyGame
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StartForm startForm = new StartForm();     //Выводим форму запроса размеров       
            Application.Run(startForm);
        }
    }
}

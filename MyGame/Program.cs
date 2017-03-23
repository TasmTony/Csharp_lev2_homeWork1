﻿
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
using System.Linq;
using System.Threading.Tasks;
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
            StartForm startForm = new StartForm();            
            Application.Run(startForm);
        }
    }
}

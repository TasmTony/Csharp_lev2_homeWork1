/*
Голиков Антон Павлович
ДЗ 2го занятия

1.Построить три класса (базовый и 2 потомка), описывающих некоторых работников с
почасовой оплатой (один из потомков) и фиксированной оплатой (второй потомок).
а) Описать в базовом классе абстрактный метод для расчёта среднемесячной заработной
платы. Для «повременщиков» формула для расчета такова: «среднемесячная заработная плата
= 20.8 * 8 * почасовую ставку», для работников с фиксированной оплатой «среднемесячная
заработная плата = фиксированной месячной оплате».
б) Создать на базе абстрактного класса массив сотрудников и заполнить его.
в) **Реализовать интерфейсы для возможности сортировки массива используя Array.Sort().
г) ***Реализовать возможность вывода данных с использованием foreach.
*/
using System;


namespace HW2_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerBase[] list = new WorkerBase[4];
         
            list[0] = new WorkerFixSalary("Иванов", "Иван", 15000);
            list[1] = new WorkerTimeSalary("Петров", "Петр", 100, 176);
            list[2] = new WorkerFixSalary("Сидоров", "Иван", 16000);
            list[3] = new WorkerTimeSalary("Коровьев", "Степан", 65, 176);

            Console.WriteLine("Исходный массив с средней з/п");

            foreach (var l in list)
            {
                Console.WriteLine($""+l.ToString()+" AverSal="+l.AverSalary()); //выводим информацию о сотруднике и его среднюю зарплату
            }

            Array.Sort(list);

            Console.WriteLine("После сортировки:");

            foreach (var l in list)
            {
                Console.WriteLine($"" + l.ToString() + " AverSal=" + l.AverSalary());
            }

            Console.WriteLine("Вывод через foreach:");

            foreach (var l in list[0])
            {
                Console.WriteLine(l);
            }


            Console.ReadKey();
        }
    }
}

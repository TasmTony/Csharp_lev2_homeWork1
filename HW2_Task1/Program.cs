using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkerBase[] list = new WorkerBase[3];
            //for (int i=0;i<3; i++)
            list[0] = new WorkerFixSalary("Иванов", "Иван", 15000);
            list[1] = new WorkerTimeSalary("Иванов", "Иван", 100, 176);
            list[2] = new WorkerFixSalary("Иванов", "Иван", 15000);
            for (int i = 0; i < 3; i++)
                Console.WriteLine(list[i].ToString());
            Console.ReadKey();
        }
    }
}

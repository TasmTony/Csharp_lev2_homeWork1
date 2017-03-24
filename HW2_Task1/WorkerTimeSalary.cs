using System;
using System.Collections;

namespace HW2_Task1
{
    /// <summary>
    /// Класс сотрудников с повременной оплатой труда
    /// </summary>
    class WorkerTimeSalary:WorkerBase
    {
        public int TimeWork { get; set; }    //Свойство для хранения отработанного времени
        public double TimeSalary { get; set; }//Свойство для хранения часовой ставки

        public WorkerTimeSalary(string _Fname, string _Lname, double _TimeSalary,int _TimeWork):base (_Fname,_Lname,_TimeSalary*_TimeWork) 
        {
            TimeWork = _TimeWork;
            TimeSalary = _TimeSalary;
        }

        public override double AverSalary() //метод расчета среднемесячной з\п
        {
            return TimeSalary*20.8*8;
        }

        public override IEnumerator GetEnumerator() //Переопределил функцию для вывода через форейч
        {
            yield return Fname;
            yield return Lname;
            yield return Salary;
            yield return TimeWork;
            yield return TimeSalary;
        }
    }
}

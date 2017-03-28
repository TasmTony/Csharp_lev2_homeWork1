using System;
using System.Collections;


namespace HW2_Task1
{
    /// <summary>
    /// Базовый абстрактный класс работников
    /// </summary>
    abstract class WorkerBase:IComparable,IEnumerable
    {
        public string Fname 
        {
            get; set;
        }
        public string Lname
        {
            get; set;
        }
        public double Salary
        {
            get; set;
        }

        public WorkerBase(string _Fname, string _Lname, double _salary)
        {
            Fname = _Fname;
            Lname = _Lname;
            Salary = _salary;
        }

        abstract public double AverSalary();// абстр. метод расчета среднемесячной з\п

        //реализация интерфейса сравнения объектов (по зарплате)
        public int CompareTo(object obj)
        {
            if (Salary > (obj as WorkerBase).Salary) return 1;
            else if (Salary == (obj as WorkerBase).Salary) return 0;
            else return -1;
        }
        public override string ToString()
        {
            return $" " + Fname + " " + Lname + " " + Salary+" RUR";
        }
        //реализация интерфейса перечеслителя для вывода полей через Форейч
        public virtual IEnumerator GetEnumerator()
        {
            yield return Fname;
            yield return Lname;
            yield return Salary;
        }

   
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Task1
{
    class WorkerTimeSalary:WorkerBase
    {
        private string Fname { get; set; }
        private string Lname { get; set; }
        private int TimeWork { get; set; }
        private double Salary { get; set; }
        private double MonthSalary
        {
            get { return Salary * TimeWork; }
        }

        public WorkerTimeSalary(string _Fname, string _Lname, double _salary,int _TimeWork):base (_Fname,_Lname,_salary)
        {
            TimeWork = _TimeWork;
        }

        public override double AverSalary()
        {
            return Salary*20.8*8;
        }
        public override string ToString()
        {
            return $" "+Fname+" "+Lname+" "+MonthSalary + " RUR";
        }
    }
}

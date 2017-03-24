using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Task1
{
    class WorkerFixSalary:WorkerBase
    {
        private string Fname
        {
            get; set;
        }
        private string Lname
        {
            get; set;
        }
        private double Salary
        {
            get; set;
        }
        public WorkerFixSalary(string _Fname, string _Lname, double _salary):base (_Fname,_Lname,_salary)
        {

        }

        public override double AverSalary()
        {
            return Salary;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Task1
{
    abstract class WorkerBase
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

        abstract public double AverSalary();
        public override string ToString()
        {
            return $" " + Fname + " " + Lname + " " + Salary+" RUR";
        }
    }
}

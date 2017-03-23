using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Task1
{
    abstract class WorkerBase
    {
        private string Fname;
        private string Lname;
        private int Salary;

        public WorkerBase(string _Fname, string _Lname, int _salary)
        {
            Fname = _Fname;
            Lname = _Lname;
            Salary = _salary;
        }


    }
}

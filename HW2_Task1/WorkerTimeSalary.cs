using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW2_Task1
{
    class WorkerTimeSalary:WorkerBase
    {
        public int TimeWork { get; set; }    
        public double TimeSalary { get; set; }

        public WorkerTimeSalary(string _Fname, string _Lname, double _TimeSalary,int _TimeWork):base (_Fname,_Lname,_TimeSalary*_TimeWork)
        {
            TimeWork = _TimeWork;
            TimeSalary = _TimeSalary;
        }

        public override double AverSalary()
        {
            return TimeSalary*20.8*8;
        }

    }
}

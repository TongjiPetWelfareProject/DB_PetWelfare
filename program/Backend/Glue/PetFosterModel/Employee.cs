using PetFoster.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.Model
{
    public class Employee
    {
        public string employee_id;
        public string employee_name;
        public double salary;
        public string phone_number;
        public string duty;
        public decimal working_start_hr;
        public decimal working_end_hr;
        public decimal working_start_min;
        public decimal working_end_min;
        public Employee() { }
        public Employee(PetData.EMPLOYEERow vrow)
        {
            employee_id = vrow.EMPLOYEE_ID;
            employee_name = vrow.EMPLOYEE_NAME;
            salary = vrow.SALARY;
            phone_number = vrow.PHONE_NUMBER;
            duty = vrow.DUTY;
            working_start_hr = vrow.WORKING_START_HR;
            working_start_min = vrow.WORKING_START_MIN;
            working_end_hr = vrow.WORKING_END_HR;
            working_end_min = vrow.WORKING_END_MIN;
        }
    }
}

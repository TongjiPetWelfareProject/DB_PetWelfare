using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace PetFoster.Model
{
    public class Vet
    {
        public string vet_id;
        public string vet_name;
        public double salary;
        public string phone_number;
        public byte[] portrait;
        public decimal working_start_hr;
        public decimal working_end_hr;
        public decimal working_start_min;
        public decimal working_end_min;
        public Vet() { }
        public Vet(PetData.VETRow vrow)
        {
            vet_id = vrow.VET_ID;
            vet_name = vrow.VET_NAME;
            salary = vrow.SALARY;
            phone_number = vrow.PHONE_NUMBER;
            working_start_hr = vrow.WORKING_START_HR;
            working_start_min = vrow.WORKING_START_MIN;
            working_end_hr = vrow.WORKING_END_HR;
            working_end_min = vrow.WORKING_END_MIN;
        }
    }
}
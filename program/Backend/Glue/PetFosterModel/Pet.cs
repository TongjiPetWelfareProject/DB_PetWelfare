using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.Model
{
    public class Pet
    {
        public string Pet_ID { get; set; }
        public string Pet_Name { get; set; }
        public string Species { get; set; }
        public DateTime birthdate { get; set; }
        public string Avatar { get; set; }
        public string Health_State { get; set; }
        public string Vaccine { get; set; }
        public decimal Read_Num { get; set; }
        public decimal Like_Num { get; set; }
        public decimal Collect_Num { get; set; }
        public Pet()
        {
            Pet_ID = "-1";
            Pet_Name = "宠物已注销";
        }
        /*
        public Pet(PetData.PETRow prow)
        {
            Pet_ID = prow.PET_ID;
            Pet_Name = prow.PET_NAME;
            Species = prow.BREED;
            birthdate = prow.BIRTHDATE;
            Avatar = prow.AVATAR;
            Health_State = prow.HEALTH_STATE;
            Vaccine = prow.VACCINE;
            Read_Num = prow.READ_NUM;
            Like_Num = prow.LIKE_NUM;
            Collect_Num = prow.COLLECT_NUM;
        }
        */
    }
}

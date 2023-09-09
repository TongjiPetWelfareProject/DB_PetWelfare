using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetFoster.Model
{
    public class Profile
    {
        public int getlikes;//总点赞数
        public int getclicks;//总阅读数
        public Profile(int getlikes, int getclicks)
        {
            this.getlikes = getlikes;
            this.getclicks = getclicks;
        }
    }
    public class User
    {
        public string User_ID;
        public string User_Name;
        public string Password;
        public string Phone_Number;
        public string Account_Status;
        public string Address;
        public string Role;
        public string Avatar;
        public User() {
            User_ID = "-1";
        }
        public User(PetData.USER2Row urow)
        {
            User_ID = urow.USER_ID;
            User_Name = urow.USER_NAME;
            Password=urow.PASSWORD;
            Phone_Number= urow.PHONE_NUMBER;
            Account_Status= urow.ACCOUNT_STATUS;
            Address=urow.ADDRESS;
        }
    }
}

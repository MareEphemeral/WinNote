using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class User
    {
        public int userID;
        public string realName;
        public int age;
        public string sex;
        public string school_name;
        public string email;
        public String[] tagsLike;
        public string userName;
        public string userPass;
        public int groupID;

        public User()
        {

        }
        public User(int UserID,string realName,int age,string sex,string school_name,string email,String[] tagsLike,int groupID)
        {
            this.userID = UserID;
            this.age = age;
            this.email = email;
            this.realName=realName;
            this.school_name=school_name;
            this.sex=sex;
            this.tagsLike = tagsLike;
            this.groupID = groupID;
        }
        public void NewUser(string userName,string userPass, string realName, int age, string sex, string school_name, string email, String[] tagsLike,int groupID)
        {
            this.userName = userName;
            this.userPass = userPass;
            this.age = age;
            this.email = email;
            this.realName = realName;
            this.school_name = school_name;
            this.sex = sex;
            this.tagsLike = tagsLike;
            this.groupID = groupID;
        }


    }
}

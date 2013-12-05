using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public interface IUserSubSystem
    {
         String Register(User user);
         String DeleteUser(User user);
         String UpdateUserInfo(User user);
         string[] getGroupMember(int groupID);
         int getGroupID(String name);
         string CreateNewGroup(string groupName, int userID);
         string enterGroup(int groupID, int userID);
         String deleteGroup(int groupID);
         String Validate(String userName, String passWord);
    }
}

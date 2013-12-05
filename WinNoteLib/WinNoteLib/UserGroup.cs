using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class UserGroup
    {
        
        string groupName;
        int groupID;
        string[] groupMembers;
        int creator;
        public UserGroup(string groupName, int groupID, string[] groupMembers,int creator)
        {
            this.groupName = groupName;
            this.groupID = groupID;
            this.groupMembers = groupMembers;
            this.creator = creator;
        }
          public UserGroup()
        {

        }
    }
}

using System;

namespace PracticaMad.HTTP.Session
{
    public class UserSession
    {

        private long userProfileId;
        private String userName;

        public long UserProfileId
        {
            get { return userProfileId; }
            set { userProfileId = value; }
        }
        
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}



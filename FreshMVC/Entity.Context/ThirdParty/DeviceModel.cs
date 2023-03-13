using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BMS.Entity.Context.ThirdParty
{
    public class EditUser : RegisterUser
    {
        public string userId;
    }
    public class RegisterUser
    {
        public string surname;
        public string givenName;
        public string card1;
        public string card2;
        public string startTime;
        public string endTime;
        public string pin;
        public string duressPin;
        public string parentPhone;
    }

    public class CreateElevatorGroup
    {
        public string groupName;
        public string timezoneId;
        public List<string> outputList;

        public CreateElevatorGroup()
        {
            outputList = new List<string>();
        }
    }

    public class DownloadUsers
    {
        public List<string> UserList;
        public List<string> DeviceList;
        public List<string> ElevatorGroupList;
        public string cmd;

        public DownloadUsers()
        {
            UserList = new List<string>();
            DeviceList = new List<string>();
            ElevatorGroupList = new List<string>();
        }
    }
}

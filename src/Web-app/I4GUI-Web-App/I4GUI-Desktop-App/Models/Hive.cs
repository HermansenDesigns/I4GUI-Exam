using System;

namespace I4GUI_Desktop_App.Models
{
    [Serializable]
    public class Hive
    {
        public string BeeHive { get; set; }

        public DateTime DateOfCount { get; set; }

        public int Amount { get; set; }

        public string Comments { get; set; }
    }
}
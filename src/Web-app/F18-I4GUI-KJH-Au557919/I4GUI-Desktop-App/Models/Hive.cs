using System;

namespace I4GUI_Desktop_App.Models
{
    [Serializable]
    public class Hive
    {
        private string _beeHive;

        public string BeeHive
        {
            get => _beeHive;
            set
            {
                if (value.Length <= 18)
                    _beeHive = value;
            }
        }

        public DateTime DateOfCount { get; set; }

        public int Amount { get; set; }

        public string Comments { get; set; }
    }
}
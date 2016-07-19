using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Artifact
{
    class keyboardKey
    {
        private int iXLocation;
        private int iYLocation;

        private bool iInUse;

        private bool iBlacklisted;

        private char iCharValue;

        public keyboardKey(char charValue, int xLocation, int yLocation)
        {
            iCharValue = charValue;
            iXLocation = xLocation;
            iYLocation = yLocation;

        }

        public int xLocation
        {
            get
            {
                return iXLocation;
            }
        }


        public int yLocation
        {
            get
            {
                return iYLocation;
            }
            
        }
       

        public char charValue
        {
            get
            {
                return iCharValue;
            }
        }

        public bool blacklisted
        {
            get
            {
                return iBlacklisted;
            }
            set
            {
                iBlacklisted = value;
            }
        }

        public bool inUse
        {
            get
            {
                return iInUse;
            }
            set
            {
                iInUse = value;
            }
        }






    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace TrialProgr
{
    static class Veryfier
    {

        //all the required data:
        const string regPath = "HKEY_CURRENT_USER\\Software\\trialProgram\\";
        const string regUntilDateKey = "Until";
        const string regSinceDateKey = "Since";
        const string regFlagIsTimesOrDate = "Flag";
        const string regTimesLeft = "Left";
        const string regTimesUsed = "Used";
        const string regHash = "Hash";

        const int times = 5;
        const double usingTime = 2;

        static string progrPath = "MyLittleProgram.exe";
        static string progrDest = "C:\\";

        //let it be like this, but of course it should be in another place


        public static bool Verify()
        {
            bool isDateOrTimes;
            object regobj = Registry.GetValue(regPath, regFlagIsTimesOrDate, null);
            if (regobj != null) isDateOrTimes = Convert.ToBoolean(regobj);
            else return false;

            if (isDateOrTimes)
            {
                long untilbin = Convert.ToInt64(Registry.GetValue(regPath, regUntilDateKey, 0));
                long nowbin = Convert.ToInt64(Registry.GetValue(regPath, regSinceDateKey, 0));
                int hash = Convert.ToInt32(Registry.GetValue(regPath, regHash, 0));

                if ((untilbin - nowbin).GetHashCode() != hash) return false;

                DateTime until = DateTime.FromBinary(untilbin);
                if (DateTime.Compare(until, DateTime.Now) != 1) return false;

            }
            else
            {
                int left = Convert.ToInt32(Registry.GetValue(regPath, regTimesLeft, 0));
                int used = Convert.ToInt32(Registry.GetValue(regPath, regTimesUsed, 0));
                int hash = Convert.ToInt32(Registry.GetValue(regPath, regHash, 0));

                if ((left + used).GetHashCode() != hash) return false;

                if (left < 1) return false;

                Registry.SetValue(regPath, regTimesLeft, left - 1);
                Registry.SetValue(regPath, regTimesUsed, used + 1);


            }
            return true;
        }


    }
}

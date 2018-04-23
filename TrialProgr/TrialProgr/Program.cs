using System;
using System.Collections.Generic;
using NDesk.Options;
using Microsoft.Win32;
using System.IO;

namespace TrialProgr
{
    class Program
    {
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
        static bool DateOrTimes;


        public static void Main(string[] args)
        {
            bool show_help = false;

            var p = new OptionSet() {
            { "p|path=", "the {NAME} to rpogram to instal trial.", v => progrPath = v },
           // { "r|repeat=", "this must be an integer.", (int v) => repeat = v },
            { "t|type=", "trial type: date or times", v => {
                if (v == "date") DateOrTimes = true;
            else if (v == "times") DateOrTimes = false;
            else throw new OptionException("Wrong parameter value", "t|type="); } },
            { "h|help",  "show this message and exit", v => show_help = v != null }
            };

            List<string> extra;

            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `greet --help' for more information.");
                return;
            }
            try
            {
                if (progrPath != "")
                {
                    //sets values that will be used to protect future program
                    //the opposite force is in the class verifier
                    Registry.SetValue(regPath, regFlagIsTimesOrDate, DateOrTimes);
                    if (DateOrTimes)
                    {
                        DateTime now = DateTime.Now;
                        DateTime until = now.AddMinutes(usingTime);

                        long nowbin = now.ToBinary();
                        long untilbin = until.ToBinary();
                        long hash = untilbin - nowbin;

                        hash = hash.GetHashCode();

                        Registry.SetValue(regPath, regSinceDateKey, nowbin);
                        Registry.SetValue(regPath, regUntilDateKey, untilbin);
                        Registry.SetValue(regPath, regHash, hash);
                    }
                    else
                    {
                        int used = 0;
                        int left = used + times;

                        int hash = (left + used).GetHashCode();

                        Registry.SetValue(regPath, regTimesUsed, used);
                        Registry.SetValue(regPath, regTimesLeft, left);
                        Registry.SetValue(regPath, regHash, hash);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.Read();
            }
            if (show_help)
            {
                ShowHelp(p);
                return;
            }
            Console.Read();
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
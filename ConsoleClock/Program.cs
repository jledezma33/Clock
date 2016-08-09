using ClockWithEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClock
{
    class Program
    {
        Clock ticker = new Clock();
        //protected static int positon;
        static void Main(string[] args)
        {
            
            Program p = new Program();
            p.Run();
        }



        private void Run()
        {
            //This line no longer works because we changed the publicly exposed delegate to an event
            //c.secondsChanged = MyClockSecondsHaveChanged;

            //These lines are equivalent, they just point to different methods

            ticker.HoursChanged += new Clock.TimeChangedDelegate(HoursChangedHandler);
            ticker.MinutesChanged += new Clock.TimeChangedDelegate(MinutesChangedHandler);
            ticker.SecondsChanged += new Clock.TimeChangedDelegate(SecondsChangedHandler);
            ticker.MilliSecondsChanged += new Clock.TimeChangedDelegate(MilliSecondsChangedHandler);
            

            ticker.Start();
        }

        public Program()
        {
            ticker = new Clock();            
        }

        private void HoursChangedHandler(int hours)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write("0"+hours.ToString() + ":");
        }

        private void MinutesChangedHandler(int minutes)
        {
            Console.SetCursorPosition(3, Console.CursorTop);
            Console.Write("0"+minutes.ToString() + ":");
        }

        private void SecondsChangedHandler(int seconds)
        {
            Console.SetCursorPosition(6, Console.CursorTop);
            Console.Write("0"+seconds.ToString() + ".");
        }

        private void MilliSecondsChangedHandler(int milliseconds)
        {
            Console.SetCursorPosition(9, Console.CursorTop);
            Console.Write(milliseconds.ToString());
        }
    }
}

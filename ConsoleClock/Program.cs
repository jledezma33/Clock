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
        Clock ticker;

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

            ticker.MilliSecondsChanged += new Clock.TimeChangedDelegate(MilliSecondsChangedHandler);
            ticker.SecondsChanged += SecondsChangedHandler;
            ticker.MinutesChanged += MinutesChangedHandler;
            ticker.HoursChanged += HoursChangedHandler;
            
            ticker.Start();
        }

        public Program()
        {
            ticker = new Clock();            
        }

        private void HoursChangedHandler(int hours)
        {
            Console.Write("\r"+ hours.ToString() + ":");
        }

        private void MinutesChangedHandler(int minutes)
        {
            Console.Write(minutes.ToString() + ":");
        }

        void SecondsChangedHandler(int seconds)
        {
            Console.Write(seconds.ToString() + ":");
        }

        private void MilliSecondsChangedHandler(int milliseconds)
        {
            Console.Write(milliseconds.ToString());
        }
    }
}

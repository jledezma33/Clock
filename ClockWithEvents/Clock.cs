﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClockWithEvents
{
    public class Clock
    {
        //This line defines a type.  It is similar in purpose to "public class AnInternalClass{...}"
        public delegate void TimeChangedDelegate(int currentTime);

        //This line declares a variable of a particular type; a custom delegate we created above.  
        //It is similar to the "public ... anInternalClassInstance" line below
        //Normally class level members should be private.  Events are public if we want external classes to handle them.
        public event TimeChangedDelegate MilliSecondsChanged;
        public event TimeChangedDelegate SecondsChanged;
        public event TimeChangedDelegate MinutesChanged;
        public event TimeChangedDelegate HoursChanged;
        public event TimeChangedDelegate DaysChanged;

        public class AnInternalClass
        {
            public int MyProperty { get; set; }
        }

        private const int MILLISECONDS_IN_SECOND = 1000;
        private const int SECONDS_IN_MINUTE = 10;
        private const int MINUTES_IN_HOUR = 3;
        private const int HOURS_IN_DAY = 3;
        public AnInternalClass anInternalClassInstance;

        int milliseconds = 0;   
        int seconds = 0;
        int minutes = 0;
        int hours = 0;
        int days = 0;

        public void Start()
        {
            //will throw, because anInternalClassInstance is not set to an instance
            //Console.WriteLine(anInternalClassInstance.MyProperty);

            for (int i = 0; i < 1000000; i++)
            {
                milliseconds++;
                if(milliseconds >= MILLISECONDS_IN_SECOND)
                {
                    milliseconds = 0;
                    seconds++;
                }
                if(seconds >= SECONDS_IN_MINUTE)
                {
                    seconds = 0;
                    minutes++;
                }
                if(minutes >= MINUTES_IN_HOUR)
                {
                    minutes = 0;
                    hours++;
                }
                if(hours >= HOURS_IN_DAY)
                {
                    hours = 0;
                    days++;
                }

                //Sleeping for 1 millisecond is a little silly and inefficient, but it will help keep our math easy to follow
                System.Threading.Thread.Sleep(1);
                if(milliseconds % 1 == 0)
                {
                    OnMilliSecondsChanged();
                }
                if (milliseconds % MILLISECONDS_IN_SECOND == 0)
                {
                    OnSecondsChanged();
                }
                if (seconds % SECONDS_IN_MINUTE == 0)
                {
                    OnMinuteChanged();
                }
                if(minutes % MINUTES_IN_HOUR == 0)
                {
                    OnHoursChanged();
                }
                if(hours % HOURS_IN_DAY == 0)
                {
                    OnDaysChanged();
                }

                //The Clock is no longer aware of the console.  It notifies calling code of changes through events
                //rather than writing or printing changes itself.
                //Console.WriteLine(seconds);
            }            
        }

        private  void OnMilliSecondsChanged()
        {
            if(MilliSecondsChanged != null)
            {
                MilliSecondsChanged.Invoke(milliseconds);
            }
        }
             
        private void OnSecondsChanged()
        {
            if (SecondsChanged != null)
            {
                //this line is incorrect.  Fix it! :)
                SecondsChanged.Invoke(milliseconds / MILLISECONDS_IN_SECOND);
            }
        }

        private void OnMinuteChanged()
        {
            if (MinutesChanged != null)
            {
                MinutesChanged.Invoke(seconds / SECONDS_IN_MINUTE);
            }
        }


        private void OnHoursChanged()
        {
            if(HoursChanged != null)
            {
                HoursChanged.Invoke(minutes / MINUTES_IN_HOUR);
            }
        }

        private void OnDaysChanged()
        {
            if(DaysChanged != null)
            {
                DaysChanged.Invoke(hours / HOURS_IN_DAY);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Delegate
{
    /// <summary>
    /// A class to hold the information about the event 
    /// in this case it will hold only information 
    /// available in the clock class, but could hold
    /// additional state information.
    /// </summary>
    public class TimeInfoEventArgs : EventArgs
    {
        public int hour;
        public int minute;
        public int second;

        public TimeInfoEventArgs(int hour, int minute, int second) 
        {
            this.hour = hour;
            this.minute = minute;
            this.second = second;
        }
    }
    
    /// <summary>
    /// The publisher: the class that others 
    /// will observe. This class publishes one delagate:
    /// SecondChangeHandler
    /// </summary>
    public class Clock
    {
        private int hour;
        private int minute;
        private int second;

        /// The delegate the suscribers must implement
        public delegate void SecondChangeHandler(object clock, TimeInfoEventArgs timeInformation);

        // An instance of the delegate
        // use event keyword to avoid execution of the
        // delegate outside the Clock class.
        public event SecondChangeHandler SecondChanged;

        // Set the clock running
        // it will raise an event for each new second
        public void Start()
        {
            for (; ; )
            {
                Thread.Sleep(100);

                DateTime dt = DateTime.Now;

                if (dt.Second != second)
                {
                    TimeInfoEventArgs timeInformation = new TimeInfoEventArgs(dt.Hour, dt.Minute, dt.Second);

                    if (SecondChanged != null)
                    {
                        SecondChanged(this, timeInformation);
                    }
                }

                // update state
                this.second = dt.Second;
                this.minute = dt.Minute;
                this.hour = dt.Hour;
            }
        }
    }
    
    // Suscriber: DisplayClock subscribes to the 
    // clock's events. The job of DisplayClock is
    // to display the current time
    public class DisplayClock
    { 
        // Given a clock, suscribe to 
        // its SecondChangeHandler event
        public void Subscribe(Clock theClock)
        {
            theClock.SecondChanged += new Clock.SecondChangeHandler(TimeHasChanged);
        }

        // The method that implements the
        // delegated functionality
        public void TimeHasChanged(object theClock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Current Time: {0}:{1}:{2}", ti.hour, ti.minute, ti.second);
        }
    }

    // A second suscriber whose job is to write to a file
    public class LogCurrentTime
    {
        public void Subscribe(Clock theClock)
        {
            theClock.SecondChanged += new Clock.SecondChangeHandler(WriteLogEntry);
        }

        public void WriteLogEntry(object theClock, TimeInfoEventArgs ti)
        {
            Console.WriteLine("Logging to file: {0}:{1}:{2}", ti.hour, ti.minute, ti.second);
        }
    }
    
    public class MyTest
    {
        public void Run()
        {
            Clock theClock = new Clock();

            DisplayClock dc = new DisplayClock();
            dc.Subscribe(theClock);

            LogCurrentTime lct = new LogCurrentTime();
            lct.Subscribe(theClock);

            theClock.Start();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Tester t = new Tester();
            t.Run();
        }
    }

}

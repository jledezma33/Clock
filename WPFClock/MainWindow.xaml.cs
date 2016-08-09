using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClockWithEvents;
using System.Windows.Threading;

namespace WPFClock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clock ticker;

        private delegate void NoArg();
        private delegate void MyEventHandler(object sender, EventArgs e);

        public MainWindow()
        {
            
            InitializeComponent();

            ticker = new Clock();

            /*
             * We could also use the Action type instead of NoArg and save some code.  
             * But then we'd need to understand Generics...
             * so I'll stick with NoArg for now.
            */
            NoArg start = ticker.Start;


            /*
             *  .NET prevents us from updating UI elements from another thread.
             *  Our clock uses Thread.Sleep which would make our app look like it crashed.
             *  We'll use a separate thread for the clock.Start method, then use the Dispatcher
             *  to update the UI in its own sweet time on its own sweet thread.  Think of 
             *  queueing up a message that is then processed by the UI thread when it's able.
             *  
             *  Importantly, we don't have to change the Clock class to take advantage of threading.
             *  All the Dispatch/BeginInvoke magic happens here in the client code.
             * 
             */
            ticker.MilliSecondsChanged += Ticker_MilliSecondsChangedOnDifferentThread;
            ticker.SecondsChanged += Ticker_SecondsChangedOnDifferentThread;
            ticker.MinutesChanged += Ticker_MinutesChangedOnDifferentThread;
            ticker.HoursChanged += Ticker_HoursChangedOnDifferentThread;
            ticker.DaysChanged += Ticker_DaysChangedOnDifferentThread;
            start.BeginInvoke(null, null);
            
        }

        private void Ticker_MilliSecondsChangedUIThread(int milliseconds)
        {
            MilliSecondsLabel.Content = milliseconds;
            
        }

        private void Ticker_MilliSecondsChangedOnDifferentThread(int milliseconds)
        {
            MilliSecondsLabel.Dispatcher.BeginInvoke(new Action<int>(Ticker_MilliSecondsChangedUIThread), milliseconds);
        }

        private void millisecondsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ticker.MilliSecondsChanged -= Ticker_MilliSecondsChangedOnDifferentThread;
            MilliSecondsLabel.Foreground = Brushes.Red;
        }

        private void millisecondsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ticker.MilliSecondsChanged += Ticker_MilliSecondsChangedOnDifferentThread;
            MilliSecondsLabel.Foreground = Brushes.Green;
        }

        private void Ticker_SecondsChangedUIThread(int seconds)
        {
            /*
             * This method is executed by the UI thread, and so can modify the label directly.
             */
            SecondsLabel.Content = "0"+seconds+":";        
        }

        private void Ticker_SecondsChangedOnDifferentThread(int seconds)
        {
            /*
             * Here's where the Clock's thread will put a message on the UI thread's queue of work,
             * again, through the use of a delegate
             */
            SecondsLabel.Dispatcher.BeginInvoke(new Action<int>(Ticker_SecondsChangedUIThread), seconds);
        }

        private void secondsCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ticker.SecondsChanged -= Ticker_SecondsChangedOnDifferentThread;
            SecondsLabel.Foreground = Brushes.Red;
        }

        private void secondsCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ticker.SecondsChanged += Ticker_SecondsChangedOnDifferentThread;
            SecondsLabel.Foreground = Brushes.Green;
        }

        private void Ticker_MinutesChangedUIThread(int minutes)
        {
            MinutesLabel.Content = "0"+minutes+":";
        }

        private void Ticker_MinutesChangedOnDifferentThread(int minutes)
        {
            MinutesLabel.Dispatcher.BeginInvoke(new Action<int>(Ticker_MinutesChangedUIThread), minutes);
        }

        private void minutesCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ticker.MinutesChanged -= Ticker_MinutesChangedOnDifferentThread;
            MinutesLabel.Foreground = Brushes.Red;
        }

        private void minutesCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ticker.MinutesChanged += Ticker_MinutesChangedOnDifferentThread;
            MinutesLabel.Foreground = Brushes.Green;

        }

        private void Ticker_HoursChangedUIThread(int hours)
        {
           HoursLabel.Content = "0"+hours+":";
        }

        private void Ticker_HoursChangedOnDifferentThread(int hours)
        {
           HoursLabel.Dispatcher.BeginInvoke(new Action<int>(Ticker_HoursChangedUIThread), hours);
        }

        private void hoursCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ticker.HoursChanged -= Ticker_HoursChangedOnDifferentThread;
            HoursLabel.Foreground = Brushes.Red;
        }

        private void hoursCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ticker.HoursChanged += Ticker_HoursChangedOnDifferentThread;
            HoursLabel.Foreground = Brushes.Green;
        }

        private void Ticker_DaysChangedUIThread(int days)
        {
            DaysLabel.Content = "0"+days;
        }

        private void Ticker_DaysChangedOnDifferentThread(int days)
        {
            DaysLabel.Dispatcher.BeginInvoke(new Action<int>(Ticker_DaysChangedUIThread), days);
        }

        private void daysCheckBox_Checked(object sender, RoutedEventArgs e)
        {  
            ticker.HoursChanged -= Ticker_HoursChangedOnDifferentThread;
            DaysLabel.Foreground = Brushes.Red;
        }

        private void daysCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ticker.DaysChanged += Ticker_DaysChangedOnDifferentThread;
            DaysLabel.Foreground = Brushes.Green;
        }
    }
}

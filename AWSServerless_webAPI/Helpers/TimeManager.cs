using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AWSServerless_webAPI.Helpers
{
    public class TimerManager
    {
        private Timer _timer;
        private AutoResetEvent _autoResetEvent;
        private Action _action;

        public DateTime TimerStarted { get; }

        public TimerManager(Action action,int firstdelayByMilliSeconds,int period)
        {
            _action = action;
            _autoResetEvent = new AutoResetEvent(false);
            _timer = new Timer(Execute, _autoResetEvent, firstdelayByMilliSeconds, period);
            TimerStarted = DateTime.Now;
        }

        public void Execute(object stateInfo)
        {
            _action();
            //to stop updating the vehicles after 10 minutes
            if ((DateTime.Now - TimerStarted).Minutes > 10)
            {
                _timer.Dispose();
            }
        }
    }
}

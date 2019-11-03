using System;

namespace AWSServerless_webAPI.Helpers
{
    public interface ITimerManager
    {
        DateTime TimerStarted { get; }

        void Configure(Action action, int firstdelayByMilliSeconds, int period);
        void Dispose();
        void Execute(object stateInfo);
    }
}
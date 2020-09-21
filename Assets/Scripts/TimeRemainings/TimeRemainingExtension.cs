using System.Collections.Generic;


namespace Assets.Scripts.TimeRemainings
{
    public static partial class TimeRemainingExtension
    {
        private static readonly List<ITimeRemaining> _timeRemainings = new List<ITimeRemaining>(10);

        public static List<ITimeRemaining> TimeRemainings
        {
            get { return _timeRemainings; }
        }

        public static void AddTimeRemaining(this ITimeRemaining value)
        {
            if (_timeRemainings.Contains(value))
            {
                return;
            }
            value.CurrentTime = value.Time;
            _timeRemainings.Add(value);
        }

        public static void RemoveTimeRemaining(this ITimeRemaining value)
        {
            if (!_timeRemainings.Contains(value))
            {
                return;
            }
            _timeRemainings.Remove(value);
        }
    }
}
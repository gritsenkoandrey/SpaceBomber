using Assets.Scripts.Interface;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.TimeRemainings
{
    public sealed class TimeRemainingController : IExecute
    {
        private readonly List<ITimeRemaining> _timeRemainings;

        public TimeRemainingController()
        {
            _timeRemainings = TimeRemainingExtension.TimeRemainings;
        }

        public void Execute()
        {
            var time = Time.deltaTime;
            for (int i = 0; i < _timeRemainings.Count; i++)
            {
                var obj = _timeRemainings[i];
                obj.CurrentTime -= time;
                if (obj.CurrentTime <= 0.0f)
                {
                    obj?.Method?.Invoke();
                    if (!obj.IsRepeating)
                    {
                        obj.RemoveTimeRemaining();
                    }
                    else
                    {
                        obj.CurrentTime = obj.Time;
                    }
                }
            }
        }
    }
}
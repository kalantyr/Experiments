using System;
using System.Threading;

namespace Speed
{
    public interface ISimulatorTimer
    {
        event Action<TimeSpan> ElapsedTimeChanged;

        void Start();
    }

    public interface ISimulatorAccelerator
    {
        double Acceleration { get; set; }

        event Action<double> AccelerationChanged;
    }

    public interface ISimulatorSpeed
    {
        event Action<double> SpeedChanged;
    }

    public interface ISimulatorDistance
    {
        event Action<double> DistanceChanged;
    }

    public interface ISimulator : ISimulatorTimer, ISimulatorAccelerator, ISimulatorSpeed, ISimulatorDistance
    {
    }

    public class Simulator : ISimulator
    {
        private Timer _timer;
        private DateTime _startTime;
        private DateTime _lastTimerTime;
        private double _acceleration;
        private double _speed;
        private double _distance;

        private static readonly TimeSpan TimerInterval = TimeSpan.FromSeconds(0.1);

        public event Action<TimeSpan> ElapsedTimeChanged;
        public event Action<double> AccelerationChanged;
        public event Action<double> SpeedChanged;
        public event Action<double> DistanceChanged;

        public void Start()
        {
            _startTime = DateTime.Now;
            _lastTimerTime = _startTime;
            _timer = new Timer(OnTimer, this, TimeSpan.Zero, TimerInterval);
        }

        private void OnTimer(object state)
        {
            var dTime = DateTime.Now - _lastTimerTime;

            ElapsedTimeChanged?.Invoke(DateTime.Now - _startTime);

            Speed += Acceleration * dTime.TotalSeconds;
            Distance += Speed * dTime.TotalSeconds;

            _lastTimerTime = DateTime.Now;
        }

        public double Acceleration
        {
            get => _acceleration;
            set
            {
                _acceleration = value;
                AccelerationChanged?.Invoke(value);
            }
        }

        public double Speed
        {
            get => _speed;
            set
            {
                _speed = value;
                SpeedChanged?.Invoke(value);
            }
        }

        public double Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                DistanceChanged?.Invoke(value);
            }
        }
    }
}

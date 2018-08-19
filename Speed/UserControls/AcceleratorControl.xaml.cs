using System.Windows;

namespace Speed.UserControls
{
    public partial class AcceleratorControl
    {
        private ISimulatorAccelerator _simulatorAccelerator;

        public ISimulatorAccelerator SimulatorAccelerator
        {
            get => _simulatorAccelerator;
            set
            {
                if (_simulatorAccelerator != null)
                    _simulatorAccelerator.AccelerationChanged -= OnAccelerationChanged;

                _simulatorAccelerator = value;

                if (_simulatorAccelerator != null)
                    _simulatorAccelerator.AccelerationChanged += OnAccelerationChanged;
            }
        }

        private void OnAccelerationChanged(double acceleration)
        {
            _tbAcceleration.Text = acceleration.ToString("F1");
        }

        public AcceleratorControl()
        {
            InitializeComponent();
        }

        private void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _simulatorAccelerator.Acceleration = e.NewValue;
        }
    }
}

namespace Speed.UserControls
{
    public partial class DistanceControl
    {
        private ISimulatorDistance _simulatorDistance;

        public ISimulatorDistance SimulatorDistance
        {
            get => _simulatorDistance;
            set
            {
                if (_simulatorDistance != null)
                    _simulatorDistance.DistanceChanged -= OnDistanceChanged;

                _simulatorDistance = value;

                if (_simulatorDistance != null)
                    _simulatorDistance.DistanceChanged += OnDistanceChanged;
            }
        }

        private void OnDistanceChanged(double distance)
        {
            if (Dispatcher.CheckAccess())
            {
                _tbMeters.Text = ((int)distance) + " м";
                _tbKilometers.Text = (distance / 1000).ToString("F1") + " км";
            }
            else
                Dispatcher.Invoke(() => OnDistanceChanged(distance));
        }

        public DistanceControl()
        {
            InitializeComponent();
        }
    }
}

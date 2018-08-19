namespace Speed
{
    public partial class MainWindow
    {
        private readonly ISimulator _simulator;

        public MainWindow()
        {
            InitializeComponent();

            _simulator = new Simulator();

            _timeControl.SimulatorTimer = _simulator;
            _acceleratorControl.SimulatorAccelerator = _simulator;
            _speedControl.SimulatorSpeed = _simulator;
            _distanceControl.SimulatorDistance = _simulator;
        }
    }
}

using System.Diagnostics;
using System.Windows;
using VTB.Portal.ExtendedLogging;

namespace PerformanceCountersTest
{
    public partial class MainWindow
    {
	    private const string COUNTER_NAME = "T_1";
		private const string COUNTER2_NAME = "T_1_";
        private static readonly PerformanceCounterCategory _performanceCounterCategory;

	    private static readonly IPerformanceCollector _performanceCollector;

		static MainWindow()
        {
            var counterCreationDatas = new[]
            {
	            new CounterCreationData(COUNTER_NAME, "Help", PerformanceCounterType.NumberOfItems64),
	            new CounterCreationData(COUNTER2_NAME, "Help", PerformanceCounterType.RateOfCountsPerSecond64)
			};
	        _performanceCollector = PerformanceCollector.Create("_Temp1", "test", counterCreationDatas);
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
	        var newValue = (long)e.NewValue;
	        _performanceCollector.SetValue(COUNTER_NAME, newValue);
			for (var i = 0; i < newValue; i++)
				_performanceCollector.Increment(COUNTER2_NAME);
		}
	}
}

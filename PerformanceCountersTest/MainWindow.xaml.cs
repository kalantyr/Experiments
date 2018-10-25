using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace PerformanceCountersTest
{
    public partial class MainWindow
    {
        private const string CATEGORY_NAME = "_Temp";
        private const string CATEGORY_HELP = "test";
        private const string COUNTER_NAME = "T_1";
        private static readonly PerformanceCounterCategory _performanceCounterCategory;

        static MainWindow()
        {
            var counterCreationDatas = new[]
            {
                new CounterCreationData(COUNTER_NAME, "Help", PerformanceCounterType.NumberOfItems64)
            };

            var category = PerformanceCounterCategory.GetCategories().FirstOrDefault(cat => cat.CategoryName == CATEGORY_NAME);
            if (category != null)
            {
                foreach (var creationData in counterCreationDatas)
                    if (!category.CounterExists(creationData.CounterName))
                    {
                        PerformanceCounterCategory.Delete(CATEGORY_NAME);

                        var counterDataCollection = new CounterCreationDataCollection();
                        counterDataCollection.AddRange(counterCreationDatas);
                        _performanceCounterCategory = PerformanceCounterCategory.Create(CATEGORY_NAME, CATEGORY_HELP, PerformanceCounterCategoryType.MultiInstance, counterDataCollection);
                        return;
                    }
                _performanceCounterCategory = category;
            }
            else
            {
                var counterDataCollection = new CounterCreationDataCollection();
                counterDataCollection.AddRange(counterCreationDatas);
                _performanceCounterCategory = PerformanceCounterCategory.Create(CATEGORY_NAME, CATEGORY_HELP, PerformanceCounterCategoryType.MultiInstance, counterDataCollection);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RangeBase_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            using (var counter = new PerformanceCounter(_performanceCounterCategory.CategoryName, COUNTER_NAME, GetInstanceName(), false))
                counter.RawValue = (long)e.NewValue;
        }

        private static string GetInstanceName()
        {
            var process = Process.GetCurrentProcess();
            return $"{process.ProcessName}_{process.Id}";
            //return Process.GetCurrentProcess().MainModule.FileName;
        }
    }
}

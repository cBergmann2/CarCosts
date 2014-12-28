using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkID=390556 dokumentiert.

namespace CarCosts.Statistics
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class LitersPerKilometer : Page
    {

        public class FinancialStuff
        {
            public string Name { get; set; }
            public int Amount { get; set; }
        }

        public LitersPerKilometer()
        {
            this.InitializeComponent();

            List<Refueling> refuelings = (App.Current as App).dataManager.getRefulings(); 
            if ( refuelings != null)
            {
                try
                {
                    Random rand = new Random();
                    List<FinancialStuff> financialStuffList = new List<FinancialStuff>();
                    financialStuffList.Add(new FinancialStuff() { Name = "MSFT", Amount = rand.Next(0, 200) });
                    financialStuffList.Add(new FinancialStuff() { Name = "AAPL", Amount = rand.Next(0, 200) });
                    financialStuffList.Add(new FinancialStuff() { Name = "GOOG", Amount = rand.Next(0, 200) });
                    financialStuffList.Add(new FinancialStuff() { Name = "BBRY", Amount = rand.Next(0, 200) });

                    chart.Series.Add(new LineSeries());
                    (chart.Series[0] as LineSeries).ItemsSource = financialStuffList;
                    
                    
                }
                catch (Exception e)
                {
                    
                }
                
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Frame angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}

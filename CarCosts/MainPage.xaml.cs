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

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=391641 dokumentiert.

namespace CarCosts
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            if ((App.Current as App).dataManager.getRefulings() != null)
            {
                this.updateRefulings();
            }
        }

        /// <summary>
        /// Wird aufgerufen, wenn diese Seite in einem Rahmen angezeigt werden soll.
        /// </summary>
        /// <param name="e">Ereignisdaten, die beschreiben, wie diese Seite erreicht wurde.
        /// Dieser Parameter wird normalerweise zum Konfigurieren der Seite verwendet.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Seite vorbereiten, um sie hier anzuzeigen.

            // TODO: Wenn Ihre Anwendung mehrere Seiten enthält, stellen Sie sicher, dass
            // die Hardware-Zurück-Taste behandelt wird, indem Sie das
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed-Ereignis registrieren.
            // Wenn Sie den NavigationHelper verwenden, der bei einigen Vorlagen zur Verfügung steht,
            // wird dieses Ereignis für Sie behandelt.
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            CdAddFuel cdAddFuel = new CdAddFuel();
            await cdAddFuel.ShowAsync();

            if(cdAddFuel.result == AddFuelResult.AddFuel)
            {
                //List of refulings is grown up
                //update statistcal data
                this.updateRefulings();
            }

        }


        #region Refulings

        private void updateRefulings()
        {
            List<Refueling> refuelings = (App.Current as App).dataManager.getRefulings();
            Calculations calc = new Calculations();

            //Clear current list of refuelings
            lbRefulings.Items.Clear();

            //Rewrite list of refuelings
            for (int i = refuelings.Count - 1; i >= 0; i--)
            {
                double litersPerKilometer = Calculations.literPer100Kilometer(refuelings.ElementAt(i).drivenDistance, refuelings.ElementAt(i).amount) * 100;



                SolidColorBrush brushBackground = new SolidColorBrush();
                SolidColorBrush brushForeground = new SolidColorBrush();

                brushForeground.Color = Windows.UI.Colors.Black;

                if (Calculations.evaluateFuelConsumption(litersPerKilometer) <= 0.9)
                {
                    brushBackground.Color = Windows.UI.Colors.Green;
                }
                else if(Calculations.evaluateFuelConsumption(litersPerKilometer) >= 1.1)
                {
                    brushBackground.Color = Windows.UI.Colors.Red;
                }
                else
                {
                    brushBackground.Color = Windows.UI.Colors.YellowGreen;
                }

                ListBoxItem item = new ListBoxItem();
                item.Background = brushBackground;
                item.Foreground = brushForeground;
                item.Content = refuelings.ElementAt(i).date.ToString() + " - " +
                    refuelings.ElementAt(i).amount + " l - " +
                    litersPerKilometer/100 + "l/100 km";

                lbRefulings.Items.Add(item);
            }
        }

        #endregion

    }
}

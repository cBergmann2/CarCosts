using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

                                                                        //Show current refuelings and statistics
            this.updateRefulings();                                     
            this.updateStatistics();
            
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

            if (sender == this.bAddRefueling)
            {
                //Neue Füllung hinzufügen
                CdAddFuel cdAddFuel = new CdAddFuel();
                await cdAddFuel.ShowAsync();

                if (cdAddFuel.result == AddFuelResult.AddFuel)
                {
                    //List of refulings is grown up
                    //update statistcal data
                    this.updateRefulings();
                    this.updateStatistics();
                }
            }
            else
            {
                if (sender == bEditRefuelings)
                {
                    //Füllung bearbeiten
                    
                    int selectedItem = this.lvRefulings.SelectedIndex;  //Index des markierten Elements holen
                    Refueling refueling = (App.Current as App).dataManager.getRefueling(selectedItem);
                    CdEditRefueling cdEditRefueling = new CdEditRefueling(refueling);
                    await cdEditRefueling.ShowAsync();

                    if (cdEditRefueling.result == EditRefuelingResult.delete || cdEditRefueling.result == EditRefuelingResult.edit)
                    {
                        this.updateRefulings();
                        this.updateStatistics();
                    }
                }
            }

        }


        #region Refulings

        private void updateRefulings()
        {
            DataManager dataManager = new DataManager();
            ObservableCollection<Refueling> refuelings = dataManager.getAllRefuelings();               //Get all refuelings from database

            Calculations calc = new Calculations();

            SolidColorBrush brushBackground = new SolidColorBrush();
            SolidColorBrush brushForeground = new SolidColorBrush();
            double litersPerKilometer;

            //Clear current list of refuelings
            lvRefulings.Items.Clear();

            //Rewrite list of refuelings
            for (int i = refuelings.Count - 1; i >= 0; i--)
            {

                ListViewItem item = new ListViewItem();
                brushForeground.Color = Windows.UI.Colors.Black;
                
                //item.Background = brushBackground;
                //item.Foreground = brushForeground;

                

                //Get liters per kilometer
                litersPerKilometer = Calculations.literPer100Kilometer(refuelings.ElementAt(i).drivenDistance, refuelings.ElementAt(i).amount) / 100;

                //Set item content
                item.Content = refuelings.ElementAt(i).date.ToString() + " - " +
                    refuelings.ElementAt(i).amount + " l - " +
                    litersPerKilometer * 100 + "l/100 km";
                
                
                //Specify cell background colour based on driving efficiency
                

                //Set brush for item background 
                if (calc.evaluateFuelConsumption(litersPerKilometer) <= 0.9)
                {
                    //Good driving efficiency
                    brushBackground.Color = Windows.UI.Colors.Green;
                    item.Style = (Style)(this.Resources["ListViewItem_GoodEfficiency"]);
                }
                else
                {
                    if (calc.evaluateFuelConsumption(litersPerKilometer) >= 1.1)
                    {
                        //Bad driving efficiency
                        brushBackground.Color = Windows.UI.Colors.Red;
                        item.Style = (Style)(this.Resources["ListViewItem_BadEfficiency"]);
                    }
                    else
                    {
                        //Average driving efficiency
                        brushBackground.Color = Windows.UI.Colors.YellowGreen;
                        item.Style = (Style)(this.Resources["ListViewItem_AverageEfficiency"]);
                    }
                }

                

                lvRefulings.Items.Add(item);

                
            }
        }

        private void updateStatistics()
        {
            DataManager dataManager = new DataManager();
            ObservableCollection<Refueling> refuelings = dataManager.getAllRefuelings();               //Get all refuelings from database


            switch (refuelings.Count)
            {
                case 0:
                    break;
                case 1:
                    this.tbAverageFuelConsumption.Text = Calculations.literPer100Kilometer(refuelings[0].drivenDistance, refuelings[0].amount).ToString();
                    break;
                case 2:
                    this.tbGoodFuelConsumption.Text = (App.Current as App).calculations.getBestFuelConsumption().ToString() + "l/100 km";
                    this.tbBadFuelConsumption.Text = (App.Current as App).calculations.getWorstFuelConsumption().ToString() + "l/100 km";
                    break;
                default:
                    this.tbGoodFuelConsumption.Text = (App.Current as App).calculations.getBestFuelConsumption().ToString() + "l/100 km";
                    this.tbAverageFuelConsumption.Text = (App.Current as App).calculations.averageLiterPer100Kilometer().ToString() + "l/100 km";
                    this.tbBadFuelConsumption.Text = (App.Current as App).calculations.getWorstFuelConsumption().ToString() + "l/100 km";
                    break;
            }
        }

        #endregion

        private void pivotElementSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as Pivot).SelectedIndex)
            {
                case 0:
                    this.bEditRefuelings.Visibility = Visibility.Visible;
                    break;
                default:
                    this.bEditRefuelings.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        
    }
}

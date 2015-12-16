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
using WinRTXamlToolkit.Controls.DataVisualization.Charting;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=391641 dokumentiert.

namespace CarCosts
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet werden kann oder auf die innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Refueling currentSelectedRefueling; 

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
                    /*
                    int selectedItem = this.lvRefulings.SelectedIndex;  //Index des markierten Elements holen
                    Refueling refueling = (App.Current as App).dataManager.getRefueling(selectedItem);
                    CdEditRefueling cdEditRefueling = new CdEditRefueling(refueling);
                    await cdEditRefueling.ShowAsync();

                    if (cdEditRefueling.result == EditRefuelingResult.delete || cdEditRefueling.result == EditRefuelingResult.edit)
                    {
                        this.updateRefulings();
                        this.updateStatistics();
                    }
                     */
                }
            }

        }


        #region Refulings

        private void updateRefulings()
        {
            DataManager dataManager = new DataManager();
            ObservableCollection<Refueling> refuelings = dataManager.getAllRefuelings();               //Get all refuelings from database

            lbRefulings.ItemsSource = refuelings.Reverse().ToList();                                   //Bind refulings in reverse order to ListBox
            
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
                    this.tbAverageFuelConsumption.Text = Calculations.literPer100Kilometer(refuelings[0].drivenDistance, refuelings[0].amount).ToString() + " l/100 km";
                    break;
                case 2:
                    this.tbGoodFuelConsumption.Text = (App.Current as App).calculations.getBestFuelConsumption().ToString() + " l/100 km";
                    this.tbBadFuelConsumption.Text = (App.Current as App).calculations.getWorstFuelConsumption().ToString() + " l/100 km";
                    break;
                default:
                    this.tbGoodFuelConsumption.Text = (App.Current as App).calculations.getBestFuelConsumption().ToString() + " l/100 km";
                    this.tbAverageFuelConsumption.Text = (App.Current as App).calculations.averageLiterPer100Kilometer().ToString() + " l/100 km";
                    this.tbBadFuelConsumption.Text = (App.Current as App).calculations.getWorstFuelConsumption().ToString() + " l/100 km";
                    break;
            }

            //update chart

            List<Tuple<int, double>> myList = new List<Tuple<int, double>>();
            int i = 0;
            foreach(Refueling refuling in refuelings){
                myList.Add(new Tuple<int, double>(i++, refuling.literPer100Kilometer));
            }

            (lineSeries.Series[0] as LineSeries).ItemsSource = myList;
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

        private void lbItemRefueling_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            // If you need the clicked element:
            currentSelectedRefueling = senderElement.DataContext as Refueling;

            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        private async void bEditRefueling_Click(object sender, RoutedEventArgs e)
        {
            CdEditFuel cdEditFuel = new CdEditFuel(currentSelectedRefueling);
            await cdEditFuel.ShowAsync();

            if (cdEditFuel.result == AddFuelResult.AddFuel)
            {
                //List of refulings is grown up
                //update statistcal data
                this.updateRefulings();
                this.updateStatistics();
            }
        }

        /// <summary>
        /// Deletes the selected refueling.
        /// Also updates the list of refuelings and the statistics.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDeleteRefueling_Click(object sender, RoutedEventArgs e)
        {
            DataManager dataManager = new DataManager();
            dataManager.deleteRefueling(currentSelectedRefueling.Id);
            this.updateRefulings();
            this.updateStatistics();
        }

        
    }
}

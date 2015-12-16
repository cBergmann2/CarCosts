using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Inhaltsdialog" ist unter "http://go.microsoft.com/fwlink/?LinkID=390556" dokumentiert.

namespace CarCosts
{

    public sealed partial class CdEditFuel : ContentDialog
    {
        private Refueling toEditRefueling;
        public AddFuelResult result { get; private set; } 

        public CdEditFuel(Refueling refueling)
        {
            this.InitializeComponent();
            this.Closing += ContentDialog_Closing;

            this.toEditRefueling = refueling;

            date.Date = refueling.date;
            amount.Text = refueling.amount.ToString();
            completeFilled.IsChecked = refueling.isCompleteFilled;
            tbCosts.Text = refueling.costs.ToString();
            tbDistance.Text = refueling.drivenDistance.ToString();
        }

        /// <summary>
        /// Adds refueling to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Refueling refueling = new Refueling();
            DataManager dataManager = new DataManager();

            double temp = 0.0;
            
            //Get user input
            refueling.date = date.Date.DateTime;

            
            if (!HelperFunctions.convertStringToDouble(amount.Text, ref temp))
            {
                //Error
            }
            refueling.amount = temp;

            if (!HelperFunctions.convertStringToDouble(tbCosts.Text, ref temp))
            {
                //Error
            }
            refueling.costs = temp;

            if (!HelperFunctions.convertStringToDouble(tbDistance.Text, ref temp))
            {
                //Error
            }
            refueling.drivenDistance = temp;

            refueling.isCompleteFilled = Convert.ToBoolean(completeFilled.IsChecked);

            refueling.calculateLiterPer100Kilometer();
            
            ////Add new refueling to refueling list            
            //(App.Current as App).dataManager.addRefuling(refueling);

            ////Save new refueling list
            //(App.Current as App).dataManager.saveRefuelings();

            refueling.Id = toEditRefueling.Id;

            dataManager.updateRefueling(refueling);

            result = AddFuelResult.AddFuel;      
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            result = AddFuelResult.Cancel;
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            this.Hide();
        }
    }
}

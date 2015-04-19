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

    public enum AddFuelResult
    {
        AddFuel,
        Cancel
    }

    public sealed partial class CdAddFuel : ContentDialog
    {

        public AddFuelResult result { get; private set; } 

        public CdAddFuel()
        {
            this.InitializeComponent();
            this.Closing += ContentDialog_Closing;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Refueling refueling = new Refueling();
            
            //Get user input
            refueling.date = date.Date.DateTime;

            if (!HelperFunctions.convertStringToDouble(amount.Text, ref refueling.amount))
            {
                //Error
            }

            if (!HelperFunctions.convertStringToDouble(tbCosts.Text, ref refueling.costs))
            {
                //Error
            }

            if (!HelperFunctions.convertStringToDouble(tbDistance.Text, ref refueling.drivenDistance))
            {
                //Error
            }
            
            refueling.isCompleteFilled = Convert.ToBoolean(completeFilled.IsChecked);
            
            //Add new refueling to refueling list            
            (App.Current as App).dataManager.addRefuling(refueling);

            //Save new refueling list
            (App.Current as App).dataManager.saveRefuelings();

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

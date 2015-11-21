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
    public enum EditRefuelingResult
    {
        edit,
        delete,
        cancel
    }

    public sealed partial class CdEditRefueling : ContentDialog
    {

        public EditRefuelingResult result { get; private set; }

        private Refueling oldRefueling;

        public CdEditRefueling(Refueling oldRefueling)
        {
            this.InitializeComponent();
            this.oldRefueling = oldRefueling;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Refueling newRefueling = new Refueling();

            //Get user input
            newRefueling.date = date.Date.DateTime;

            if (!HelperFunctions.convertStringToDouble(amount.Text, ref newRefueling.amount))
            {
                //Error
            }

            if (!HelperFunctions.convertStringToDouble(tbCosts.Text, ref newRefueling.costs))
            {
                //Error
            }

            if (!HelperFunctions.convertStringToDouble(tbDistance.Text, ref newRefueling.drivenDistance))
            {
                //Error
            }

            newRefueling.isCompleteFilled = Convert.ToBoolean(completeFilled.IsChecked);
            
            (App.Current as App).dataManager.editRefueling(oldRefueling, newRefueling);                     //Edit refueling

            (App.Current as App).dataManager.saveRefuelings();                                              //Save refuelings

            result = EditRefuelingResult.edit;
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            result = EditRefuelingResult.cancel;
        }

        private void bDeleteRefueling_Click(object sender, RoutedEventArgs e)
        {
            (App.Current as App).dataManager.deleteRefueling(oldRefueling);
            result = EditRefuelingResult.delete;
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            this.Hide();
        }

    }
}

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
    public sealed partial class CdAddFuel : ContentDialog
    {
        public CdAddFuel()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            RefuelingItem refuling = new RefuelingItem();
            
            //Get user input
            refuling.date = date.Date.DateTime;
            refuling.amount = Convert.ToDouble(amount.Text);
            refuling.costs = Convert.ToDouble(tbCosts.Text);
            refuling.drivenDistance = Convert.ToDouble(tbDistance.Text);
            refuling.isCompleteFilled = Convert.ToBoolean(completeFilled.IsChecked);

            //Add refuling item to database
            this.InsertRefueling(refuling);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }


        private int InsertRefueling(RefuelingItem refuling)
        {
            //validate parameters
            if (refuling.isCompleteFilled)
            {
                //Add instance of RefulingItem to database
                App.DB.RefuelingItems.InsertOnSubmit(refuling);
                App.DB.SubmitChanges();

                //Function complete and successful executed
                return 0;
            }

            //cause of missing input data adding the refueling item was not successful
            return 1;
        }

        private int InsertRefueling(DateTime date, double amount, bool isCompleteFilled, double costs, double drivenDistance)
        {

            //Create Instance of RefulingItem
            RefuelingItem refuling = new RefuelingItem();
            refuling.date = date;
            refuling.amount = amount;
            refuling.isCompleteFilled = isCompleteFilled;
            refuling.costs = costs;
            refuling.drivenDistance = drivenDistance;

            //validate parameters
            if (refuling.isCompleteFilled)
            {
                //Add instance of RefulingItem to database
                App.DB.RefuelingItems.InsertOnSubmit(refuling);
                App.DB.SubmitChanges();

                //Function complete and successful executed
                return 0;
            }

            //cause of missing input data adding the refueling item was not successful
            return 1;
        }
    }
}

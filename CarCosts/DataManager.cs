using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;

namespace CarCosts
{
    public class DataManager
    {

        private const string XML_REFUELING_DATA_FILENAME = "refuelingData.xml";
        private List<Refueling> refuelings;
        private double averageLiterPerKilometer;


        #region refuling

        public double getAverageLitersPerKilometers()
        {
            return this.averageLiterPerKilometer;
        }

        private void recalculateAverageLitersPerKilometer()
        {
            double sumRefuelungAmount = 0.0;
            double sumDrivenDistance = 0.0;
            foreach (Refueling reful in this.refuelings)
            {
                sumRefuelungAmount += reful.amount;
                sumDrivenDistance += reful.drivenDistance;
            }
            this.averageLiterPerKilometer = sumRefuelungAmount / sumDrivenDistance;
        }

        /// <summary>
        /// Saves refuling data in XML file
        /// </summary>
        /// <param name="refuelings">List that contains all refuling data that should be saved.</param>
        /// <returns>0 on success, else != 0</returns>
        public async Task saveRefuelings()
        {
            var serializer = new DataContractSerializer(typeof(List<Refueling>));
            
            using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync(
                   XML_REFUELING_DATA_FILENAME,
                   CreationCollisionOption.ReplaceExisting))
            {
                serializer.WriteObject(stream, this.refuelings);
            }
        }

        /// <summary>
        /// Loads alle saved refueling data from XML file
        /// </summary>
        /// <returns>List that contains all saved refueling Data</returns>
        public async Task loadRefuelingsAsync()
        {
            var serializer = new DataContractSerializer(typeof(List<Refueling>));

            var myStream = ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(XML_REFUELING_DATA_FILENAME);
            
            try
            {
                using (var stream = await ApplicationData.Current.LocalFolder.OpenStreamForReadAsync(
                       XML_REFUELING_DATA_FILENAME))
                {
                    //Set refuelings
                    this.refuelings = (List<Refueling>)serializer.ReadObject(stream);
                    
                    //Recalculate average fuel consumption
                    this.recalculateAverageLitersPerKilometer();
                }
            }
            catch (FileNotFoundException e)
            {
                this.refuelings = new List<Refueling>();
            }
        }

        public List<Refueling> getRefulings()
        {
            return this.refuelings;
        }

        /// <summary>
        /// Adds a refuling to the refulings list.
        /// </summary>
        /// <param name="refuling"></param>
        /// <returns>Value != 0 on error</returns>
        public int addRefuling(Refueling refueling)
        {
            if (Refueling.isComplete(refueling))
            {
                //Add refueling to List
                this.refuelings.Add(refueling);

                //Recalculate average fuel consumption
                this.recalculateAverageLitersPerKilometer();

                //Add refueling successfully
                return 0;
            }

            //Parameter refueling is not complete
            return 1;
        }

        #endregion


        #region repairs
        #endregion

        #region tax
        #endregion

        #region insurance
        #endregion
    }
}

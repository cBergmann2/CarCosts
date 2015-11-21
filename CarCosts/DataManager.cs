using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Windows.Storage;
using System.Collections.ObjectModel;
using SQLite;

namespace CarCosts
{
    public class DataManager
    {

        private double averageLiterPerKilometer;

        private SQLiteConnection dbConn;                        //Instance variable for database connection


        /// <summary>
        /// Create table(s) {Refueling} if not exists 
        /// </summary>
        /// <param name="DB_PATH"></param>
        /// <returns></returns>
        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!CheckFileExists(DB_PATH).Result)
                {
                    using (dbConn = new SQLiteConnection(DB_PATH))
                    {
                        dbConn.CreateTable<Refueling>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if file exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>true if file exists</returns>
        private async Task<bool> CheckFileExists(string fileName)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region refuling

        /// <summary>
        /// Inserts new refueling to database
        /// </summary>
        /// <param name="newRefuling"></param>
        public void addRefueling(Refueling newRefuling)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newRefuling);
                });
            }
        }

        /// <summary>
        /// Returns refueling with Id == refuelingId
        /// </summary>
        /// <param name="refulingId">Id of requested refueling</param>
        /// <returns>Requested refueling if exists otherwise null</returns>
        public Refueling getRefueling(int refulingId)
        {
            try
            {
                using (var dbConn = new SQLiteConnection(App.DB_PATH))
                {
                    var existingconact = dbConn.Query<Refueling>("select * from Refueling where Id =" + refulingId).FirstOrDefault();
                    return existingconact;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns all existing refuelings
        /// </summary>
        /// <returns>All existing refuelings</returns>
        public ObservableCollection<Refueling> getAllRefuelings()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Refueling> myCollection = dbConn.Table<Refueling>().ToList<Refueling>();
                ObservableCollection<Refueling> ContactsList = new ObservableCollection<Refueling>(myCollection);
                return ContactsList;
            }
        }

        /// <summary>
        /// Updates the refueling
        /// </summary>
        /// <param name="refuling"></param>
        public void updateRefueling(Refueling refuling)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRefueling = dbConn.Query<Refueling>("select * from Refueling where Id =" + refuling.Id).FirstOrDefault();
                if (existingRefueling != null)
                {
                    //TO DO: change
                    existingRefueling.date = refuling.date;
                    existingRefueling.amount = refuling.amount;
                    existingRefueling.costs = refuling.costs;
                    existingRefueling.drivenDistance = refuling.drivenDistance;
                    existingRefueling.isCompleteFilled = refuling.isCompleteFilled;
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(existingRefueling);
                    });
                }
            }
        }

        /// <summary>
        /// Delete specific refueling
        /// </summary>
        /// <param name="Id"></param>
        public void deleteRefueling(int Id)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingRefueling = dbConn.Query<Refueling>("select * from Refueling where Id =" + Id).FirstOrDefault();
                if (existingRefueling != null)
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Delete(existingRefueling);
                    });
                }
            }
        }

        /// <summary>
        /// Delete all refuelings
        /// </summary>
        public void deleteAllRefuelings()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                //dbConn.RunInTransaction(() => 
                //   { 
                dbConn.DropTable<Refueling>();
                dbConn.CreateTable<Refueling>();
                dbConn.Dispose();
                dbConn.Close();
                //}); 
            }
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

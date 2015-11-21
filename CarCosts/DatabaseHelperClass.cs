using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Collections.ObjectModel;

namespace CarCosts
{
    
    
    
    /// <summary>
    /// Outdated!
    /// This class for perform all database CRUD operations 
    /// </summary>
    public class DatabaseHelperClass
    {
        SQLiteConnection dbConn;

        //Create Tabble 
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

        // Retrieve the specific contact from the database. 
        public Refueling readRefuling(int refulingId)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingconact = dbConn.Query<Refueling>("select * from Refueling where Id =" + refulingId).FirstOrDefault();
                return existingconact;
            }
        }

        // Retrieve the all contact list from the database. 
        public ObservableCollection<Refueling> readRefuling()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<Refueling> myCollection = dbConn.Table<Refueling>().ToList<Refueling>();
                ObservableCollection<Refueling> ContactsList = new ObservableCollection<Refueling>(myCollection);
                return ContactsList;
            }
        }

        //Update existing conatct 
        public void UpdateRefueling(Refueling refuling)
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

        // Insert the new contact in the Contacts table. 
        public void Insert(Refueling newRefuling)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.RunInTransaction(() =>
                {
                    dbConn.Insert(newRefuling);
                });
            }
        }

        //Delete specific contact 
        public void DeleteRefueling(int Id)
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

        //Delete all contactlist or delete Contacts table 
        public void DeleteAllRefueling()
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
    }
}

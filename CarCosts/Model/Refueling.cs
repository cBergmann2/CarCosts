using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace CarCosts
{
    public class RefuelingDataContext : DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/CarCosts.sdf";

        public RefuelingDataContext() : base(DBConnectionString) { }

        public Table<RefuelingItem> RefuelingItems;

    }

    [Table(Name = "Refuelings")]
    public class RefuelingItem
    {
        //Intern ID
        [Column(IsPrimaryKey = true,
            AutoSync = AutoSync.OnInsert,
            DbType = "INT IDENTITY",
            IsDbGenerated = true)]
        public int ID {get; set;}

        //Date of refueling action
        [Column(CanBeNull = false)]
        public DateTime date { get; set; }

        //Refueling amount
        [Column]
        public double amount { get; set; }

        //true if tank was complete filled
        [Column]
        public bool isCompleteFilled { get; set; }

        //total costs of refueling
        [Column]
        public double costs { get; set; }

        //driven distance since last refueling
        [Column]
        public double drivenDistance {get; set;}


        public bool isCompleteDataset()
        {
            if (this.amount == 0)
            {
                return false;
            }

            if (this.costs == 0)
            {
                return false;
            }

            if (this.drivenDistance == 0)
            {
                return false;
            }
            
            //everythings seems to be ok
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCosts
{
    public class Refueling
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int Id { get; set; }
        public DateTime date { get; set; }           //Date of refueling action
        public double amount { get; set; }           //Refueling amount
        public bool isCompleteFilled { get; set; }   //true if tank was complete filled
        public double costs { get; set; }            //total costs of refueling
        public double drivenDistance { get; set; }   //driven distance since last refueling

        public static bool isComplete(Refueling refueling)
        {
            if (refueling.amount == 0) { return false; }
            if (refueling.drivenDistance == 0) { return false; }

            //All needed attributes are set
            return true;
        }
    }
}

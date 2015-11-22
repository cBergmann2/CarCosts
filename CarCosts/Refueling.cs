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
        public double literPer100Kilometer { get; set; }

        public static bool isComplete(Refueling refueling)
        {
            if (refueling.amount == 0) { return false; }
            if (refueling.drivenDistance == 0) { return false; }

            //All needed attributes are set
            return true;
        }

        /// <summary>
        /// Calculates average oil consumption since last refueling.
        /// Writes only an result, if the tank was completely filled.
        /// </summary>
        /// <returns>true if tank was completely filled, else false</returns>
        public bool calculateLiterPer100Kilometer()
        {
            if (this.amount != 0.0 && drivenDistance != 0.0 && isCompleteFilled)
            {
                //To Do: Check if the tank was completely filled when it was last time refueled
                this.literPer100Kilometer = this.amount / this.drivenDistance * 100;    //calculate average oil consumption per 100 km
                return true;
            }

            return false;
        }
    }
}

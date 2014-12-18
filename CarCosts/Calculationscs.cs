using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCosts
{
    class Calculationscs
    {

        private Refueling[] refulings;

        public static double literPer100Kilometer(double km, double l)
        {
            return l / km / 100;
        }

        public double averageLiterPer100Kilometer()
        {
            double sumKm = 0;
            double sumFuelAmount = 0;

            foreach(Refueling reful in refulings){
                if(reful.isCompleteFilled){
                    sumKm += reful.drivenDistance;
                    sumFuelAmount += reful.amount;
                }
            }

            return sumFuelAmount / sumKm / 100;
        }

        public double averageFuelCostsPerYear()
        {
            return -1;
        }

        public double averageFuelCostsPerMounth()
        {
            return -1;
        }

        public double averageFuelCostsPerWeek()
        {
            return -1;
        }

        public double averageFuelCostsPerDay()
        {
            return -1;
        }

        public double averageFuelCostsperKm()
        {
            return -1;
        }
    }
}

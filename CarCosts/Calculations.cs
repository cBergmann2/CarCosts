using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCosts
{
    public class Calculations
    {

        private List<Refueling> refulings;

        public void setRefuelings(List<Refueling> refuelings)
        {
            this.refulings = refuelings;
        }


        public static double literPer100Kilometer(double km, double l)
        {
            return l / km * 100;
        }

        public double getBestFuelConsumption() 
        {
            double value = -1;
            if(this.refulings.Count > 0){
                foreach(Refueling reful in this.refulings){
                    if(value == -1){
                        value = Calculations.literPer100Kilometer(reful.drivenDistance, reful.amount);
                    }
                    else{
                        if(value > Calculations.literPer100Kilometer(reful.drivenDistance, reful.amount)){
                            value = Calculations.literPer100Kilometer(reful.drivenDistance, reful.amount);
                        }
                    }
                }
            }
            return value;
        }

        public double getWorstFuelConsumption()
        {
            double value = -1;
            if (this.refulings.Count > 0)
            {
                foreach (Refueling reful in this.refulings)
                {
                    if (value == -1)
                    {
                        value = Calculations.literPer100Kilometer(reful.drivenDistance, reful.amount);
                    }
                    else
                    {
                        if (value < Calculations.literPer100Kilometer(reful.drivenDistance, reful.amount))
                        {
                            value = Calculations.literPer100Kilometer(reful.drivenDistance, reful.amount);
                        }
                    }
                }
            }
            return value;
        }


        public double averageLiterPer100Kilometer()
        {
            double sumKm = 0;
            double sumFuelAmount = 0;

            foreach(Refueling reful in refulings){
                //if(reful.isCompleteFilled){
                    sumKm += reful.drivenDistance;
                    sumFuelAmount += reful.amount;
                //}
            }

            return sumFuelAmount / sumKm * 100;
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

        public static double evaluateFuelConsumption(double litersPerKilometer)
        {
            if ((App.Current as App).dataManager.getAverageLitersPerKilometers() != 0)
            {
                return (litersPerKilometer / (App.Current as App).dataManager.getAverageLitersPerKilometers());
            }
            else
            {
                return 1.0;
            }
        }
    }
}

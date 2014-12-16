using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarCosts
{
    class Refueling
    {
        public DateTime date;           //Date of refueling action
        public double amount;           //Refueling amount
        public bool isCompleteFilled;   //true if tank was complete filled
        public double costs;            //total costs of refueling
        public double drivenDistance;   //driven distance since last refueling

    }
}

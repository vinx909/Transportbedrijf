using System;
using System.Collections.Generic;

namespace Transportbedrijf
{
    internal class TransportCost
    {
        private const double percentageTotal = 100;

        private const string cargoFormOptionOneName = "not liquid";
        private const double cargoFormOptionOnePricePerCubicMeterPerKm = 0.8;
        private const double cargoFormOptionOnePricePerKgPerKm = 0.55;
        private const string cargoFormOptionTwoName = "liquid";
        private const double cargoFormOptionTwoPricePerCubicMeterPerKm = 1.25;
        private const double cargoFormOptionTwoPricePerKgPerKm = 0.45;

        private const double kmAwayAdditionPercentage = 45;
        private const double customsAdditionPercentage = 3.5;
        private const double customsMinimum = 45;

        private static List<CargoForm> cargoForms;
        internal static string[] GetCargoFormOption()
        {
            SetupCargoForms();
            string[] toReturn = new string[cargoForms.Count];
            for(int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = cargoForms[i].GetName();
            }
            return toReturn;
        }
        internal static double CalculateCost(int volume, int weight, string formOption, int kmLocal, int kmAway, int value)
        {
            double effectiveKm = kmLocal + kmAway / percentageTotal * (percentageTotal + kmAwayAdditionPercentage);
            double cost = 0;
            foreach(CargoForm form in cargoForms)
            {
                if (form.IsSameAsName(formOption))
                {
                    cost += form.GetPricePerCubicMeterPerKm() * volume * effectiveKm;
                    cost += form.GetPricePerKgPerKm() * weight * effectiveKm;
                    break;
                }
            }
            if (kmAway > 0)
            {
                cost += Math.Max(value / percentageTotal * customsAdditionPercentage, customsMinimum);
            }
            return cost;
        }
        private static void SetupCargoForms()
        {
            if (cargoForms == null)
            {
                cargoForms = new List<CargoForm>();
                cargoForms.Add(new CargoForm(cargoFormOptionOneName, cargoFormOptionOnePricePerCubicMeterPerKm, cargoFormOptionOnePricePerKgPerKm));
                cargoForms.Add(new CargoForm(cargoFormOptionTwoName, cargoFormOptionTwoPricePerCubicMeterPerKm, cargoFormOptionTwoPricePerKgPerKm));
            }
        }
        internal class CargoForm
        {
            private string name;
            private double pricePerCubicMeterPerKm;
            private double pricePerKgPerKm;

            internal CargoForm(string name, double pricePerCubicMeterPerKm, double pricePerKgPerKm)
            {
                this.name = name;
                this.pricePerCubicMeterPerKm = pricePerCubicMeterPerKm;
                this.pricePerKgPerKm = pricePerKgPerKm;
            }
            internal string GetName()
            {
                return name;
            }
            internal double GetPricePerCubicMeterPerKm()
            {
                return pricePerCubicMeterPerKm;
            }
            internal double GetPricePerKgPerKm()
            {
                return pricePerKgPerKm;
            }
            internal bool IsSameAsName(string toTest)
            {
                if (toTest.Equals(name))
                {
                    return true;
                }
                return false;
            }
        }
    }
}
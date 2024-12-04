using System;
using System.Collections.Generic;
using Talabat.core.Services.Contract;
using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.Applacation
{
    public class TaxRegionService : ITaxRegion
    {

        private readonly Dictionary<string, decimal> _taxRates = new()
        {
            { TaxRegion.Cairo.ToString(), 0.10m },       // 10%
            { TaxRegion.Giza.ToString(), 0.08m },        // 8%
            { TaxRegion.Alexandria.ToString(), 0.07m },  // 7%
            { TaxRegion.Dakahlia.ToString(), 0.06m },    // 6%
            { TaxRegion.Sharqia.ToString(), 0.05m }      // 5%
        };

        public decimal GetTaxRate(TaxRegion region)
        {
            return _taxRates.TryGetValue(region.ToString(), out var rate) ? rate : 0m;
        }

        public decimal CalculateTax(decimal subTotal, TaxRegion region)
        {
            var taxRate = GetTaxRate(region);
            return subTotal * taxRate;
        }


    }
}

using Talabat.core.Entitys.Order_Aggregate.Enum_Order_Aggregate;

namespace Talabat.core.Services.Contract
{
    public interface ITaxRegion
    {

         decimal  GetTaxRate(TaxRegion region);

         decimal CalculateTax(decimal subTotal, TaxRegion region);
    }
}

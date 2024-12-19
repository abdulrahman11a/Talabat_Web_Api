using Talabat.core.Entitys.Order_Aggregate;

namespace Talabat.core.Specifications.Order_Spec
{
    public class OrderBaseSpecification : BaseSpecification<Order>
    {
        public OrderBaseSpecification():base()
        {
            
        }
        // Constructor for filtering by buyer email
        public OrderBaseSpecification(string buyerEmail)
            : base(o => o.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            ApplyOrderByDescending(o => o.OrderDate);
        }

        // Constructor for filtering by order ID and buyer email
        public OrderBaseSpecification(int orderId, string buyerEmail)
            : base(o => o.BuyerEmail == buyerEmail && o.Id == orderId)
        {
            AddIncludes();
        }
        public void UpdateCriteriaForPaymentIntentId(string paymentIntentId)
        {
            Criteria = o => o.paymentInterntId == paymentIntentId;
        }

        // Private helper to add common includes
        private void AddIncludes()
        {
            Includes.Add(o => o.DeliveryMethod);
            Includes.Add(o => o.Items);
        }
    }
}

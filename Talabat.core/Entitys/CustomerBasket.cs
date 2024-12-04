namespace Talabat.core.Entitys
{
    public class CustomerBasket
    {

        public  string Id { get; set; } //GUID
        public List<BasketItem> Items { get; set; }
        public CustomerBasket(string id)
        { Id = id; }
    }
}

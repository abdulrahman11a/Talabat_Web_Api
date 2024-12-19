namespace Talabat.core.Specifications.Product_Spec
{
    public class ProductSpecparams
    {
        private const  int MaxpageSize = 10;
        private int pageSize { get; set; } = 10;


        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxpageSize ? MaxpageSize : value; }
        }
        public int PageIndex { get; set; } = 1;
        public string? sort { get; set; }        
        public int? BrandId { get; set; }    
        public int? TypeId { get; set; }    
        public int? Take { get; set; }
        public int? Skip { get; set; }
        public string? search { get; set; }

        public string? Search
        {
            get { return search; }
            set { search = value?.ToLower(); }
        }

        public bool IsPaginationEnabled { get; set; }/*=true;*/



    }
}

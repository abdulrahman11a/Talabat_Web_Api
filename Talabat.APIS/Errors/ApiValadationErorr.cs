using System.Security.Principal;

namespace Talabat.APIS.Errors
{
    public class ApiValadationErorr : ApiResponse
    {

        public IEnumerable<string> errors { get; set; }
        public ApiValadationErorr() : base(400)
        {

            errors = new List<string>();    
        }
    }
}

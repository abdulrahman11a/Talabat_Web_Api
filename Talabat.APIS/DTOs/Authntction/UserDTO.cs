using System.ComponentModel.DataAnnotations;

namespace Talabat.APIS.DTOs.Authntction
{
    public class UserDTO
    {
        public string DisplayName { get; set; }

        public string Email { get; set; }

        public string Token { get; set; }
    }
}
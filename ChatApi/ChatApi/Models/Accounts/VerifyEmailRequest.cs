using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models.Accounts
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
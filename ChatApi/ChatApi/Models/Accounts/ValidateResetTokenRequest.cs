using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models.Accounts
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
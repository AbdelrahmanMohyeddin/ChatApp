using System.ComponentModel.DataAnnotations;

namespace ChatApi.Models.Accounts
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
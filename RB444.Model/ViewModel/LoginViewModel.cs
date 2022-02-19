using System.ComponentModel.DataAnnotations;

namespace RB444.Model.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }        

        // Because of using _UserLayout page on my profile
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public int Role { get; set; }
        public float AssignCoin { get; set; }
        public float Commision { get; set; }
        public int ExposureLimit { get; set; }
        public string MobileNumber { get; set; }
    }
}

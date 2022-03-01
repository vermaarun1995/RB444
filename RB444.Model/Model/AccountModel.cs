﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RB444.Models.Model
{
    public class UserModel
    {
        public int userid { get; set; }
        public string fullname { get; set; }
        public string email { get; set; }
        public string phoneno { get; set; }
        public string address { get; set; }
        public int roleid { get; set; }
        public string access_token { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email can't be blank")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "Password can't be blank")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public bool rememberme { get; set; }
    }

    public class VerifyEmailModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
    }

    public class ResetPasswordModel
    {
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }

    public class ForgotPasswordModel
    {
        public string EmailAddress { get; set; }
    }

    public class LoginWith2faViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "Authenticator code")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "Remember this machine")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }

    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Recovery Code")]
        public string RecoveryCode { get; set; }
    }

    public class RegisterViewModel
    {
        public int id { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        //[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool RollingCommission { get; set; }
        public long AssignCoin { get; set; }
        public float Commision { get; set; }
        public long ExposureLimit { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
    }

    public class RegisterVM
    {
        public int id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }      
    }

    public class RegisterPagingVM
    {
        public List<RegisterVM> RegisterVMs { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int TotalRecord { get; set; }
        public string ShowPageInfo { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        public string UserId { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}

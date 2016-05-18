using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Resources;

namespace Web.ViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(ResourceType = typeof (Account), Name = "VerifyCodeViewModel_Code")]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        [Display(ResourceType = typeof (Account), Name = "VerifyCodeViewModel_RememberBrowser")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_PasswordRequired")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Password")]
        public string Password { get; set; }

        [Display(ResourceType = typeof (Account), Name = "ViewModel_RememberMe")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [EmailAddress(ErrorMessageResourceType = typeof (Account), ErrorMessageResourceName = "ViewModel_EmailInvalid")]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_PasswordLengthError", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_ConfirmPasswordNoMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [EmailAddress]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_PasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_PasswordLengthError", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_ConfirmPassword")]
        [Compare("Password", ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_ConfirmPasswordNoMatch")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Account),
            ErrorMessageResourceName = "ViewModel_EmailRequired")]
        [EmailAddress]
        [Display(ResourceType = typeof (Account), Name = "ViewModel_Email")]
        public string Email { get; set; }
    }
}
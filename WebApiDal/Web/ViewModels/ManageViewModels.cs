using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Resources;

namespace Web.ViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_NewPasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_PasswordLengthError", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_ConfirmPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_ConfirmPasswordNoMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_OldPasswordRequired")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_OldPassword")]
        public string OldPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_NewPasswordRequired")]
        [StringLength(100, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_PasswordLengthError", MinimumLength = 1)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_NewPassword")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_ConfirmPassword")]
        [Compare("NewPassword", ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_ConfirmPasswordNoMatch")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_PhoneNumberRequired")]
        [Phone]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_PhoneNumber")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_CodeRequired")]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_Code")]
        public string Code { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessageResourceType = typeof (Manage),
            ErrorMessageResourceName = "ViewModel_PhoneNumberRequired")]
        [Phone]
        [Display(ResourceType = typeof (Manage), Name = "ViewModel_PhoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}
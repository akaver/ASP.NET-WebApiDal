using System.Collections.Generic;

namespace Web.ViewModels
{
    // Models returned by ApiAccountController actions.

    public class ApiExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ApiManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<ApiUserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ApiExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class ApiUserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class ApiUserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
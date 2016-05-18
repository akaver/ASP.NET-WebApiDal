using System.ComponentModel;
using System.Web.Mvc;
using Domain.Identity;

namespace Web.Areas.Admin.ViewModels
{
    //http://stackoverflow.com/questions/10092899/asp-net-mvc2-system-missingmethodexception-no-parameterless-constructor-defin
    [Bind(Exclude = "UserSelectList,RoleSelectList")]
    public class EditViewModel
    {
        public UserRoleInt UserRole { get; set; }
        public UserRoleInt OriginalUserRole { get; set; }

        [DisplayName("User")]
        public SelectList UserSelectList { get; set; }

        [DisplayName("Role")]
        public SelectList RoleSelectList { get; set; }
    }
}
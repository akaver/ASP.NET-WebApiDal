using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Interfaces
{
    /// <summary>
    /// Dummy interface - used for identifying UOW type
    /// </summary>
    public interface IWebApiUOW
    {
        string GetWebApiToken(string userName, string password);

    }
}

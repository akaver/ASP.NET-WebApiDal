using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace DAL.Interfaces
{
    public interface IArticleRepository : IEFRepository<Article>
    {
        Article FindArticleByName(string articleName);

        List<Article> AllWithTranslations();

        void DeleteArticleWithTranslations(params object[] id);
    }
}
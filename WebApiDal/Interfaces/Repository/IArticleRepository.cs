using System.Collections.Generic;
using Domain;

namespace Interfaces.Repository
{
    public interface IArticleRepository : IRepository<Article>
    {
        Article FindArticleByName(string articleName);

        List<Article> AllWithTranslations();

        void DeleteArticleWithTranslations(params object[] id);
    }
}
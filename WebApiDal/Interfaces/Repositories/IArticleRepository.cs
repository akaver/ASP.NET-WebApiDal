using System.Collections.Generic;
using Domain;

namespace Interfaces.Repositories
{
    public interface IArticleRepository : IBaseRepository<Article>
    {
        Article FindArticleByName(string articleName);

        List<Article> AllWithTranslations();

        void DeleteArticleWithTranslations(params object[] id);
    }
}
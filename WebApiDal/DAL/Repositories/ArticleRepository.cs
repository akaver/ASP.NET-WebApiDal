using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Domain;

namespace DAL.Repositories
{
    public class ArticleRepository : EFRepository<Article>, IArticleRepository
    {
        public ArticleRepository(IDbContext dbContext) : base(dbContext)
        {
        }

        public Article FindArticleByName(string articleName)
        {
            var res =
                DbSet.Include(a => a.ArticleHeadline.Translations)
                    .Include(a => a.ArticleBody.Translations)
                    .FirstOrDefault(a => a.ArticleName == articleName) ?? new Article()
                    {
                        ArticleName = "NotFound",
                        ArticleHeadline = new MultiLangString("Article not found!"),
                        ArticleBody = new MultiLangString("Article not found!")
                    };

            return res;
        }

        public List<Article> AllWithTranslations()
        {
            var res =
                DbSet.Include(a => a.ArticleHeadline.Translations)
                    .Include(a => a.ArticleBody.Translations)
                    .OrderBy(a => a.ArticleName)
                    .ToList();
            return res;
        }

        public void DeleteArticleWithTranslations(params object[] id)
        {
            var article = DbSet.Find(id);
            foreach (var translation in article.ArticleHeadline.Translations.ToArray())
            {
                DeleteEntity(translation, DbContext.Set<Translation>());
            }
            DeleteEntity(article.ArticleHeadline, DbContext.Set<MultiLangString>());

            foreach (var translation in article.ArticleBody.Translations.ToArray())
            {
                DeleteEntity(translation, DbContext.Set<Translation>());
            }
            DeleteEntity(article.ArticleBody, DbContext.Set<MultiLangString>());
            Delete(article);
        }

        private void DeleteEntity(Object entity, DbSet dbSet)
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }
    }
}
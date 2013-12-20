using Ninesky.Models;
using System.Linq;

namespace Ninesky.Repository
{
    public class ArticleRepository:RepositoryBase<Article>
    {
        /// <summary>
        ///添加文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns></returns>
        public override bool Add(Article article)
        {
            dbContext.Articles.Add(article);
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article">文章</param>
        /// <returns></returns>
        public override bool Update(Article article)
        {
            dbContext.Articles.Attach(article);
            dbContext.Entry<Article>(article).State = System.Data.EntityState.Modified;
            dbContext.Entry<CommonModel>(article.CommonModel).State = System.Data.EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="Id">文章id</param>
        /// <returns></returns>
        public override bool Delete(int Id)
        {
            dbContext.Articles.Remove(dbContext.Articles.SingleOrDefault(a => a.ArticleId == Id));
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 查找文章
        /// </summary>
        /// <param name="Id">文章id</param>
        /// <returns></returns>
        public override Article Find(int Id)
        {
            return dbContext.Articles.AsNoTracking().Include("CommonModel").SingleOrDefault(a => a.ArticleId == Id);
        }
    }
}
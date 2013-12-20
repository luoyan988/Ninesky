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
        public override bool Add(Article article)
        {
            dbContext.Articles.Add(article);
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article">文章</param>
        public override bool Update(Article article)
        {
            dbContext.Articles.Attach(article);
            dbContext.Entry<Article>(article).State = System.Data.EntityState.Modified;
            dbContext.Entry<ItemModel>(article.CommonModel).State = System.Data.EntityState.Modified;
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="commonModelId">公共模型id</param>
        /// <returns></returns>
        public override bool Delete(int commonModelId)
        {
            dbContext.Items.Remove(dbContext.Items.SingleOrDefault(cM => cM.ItemId == commonModelId));
            dbContext.Articles.Remove(dbContext.Articles.SingleOrDefault(a => a.ItemId == commonModelId));
            return dbContext.SaveChanges() > 0;
        }
        /// <summary>
        /// 查找文章
        /// </summary>
        /// <param name="Id">文章id</param>
        public override Article Find(int Id)
        {
            return dbContext.Articles.AsNoTracking().Include("CommonModel").SingleOrDefault(a => a.ArticleId == Id);
        }
        /// <summary>
        /// 根据公共模型id查找文章
        /// </summary>
        /// <param name="commonModelId">公共模型Id</param>
        /// <returns>文章</returns>
        public Article FindByCModelId(int commonModelId)
        {
            return dbContext.Articles.AsNoTracking().Include("CommonModel").SingleOrDefault(a => a.ItemId == commonModelId);
        }
    }
}
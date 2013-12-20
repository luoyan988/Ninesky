using Ninesky.Models;

namespace Ninesky.Repository
{
    public class RepositoryBase<TModel>
    {
        protected NineskyContext dbContext;
        public RepositoryBase()
        {
            dbContext = new NineskyContext();
        }
        /// <summary>
        /// 添加【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Add(TModel Tmodel) { return false; }
        /// <summary>
        /// 更新【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Update(TModel Tmodel) { return false; }
        /// <summary>
        /// 删除【继承类重写后才能正常使用】
        /// </summary>
        public virtual bool Delete(int Id) { return false; }
        /// <summary>
        /// 查找指定值【继承类重写后才能正常使用】
        /// </summary>
        public virtual TModel Find(int Id) { return default(TModel); }
        ~RepositoryBase()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
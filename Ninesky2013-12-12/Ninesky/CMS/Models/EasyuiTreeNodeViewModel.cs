using System.Collections.Generic;

namespace Ninesky.Models
{
    /// <summary>
    /// Easyui树形节点视图模型
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.11.14
    /// 修改2013.11.27
    /// </remarks>
    /// </summary>
    public class EasyuiTreeNodeViewModel
    {
        /// <summary>
        /// 父id
        /// </summary>
        public int parentid { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string text { get; set; }

        /// <summary>
        ///状态【值为'open' 或 'closed'】
        /// </summary>
        public string state { get; set; }

        /// <summary>
        /// 是否选中
        /// </summary>
        public bool @checked { get; set; }

        /// <summary>
        /// 其他属性
        /// </summary>
        public object attributes { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<EasyuiTreeNodeViewModel> children;

        public EasyuiTreeNodeViewModel()
        {
            children = new List<EasyuiTreeNodeViewModel>();
        }
    }
}
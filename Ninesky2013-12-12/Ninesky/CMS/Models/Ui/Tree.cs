using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ninesky.Models.Ui
{
    /// <summary>
    /// 计划删除用ZtrreNode代替
    /// </summary>
    public class Tree
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  节点状态：'open'或'closed'，默认'open'。
        /// </summary>
        public string state { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string iconCls { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<Tree> children { get; set; }
    }
}
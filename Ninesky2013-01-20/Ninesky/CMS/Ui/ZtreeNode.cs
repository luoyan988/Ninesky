using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ninesky.Ui
{
    /// <summary>
    /// Ztree节点数据
    /// </summary>
    public class ZtreeNode
    {
        public int id { get; set; }
        public int pId { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string iconSkin { get; set; }
        public List<ZtreeNode> children { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace Ninesky.Models
{
    /// <summary>
    /// Json视图模型
    /// <remarks>
    /// 版本v1.0
    /// 创建2013.10.23
    /// 修改2013.11.26
    /// </remarks>
    /// </summary>
    public class JsonViewModel
    {
        /// <summary>
        /// 验证结果【0-成功，1-未登录。】
        /// </summary>
        public int Authentication { get; set; }
        /// <summary>
        /// 操作结果
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 消息列表
        /// </summary>
        public Dictionary<string, string> ValidationList { get; set; }
        public JsonViewModel()
        {
            ValidationList = new Dictionary<string, string>();
        }

        public JsonViewModel(System.Web.Mvc.ModelStateDictionary modelState)
        {
            if(modelState.IsValid)
            {
                this.Success = true;
                this.Message = "数据验证成功。";
            }
            else
            {
                this.Success = false;;
                this.Message = "数据验证失败。";
                var _eItem = modelState.Where(m => m.Value.Errors.Count > 0);
                string _errorMessage;
                ValidationList = new Dictionary<string, string>();
                foreach (var i in _eItem)
                {
                    _errorMessage = string.Empty;
                    foreach (var m in i.Value.Errors)
                    {
                        _errorMessage += m.ErrorMessage;
                    }
                    this.ValidationList.Add(i.Key, _errorMessage);
                }
            }
        }
    }
}
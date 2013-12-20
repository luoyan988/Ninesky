using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 网站信息设置
    ///<remarks>
    ///版本v1.0
    ///创建：2013-8-1
    ///修改：2013-8-4
    /// </remarks>
    /// </summary>
    public class SiteConfig
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 网站名称
        /// </summary>
        [Required(ErrorMessage="必须输入网站名称!")]
        [StringLength(50,MinimumLength=4, ErrorMessage="必须是4-50个字符!")]
        [Display(Name="网站名称",Description="必填,4-50个字符。")]
        public string Name { get; set; }
        /// <summary>
        /// 网站标题
        /// </summary>
        [Required(ErrorMessage = "必须输入网站标题!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "必须是4-50个字符!")]
        [Display(Name = "网站标题", Description = "必填,4-50个字符。")]
        public string Title { get; set; }
        /// <summary>
        /// 网站地址
        /// </summary>
        [Required(ErrorMessage = "必须输入网站地址!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "必须是4-50个字符!")]
        [Display(Name = "网站地址", Description = "必填,4-50个字符。")]
        public string Url { get; set; }
        /// <summary>
        /// Logo地址
        /// </summary>
        [StringLength(255, ErrorMessage = "必须少于255个字符!")]
        [Display(Name = "Logo地址", Description = "小于255个字符。")]
        public string LogoUrl { get; set; }
        /// <summary>
        /// Meta描述语
        /// </summary>
        [Required(ErrorMessage = "必须输入Meta描述语!")]
        [StringLength(500, ErrorMessage = "描述语之间用“,”隔开，必须少于500个字符!")]
        [Display(Name = "Meta描述语", Description = "小于500个字符。")]
        [DataType(DataType.MultilineText)]
        public string MetaDescription { get; set; }
        /// <summary>
        /// Meta关键字
        /// </summary>
        [Required(ErrorMessage = "必须输入Meta关键字!")]
        [StringLength(500, ErrorMessage = "关键字之间用“,”隔开，必须少于500个字符!")]
        [Display(Name = "Meta关键字", Description = "小于500个字符。")]
        [DataType(DataType.MultilineText)]
        public string MetaKeywords { get; set; }
        /// <summary>
        /// 版权信息
        /// </summary>
        [Required(ErrorMessage = "必须输入版权!")]
        [StringLength(500, ErrorMessage = "必须少于500个字符!")]
        [Display(Name = "版权信息", Description = "支持Html，小于500个字符。")]
        [DataType(DataType.MultilineText)]
        public string Copyright { get; set; }
    }
}
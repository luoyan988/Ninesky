using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 内容模块
    /// </summary>
    public class Module
    {
        [Key]
        public int ModuleId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage="×")]
        [Display(Name="模块名称")]
        public string Name { get; set; }
        /// <summary>
        /// 模块模型
        /// </summary>
        [Required(ErrorMessage = "×")]
        [Display(Name = "模块模型")]
        public string Model { get; set; }
        /// <summary>
        /// 启用模块
        /// </summary>
        [Required(ErrorMessage = "×")]
        [Display(Name = "启用模块")]
        public bool Enable { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        [Required(ErrorMessage = "×")]
        [Display(Name = "说明")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
}
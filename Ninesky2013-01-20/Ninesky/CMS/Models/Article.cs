using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ninesky.Models
{
      /// <summary>
    /// 文章模型
    /// </summary>
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        /// <summary>
        /// 公共模型id
        /// </summary>
        [Display(Name="公共模型编号")]
        [Required(ErrorMessage="×")]
        public int CommonModelId { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [Display(Name="来源")]
        [StringLength(255, ErrorMessage = "×")]
        public string Source { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        [Display(Name = "作者")]
        [StringLength(50, ErrorMessage = "×")]
        public string Author { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        [NotMapped]
        [Display(Name="摘要",Description="最多255个字符。")]
        [StringLength(255, ErrorMessage = "×")]
        public string Intro { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name="内容")]
        [Required(ErrorMessage = "×")]
        [DataType(DataType.Html)]
        public string Content { get; set; }


        public virtual CommonModel CommonModel { get; set; }
    }
}
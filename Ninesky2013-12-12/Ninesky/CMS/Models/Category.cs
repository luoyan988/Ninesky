﻿using System.ComponentModel.DataAnnotations;

namespace Ninesky.Models
{
    /// <summary>
    /// 栏目模型
    /// </summary>
    ///
    public class Category
    {
        [Key]
        [Display(Name = "栏目Id")]
        public int CategoryId { get; set; }
        
        /// <summary>
        /// 栏目名称
        /// </summary>
        [Display(Name="栏目名称",Description="2-20个字符")]
        [Required(ErrorMessage="×")]
        [StringLength(50,ErrorMessage="×")]
        public string Name { get; set; }
        
        /// <summary>
        /// 父栏目编号
        /// </summary>
        [Display(Name="父栏目")]
        [Required(ErrorMessage="×")]
        public int ParentId { get; set; }
        
        /// <summary>
        /// 父栏目路径【根节点的值为0，子节点的值为：0,1,6,76】
        /// </summary>
        [Required()]
        public string ParentPath { get; set; }
        
        /// <summary>
        /// 栏目类型【0-常规栏目；1-单页栏目；2-外部链接】
        /// </summary>
        [Display(Name="栏目类型")]
        [Required(ErrorMessage = "×")]
        public int Type { get; set; }
        
        /// <summary>
        /// 内容模型【仅在栏目为普通栏目时有效】
        /// </summary>
        [Display(Name="内容模型")]
        [StringLength(50, ErrorMessage = "×")]
        public string Model { get; set; }
        
        /// <summary>
        /// 栏目视图
        /// </summary>
        [Display(Name = "栏目视图", Description = "栏目页的视图，最多255个字符。。")]
        [StringLength(255, ErrorMessage = "×")]
        public string CategoryView { get; set; }
        
        /// <summary>
        /// 内容页视图
        /// </summary>
        [Display(Name = "内容视图", Description = "内容页视图，最多255个字符。。")]
        [StringLength(255, ErrorMessage = "×")]
        public string ContentView { get; set; }
        
        /// <summary>
        /// 链接地址
        /// </summary>
        [Display(Name="链接地址",Description="点击栏目时跳转到的链接地址,最多255个字符。")]
        [StringLength(255,ErrorMessage = "×")]
        public string LinkUrl { get; set; }
        
        /// <summary>
        /// 栏目排序
        /// </summary>
        [Display(Name = "栏目排序", Description = "针对同级栏目,数字越小顺序越靠前。")]
        [Required(ErrorMessage = "×")]
        public int Order { get; set; }
        
        /// <summary>
        /// 内容排序
        /// </summary>
        [Display(Name = "内容排序", Description = "栏目所属内容的排序方式。")]
        public int? ContentOrder { get; set; }
        
        /// <summary>
        /// 每页记录数
        /// </summary>
        [Display(Name = "每页记录数", Description = "栏目所属内容的排序方式。")]
        public int? PageSize { get; set; }
        
        /// <summary>
        /// 记录单位
        /// </summary>
        [Display(Name = "记录单位", Description = "记录的数量单位。如文章为“篇”；新闻为“条”。")]
        [StringLength(255, ErrorMessage = "×")]
        public string RecordUnit { get; set; }
        
        /// <summary>
        /// 记录名称
        /// </summary>
        [Display(Name = "记录名称", Description = "记录的名称。如“文章”、“新闻”、“教程”等。")]
        [StringLength(255, ErrorMessage = "×")]
        public string RecordName { get; set; }
        public Category()
        {
            ParentPath = "0";
            Type = 0;
            CategoryView = "Index";
            ContentView = "Index";
            Order = 0;
            ContentOrder = 1;
            PageSize = 20;
            RecordUnit = "条";
            RecordName = "篇";
        }
    }
    /// <summary>
    /// 栏目类型
    /// </summary>
    public enum CategoryType
    {
        常规栏目, 单页栏目, 外部链接
    }
}
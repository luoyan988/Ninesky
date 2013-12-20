using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace Ninesky.Models
{
    /// <summary>
    /// 公共模型
    /// <remarks>
    /// 版本v1.0
    /// 修改2013.11.18
    /// </remarks>
    /// </summary>
    public class ItemModel
    {
        [Key]
        public int ItemId { get; set; }
        /// <summary>
        /// 栏目Id
        /// </summary>
        [Display(Name="栏目")]
        [Required(ErrorMessage="×")]
        public int CategoryId { get; set; }
        /// <summary>
        /// 录入者
        /// </summary>
        [Display(Name="录入者")]
        [Required(ErrorMessage = "×")]
        [StringLength(255, ErrorMessage = "×")]
        public string Inputer { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        [Display(Name="模型名称")]
        [Required()]
        [StringLength(50)]
        public string Model { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name="标题")]
        [Required(ErrorMessage = "×")]
        [StringLength(255, ErrorMessage = "×")]
        public string Title { get; set; }
        /// <summary>
        /// 点击
        /// </summary>
        [Display(Name = "点击")]
        [Required(ErrorMessage = "×")]
        public int Hits { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        [Display(Name = "发布日期")]
        [Required(ErrorMessage = "×")]
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 状态【0-待审核；1-正常】
        /// </summary>
        [Display(Name="状态")]
        [Required(ErrorMessage = "×")]
        public int Status { get; set; }
        /// <summary>
        /// 首页图片
        /// </summary>
        [Display(Name="首页图片")]
        [StringLength(255, ErrorMessage = "×")]
        public string PicUrl { get; set; }
        /// <summary>
        /// 评论状态
        /// </summary>
        [Display(Name="评论状态")]
        [Required(ErrorMessage = "×")]
        public bool CommentStatus { get; set; }
        /// <summary>
        /// 栏目
        /// </summary>
        public virtual Category Category { get; set; }

        public ItemModel()
        {
            ReleaseDate = System.DateTime.Now;
        }

        /// <summary>
        /// 栏目排序列表
        /// </summary>
        [NotMapped]
        public static List<ParameterViewModel> OrderList
        {
            get
            {
                return new List<ParameterViewModel>(){new ParameterViewModel{ Name="默认排序", Value="0"},new ParameterViewModel{ Name="Id降序", Value="1"},
                    new ParameterViewModel{ Name="Id升序", Value="2"},new ParameterViewModel{ Name="发布时间降序", Value="3"},new ParameterViewModel{ Name="发布时间升序", Value="4"},
                new ParameterViewModel{ Name="点击降序", Value="5"},new ParameterViewModel{ Name="点击升序", Value="6"}};
            }
        }
        /// <summary>
        /// 计划删除
        /// </summary>
        [NotMapped]
        public static List<SelectListItem> ContentOrders
        {
            get
            {
                List<SelectListItem> _cOrders = new List<SelectListItem>(7);
                _cOrders.Add(new SelectListItem { Text = "默认排序", Value = "0" });
                _cOrders.Add(new SelectListItem { Text = "Id降序", Value = "1" });
                _cOrders.Add(new SelectListItem { Text = "Id升序", Value = "2" });
                _cOrders.Add(new SelectListItem { Text = "发布时间降序", Value = "3" });
                _cOrders.Add(new SelectListItem { Text = "发布时间升序", Value = "4" });
                _cOrders.Add(new SelectListItem { Text = "点击降序", Value = "5" });
                _cOrders.Add(new SelectListItem { Text = "点击升序", Value = "6" });
                return _cOrders;
            }
        }
        [NotMapped]
        public static List<SelectListItem> ContentStatus
        {
            get
            {
                List<SelectListItem> _cStatus = new List<SelectListItem>(2);
                _cStatus.Add(new SelectListItem { Text = "待审核", Value = "0" });
                _cStatus.Add(new SelectListItem { Text = "正常", Value = "1" });
                return _cStatus;
            }
        }
    }
}
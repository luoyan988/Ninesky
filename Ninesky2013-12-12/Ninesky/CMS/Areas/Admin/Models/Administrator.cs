using System.ComponentModel.DataAnnotations;

namespace Ninesky.Areas.Admin.Models
{
    /// <summary>
    /// 管理员模型
    /// </summary>
    public class Administrator
    {
        [Key]
        public int AdministratorId { get; set; }
        [Display(Name = "系统账号")]
        [Required(ErrorMessage = "×")]
        public bool IsPreset { get; set; }
        [Display(Name="用户名",Description="（必填） 4-20个字符。")]
        [Required(ErrorMessage="×")]
        [StringLength(20,MinimumLength=4,ErrorMessage="×")]
        public string AdminName { get; set; }
        [Display(Name = "密码", Description = "（必填） 6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(256, MinimumLength = 6, ErrorMessage = "×")]
        public string PassWord { get; set; }
        [Display(Name = "姓名", Description = "填写姓名可以更容易识别管理员。")]
        [StringLength(20, ErrorMessage = "×")]
        public string Name { get; set; }
        [Display(Name = "电子邮件", Description = "（必填） 不多于255个字符。")]
        [Required(ErrorMessage = "×")]
        [EmailAddress()]
        [StringLength(256, ErrorMessage = "×")]
        public string Email { get; set; }
    }
}
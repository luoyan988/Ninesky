using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ninesky.Models
{
    /// <summary>
    /// 用户模型
    /// </summary>
    
    public class User
    {
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// 用户组Id
        /// </summary>
        [Display(Name="用户组Id")]
        [Required(ErrorMessage = "×")]
        [System.Web.Mvc.Remote("Exists", "User", ErrorMessage = "用户名已存在")]
        public int GroupId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name="用户名",Description="4-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "×")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [Required(ErrorMessage = "×")]
        [StringLength(512)]
        public string Password { get; set; }
        /// <summary>
        /// 性别【0-男；1-女；2-保密】
        /// </summary>
        [Display(Name="性别")]
        [Required(ErrorMessage = "×")]
        [Range(0,2,ErrorMessage = "×")]
        public byte Gender { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        [Display(Name="Email",Description="请输入您常用的Email。")]
        [Required(ErrorMessage = "×")]
        [EmailAddress(ErrorMessage = "×")]
        public string Email { get; set; }
        /// <summary>
        /// 密保问题
        /// </summary>
        [Display(Name="密保问题",Description="请正确填写，在您忘记密码时用户找回密码。4-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "×")]
        public string SecurityQuestion { get; set; }
        /// <summary>
        /// 密保答案
        /// </summary>
        [Display(Name="密保答案",Description="请认真填写，忘记密码后回答正确才能找回密码。2-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "×")]
        public string SecurityAnswer { get; set; }
        /// <summary>
        /// QQ号码
        /// </summary>
        [Display(Name="QQ号码")]
        [RegularExpression("^[1-9][0-9]{4-13}$",ErrorMessage = "×")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "×")]
        public string QQ { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name="电话号码",Description="常用的联系电话（手机或固话），固话格式为：区号-号码。")]
        [RegularExpression("^[0-9-]{11-13}$",ErrorMessage = "×")]
        public string Tel { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [Display(Name="联系地址",Description="常用地址，最多80个字符。")]
        [StringLength(80, ErrorMessage = "×")]
        public string Address { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name="邮编")]
        [RegularExpression("^[0-9]{6}$",ErrorMessage = "×")]
        public string PostCode { get; set; }
        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime? RegTime { get; set; }
        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 用户组
        /// </summary>
        public virtual UserGroup Group { get; set; }
    }
    /// <summary>
    /// 用户注册模型
    /// </summary>
    [NotMapped]
    public class UserRegister : User
    {
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name="密码",Description="6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20,MinimumLength=6,ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public new string Password { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码", Description = "再次输入密码。")]
        [Compare("Password", ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码", Description = "请输入图片中的验证码。")]
        [Required(ErrorMessage = "×")]
        [StringLength(6,MinimumLength=6,ErrorMessage = "×")]
        public string VerificationCode { get; set; }
        /// <summary>
        /// 密码已加密
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            return new User { Address = this.Address, Email = this.Email, Gender = this.Gender, GroupId = this.GroupId, Password = Common.Text.Sha256(this.Password), PostCode = this.PostCode, QQ = this.QQ, RegTime = this.RegTime, SecurityAnswer = this.SecurityAnswer, SecurityQuestion = this.SecurityQuestion, Tel = this.Tel, UserName = this.UserName };
        }
    }

    /// <summary>
    /// 用户登陆模型
    /// </summary>
    [NotMapped]
    public class UserLogin
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名", Description = "4-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "×")]
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        [Display(Name = "验证码", Description = "请输入图片中的验证码。")]
        [Required(ErrorMessage = "×")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "×")]
        public string VerificationCode { get; set; }

    }

    /// <summary>
    /// 用户修改模型
    /// </summary>
    [NotMapped]
    public class UserChangePassword
    {
        /// <summary>
        /// 原密码
        /// </summary>
        [Display(Name = "原密码")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        /// <summary>
        /// 新密码
        /// </summary>
        [Display(Name = "新密码", Description = "6-20个字符。")]
        [Required(ErrorMessage = "×")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [Display(Name = "确认密码", Description = "再次输入密码。")]
        [Compare("NewPassword", ErrorMessage = "×")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PetsMapsCF.Models
{
    public class VMLogin
    {
        [Required]
        [StringLength(3, ErrorMessage = "管理員編號不超過3字")]
        [RegularExpression("[A][0-9]{2}", ErrorMessage = "管理員編號格式錯誤")]
        [DisplayName("管理員編號")]
        public string AdminID { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [MaxLength(20, ErrorMessage = "密碼最多為20碼")]
        [DataType(DataType.Password)]
        [DisplayName("密碼")]
        public string AdminPassword { get; set; }
    }

    public class MemVMLogin
    {
        [Required(ErrorMessage = "請輸入帳號(EMAIL)")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("會員帳號(EMAIL)")]
        public string MemberAccount { get; set; }

        [Required(ErrorMessage = "請輸入密碼")]
        [MinLength(8, ErrorMessage = "密碼最少為8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多為20碼")]
        [DataType(DataType.Password)]
        [DisplayName("密碼")]
        public string MemberPassword { get; set; }
    }

    public class ForgetMemVMLogin
    {
        [Required(ErrorMessage = "請輸入帳號(EMAIL)")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("會員帳號(EMAIL)")]
        public string MemberAccount { get; set; }

        [Required(ErrorMessage = "請輸入生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("生日")]
        public DateTime MemberBirthday { get; set; }

        [Required(ErrorMessage = "請輸入手機")]
        [StringLength(10, ErrorMessage = "手機不超過10個數字")]
        [DisplayName("手機")]
        public string MemberMobile { get; set; }

        [Required(ErrorMessage = "請輸入新密碼")]
        [MinLength(8, ErrorMessage = "密碼最少為8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多為20碼")]
        [DataType(DataType.Password)]
        [DisplayName("新密碼")]
        public string MemberPassword { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請再填寫一次密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        [Compare("MemberPassword", ErrorMessage = "兩次輸入不同")]
        public string ConfirmPassword { get; set; }
    }

}
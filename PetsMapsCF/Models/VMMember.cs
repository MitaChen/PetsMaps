using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PetsMapsCF.Models
{
    public class VMMember
    {
        [Key]
        public string MemberID { get; set; }

        [Required(ErrorMessage = "請輸入帳號(EMAIL)")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("會員帳號(EMAIL)")]
        public string MemberAccount { get; set; }

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [MinLength(8, ErrorMessage = "密碼最少為8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多為20碼")]
        [DataType(DataType.Password)]
        public string MemberPassword { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請再填寫一次密碼")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "密碼最少8碼")]
        [MaxLength(20, ErrorMessage = "密碼最多20碼")]
        [Compare("MemberPassword", ErrorMessage = "兩次輸入不同")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "請輸入姓名")]
        [StringLength(30, ErrorMessage = "姓名不超過30字")]
        [DisplayName("會員姓名")]
        public string MemberName { get; set; }

        [Required(ErrorMessage = "請輸入性別(男/女)")]
        [StringLength(1, ErrorMessage = "性別請輸入一字：男/女。")]
        [DisplayName("性別")]
        public string MemberGender { get; set; }

        [Required(ErrorMessage = "請輸入生日")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("生日")]
        public DateTime MemberBirthday { get; set; }

        [Required(ErrorMessage = "請輸入手機")]
        [StringLength(10, ErrorMessage = "手機不超過10個數字")]
        [DisplayName("手機")]
        public string MemberMobile { get; set; }

        [StringLength(100, ErrorMessage = "地址不超過100字")]
        [DisplayName("地址")]
        public string MemberAddress { get; set; }
    }


    public class CheckAccount : ValidationAttribute /*父類別*/
    {
        //建構子
        public CheckAccount()
        {
            ErrorMessage = "此帳號已有人使用。";
        }

        public override bool IsValid(object value) /*子類別*/
        {
            if (value == null)
                value = "aa";

            Context db = new Context();

            var account = db.Members.Where(m => m.MemberAccount == value.ToString()).FirstOrDefault();

            return (account == null) ? true : false;
        }
    }

    }
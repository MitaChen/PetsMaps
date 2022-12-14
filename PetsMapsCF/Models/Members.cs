using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations.Schema;


namespace PetsMapsCF.Models
{
    public class Members
    {
        [Key]
        [StringLength(6, ErrorMessage = "會員編號不超過6字")]
        [RegularExpression("[M][0-9]{5}", ErrorMessage = "帳號格式錯誤")]
        [DisplayName("會員編號")]
        public string MemberID { get; set; }

        [Required(ErrorMessage = "請輸入EMAIL作為帳號")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("會員帳號(EMAIL)")]       
        public string MemberAccount { get; set; }


        [Required(ErrorMessage = "請輸入姓名")]
        [StringLength(30, ErrorMessage = "姓名不超過30字")]
        [DisplayName("會員姓名")]
        public string MemberName { get; set; }

        [Required(ErrorMessage = "請輸入性別(男/女)")]
        [StringLength(1, ErrorMessage = "性別請輸入一字：男/女。")]
        [DisplayName("性別")]
        public string MemberGender { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("生日")]
        [Required(ErrorMessage = "請輸入生日")]
        public DateTime MemberBirthday { get; set; }

        [Required(ErrorMessage = "請輸入手機")]
        [StringLength(10, ErrorMessage = "手機不超過10個數字")]
        [DisplayName("手機")]
        public string MemberMobile { get; set; }

        [StringLength(100, ErrorMessage = "地址不超過100字")]
        [DisplayName("地址")]
        public string MemberAddress { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [DisplayName("創建日期")]
        public DateTime CreateDate { get; set; }


        //field
        string password;  //定義一個password的field

        [DisplayName("密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [MinLength(8, ErrorMessage = "密碼最少為8碼")]
        //[MaxLength(20, ErrorMessage = "密碼最多為20碼")]
        [DataType(DataType.Password)]

        public string MemberPassword
        {
            get
            {
                return password;
            }
            set
            {
                password = getHashPassword(value);
            }
        }

        public static string getHashPassword(string pw)
        {
            byte[] hashValue;
            string result = "";

            System.Text.UnicodeEncoding ue = new System.Text.UnicodeEncoding();

            byte[] pwBytes = ue.GetBytes(pw);

            SHA256 shHash = SHA256.Create();

            hashValue = shHash.ComputeHash(pwBytes);

            foreach (byte b in hashValue)
            {
                result += b.ToString();
            }

            return result;

        }



        //自訂驗證規則的寫法
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
}
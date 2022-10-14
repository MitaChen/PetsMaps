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
    public class Administrators
    {
        [Key]
        [Required]
        [StringLength(3, ErrorMessage = "管理員編號不超過3字")]
        [RegularExpression("[A][0-9]{2}", ErrorMessage = "管理員編號格式錯誤")]
        [DisplayName("管理員編號")]
        public string AdminID { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "姓名不超過30字")]
        [DisplayName("姓名")]
        public string AdminName { get; set; }
        [Required(ErrorMessage = "請輸入密碼")]
        [MaxLength(20, ErrorMessage = "密碼最多為20碼")]
        [DataType(DataType.Password)]
        [DisplayName("密碼")]
        public string AdminPassword { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "權限不超過30字")]
        [DisplayName("權限")]
        public string AdminPermission { get; set; }
    }
}
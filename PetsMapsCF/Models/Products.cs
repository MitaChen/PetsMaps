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
    public class Products
    {
        [Key]
        [StringLength(6, ErrorMessage = "產品編號不超過6字")]
        [RegularExpression("[P][0-9]{5}", ErrorMessage = "產品編號格式錯誤")]
        [DisplayName("產品編號")]
        public string ProductID { get; set; }

        [Required(ErrorMessage = "請輸入產品名稱")]
        [StringLength(50, ErrorMessage = "產品名稱不超過50字")]
        [DisplayName("產品名稱")]
        public string ProductName { get; set; }

        [DisplayName("商品單價")]
        [Required(ErrorMessage = "請輸入商品單價")]
        [Range(0, short.MaxValue, ErrorMessage = "單價不可小於0")]
        public short UnitPrice { get; set; }

        [DisplayName("產品照片")]
        [MaxLength]
        public string ProductImg { get; set; }

    }
}
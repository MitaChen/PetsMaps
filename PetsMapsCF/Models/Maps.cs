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
    public class Maps
    {
        [Key]
        [StringLength(6, ErrorMessage = "地圖編號不超過6字。")]
        [RegularExpression("[K][0-9]{5}", ErrorMessage = "地圖編號格式錯誤。")]
        [DisplayName("地圖編號")]
        public string MapID { get; set; }

        [Required(ErrorMessage = "請輸入店名")]
        [StringLength(50, ErrorMessage = "店名不超過30字")]
        [DisplayName("店名")]
        public string MapName { get; set; }

        [Required(ErrorMessage = "請輸入城市")]
        [StringLength(10, ErrorMessage = "城市不超過10字")]
        [DisplayName("城市")]
        public string MapCity { get; set; }

        [Required(ErrorMessage = "請輸入區域")]
        [StringLength(10, ErrorMessage = "區域不超過10字")]
        [DisplayName("區域")]
        public string MapDistrict { get; set; }

        [Required(ErrorMessage = "請輸入地址")]
        [StringLength(100, ErrorMessage = "地址不超過100字")]
        [DisplayName("地址")]
        public string MapAddress { get; set; }

        [Required(ErrorMessage = "請輸入電話")]
        [StringLength(30, ErrorMessage = "電話不超過30個數字")]
        [DisplayName("電話")]
        public string MapTel { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "介紹不超過500字")]
        [DisplayName("介紹")]
        public string MapInfo { get; set; }

        [DisplayName("照片")]
        [MaxLength]
        public string MapImg { get; set; }
    }
}
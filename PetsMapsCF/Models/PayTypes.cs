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
    public class PayTypes
    {
        [Key]
        [StringLength(3, ErrorMessage = "付款編號不超過3字")]

        public string PayTypeID { get; set; }

        [Required(ErrorMessage = "請輸入付款方式")]
        [StringLength(10, ErrorMessage = "付款方式不超過10字")]
        [DisplayName("付款方式")]
        public string PayTypeName { get; set; }
    }
}
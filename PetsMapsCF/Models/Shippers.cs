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
    public class Shippers
    {
        [Key]
        [StringLength(3, ErrorMessage = "運送編號不超過3字")]
        public string ShipID { get; set; }

        [Required(ErrorMessage = "請輸入運送方式")]
        [StringLength(10, ErrorMessage = "運送方式不超過10字")]
        [DisplayName("運送方式")]
        public string ShipVia { get; set; }
    }
}
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
    public class GroupBuyingOrders
    {
        [Key]
        [DisplayName("訂單編號")]
        [StringLength(12, ErrorMessage = "訂單編號不超過6字")]
        public string OrderID { get; set; }

        [DisplayName("訂單成立日期")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "請填寫收貨人姓名")]
        [StringLength(30, ErrorMessage = "收貨人不超過30字")]
        [DisplayName("收貨人姓名")]
        public string Consignee { get; set; }

        [DisplayName("出貨日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> ShipDate { get; set; }

        [DisplayName("到貨日期")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> DeliveryDate { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "地址不超過100字")]
        [DisplayName("收貨地址")]
        public string ShipAddress { get; set; }

        //Forign Key
        public string ShipID { get; set; }
        public string PayTypeID { get; set; }
        public string MemberID { get; set; }

        //拉關聯
        public virtual Shippers Shipper { get; set; }
        public virtual PayTypes PayType { get; set; }
        public virtual Members Member { get; set; }
    }
}
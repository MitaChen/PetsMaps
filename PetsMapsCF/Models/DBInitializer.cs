using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace PetsMapsCF.Models
{
    public class DBInitializer:DropCreateDatabaseAlways<Context>
    {
        protected override void Seed(Context context)
        {
            base.Seed(context);
            ////////////////////////////////////////////
            ///
            List<Members> members = new List<Members>
            {
                new Members
                {
                   MemberID="M00001",
                   MemberAccount="DOGWANG@gmail.com",
                   MemberPassword="A1234567",
                   MemberName="王小狗",
                   MemberGender="男",
                   MemberBirthday=Convert.ToDateTime("1993/7/19"),
                   MemberMobile="0988888888",
                   MemberAddress="高雄市前鎮區凱旋四路105號",
                   CreateDate=DateTime.Today,
                }
            };

            members.ForEach(s => context.Members.Add(s));
            context.SaveChanges();

            List<Administrators> administrators = new List<Administrators>
            {
                new Administrators
                {
                   AdminID="A01",
                   AdminName="陳貓貓",
                   AdminPassword="123456",
                   AdminPermission="會員、商品管理",
                }
            };

            administrators.ForEach(s => context.Administrators.Add(s));
            context.SaveChanges();
        }

        }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetsMapsCF.Models
{
    public class ChangeIDAuto
    {
        /// <summary>
        /// 自動編碼功能
        /// </summary>
        /// <param name="idIn"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string changeIDNumber(string idIn, string data) 
        { 
            int id = Int32.Parse(idIn.Substring(2))+1;

            string CheckID = data + id.ToString().PadLeft(5,'0');

            return CheckID;
        }

        ///// <summary>
        ///// 地圖編號自動編碼
        ///// </summary>
        ///// <param name="mapidIn"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public string changeMapID(string idIn, string data)
        //{

        //        int id = Int32.Parse(idIn.Substring(2)) + 1;

        //        string CheckMapID = data + id.ToString().PadLeft(2, '0');

        //        return CheckMapID;

        //}
    }   
}
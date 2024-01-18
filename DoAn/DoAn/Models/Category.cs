using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn.Models
{
    public class Category
    {
        [Key]
        public int CatelogyId { get; set; }
        public string CatelogyName { get; set; }
    }
}
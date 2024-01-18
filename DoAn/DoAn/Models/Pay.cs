using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace DoAn.Models
{
    public class Pay
    {
        [Key]
        public int IdPay { get; set; }
        [Required]
        public string HoTenKH { get; set; }
        [Required]
        public string SDT { get; set; }
        [Required]
        public string DiaChi { get; set; }
        [Required]
        public string HinhThucTT { get; set; }
        [Required]
        public Nullable<decimal> TienTT { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
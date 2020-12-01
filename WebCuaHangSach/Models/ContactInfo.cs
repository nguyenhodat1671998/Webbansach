using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class ContactInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public string AddressNumber { get; set; }
        public string Street { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public bool IsDeleted { get; set; }
        

        public Account Account { get; set; }
    }
}
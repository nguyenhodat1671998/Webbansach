using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class Log
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public string Activity { get; set; }
        public DateTime ActivityDate { get; set; }
        [ForeignKey("Account")]
        public int Account_id { get; set; }
        public Account Account { get; set; }

        public Log()
        {

        }
        public Log(int Account_id, string Activity)
        {
            this.Account_id = Account_id;
            this.Activity = Activity;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebCuaHangSach.Models
{
    public class BookType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Book> Books { get; set; }

        public BookType()
        { }

        
    }
}
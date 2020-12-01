using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace WebCuaHangSach.Models
{
    
    public partial class BookContext : DbContext
    {

        public BookContext() : base()
        {
            string databasename = "BookStoreDB";
            this.Database.Connection.ConnectionString = "Data Source=(local);Initial Catalog=" + databasename + ";Trusted_Connection=Yes";
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BookType> BookTypes { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Log> Logs { get; set; }

    }
}

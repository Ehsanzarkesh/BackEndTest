using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using My.Backend.Entities;

namespace My.Backend.DB
{
    public class BookDB : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public BookDB(DbContextOptions options)
        :base(options)
        {
            
        }
    }
}
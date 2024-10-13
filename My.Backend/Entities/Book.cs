using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.Backend.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string? Bookname { get; set; }
        public string? Genre { get; set; }
        public double Price { get; set; }
    }
}
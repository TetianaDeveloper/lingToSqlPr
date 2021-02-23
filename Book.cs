using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSqlLibraryApp
{
    [Table(Name = "Books")]
    public class Book
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public int Pages { get; set; }
        [Column]
        public int AuthorId { get; set; }
    }
}

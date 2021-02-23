using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSqlLibraryApp
{
    [Table(Name = "Authors")]
    public class Author
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Surname { get; set; }
        [Column]
        public int CountryId { get; set; }
        public int BooksCount { get; set; }
    }
}

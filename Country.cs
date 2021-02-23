using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSqlLibraryApp
{
    [Table(Name = "Countries")]
    public class Country
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        public int AuthorsCount { get; set; }
    }
}

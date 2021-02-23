using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToSqlLibraryApp
{
    public class Helper
    {
        private string connectionString = @"Data Source=DESKTOP-RGBDG5L\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private DataContext db;
        public Helper()
        {
            db = new DataContext(connectionString);
        }


        public IEnumerable<Book> GetBooksByPages(int pagesCount)
        {
            return db.GetTable<Book>()
                .Where(b => b.Pages > pagesCount)
                .OrderBy(b => b.Id);
        }

        public IEnumerable<Book> GetBooksByFirstLetter(string letter)
        {
            return db.GetTable<Book>()
                 .Where(b => b.Name.StartsWith(letter));
        }

        public IEnumerable<Book> GetBooksByAuthor(string name, string surname)
        {
            var query = from book in db.GetTable<Book>()
                        join author in db.GetTable<Author>() on book.AuthorId equals author.Id
                        where author.Name == name
                        where author.Surname == surname
                        select book;

            return query;
        }
        public IEnumerable<Book> GetBooksByAuthorCountry(string countryName)
        {
            var query = from book in db.GetTable<Book>()
                        join author in db.GetTable<Author>() on book.AuthorId equals author.Id
                        join country in db.GetTable<Country>() on author.CountryId equals country.Id
                        where country.Name == countryName
                        orderby book.Name
                        select book;
            return query;
        }
        public IEnumerable<Book> GetBooksByNameLenght(int lenght)
        {
            var query = from book in db.GetTable<Book>()
                        where book.Name.Length < lenght
                        select book;
            return query;
        }
        public Book GetBookWithMaxPageCountByAuthorCountry(string countryName)
        {
            Book resBook = new Book();
            var books = from book in db.GetTable<Book>()
                        join author in db.GetTable<Author>() on book.AuthorId equals author.Id
                        join country in db.GetTable<Country>() on author.CountryId equals country.Id
                        where country.Name == countryName
                        select book;
            var result = books.Where(b => b.Pages == books.Max(g => g.Pages));
            foreach (var r in result)
            {
                resBook.Id = r.Id;
                resBook.Name = r.Name;
                resBook.Pages = r.Pages;
                resBook.AuthorId = r.AuthorId;
            }

            return resBook;
        }
        public Author GetAuthorWithMinBooks()
        {            
            var authors = from book in db.GetTable<Book>()
                          join author in db.GetTable<Author>() on book.AuthorId equals author.Id
                          select author;
            var counts = authors.GroupBy(a => new { a.Id, a.Name, a.Surname, a.CountryId })
                .Select(g => new { g.Key.Id, g.Key.Name, g.Key.Surname, g.Key.CountryId, MyCount = g.Count() });

            var result = counts.Where(u => u.MyCount == counts.Min(g => g.MyCount));

            Author author1 = new Author();
            foreach (var r in result)
            {
                author1.Id = r.Id;
                author1.Name = r.Name;
                author1.Surname = r.Surname;
                author1.CountryId = r.CountryId;
                author1.BooksCount = r.MyCount;
            }
            return author1;
        }
        public Country GetCountryWithMaxAuthors()
        {           
            Country resCountry = new Country();
            var countries = from author in db.GetTable<Author>()
                            join country in db.GetTable<Country>() on author.CountryId equals country.Id
                            select country;
            var counts = countries.GroupBy(c => new { c.Id, c.Name })
                .Select(g => new { g.Key.Id, g.Key.Name, MyCount = g.Count() });
            var result = counts.Where(u => u.MyCount == counts.Max(g => g.MyCount));
            foreach (var r in result)
            {
                resCountry.Id = r.Id;
                resCountry.Name = r.Name;
                resCountry.AuthorsCount = r.MyCount;
            }
            return resCountry;
        }
        public IList<Author> getAllAuthors()
        {
            var authors = from book in db.GetTable<Book>()
                          join author in db.GetTable<Author>() on book.AuthorId equals author.Id
                          select author;
            var counts = authors.GroupBy(a => new { a.Id, a.Name, a.Surname, a.CountryId })
                .Select(g => new { g.Key.Id, g.Key.Name, g.Key.Surname, g.Key.CountryId, MyCount = g.Count() });
            IList<Author> authorsList = new List<Author>();
            foreach (var c in counts)
            {
                Author author = new Author();
                author.Id = c.Id;
                author.Name = c.Name;
                author.Surname = c.Surname;
                author.CountryId = c.CountryId;
                author.BooksCount = c.MyCount;
                authorsList.Add(author);
            }
            return authorsList;
        }
        public IList<Country> getAllCountries()
        {
            var countries = from author in db.GetTable<Author>()
                            join country in db.GetTable<Country>() on author.CountryId equals country.Id
                            select country;
            var counts = countries.GroupBy(c => new { c.Id, c.Name })
                .Select(g => new { g.Key.Id, g.Key.Name, MyCount = g.Count() });
            IList<Country> countriesList = new List<Country>();
            foreach (var c in counts)
            {
                Country country = new Country();
                country.Id = c.Id;
                country.Name = c.Name;
                country.AuthorsCount = c.MyCount;
                countriesList.Add(country);
            }
            return countriesList;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqToSqlLibraryApp
{
    public partial class Form1 : Form
    {
        private Helper helper;
        public Form1()
        {
            InitializeComponent();
            helper = new Helper();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            authorsGridView.DataSource = helper.getAllAuthors();
            countriesGridView.DataSource = helper.getAllCountries();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                label1.Text = "К-сть сторінок:";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                label1.Text = "Перша літера:";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = true;
                label1.Text = "Ім'я автора:";
                label2.Text = "Прізвище:";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                label1.Text = "Країна автора:";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                label1.Text = "N символів:";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton6.Checked)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                label1.Text = "Країна автора:";
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
        private void doMethodBtn_Click(object sender, EventArgs e)
        {
            
            if (radioButton1.Checked)
            {
                int pages = int.Parse(textBox1.Text);
                IList<Book> books = helper.GetBooksByPages(pages).ToList();
                booksGridView.DataSource = books;
            }
            else if (radioButton2.Checked)
            {
                string letter = textBox1.Text;
                List<Book> books = helper.GetBooksByFirstLetter(letter).ToList();
                booksGridView.DataSource = books;
            }
            else if (radioButton3.Checked)
            {
                string name = textBox1.Text;
                string surname = textBox2.Text;
                IList<Book> books = helper.GetBooksByAuthor(name, surname).ToList();
                booksGridView.DataSource = books;
            }
            else if (radioButton4.Checked)
            {
                string countryName = textBox1.Text;
                IList<Book> books = helper.GetBooksByAuthorCountry(countryName).ToList();
                booksGridView.DataSource = books;
            }
            else if (radioButton5.Checked)
            {
                int bookNameCount = int.Parse(textBox1.Text);
                IList<Book> books = helper.GetBooksByNameLenght(bookNameCount).ToList();
                booksGridView.DataSource = books;
            }
            else if (radioButton6.Checked)
            {
                string countryName = textBox1.Text;
                Book book = helper.GetBookWithMaxPageCountByAuthorCountry(countryName);
                IList<Book> books = new List<Book>();
                books.Add(book);
                booksGridView.DataSource = books;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            IList<Author> authors = new List<Author>();
            Author author = helper.GetAuthorWithMinBooks();
            authors.Add(author);
            authorsGridView.DataSource = authors;
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            IList<Country> countries = new List<Country>();
            countries.Add(helper.GetCountryWithMaxAuthors());
            countriesGridView.DataSource = countries;
        }

        private void resetAuthorsList_Click(object sender, EventArgs e)
        {
            radioButton7.Checked = false;
            authorsGridView.DataSource = helper.getAllAuthors();
        }

        private void resetCountriesList_Click(object sender, EventArgs e)
        {
            radioButton8.Checked = false;
            countriesGridView.DataSource = helper.getAllCountries();
        }
    }
}

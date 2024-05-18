using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Configuration;
using Microsoft.Win32;
using System.IO;
using ELibraryProject.Database;
using System.Diagnostics;
using System.Security.Policy;
using Microsoft.IdentityModel.Tokens;
using ELibraryProject.Database.Models;

namespace ELibraryProject.AdminPages.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddGoodsPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
	{
		private OpenFileDialog openFileDialog;
        private bool isChange;
        private Book book;

        public AddBookPage()
		{
			InitializeComponent();
            isChange = false;
        }

        public AddBookPage(Book book)
        {
            InitializeComponent();
            isChange = true;

            addBookButton.Content = "Изменить книгу";

            this.book = book;
            title.Text = book.Title;
            author.Text = book.Author;
            price.Text = book.Price.ToString();
            count.Text = book.Count.ToString();
            publicationDate.Text = book.PublicationDate.ToString();
            description.Text = book.Description;
            pageCount.Text = book.PageCount.ToString();
            publisher.Text = book.Publisher;
            category.Text = book.Category;
            coverType.Text = book.CoverType;

            if (book.PicturePath != "")
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "tempImage.jpg");
                File.Copy(GetFullPath(book.PicturePath), tempPath, true);
                Uri fileUri = new Uri(GetFullPath(tempPath));
                BitmapImage bitmapImage = new BitmapImage(fileUri);
                bitmapImage.Freeze();
                image.Source = bitmapImage;
            }
        }


        private void AddBookButton_Click(object sender, RoutedEventArgs e)
		{
            string newPicturePath = GetPicturePath();
            if (isChange)
            {
                book.Title = title.Text;
                book.Author = author.Text;
                book.Price = Convert.ToDecimal(price.Text);
                book.Count = Convert.ToInt32(count.Text);
                book.PublicationDate = Convert.ToInt16(publicationDate.Text);
                book.Description = description.Text;
                book.Publisher = publisher.Text;
                book.PageCount = Convert.ToInt16(pageCount.Text);
                book.PicturePath = newPicturePath != "" ? newPicturePath : book.PicturePath;
                book.CoverType = coverType.Text;
                book.Category = category.Text;
                DatabaseHandler.UpdateBook(book);
            }
            else
            {
                Book newBook = new Book
                {
                    Title = title.Text,
                    Author = author.Text,
                    Price = Convert.ToDecimal(price.Text),
                    Count = Convert.ToInt32(count.Text),
                    PublicationDate = Convert.ToInt16(publicationDate.Text),
                    Description = description.Text,
                    Publisher = publisher.Text,
                    PageCount = Convert.ToInt16(pageCount.Text),
                    PicturePath = newPicturePath,
                    CoverType = coverType.Text,
                    Category = category.Text
                };
                DatabaseHandler.AddBook(newBook);
            }
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                BitmapImage bitmapImage = new BitmapImage(fileUri);
                bitmapImage.Freeze();
                image.Source = bitmapImage;
            }
        }

        private string GetPicturePath()
        {
            string newPicturePath = "";
            if (isChange)
            {
                if (title.Text != book.Title || author.Text != book.Author)
                {
                    
                    File.Move(GetFullPath(book.PicturePath), GetFullPath(GetNewPath()));
                    return GetNewPath();
                }
            }
            if (openFileDialog != null)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                var bitmapImage = new BitmapImage(fileUri);
                image.Source = bitmapImage;


                newPicturePath = GetNewPath();

                using (FileStream fs = new FileStream(GetFullPath(newPicturePath), FileMode.Create))
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                    encoder.Save(fs);
                }
            }
            return newPicturePath;
        }

        private static string GetFullPath(string path)
        {
            return path.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                    .Replace("\\bin\\Debug\\net8.0-windows\\", "");
        }

        private string GetNewPath()
        {
            return Path.Combine("{AppDir}", $"Database\\Pictures\\{title.Text}-{author.Text}.jpeg");
        }
    }
}

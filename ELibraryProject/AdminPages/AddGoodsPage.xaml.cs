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
using ELibraryProject.Classes;
using ELibraryProject.Databases;

namespace ELibraryProject.AdminPages.Pages
{
	/// <summary>
	/// Логика взаимодействия для AddGoodsPage.xaml
	/// </summary>
	public partial class AddGoodsPage : Page
	{
		private OpenFileDialog openFileDialog;

        public AddGoodsPage()
		{
			InitializeComponent();
            title.Foreground = Brushes.LightGray;
            title.Text = "Название";

            title.GotFocus += RemoveText;
            title.LostFocus += AddText;
        }

        private void AttachHandlersForTextBoxes(DependencyObject parent)
        {
            // Ищем все TextBox в элементах управления внутри указанного родительского элемента
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is TextBox textBox)
                {
                    // Привязываем обработчики событий GotFocus и LostFocus к TextBox
                    textBox.GotFocus += RemoveText;
                    textBox.LostFocus += AddText;
                }
                else
                {
                    // Если текущий элемент не TextBox, проверяем его дочерние элементы
                    AttachHandlersForTextBoxes(child);
                }
            }
        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (textBox.Text == "Название")
            {
                textBox.Foreground = Brushes.Black;
                textBox.Text = "";
            }
        }

        private void AddText(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Foreground = Brushes.LightGray;
                textBox.Text = "Название";
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
		{
			Uri fileUri = new Uri(openFileDialog.FileName);
			var bitmapImage = new BitmapImage(fileUri);
			image.Source = bitmapImage;

            
            string newPicturePath = Path.Combine("{AppDir}", $"Databases\\Pictures\\{title.Text}-{author.Text}.jpeg");

            using (FileStream fs = new FileStream(newPicturePath.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
                .Replace("\\bin\\Debug\\net8.0-windows\\", ""), FileMode.Create))
			{
				JpegBitmapEncoder encoder = new JpegBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
				encoder.Save(fs);
			}

            Book newBook = new Book
            {
                Title = title.Text,
                Author = author.Text,
                Price = Convert.ToDecimal(price.Text),
                Count = Convert.ToInt32(count.Text),
                PublicationDate = Convert.ToInt16(writingDate.Text),
                Description = description.Text,
                Publisher = publisher.Text,
                PageCount = Convert.ToInt16(pageCount.Text),
				PicturePath = newPicturePath,
                CoverType = coverType.Text,
                Category = category.Text
            };
			DatabaseHandler.AddBook(newBook);
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                image.Source = new BitmapImage(fileUri);
            }
        }
    }
}

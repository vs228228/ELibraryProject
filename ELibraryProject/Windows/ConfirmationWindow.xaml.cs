using ELibraryProject.Classes;
using ELibraryProject.Managers;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ELibraryProject
{
    /// <summary>
    /// Логика взаимодействия для ConfirmationWindow.xaml
    /// </summary>
    public partial class ConfirmationWindow : Window
    {
        BookView book;
        public ConfirmationWindow(BookView book)
        {
            InitializeComponent();
            this.book = book;
            BookName.Text = book.Title;
            AuthorName.Text = book.Author;
            CountOfBookBlock.Text = $"В наличии {this.book.Count} книг. Сколько желаете заказать?";
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(int.Parse(CountOfBookTextBox.Text) >= book.Count)
                {
                    MessageBox.Show("Нельзя купить книг больше, чем их есть");
                }
                else
                {
                    OrderManager.CreateOrder(book, int.Parse(CountOfBookTextBox.Text));
                    MessageBox.Show("Заказ успешно оформлен");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Введите корректное значение книг");
            }
        }
    }
}

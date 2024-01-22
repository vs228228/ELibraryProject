using Microsoft.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Configuration;

namespace ELibraryProject.PersonalAccounts.Pages
{
	/// <summary>
	/// Логика взаимодействия для AddGoodsPage.xaml
	/// </summary>
	public partial class AddGoodsPage : Page
	{
		private SqlConnection? sqlConnection;

		public AddGoodsPage()
		{
			InitializeComponent();
			string connectionString = ConfigurationManager.ConnectionStrings["Goods"].ConnectionString;
			if (connectionString == null)
			{
				throw new InvalidOperationException("Connection string is null");
			}

			connectionString = connectionString
				.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory)
				.Replace("\\ELibraryProject\\bin\\Debug\\net8.0-windows\\", "");

			sqlConnection = new SqlConnection(connectionString);
		}

		private void addBookButton_Click(object sender, RoutedEventArgs e)
		{
			if (sqlConnection == null)
			{
				throw new InvalidOperationException("SQL connection object is null");
			}

			sqlConnection.Open();
			if (sqlConnection.State != ConnectionState.Open)
			{
				throw new InvalidOperationException("SQL connection not open");
			}

			string sqlExpression = "INSERT INTO Books (Title, Author, Price, Count, WritingDate) " +
				$"VALUES ('{title.Text}', '{author.Text}', {price.Text}, {count.Text}, {writingDate.Text})";

			SqlCommand command = new SqlCommand(sqlExpression, sqlConnection);
			command.ExecuteNonQuery();
			sqlConnection.Close();
		}
	}
}

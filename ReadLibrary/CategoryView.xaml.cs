using System;
using System.Collections.Generic;
using System.IO;
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
using Newtonsoft.Json;

namespace ReadLibrary
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class CategoryView : Window
	{
		public string mainPath = "";
		public string categoriesPath = "";

		public CategoryView()
		{
			string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

			string sFile = System.IO.Path.Combine(currentDirectory, @"..\..\..\DataStorage\");
			mainPath = Path.GetFullPath(sFile);
			categoriesPath = mainPath + "Categories\\";
			InitializeComponent();
			DisplayCategories();
		}

		private void DisplayCategories()
		{
			// Make a reference to a directory.
			DirectoryInfo Categories = new DirectoryInfo(categoriesPath);

			// Get a reference to each directory in that directory.
			DirectoryInfo[] categoryArr = Categories.GetDirectories();

			// Display the names of the directories.
			int counter = 0;
			foreach (DirectoryInfo category in categoryArr)
			{
				GenerateCategoryBox(category.Name, counter);
				counter++;
			}
		}

		private void GenerateCategoryBox(string categoryName, int counter)
		{
			// 6 boxes in a row
			Button button = new Button();
			button.Content = categoryName;
			button.Margin = new Thickness(10);
			button.Height = 150;
			button.Width = 110;

			// use one event handler for all buttons, then checn the sender to decide what happens!!!
			button.Click += Category_Click;
			if (counter < 6)
			{
				Row1.Children.Add(button);
			}
			else if (counter < 12)
			{
				Row2.Children.Add(button);
			}
			else if (counter < 18)
			{
				Row3.Children.Add(button);
			}
			else
			{
/*				// add new stack panel if count %6 = ... something
				if (counter % 6 == 0)
				{
					StackPanel stackPanel = new StackPanel();
					int rowNumber = counter / 6 + 1;
					string stackPanelName = "Row" + rowNumber.ToString();
					RegisterName(stackPanelName, stackPanel);
					stackPanel.Children.Add(button);
				}
				else
				{
					int rowNumber = counter / 6 + 1;
					string stackPanelName = "Row" + rowNumber.ToString();
					// need to access the dynamic textBox first as an object
					StackPanel category = (StackPanel)this.MainPageStackPanel.FindName(stackPanelName);

					category.Children.Add(button);
				}
				// counter/6 int is the row# + 1 cause numbers are weird? i think?*/
			}

		}

		// Click events
		private void Category_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string buttonText = (string)button.Content;

			var window = new GroupView(buttonText, mainPath, categoriesPath);
			window.ShowDialog();
			RefreshPage();
		}

		private void Add_Book_Click(object sender, RoutedEventArgs e)
		{
			var window = new AddBook(mainPath, categoriesPath);
			window.ShowDialog();
		}

		private void Add_Category_Click(object sender, RoutedEventArgs e)
		{
			var window = new AddCategory(mainPath, categoriesPath);
			window.ShowDialog();
			RefreshPage();
		}

		private void LibraryView_Click(object sender, RoutedEventArgs e)
		{
			LibraryView window = new LibraryView(mainPath);
			window.ShowDialog();
			RefreshPage();

		}


		private void RefreshPage()
		{
			Row1.Children.Clear();
			Row2.Children.Clear();
			Row3.Children.Clear();
			DisplayCategories();
		}
	}
}

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
using System.Windows.Shapes;

namespace ReadLibrary
{
	/// <summary>
	/// Interaction logic for LibraryView.xaml
	/// </summary>
	public partial class LibraryView : Window
	{
		string mainPath = string.Empty;
		string categriesPath = string.Empty;
		string libraryPath = string.Empty;

		public LibraryView(string _mainPath)
		{
			mainPath = _mainPath;
			categriesPath = _mainPath + "Categories\\";
			libraryPath = _mainPath + "Library\\";
			InitializeComponent();
			DisplayBooksInGroup();
		}

		private void DisplayBooksInGroup()
		{
			DirectoryInfo library = new DirectoryInfo(libraryPath);

			// Get a reference to each directory in that directory.
			FileInfo[] bookArr = library.GetFiles();

			// Display the names of the directories.
			int counter = 0;
			foreach (FileInfo book in bookArr)
			{
				GenerateBookBox(book.Name, counter);
				counter++;
			}
		}

		private void GenerateBookBox(string bookFile, int counter)
		{
			// 6 boxes in a row
			Button button = new Button();
			int bookNL = bookFile.Length;
			string bookName = string.Empty;
			for (int i = bookNL - 1; i >= (bookNL - 5); i--)
			{
				bookName = bookFile.Remove(i);
			}
			button.Content = bookName;
			button.Margin = new Thickness(10);
			button.Height = 150;
			button.Width = 110;

			// use one event handler for all buttons, then checn the sender to decide what happens!!!
			button.Click += ViewBook_Click;

			// now add the button to the correct stackpanel
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
				/*// add new stack panel if count %6 = ... something
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
					// need to get the right stackPanel name
					int rowNumber = counter / 6 + 1;
					string stackPanelName = "Row" + rowNumber.ToString();

					// need to access the dynamic stackPanel first as an object
					StackPanel category = (StackPanel)this.MainPageStackPanel.FindName(stackPanelName);

					// then add the button child
					category.Children.Add(button);
				}*/
			}
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			Close();

		}

		private void AddBook_Click(object sender, RoutedEventArgs e)
		{
			AddBook addBook = new AddBook(mainPath, categriesPath);
			addBook.ShowDialog();
		}

		private void ViewBook_Click(Object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string buttonText = (string)button.Content;

			BookView bookView = new BookView(buttonText, mainPath, categriesPath);
			bookView.ShowDialog();
		}

		private void Book_Click(object sender, RoutedEventArgs e)
		{
			// call bookInfoPage here
			Button button = (Button)sender;
			string buttonText = (string)button.Content;

			BookView bookView = new BookView(buttonText, mainPath, categriesPath);
			bookView.ShowDialog();
		}

	}
}

using Newtonsoft.Json;
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
	/// Interaction logic for ShelfView.xaml
	/// </summary>
	public partial class ShelfView : Window
	{
		string mainPath = string.Empty;
		string categoreisPath = string.Empty;
		string categoryPath = string.Empty;
		string groupPath = string.Empty;
		string jsonTag = ".json";
		string groupName = string.Empty;

		public ShelfView(string _groupName, string _categoryPath, string _mainPath, string _categoriesPath)
		{
			mainPath = _mainPath;
			categoreisPath = _categoriesPath;
			categoryPath = _categoryPath;
			groupName = _groupName;

			groupPath =categoryPath + _groupName + jsonTag;
			InitializeComponent();

			DisplayBooksInGroup();
		}

		private void DisplayBooksInGroup()
		{
			GroupHeader.Text = groupName;

			var jsonString = File.ReadAllText(groupPath);

			GroupShelf groupShelf = new GroupShelf();
			groupShelf = JsonConvert.DeserializeObject<GroupShelf>(jsonString);

			// Display the names of the directories.
			int counter = 0;
			foreach (string book in groupShelf.fileNames_BI)
			{
				GenerateBookBox(book, counter);
				counter++;
			}
		}

		private void GenerateBookBox(string fileName, int counter)
		{
			// 6 boxes in a row
			Button button = new Button();
			int bookNL = fileName.Length;
			string bookName = string.Empty;
			for (int i = bookNL - 1; i >= (bookNL - 5); i--)
			{
				bookName = fileName.Remove(i);
			}
			button.Content = new TextBlock()
			{
				TextAlignment = TextAlignment.Center,
				TextWrapping = TextWrapping.Wrap
			};
			button.Content = bookName;
			button.Margin = new Thickness(10);
			button.Height = 150;
			button.Width = 110;

			// use one event handler for all buttons, then checn the sender to decide what happens!!!
			button.Click += Book_Click;

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

		private void Book_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string buttonText = (string)button.Content;

			// call bookInfoPage here
			BookView window = new BookView(buttonText, mainPath, categoreisPath);
			window.ShowDialog();
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			Close();

		}

		private void AddBook_Click(object sender, RoutedEventArgs e)
		{
			AddBook window = new AddBook(mainPath, categoreisPath);
			window.ShowDialog();
			RefreshPage();

		}

		private void AddToShelf_Click(object sender, RoutedEventArgs e)
		{
			AddToShelf window = new AddToShelf(categoryPath, groupName);
			window.ShowDialog();
			RefreshPage();
		}

		private void RefreshPage()
		{
			Row1.Children.Clear();
			Row2.Children.Clear();
			Row3.Children.Clear();
			DisplayBooksInGroup();
		}

	}

	public class GroupShelf
	{
		public GroupShelf()
		{
			fileNames_BI = new List<string> { };
		}
		public List<string> fileNames_BI { get; set; }

	}
}

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
	/// Interaction logic for AddCategory.xaml
	/// </summary>
	public partial class AddCategory : Window
	{
		string mainpath = string.Empty;
		public string categoriesPath = string.Empty;
		bool enterKeyGroup = false;
		public AddCategory(string _mainPath, string _categoriesPath)
		{
			mainpath = _mainPath;
			categoriesPath = _categoriesPath;
			InitializeComponent();
			enterKeyGroup = false;
		}



		private List<string> FillDefaultGroup()
		{
			List<string> library = new List<string> { };

			DirectoryInfo Library = new DirectoryInfo("C:\\Users\\csins\\source\\repos\\ReadLibrary\\ReadLibrary\\DataStorage\\Library\\");

			FileInfo[] bookArr = Library.GetFiles();

			int counter = 0;

			foreach (FileInfo file in bookArr)
			{
				library.Add(file.Name);
				counter++;
			}
			return library;
		}

		private void GenerateGroupObject()
		{
			int numGroups = Int32.Parse(GroupNumber.Text);

			for (int i = 1; i <= numGroups; i++)
			{

				StackPanel stackPanel = new StackPanel();
				stackPanel.Orientation = Orientation.Horizontal;

				TextBlock textBlock = new TextBlock();
				textBlock.Text = "Group Name : ";

				TextBox textBox = new TextBox();
				textBox.Name = "Group" + i.ToString();
				RegisterName("Group" + i.ToString(), textBox);
				textBox.Width = 670;
				textBox.Height = 30;
				textBox.Margin = new Thickness(5);


				stackPanel.Children.Add(textBlock);
				stackPanel.Children.Add(textBox);

				Groups.Children.Add(stackPanel);
			}

		}

		// Click Events
		private void SaveCategory_Click(object sender, RoutedEventArgs e)
		{
			// get category name
			string categoryName = CategoryName.Text;
			// create folder for category
			string categoryPath = categoriesPath + categoryName;
			if (!Directory.Exists(categoryPath))
			{
				Directory.CreateDirectory(categoryPath);
			}

			// get number of groups
			int numGroups = Int32.Parse(GroupNumber.Text);

			// use the number to get the correct number of group files created.
			for (int i = 1; i <= numGroups; i++)
			{
				// need to access the dynamic textBox first as an object
				TextBox group = (TextBox)this.Groups.FindName("Group" + i.ToString());

				// now get the name of the group
				string groupName = group.Text;
				GroupShelf shelf = new GroupShelf();

				// now create file using that name
				string stringjson = JsonConvert.SerializeObject(shelf, Formatting.Indented);

				// write the string to the file
				File.WriteAllText(categoryPath + "\\" + groupName + ".json", stringjson);
			}
			string defaultGroupName = "Miscellaneous";
			GroupShelf groupshelf = new GroupShelf();

			//get all books in library
			List<string> library = FillDefaultGroup();
			groupshelf.fileNames_BI = library;

			// now create the default json file
			string stringjson2 = JsonConvert.SerializeObject(groupshelf, Formatting.Indented);

			// write the string to the file
			File.WriteAllText(categoryPath + "\\" + defaultGroupName + ".json", stringjson2);

			Close();
		}

		private void DetectEnterKey(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (!enterKeyGroup)
				{
					GenerateGroupObject();
					enterKeyGroup = true;
				}
			}
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}

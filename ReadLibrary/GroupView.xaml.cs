using System.IO;
using System.Windows;
using System.Windows.Controls;


namespace ReadLibrary
{
	/// <summary>
	/// Interaction logic for GroupView.xaml
	/// </summary>
	public partial class GroupView : Window
	{
		string categoryName = string.Empty;
		public string mainPath = "";
		public string categoriesPath = "";
		public string groupPath = string.Empty;

		public GroupView(string _categoryName, string _mainPath, string _categoriesPath)
		{
			mainPath = _mainPath; categoriesPath = _categoriesPath;
			categoryName = _categoryName;
			groupPath = _categoriesPath + categoryName + "\\";
			InitializeComponent();
			DisplayGroups();
		}


		private void DisplayGroups()
		{
			CategoryHeader.Text = categoryName;

			// Make a reference to a directory.
			DirectoryInfo Categories = new DirectoryInfo(groupPath);

			// Get a reference to each directory in that directory.
			FileInfo[] groupArr = Categories.GetFiles();

			// Display the names of the directories.
			int counter = 0;
			foreach (FileInfo group in groupArr)
			{
				GenerateGroupBox(group.Name, counter);
				counter++;
			}
		}

		private void GenerateGroupBox(string fileName, int counter)
		{
			// 6 boxes in a row
			Button button = new Button();
			int gNL = fileName.Length;
			string groupName = string.Empty;
			for (int i = gNL - 1; i >= (gNL - 5); i--)
			{
				groupName = fileName.Remove(i);
			}
			button.Content = groupName;
			button.Margin = new Thickness(10);
			button.Height = 150;
			button.Width = 110;

			// use one event handler for all buttons, then checn the sender to decide what happens!!!
			button.Click += Group_Click;

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


		// Click Events 
		private void Add_Book_Click(object sender, RoutedEventArgs e)
		{
			AddBook window = new AddBook(mainPath, categoriesPath);
			window.ShowDialog();
			RefreshPage();
		}

		private void Group_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			string buttonText = (string)button.Content;

			ShelfView window = new ShelfView(buttonText, groupPath, mainPath, categoriesPath);
			window.ShowDialog();
			RefreshPage();
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void AddGroup_Click(object sender, RoutedEventArgs e)
		{
			AddGroup window = new AddGroup(groupPath);
			window.ShowDialog();
			RefreshPage();
		}



		private void RefreshPage()
		{
			Row1.Children.Clear();
			Row2.Children.Clear();
			Row3.Children.Clear();
			DisplayGroups();
		}
	}
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ReadLibrary
{
	/// <summary>
	/// Interaction logic for AddToShelf.xaml
	/// </summary>
	public partial class AddToShelf : Window
	{
		string categoryPath = string.Empty;
		string groupName = string.Empty;
		GroupShelf groupShelf = new GroupShelf();
		List <string> names = new List<string>();
		public AddToShelf(string _categoryPath, string _groupName)
		{
			categoryPath = _categoryPath;
			groupName = _groupName;
			InitializeComponent();
			SetUpPage();
		}

		private void SetUpPage()
		{
			DirectoryInfo Category = new DirectoryInfo(categoryPath);

			// Get a reference to each directory in that directory.
			FileInfo[] groupArr = Category.GetFiles();

			foreach (FileInfo group in groupArr)
			{
				if (group.Name != groupName + ".json")
				{
					string jsonString = File.ReadAllText(group.FullName);

					GroupShelf shelf = new GroupShelf();

					shelf = JsonConvert.DeserializeObject<GroupShelf>(jsonString)!;

					foreach (string book in shelf.fileNames_BI)
					{
						groupShelf.fileNames_BI.Add(book);
					}
				}
			}

			// now to create the elements
			int count = 1;
			foreach (string book in groupShelf.fileNames_BI)
			{ 
				CreateToggleButtons(book, count);
				count++;
			}
		}

		private void CreateToggleButtons(string fileName, int counter)
		{
			// 6 boxes in a row
			ToggleButton button = new ToggleButton();
			button.Name = "Element" + counter.ToString();
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
			button.Checked += Checked_Click;
			button.Unchecked += UnChecked_Click;

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




		private void Checked_Click(object sender, RoutedEventArgs e)
		{
			ToggleButton button = (ToggleButton)sender;
			string buttonText = (string)button.Content;

			bool contains = names.Contains(buttonText);

			if (!contains) 
				names.Add(buttonText);
		}
		private void UnChecked_Click(object sender, RoutedEventArgs e)
		{
			ToggleButton button = (ToggleButton)sender;
			string buttonText = (string)button.Content;

			names.Remove(buttonText);
		}


		private void Confirm_Click(object sender, RoutedEventArgs e)
		{
			DirectoryInfo Category = new DirectoryInfo(categoryPath);

			FileInfo[] groupArr = Category.GetFiles();

			foreach (FileInfo group in groupArr)
			{
				string jsonString = File.ReadAllText(group.FullName);

				GroupShelf shelf = new GroupShelf();

				shelf = JsonConvert.DeserializeObject<GroupShelf>(jsonString)!;
				if (group.Name == groupName + ".json")
				{
					foreach (string name in names)
					{
						shelf.fileNames_BI.Add(name + ".json");

					}
				}
				else
				{
					// if the filename exists in the files list it should tell you
					foreach (string name in names)
					{
						bool onShelf = false;
						foreach (string name2 in shelf.fileNames_BI)
						{
							onShelf = name2.Contains(name);

							if (onShelf) 
							{ 
								shelf.fileNames_BI.Remove(name2);
								break; 
							}
						}

					}
				}

				string jsonString2 = JsonConvert.SerializeObject(shelf);
				File.WriteAllText(group.FullName, jsonString2);
			}




			int count = 1;
			foreach (string name in names)
			{
				
			}
			Close();
		}

		private void Back_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}

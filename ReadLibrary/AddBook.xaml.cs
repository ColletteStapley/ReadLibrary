using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon.Primitives;
using System.Windows.Media;
using System.Xml.Linq;
using Newtonsoft.Json;


namespace ReadLibrary
{
	/// <summary>
	/// Interaction logic for AddBook.xaml
	/// </summary>
	public partial class AddBook : Window
	{

		private BookInfo bookInfo = new BookInfo();
		private bool IsSeries = false;
		public string filename = string.Empty;
		public string mainPath = "";
		public string categoriesPath = "";
		public string libraryPath = "";
		private List<string> series = new List<string>();
		private bool loaded = false;

		public AddBook(string _mainPath, string _categoriesPath)
		{
			mainPath = _mainPath;
			categoriesPath = _categoriesPath;
			libraryPath = mainPath + "Library\\";

			InitializeComponent();
			DisplayCategoryRows();

		}
		public AddBook(string fileName1, string _mainPath, string _categoriesPath)
		{
			mainPath = _mainPath;
			categoriesPath = _categoriesPath;
			libraryPath = mainPath + "Library\\";

			filename = fileName1;

			InitializeComponent();

			DisplayCategoryRows();
			if (fileName1 != null)
				LoadData(filename);
		}



		private void DisplayCategoryRows()
		{

			DirectoryInfo categoriesDirectory = new DirectoryInfo(categoriesPath);

			// Get a reference to each directory in that directory.
			DirectoryInfo[] categoryArr = categoriesDirectory.GetDirectories();

			int count = 1;
			foreach (DirectoryInfo category in categoryArr)
			{
				CreateCategoryRow(category, count);
				count++;
			}
		}

		private void CreateCategoryRow(DirectoryInfo categoryData, int count)
		{

			TextBlock categoryName = new TextBlock();
			categoryName.Text = categoryData.Name + " : ";
			categoryName.Margin = new Thickness(5);

			FileInfo[] groupArr = categoryData.GetFiles();

			ComboBox category = new ComboBox();

			// fix the name of the category so it doesn',t throw error
			string categoryN = CleanUpString(categoryData.Name);

			//now save an continue setting up the category and it's combobox
			category.Name = categoryN;
			series.Add(categoryData.Name);

			foreach (FileInfo group in groupArr)
			{
				string name = group.Name;
				int length = name.Length;
				for (int i = length - 1; i >= (length - 5); i--)
				{
					name = name.Remove(i);
				}
				category.Items.Add(name);
			}

			if (count == 1) { Category1.Children.Add(categoryName); Category1.Children.Add(category); }
			else if (count == 2) { Category2.Children.Add(categoryName); Category2.Children.Add(category); }
			else if (count == 3) { Category3.Children.Add(categoryName); Category3.Children.Add(category); }
			else if (count == 4) { Category4.Children.Add(categoryName); Category4.Children.Add(category); }
			else if (count == 5) { Category5.Children.Add(categoryName); Category5.Children.Add(category); }
			else if (count == 6) { Category6.Children.Add(categoryName); Category1.Children.Add(category); }
			else if (count == 7) { Category7.Children.Add(categoryName); Category7.Children.Add(category); }
			else if (count == 8) { Category8.Children.Add(categoryName); Category8.Children.Add(category); }
			else if (count == 9) { Category9.Children.Add(categoryName); Category9.Children.Add(category); }
			else if (count == 10) { Category10.Children.Add(categoryName); Category10.Children.Add(category); }
			else if (count == 11) { Category11.Children.Add(categoryName); Category11.Children.Add(category); }
			else if (count == 12) { Category12.Children.Add(categoryName); Category12.Children.Add(category); }
			else if (count == 13) { Category13.Children.Add(categoryName); Category13.Children.Add(category); }
			else if (count == 14) { Category14.Children.Add(categoryName); Category14.Children.Add(category); }
			else if (count == 15) { Category15.Children.Add(categoryName); Category15.Children.Add(category); }
			else if (count == 16) { Category16.Children.Add(categoryName); Category16.Children.Add(category); }
			else if (count == 17) { Category17.Children.Add(categoryName); Category17.Children.Add(category); }
			else if (count == 18) { Category18.Children.Add(categoryName); Category18.Children.Add(category); }
			else { }

		}

		private void LoadData(string filename)
		{
			loaded = true;
			// needs to pull the file from data storage
			var jsonString = File.ReadAllText(libraryPath + filename + ".json");

			// save that data to the bookInfo object
			bookInfo = JsonConvert.DeserializeObject<BookInfo>(jsonString)!;

			// load that data into the page so the user can see and edit
			Title.Text = bookInfo.Title_BI;
			AuthorName.Text = bookInfo.AuthorName_BI;
			Rating.Text = bookInfo.Rating_BI;
			if (IsSeries)
			{
				IsSeries = bookInfo.Series_BI;
				SeriesName.Text = bookInfo.SeriesName_BI;
			}
			else
			{
				IsSeries = bookInfo.Series_BI;
			};
			OfficialDescription.Text = bookInfo.OfficialDescription_BI;
			PersonalDescription.Text = bookInfo.PersonalDescription_BI;
			LikesDislikes.Text = bookInfo.LikesDislikes_BI;

			int count = 1;

			foreach (Tuple<string, string> category in bookInfo.categories_BI)
			{
				string categoryName = category.Item1;

				string cleanName = CleanUpString(categoryName);
				ComboBox group = new ComboBox();

				if (count == 1) { group = (ComboBox)Category1.FindName(cleanName); }
				else if (count == 2) { group = (ComboBox)Category2.FindName(cleanName); }
				else if (count == 3) { group = (ComboBox)Category3.FindName(cleanName); }
				else if (count == 4) { group = (ComboBox)Category4.FindName(cleanName); }
				else if (count == 5) { group = (ComboBox)Category5.FindName(cleanName); }
				else if (count == 6) { group = (ComboBox)Category6.FindName(cleanName); }
				else if (count == 7) { group = (ComboBox)Category7.FindName(cleanName); }
				else if (count == 8) { group = (ComboBox)Category8.FindName(cleanName); }
				else if (count == 9) { group = (ComboBox)Category9.FindName(cleanName); }
				else if (count == 10) { group = (ComboBox)Category10.FindName(cleanName); }
				else if (count == 11) { group = (ComboBox)Category11.FindName(cleanName); }
				else if (count == 12) { group = (ComboBox)Category12.FindName(cleanName); }
				else if (count == 13) { group = (ComboBox)Category13.FindName(cleanName); }
				else if (count == 14) { group = (ComboBox)Category14.FindName(cleanName); }
				else if (count == 15) { group = (ComboBox)Category15.FindName(cleanName); }
				else if (count == 16) { group = (ComboBox)Category16.FindName(cleanName); }
				else if (count == 17) { group = (ComboBox)Category17.FindName(cleanName); }
				else if (count == 18) { group = (ComboBox)Category18.FindName(cleanName); }
				else { }

				string miscellaneous = string.Empty;

				if (group?.Text == null)
				{
					miscellaneous = "Miscellaneous";
				}
				else
				{
					group.Text = category.Item2;
				}
				count++;
			}

		}



		// Click events
		private void SaveBookData_Click(object sender, RoutedEventArgs e)
		{
			// Save the info from the to the bookObject
			bookInfo.Title_BI = Title.Text;
			bookInfo.AuthorName_BI = AuthorName.Text;
			bookInfo.Rating_BI = Rating.Text;

			if (IsSeries)
			{
				bookInfo.Series_BI = IsSeries;
				bookInfo.SeriesName_BI = SeriesName.Text;
			}
			else
			{
				bookInfo.Series_BI = IsSeries;
			};
			bookInfo.OfficialDescription_BI = OfficialDescription.Text;
			bookInfo.PersonalDescription_BI = PersonalDescription.Text;
			bookInfo.LikesDislikes_BI = LikesDislikes.Text;

			// need to decide if the filename needs to be name or not
			if (bookInfo.fileName_BI == string.Empty)
			{
				bookInfo.fileName_BI = BuildFileName(bookInfo.Title_BI);
			};

			if (!loaded)
			{
				bookInfo.categories_BI.Clear();

				int count = 1;
				foreach (string category in series)
				{
					string cleanName = CleanUpString(category);
					ComboBox group = new ComboBox();

					if (count == 1) { group = (ComboBox)this.Category1.FindName(cleanName); }
					else if (count == 2) { group = (ComboBox)this.Category2.FindName(cleanName); }
					else if (count == 3) { group = (ComboBox)this.Category3.FindName(cleanName); }
					else if (count == 4) { group = (ComboBox)this.Category4.FindName(cleanName); }
					else if (count == 5) { group = (ComboBox)this.Category5.FindName(cleanName); }
					else if (count == 6) { group = (ComboBox)this.Category6.FindName(cleanName); }
					else if (count == 7) { group = (ComboBox)this.Category7.FindName(cleanName); }
					else if (count == 8) { group = (ComboBox)this.Category8.FindName(cleanName); }
					else if (count == 9) { group = (ComboBox)this.Category9.FindName(cleanName); }
					else if (count == 10) { group = (ComboBox)this.Category10.FindName(cleanName); }
					else if (count == 11) { group = (ComboBox)this.Category11.FindName(cleanName); }
					else if (count == 12) { group = (ComboBox)this.Category12.FindName(cleanName); }
					else if (count == 13) { group = (ComboBox)this.Category13.FindName(cleanName); }
					else if (count == 14) { group = (ComboBox)this.Category14.FindName(cleanName); }
					else if (count == 15) { group = (ComboBox)this.Category15.FindName(cleanName); }
					else if (count == 16) { group = (ComboBox)this.Category16.FindName(cleanName); }
					else if (count == 17) { group = (ComboBox)this.Category17.FindName(cleanName); }
					else if (count == 18) { group = (ComboBox)this.Category18.FindName(cleanName); }
					else { }

					string miscellaneous = string.Empty;

					if (group?.Text == null)
					{
						miscellaneous = "Miscellaneous";
					}
					else
					{
						miscellaneous = group.Text;
					}

					Tuple<string, string> categoryTuple = new Tuple<string, string>(category, miscellaneous);

					EditCategorys(categoryTuple);

					bookInfo.categories_BI.Add(categoryTuple);
					count++;
				}

			}
			// convert data to a json string
			string stringjson = JsonConvert.SerializeObject(bookInfo, Formatting.Indented);

			// write the string to the file
			File.WriteAllText(libraryPath + bookInfo.fileName_BI, stringjson);
			Close();
		}

		private void EditCategorys(Tuple<string, string> categoryTuple)
		{
			// use the tuple.Item1 aka category name to check if the filename
			//		which already exists inside the bookInfo.fileName_BI
			//		exists iside the SPECIFIC group, aka tuple.Item2


			// Make a reference to a directory.
			bool fileChanged = true;
			DirectoryInfo Categories = new DirectoryInfo(categoriesPath + categoryTuple.Item1 + "//");

			// Get a reference to each directory in that directory.
			FileInfo[] groupArr = Categories.GetFiles();

			foreach (FileInfo group in groupArr)
			{
				// you only want to do further checks into the file that we are actually
				//			wanting to change
				if (group.Name == categoryTuple.Item2 + ".json")
				{
					string jsonString = File.ReadAllText(group.FullName);

					GroupShelf shelf = new GroupShelf();

					shelf = JsonConvert.DeserializeObject<GroupShelf>(jsonString)!;

					// if the filename exists in the files list it should tell you
					bool onShelf = shelf.fileNames_BI.Contains(bookInfo.fileName_BI);
					if (!onShelf)
					{
						shelf.fileNames_BI.Add(bookInfo.fileName_BI);
						string jsonString2 =JsonConvert.SerializeObject(shelf);
						File.WriteAllText(group.FullName, jsonString2);

					}
					else
					{
						// why make any changes if it is already where it's supposed to be
						fileChanged = false;

						// just make sure we don't check the other groups for a filename 
						//		that wont be there
						break;
					}
				}
			}

			// no we need to find the file it's getting removed from
			if (fileChanged)
			{
				foreach (FileInfo group in groupArr)
				{
					// this just makes sure that you aren't accidentaly checking the one it's
					//		supposed to be in
					if (group.Name != categoryTuple.Item2 + ".json")
					{
						string jsonString = File.ReadAllText(group.FullName);

						GroupShelf shelf = new GroupShelf();

						shelf = JsonConvert.DeserializeObject<GroupShelf>(jsonString)!;


						bool onShelf = shelf.fileNames_BI.Contains(bookInfo.fileName_BI);
						bool found = false;

						if (onShelf)
						{
							foreach(string book in shelf.fileNames_BI)
							{
								found = book.Contains(bookInfo.fileName_BI);
								if (found)
								{
									// yay you found the rigth file. now remove the string and 
									//		save the file with the changes.
									shelf.fileNames_BI.Remove(book);
									string jsonString2 = JsonConvert.SerializeObject(shelf);
									File.WriteAllText(group.FullName, jsonString2);
									break;
								}
							}
							if (found)
							{
								// no need to search other groups if the one it's changed out
								//		of is already found and updated
								break;
							}
						}
					}
				}
			}
		}

		private void HandleCheck(object sender, RoutedEventArgs e)
		{
			IsSeries = true;
		}

		private void HandleUnchecked(object sender, RoutedEventArgs e)
		{
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void AddPicture_Click(object sender, RoutedEventArgs e)
		{


		}



		// extra functions for easy use
		private string BuildFileName(string bookTitle)
		{
			// / \ : * ? " < > and | 
			//    filename can't have these characters
			foreach (char c in System.IO.Path.GetInvalidFileNameChars())
			{
				bookTitle = bookTitle.Replace(c, '_');
			}
			// add path to string?
			string filename = bookTitle + ".json";

			return filename;
		}

		private string CleanUpString(string name)
		{
			foreach (char c in System.IO.Path.GetInvalidFileNameChars())
			{
				name = name.Replace(c, '_');
			}

			name = name.Replace(' ', '_');
			return name;
		}


		private childItem FindVisualChild<childItem>(DependencyObject obj)
			where childItem : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(obj, i);
				if (child != null && child is childItem)
				{
					return (childItem)child;
				}
				else
				{
					childItem childOfChild = FindVisualChild<childItem>(child);
					if (childOfChild != null)
						return childOfChild;
				}
			}
			return null;
		}
	}




	public class BookInfo
	{
		public BookInfo()
		{
			Title_BI = string.Empty;
			AuthorName_BI = string.Empty;
			Rating_BI = string.Empty;

			Series_BI = false;
			SeriesName_BI = string.Empty;
			OfficialDescription_BI = string.Empty;
			PersonalDescription_BI = string.Empty;
			LikesDislikes_BI = string.Empty;
			fileName_BI = string.Empty;
			categories_BI = new List<Tuple<string, string>>();
		}

		public string Title_BI { get; set; }
		public string AuthorName_BI { get; set; }
		public string Rating_BI { get; set; }

		public bool Series_BI { get; set; }
		public string SeriesName_BI { get; set; }
		public string OfficialDescription_BI { get; set; }
		public string PersonalDescription_BI { get; set; }
		public string LikesDislikes_BI { get; set; }
		public string fileName_BI { get; set; }
		public List<Tuple<string, string>> categories_BI { get; set; }

	}


}

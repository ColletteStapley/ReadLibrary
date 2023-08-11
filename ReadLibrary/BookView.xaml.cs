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
	/// Interaction logic for BookView.xaml
	/// </summary>
	public partial class BookView : Window
	{
		string mainPath = string.Empty;
		string categoreisPath = string.Empty;
		public string libraryPath = string.Empty;
		private string fileName = string.Empty;
		private string bookName = string.Empty;
		BookInfo bookInfo = new BookInfo();
		public BookView(string _bookName, string _mainPath, string _categoreisPath)
		{
			mainPath = _mainPath;
			categoreisPath = _categoreisPath;
			libraryPath = _mainPath + "Library\\";
			fileName = _bookName + ".json";
			bookName = _bookName;
			InitializeComponent();
			LoadData();
		}

		private void LoadData()
		{
			// needs to pull the file from data storage
			var jsonString = File.ReadAllText(libraryPath + fileName);

			// save that data to the bookInfo object
			bookInfo = JsonConvert.DeserializeObject<BookInfo>(jsonString);

			// load that data into the page so the user can see and edit
			Title.Text = bookInfo.Title_BI;
			AuthorName.Text = bookInfo.AuthorName_BI;
			Rating.Text = bookInfo.Rating_BI;

			if (bookInfo.Series_BI)
			{
				IsSeries.Text = "y";
				SeriesName.Text = bookInfo.SeriesName_BI;
			}
			else
			{
				IsSeries.Text = "n";
			};
			OfficialDescription.Text = bookInfo.OfficialDescription_BI;
			PersonalDescription.Text = bookInfo.PersonalDescription_BI;
			LikesDislikes.Text = bookInfo.LikesDislikes_BI;

			int count = 1;

			foreach (Tuple<string, string> category in bookInfo.categories_BI)
			{
				string categoryName = category.Item1;

				string cleanName = CleanUpString(categoryName);
				TextBlock group = new TextBlock();

				if (count == 1) { group = (TextBlock)Category1.FindName(cleanName); }
				else if (count == 2) { group = (TextBlock)Category2.FindName(cleanName); }
				else if (count == 3) { group = (TextBlock)Category3.FindName(cleanName); }
				else if (count == 4) { group = (TextBlock)Category4.FindName(cleanName); }
				else if (count == 5) { group = (TextBlock)Category5.FindName(cleanName); }
				else if (count == 6) { group = (TextBlock)Category6.FindName(cleanName); }
				else if (count == 7) { group = (TextBlock)Category7.FindName(cleanName); }
				else if (count == 8) { group = (TextBlock)Category8.FindName(cleanName); }
				else if (count == 9) { group = (TextBlock)Category9.FindName(cleanName); }
				else if (count == 10) { group = (TextBlock)Category10.FindName(cleanName); }
				else if (count == 11) { group = (TextBlock)Category11.FindName(cleanName); }
				else if (count == 12) { group = (TextBlock)Category12.FindName(cleanName); }
				else if (count == 13) { group = (TextBlock)Category13.FindName(cleanName); }
				else if (count == 14) { group = (TextBlock)Category14.FindName(cleanName); }
				else if (count == 15) { group = (TextBlock)Category15.FindName(cleanName); }
				else if (count == 16) { group = (TextBlock)Category16.FindName(cleanName); }
				else if (count == 17) { group = (TextBlock)Category17.FindName(cleanName); }
				else if (count == 18) { group = (TextBlock)Category18.FindName(cleanName); }
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

		private string CleanUpString(string name)
		{
			foreach (char c in System.IO.Path.GetInvalidFileNameChars())
			{
				name = name.Replace(c, '_');
			}

			name = name.Replace(' ', '_');
			return name;
		}



		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void EditBook_Click(object sender, RoutedEventArgs e)
		{
			AddBook window = new AddBook(bookName, mainPath, categoreisPath);
			window.ShowDialog();

		}

		private void DeleteBook_Click(object sender, RoutedEventArgs e)
		{
			File.Delete(libraryPath + fileName);

			DirectoryInfo categories = new DirectoryInfo(categoreisPath);

			DirectoryInfo[] categoryS = categories.GetDirectories();

			foreach (DirectoryInfo category in categoryS)
			{
				FileInfo[] groupArr = category.GetFiles();
				string groupName = string.Empty;


				foreach (FileInfo group in groupArr)
				{
					int gNL = group.Name.Length;
					for (int i = gNL - 1; i >= (gNL - 5); i--)
					{
						groupName = group.Name.Remove(i);
					}
					string jsonString = File.ReadAllText(group.FullName);

					GroupShelf shelf = new GroupShelf();

					shelf = JsonConvert.DeserializeObject<GroupShelf>(jsonString)!;

					// if the filename exists in the files list it should tell you

					bool onShelf = false;
					foreach (string name2 in shelf.fileNames_BI)
					{
						onShelf = name2.Contains(fileName);

						if (onShelf)
						{
							shelf.fileNames_BI.Remove(name2);
							break;
						}
					}
					string jsonString2 = JsonConvert.SerializeObject(shelf);
					File.WriteAllText(group.FullName, jsonString2);
				}

			}
			Close();
		}
	}
}

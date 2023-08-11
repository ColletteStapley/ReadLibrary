using Newtonsoft.Json;
using System.IO;
using System.Windows;


namespace ReadLibrary
{
	/// <summary>
	/// Interaction logic for AddGroup.xaml
	/// </summary>
	public partial class AddGroup : Window
	{
		string categoryPath = string.Empty;
		public AddGroup(string _categoryPath)
		{
			categoryPath = _categoryPath;
			InitializeComponent();
		}

		private void AddGroup_Click(object sender, RoutedEventArgs e)
		{
			GroupShelf groupShelf = new GroupShelf();

			string groupName = NewGroupName.Text;

			var jsonString = JsonConvert.SerializeObject(groupShelf, Formatting.Indented);

			File.WriteAllText(categoryPath + groupName + ".json", jsonString);
			Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();

		}
	}
}

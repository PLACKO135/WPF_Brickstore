using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Win32;
using System.Reflection;


namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Brkdata> _brickDataList;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtSearchboxItemname_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void txtSearchboxItemId_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            string itemNameSearchText = txtSearchboxItemname.Text.ToLower();
            string itemIdSearchText = txtSearchboxItemId.Text.ToLower();
            string selectedCategory = cmbCategory.SelectedItem?.ToString().ToLower();

            var filteredData = _brickDataList.Where(x =>
                (string.IsNullOrEmpty(itemNameSearchText) || x.ItemName.ToLower().StartsWith(itemNameSearchText)) &&
                (string.IsNullOrEmpty(itemIdSearchText) || x.ItemID.ToLower().StartsWith(itemIdSearchText)) &&
                (selectedCategory == null || x.CategoryName.ToLower().StartsWith(selectedCategory)));

            drgBrick.ItemsSource = filteredData;

            var categories = filteredData.Select(x => x.CategoryName).Distinct().OrderByDescending(x => x).ToList();

            cmbCategory.ItemsSource = categories;


            if (selectedCategory != null)
            {
                cmbCategory.SelectedItem = categories.FirstOrDefault(x => x.ToLower() == selectedCategory);
            }
            else
            {
                cmbCategory.SelectedIndex = -1;
            }
        }


        //todo make it to work with folders instead of single files
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BSX files (*.bsx)|*.bsx";
            openFileDialog.Title = "Select a .BSX file";

            if (openFileDialog.ShowDialog() == true)
            {
                _brickDataList = new List<Brkdata>();

                XDocument xaml = XDocument.Load(openFileDialog.FileName);
                foreach (var elem in xaml.Descendants("Item"))
                {
                    string id = elem.Element("ItemID").Value;
                    string nev = elem.Element("ItemName").Value;
                    string kategoria = elem.Element("CategoryName").Value;
                    string szinnev = elem.Element("ColorName").Value;
                    int mennyiseg = int.Parse(elem.Element("Qty").Value);

                    Brkdata data = new Brkdata(id, nev, kategoria, szinnev, mennyiseg);
                    _brickDataList.Add(data);
                }

                drgBrick.ItemsSource = _brickDataList;
                LoadcmbItems();
            }
            else
            {
                MessageBox.Show("Select a .BSX file to begin!");
            }
        }

        private void LoadcmbItems()
        {
            ApplyFilters();
        }

        private void btnclearfilters_Click(object sender, RoutedEventArgs e)
        {
            txtSearchboxItemId.Text="";
            txtSearchboxItemname.Text="";
            cmbCategory.SelectedIndex = -1; 

            ApplyFilters();

        }
    }


}
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

namespace WPF_Brickstore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window

    {

        public MainWindow()

        {

            InitializeComponent();

        }


        private void ReadFileToDataGrid(string filePath)

        {

            List<Brkdata> data = new List<Brkdata>();


            XDocument xaml = XDocument.Load("brickstore_parts_4643-1-power-boat-transporter.bsx");
            foreach (var elem in xaml.Descendants("Item"))
            {
                string id = elem.Element("ItemID").Value;
                string nev = elem.Element("ItemName").Value;
                string kategoria = elem.Element("CategoryName").Value;
                string szinnev = elem.Element("ColorName").Value;
                int mennyiseg = int.Parse(elem.Element("Qty").Value);

            }



            datagrid.ItemsSource = data;

        }

    }


}
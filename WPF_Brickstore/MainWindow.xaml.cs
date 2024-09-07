﻿using System.Text;
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
            ReadFileToDataGrid();
        }

        private void ReadFileToDataGrid()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BSX files (*.bsx)|*.bsx";
            openFileDialog.Title = "Select a BSX file";

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
            }
        }

     

        private void txtSearchbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchbox.Text.ToLower();
            var filteredData = _brickDataList.Where(x =>
                x.ItemID.ToLower().Contains(searchText) ||
                x.ItemName.ToLower().Contains(searchText) ||
                x.CategoryName.ToLower().Contains(searchText));

            drgBrick.ItemsSource = filteredData;
        }
    }


}
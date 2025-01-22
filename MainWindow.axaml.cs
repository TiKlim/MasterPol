using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using Avtoservice.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Avtoservice
{
    public partial class MainWindow : Window
    {
        private Product Product { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Product = new Product();
            sortCB.SelectionChanged += SortCB_SelectionChanged;
            fltrCB.SelectionChanged += FltrCB_SelectionChanged;
            add.Click += Add_Click;
            SetData(); 
        }

        private void Edit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int id = (int)(sender as Button)?.Tag!;
            var product = DataSource.Helper.DataBase.Products.Find(id);

            if (product != null)
            {
                EditWindow editWindow = new EditWindow(product!);
                editWindow.Show();
                Close();
            }
        }

        private void Remove_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int id = (int)(sender as Button)?.Tag!;
            var product = DataSource.Helper.DataBase.Products.Find(id);

            if (product != null)
            {
                DataSource.Helper.DataBase.Remove(product);
                DataSource.Helper.DataBase.Update(product);
            }
        }

        private void Add_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e){}

        private void FltrCB_SelectionChanged(object? sender, SelectionChangedEventArgs e) => SetData();

        private void SortCB_SelectionChanged(object? sender, SelectionChangedEventArgs e) => SetData();

        private void SetData()
        {
            var sortList = DataSource.Helper.DataBase.Products.Include(x => x.ProductManufacturerNavigation).ToList();

            if (sortCB.SelectedIndex == 1)
            {
                sortList = sortList.OrderBy(x => x.DiscountCost).ToList();
            }
            else if (sortCB.SelectedIndex == 2)
            {
                sortList = sortList.OrderByDescending(x => x.DiscountCost).ToList();
            }

            if (fltrCB.SelectedIndex == 1)
            {
                sortList = sortList.Where(x => x.ProductDiscountNow < 10).ToList();
            }
            else if (fltrCB.SelectedIndex == 2)
            {
                sortList = sortList.Where(x => x.ProductDiscountNow > 10 && x.ProductDiscountNow < 15).ToList();
            }
            else if (fltrCB.SelectedIndex == 3)
            {
                sortList = sortList.Where(x => x.ProductDiscountNow >= 15).ToList();
            }

            productLB.ItemsSource = sortList;
            output.Text = sortList.Count().ToString();
            total.Text = DataSource.Helper.DataBase.Products.Count().ToString();
        }
    }
}
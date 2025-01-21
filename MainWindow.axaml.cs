using Avalonia.Controls;
using Avalonia.Rendering.Composition;
using Avtoservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Avtoservice
{
    public partial class MainWindow : Window
    {
        private Product Product { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Product = new Product();
            SetData();
        }

        private void SetData()
        {
            /*if (Product.ProductDiscountNow > 0)
            {
                
            }*/
            productLB.ItemsSource = DataSource.Helper.DataBase.Products.Include(x => x.ProductManufacturerNavigation);
            
        }
    }
}
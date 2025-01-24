using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avtoservice.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Avtoservice;

public partial class EditWindow : Window
{
    Product Product { get; set; }
    private List<Unit> UnitList = new List<Unit>();
    private List<ProductCategory> ProductCategories = new List<ProductCategory>();
    private List<Provider> providers = new List<Provider>();
    private List<Manufacturer> manMan = new List<Manufacturer>();
    private bool error;

    public EditWindow(Product product)
    {
        InitializeComponent();
        Product = product;
        back.Click += Back_Click;
        gridEdit.DataContext = Product;
        save.Click += Save_Click;
        addPhoto.Click += AddPhoto_Click;
        deletePhoto.Click += DeletePhoto_Click;
        Update();
        SetData();
    }

    private void WarningText()
    {
        if (articule.Text == null)
        {
            warning.Text = "Введите артикул товара";
            error = true;
        }
        if (articule.Text == Product.ProductArticule) //?????????????????????????????
        {
            warning.Text = "Такой артикул уже существует";
            error = true;
        }
        if (name.Text == null)
        {
            warning.Text = "Введите название товара";
            error = true;
        }
        if (description.Text == null)
        {
            warning.Text = "Введите описание товара";
            error = true;
        }
        if (cost.Text == null)
        {
            warning.Text = "Введите цену товара";
            error = true;
        }
        if (Convert.ToDecimal(cost.Text) < 0)
        {
            warning.Text = "Цена не может быть отрицательной";
            error = true;
        }
        if (maxDiscount.Text == null)
        {
            warning.Text = "Введите максимальную скидку для товара";
            error = true;
        }
        if (Convert.ToDecimal(maxDiscount.Text) > 100)
        {
            warning.Text = "Макисмально возможная скидка не может быть больше 100%";
            error = true;
        }
        if (Convert.ToDecimal(maxDiscount.Text) < 0)
        {
            warning.Text = "Максимально возможная скидка не может быть отрицательной";
            error = true;
        }
        if (discount.Text == null)
        {
            warning.Text = "Введите действующую скидку для товара";
            error = true;
        }
        if (Convert.ToDecimal(discount.Text) > Convert.ToDecimal(maxDiscount.Text))
        {
            warning.Text = "Действующая скидка не может быть больше максимально возможной";
            error = true;
        }
        if (Convert.ToDecimal(discount.Text) < 0)
        {
            warning.Text = "Скидка не может быть отрицательной";
            error = true;
        }
        if (count.Text == null)
        {
            warning.Text = "Введите количество товара на складе";
            error = true;
        }
        if (Convert.ToInt32(count.Text) < 0)
        {
            warning.Text = "Количество на складе не может быть отрицательным";
            error = true;
        }
    }

    private void DeletePhoto_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        
    } 

    private async void AddPhoto_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var openFileDialog = new OpenFileDialog
        {
            AllowMultiple = false,
            Filters = new List<FileDialogFilter>
            {
                new FileDialogFilter { Name = "Image files", Extensions = new List<string> {"jpg"} }
            }
        };
        var result = await openFileDialog.ShowAsync(this);

        if (result?.Length > 0)
        {
            /*var filePath = result[0];
            var appDir = AppContext.BaseDirectory;
            var fileName = Path.GetFileName(filePath);
            var destinationPath = Path.Combine(appDir, "Assets", fileName);
            Product.ProductImage = fileName;
            imageImage.Source = new Bitmap(destinationPath);*/
        }
    } 

    public EditWindow()
    {
        InitializeComponent();
        Product = new Product();
        back.Click += Back_Click;
        gridEdit.DataContext = Product;
        save.Click += Save_Click;
        Update();
        SetData();
    }

    private void Update()
    {
        if (Product.ProductId != 0)
        {
            UnitList = DataSource.Helper.DataBase.Units.ToList();
            unit.ItemsSource = UnitList.OrderByDescending(x => x.UnitId == Product.ProductUnit).ToList();
            unit.SelectedIndex = 0;

            ProductCategories = DataSource.Helper.DataBase.ProductCategories.ToList();
            category.ItemsSource = ProductCategories.OrderByDescending(x => x.CategoryId == Product.ProductCategory).ToList();
            category.SelectedIndex = 0;

            providers = DataSource.Helper.DataBase.Providers.ToList();
            provider.ItemsSource = providers.OrderByDescending(x => x.ProviderId == Product.ProductProvider).ToList();
            provider.SelectedIndex = 0;

            manMan = DataSource.Helper.DataBase.Manufacturers.ToList();
            manufacturer.ItemsSource = manMan.OrderByDescending(x => x.ManId == Product.ProductManufacturer).ToList();
            manufacturer.SelectedIndex = 0;
        }
        else
        {
            UnitList = DataSource.Helper.DataBase.Units.ToList();
            UnitList.Add(new Unit() { UnitName = "Выбрать" });
            unit.ItemsSource = UnitList.OrderByDescending(x => x.UnitName == "Выбрать").ToList();
            unit.SelectedIndex = 0;

            ProductCategories = DataSource.Helper.DataBase.ProductCategories.ToList();
            ProductCategories.Add(new ProductCategory() { CategoryName = "Выбрать" });
            category.ItemsSource = ProductCategories.OrderByDescending(x => x.CategoryName == "Выбрать").ToList();
            category.SelectedIndex = 0;

            providers = DataSource.Helper.DataBase.Providers.ToList();
            providers.Add(new Provider() { ProviderName = "Выбрать" });
            provider.ItemsSource = providers.OrderByDescending(x => x.ProviderName == "Выбрать").ToList();
            provider.SelectedIndex = 0;

            manMan = DataSource.Helper.DataBase.Manufacturers.ToList();
            manMan.Add(new Manufacturer() { ManName = "Выбрать" });
            manufacturer.ItemsSource = manMan.OrderByDescending(x => x.ManName == "Выбрать").ToList();
            manufacturer.SelectedIndex = 0;
        }
    }

    private void Save_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        WarningText();
        if (error == false)
        {
            if (Product.ProductId == 0)
            {
                Product.ProductArticule = articule.Text;
                Product.ProductName = name.Text;
                Product.ProductDescription = description.Text;
                Product.ProductCost = Convert.ToDecimal(cost.Text);
                Product.ProductMaxDiscount = Convert.ToDecimal(maxDiscount.Text);
                Product.ProductDiscountNow = Convert.ToDecimal(discount.Text);
                Product.ProductCount = count.Text;
                Product.ProductManufacturer = manufacturer.SelectedIndex;
                Product.ProductProvider = provider.SelectedIndex;
                Product.ProductCategory = category.SelectedIndex;
                Product.ProductUnit = unit.SelectedIndex;

                DataSource.Helper.DataBase.Products.Add(Product!);
                DataSource.Helper.DataBase.SaveChanges();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                DataSource.Helper.DataBase.Products.Update(Product);
                DataSource.Helper.DataBase.SaveChanges();

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
        }
    }

    private void Back_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    } 


    private void SetData()
    {
        /*if (articule.Text == null || name.Text == null 
            || description == null || cost == null 
            || maxDiscount == null || discount == null 
            || count == null)
        {
            save.IsEnabled = false;
        }
        else
        {
            save.IsEnabled = true;
        }*/
    }
}
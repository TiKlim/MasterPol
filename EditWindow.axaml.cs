using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
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

    public EditWindow(Product product)
    {
        InitializeComponent();
        Product = product;
        back.Click += Back_Click;
        gridEdit.DataContext = Product;
        save.Click += Save_Click;
        Update();
        SetData();
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
        }
    }

    private void Save_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
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
    }

    private void Back_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    } 


    private void SetData()
    {

    }
}
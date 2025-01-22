using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avtoservice.Models;

namespace Avtoservice;

public partial class EditWindow : Window
{
    Product Product { get; set; }

    public EditWindow()
    {
        InitializeComponent();
        Product = new Product();
        back.Click += Back_Click;
    }

    private void Back_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        Close();
    } 

    public EditWindow(Product product)
    {
        InitializeComponent();
        Product = product;
    }


}
using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Avtoservice.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string? ProductArticule { get; set; }

    public string? ProductName { get; set; }

    public decimal? ProductCost { get; set; }

    public decimal? ProductMaxDiscount { get; set; }

    public int ProductManufacturer { get; set; }

    public int ProductProvider { get; set; }

    public int ProductCategory { get; set; }

    public decimal? ProductDiscountNow { get; set; }

    public string? ProductCount { get; set; }

    public string? ProductDescription { get; set; }

    public string? ProductImage { get; set; }

    public int ProductUnit { get; set; }

    public virtual ProductCategory ProductCategoryNavigation { get; set; } = null!;

    public virtual Manufacturer ProductManufacturerNavigation { get; set; } = null!;

    public virtual Provider ProductProviderNavigation { get; set; } = null!;

    public virtual Unit ProductUnitNavigation { get; set; } = null!;

    public Bitmap Picture => ProductImage != null ? new Bitmap($@"Assets\\{ProductImage}") : new Bitmap($@"Assets\\picture.png");

    public decimal? DiscountCost => ProductCost - (ProductCost * ProductDiscountNow/100);

    public SolidColorBrush Colors => ProductDiscountNow >= 15 ? new SolidColorBrush(Color.Parse("#7fff00")) : new SolidColorBrush(Color.Parse("White"));
}

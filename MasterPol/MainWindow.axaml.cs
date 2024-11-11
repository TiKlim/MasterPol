using Avalonia.Controls;
using Microsoft.EntityFrameworkCore;

namespace MasterPol
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetData();
        }

        private void SetData()
        {
            PartnersLB.ItemsSource = DataSource.Helper.DataBase.Partners.Include(x => x.Type).Include(x => x.PartnerProductsImports);
        }
    }
}
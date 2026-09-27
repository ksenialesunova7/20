using System.Collections.ObjectModel;
using System.Linq;
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
using System.Xml.Linq;

namespace _20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Product> products = new ObservableCollection<Product>();
        ObservableCollection<Product> filteredProduct = new ObservableCollection<Product>();
        ObservableCollection<Product> cart = new ObservableCollection<Product>();
        ObservableCollection<string> categories = new ObservableCollection<string>();

        public MainWindow()
        {

            InitializeComponent();

            products.Add(new Product { Name = "Чипсы LitEnergy", Quantity = 15, Category = "Чипсы", });
            products.Add(new Product { Name = "Бананы", Quantity = 12, Category = "Фрукты", });
            products.Add(new Product { Name = "Жвачка Orbit", Quantity = 4, Category = "Жвачки", });
            products.Add(new Product { Name = "Сырный соус", Quantity = 6, Category = "Соусы", });
            products.Add(new Product { Name = "Колбаса", Quantity = 7, Category = "Мясные изделия", });
            products.Add(new Product { Name = "Яблоки", Quantity = 8, Category = "Фрукты", });

            listProduct.ItemsSource = products;

            categories.Add("Все");
            categories.Add("Чипсы");
            categories.Add("Жвачки");
            categories.Add("Соусы");
            categories.Add("Фрукты");
            categories.Add("Мясные изделия");

            comboCategory.ItemsSource = categories;
            comboCategory.SelectedIndex = 0;

            filteredProduct = new ObservableCollection<Product>(products);
            listProduct.ItemsSource = filteredProduct;
            listCart.ItemsSource = cart;



        }
        private void listProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (listProduct.SelectedItem is Product b)
            {

                txtName.Text = b.Name;
                txtQuantity.Text = b.Quantity.ToString();
                btnBuy.IsEnabled = true;
            }
        }

        private void comboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboCategory.SelectedItem != null)
            {

                string g = comboCategory.SelectedItem.ToString();


                filteredProduct = new ObservableCollection<Product>(
                    products.Where(b => b.Category == g)
                    );

                listProduct.ItemsSource = filteredProduct;
            }
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (listProduct.SelectedItem is Product b)
            {
                if (b.Quantity > 0)
                {
                    b.Quantity--;

                    cart.Add(new Product { Name = b.Name, Quantity = 1 });


                    listProduct.SelectedItem = null;
                    txtName.Text = b.Name;
                    txtQuantity.Text = b.Quantity.ToString();
                    btnBuy.IsEnabled = false;
                }
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;

            }
            string result = "Ваш заказ: \n";

            foreach (var item in cart)
            {
                result += item.Name + "\n";

            }
            result += "\nВсего: " + cart.Count + " товаров";

            MessageBox.Show(result, "Заказ оформлен");

            cart.Clear();

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Services;

namespace WpfApp.Windows
{
    public partial class OrderItemRedactWindow : Window
    {
        SOrders SOrders = new SOrders(); SProducts SProducts = new SProducts();
        List<Order> orders; List<Product> products;
        public OrderItemRedactWindow(Order order)
        {
            InitializeComponent();
            this.Title = "Добавление единиц заказов";
            Accept.Content = "Добавить";
            this.orders = SOrders.ReadAll();
            this.products = SProducts.ReadAll();
            foreach (Order item in orders)
            {
                Order_CB.Items.Add(item.Id);
            }
            foreach (Product item in products)
            {
                ProductName_CB.Items.Add(item.Name);
            }
            Order_CB.SelectedItem = order.Id;
        }

        public OrderItemRedactWindow(OrderItem input, Order order)
        {
            InitializeComponent();
            this.Title = "Редактирование единиц заказов";
            Accept.Content = "Редактировать";
            this.orders = SOrders.ReadAll();
            this.products = SProducts.ReadAll();
            foreach (Order item in orders)
            {
                Order_CB.Items.Add(item.Id);
            }
            foreach (Product item in products)
            {
                ProductName_CB.Items.Add(item.Name);
            }
            Order_CB.SelectedItem = order.Id;
            ProductName_CB.SelectedItem = products.Find(x => x.Article == input.ProductArticle).Name;
            Quantity_TB.Text = input.Quantity.ToString();
        }

        public OrderItem? GetData()
        {
            if (Check())
            {
                OrderItem output = new OrderItem
                {
                    OrderId = int.Parse(Order_CB.SelectedItem.ToString()),
                    ProductArticle = products.Find(x=>x.Name == ProductName_CB.SelectedItem.ToString()).Article,
                    Quantity = int.Parse(Quantity_TB.Text)
                };
                return output;
            }
            return null;
        }

        public bool Check()
        {
            if (Order_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите ID заказа!");
                return false;
            }
            if (ProductName_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите товар!");
                return false;
            }
            if (Quantity_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите количество!");
                return false;
            }
            if (!int.TryParse(Quantity_TB.Text, out int Quantity))
            {
                MessageBox.Show("Количество должно быть целым числом!");
                return false;
            }
            else
            {
                if (Quantity <= 0)
                {
                    MessageBox.Show("Количество должно быть положительным числом!");
                    return false;
                }
            }
            return true;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Check())
                DialogResult = true;
        }

        private void Declaim_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

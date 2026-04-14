using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Services;

namespace WpfApp.Windows
{
    public partial class ProductRedactWindow : Window
    {
        SProducts SProducts = new SProducts();
        Product input = new Product();
        SCategories SCategories = new SCategories(); SManufactures SManufactures = new SManufactures(); SSuppliers SSuppliers = new SSuppliers();
        List<Category> categories; List<Manufacture> manufactures; List<Supplier> suppliers;
        public ProductRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление товаров";
            Accept.Content = "Добавить";
            Article_TB.Text = "0";
            this.categories = SCategories.ReadAll();
            this.manufactures = SManufactures.ReadAll();
            this.suppliers = SSuppliers.ReadAll();
            foreach (var item in categories)
            {
                Category_CB.Items.Add(item.Name);
            }
            foreach (var item in manufactures)
            {
                Manufacturer_CB.Items.Add(item.Name);
            }
            foreach (var item in suppliers)
            {
                Supplier_CB.Items.Add(item.Name);
            }
            Image.Source = new BitmapImage(new Uri("/images/picture.png", UriKind.Relative));
        }

        public ProductRedactWindow(Product input)
        {
            InitializeComponent();
            this.input = input;
            this.Title = "Редактирование товаров";
            Accept.Content = "Редактировать";
            Article_TB.Text = input.Article.ToString();
            this.categories = SCategories.ReadAll();
            this.manufactures = SManufactures.ReadAll();
            this.suppliers = SSuppliers.ReadAll();
            foreach (var item in categories)
            {
                Category_CB.Items.Add(item.Name);
            }
            foreach (var item in manufactures)
            {
                Manufacturer_CB.Items.Add(item.Name);
            }
            foreach (var item in suppliers)
            {
                Supplier_CB.Items.Add(item.Name);
            }
            Name_TB.Text = input.Name;
            Unit_TB.Text = input.Unit;
            Price_TB.Text = input.Price.ToString();
            Category_CB.SelectedItem = categories.Find(x => x.Id == input.CategoryId).Name;
            Manufacturer_CB.SelectedItem = manufactures.Find(x => x.Id == input.ManufacturerId).Name;
            Supplier_CB.SelectedItem = suppliers.Find(x => x.Id == input.SupplierId).Name;
            DiscountPercent_TB.Text = input.DiscountPercent.ToString();
            StockQuantity_TB.Text = input.StockQuantity.ToString();
            Description_TB.Text = input.Description.ToString();
            string path = GetImage(input.PhotoPath.ToString().Split('\\').Last());
            Image.Source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        }

        public Product? GetData()
        {
            if (Check())
            {
                string path = GetImage(Image.Source.ToString().Split('/').Last()).Split('\\').Last();
                Product output = new Product
                {
                    Article = Article_TB.Text,
                    Name = Name_TB.Text,
                    Unit = Unit_TB.Text,
                    Price = double.Parse(Price_TB.Text),
                    CategoryId = categories[Category_CB.SelectedIndex].Id,
                    ManufacturerId = manufactures[Manufacturer_CB.SelectedIndex].Id,
                    SupplierId = suppliers[Supplier_CB.SelectedIndex].Id,
                    DiscountPercent = int.Parse(DiscountPercent_TB.Text),
                    StockQuantity = int.Parse(StockQuantity_TB.Text),
                    Description = Description_TB.Text,
                    PhotoPath = path
                };
                if (Article_TB.Text.Length != 0)
                {
                    output.Article = Article_TB.Text;
                }
                return output;
            }
            return null;
        }

        public bool Check()
        {
            if (Category_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите категорию!");
                return false;
            }
            if (Manufacturer_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите производителя!");
                return false;
            }
            if (Supplier_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите поставщика!");
                return false;
            }
            if (Article_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите артикул!");
                return false;
            }
            if (Accept.Content.ToString() == "Добавить")
            {
                if (SProducts.ReadAll().Any(x => x.Article == Article_TB.Text))
                {
                    MessageBox.Show("Артикул должен быть уникален!");
                    return false;
                }
            }
            else
            {
                if (Article_TB.Text != input.Article)
                {
                    if (SProducts.ReadAll().Any(x => x.Article == Article_TB.Text))
                    {
                        MessageBox.Show("Артикул должен быть уникален!");
                        return false;
                    }
                }
            }
            if (Name_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите наименование!");
                return false;
            }
            if (Unit_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите единицу измерения!");
                return false;
            }
            if (!double.TryParse(Price_TB.Text, out double price))
            {
                MessageBox.Show("Цена должна быть числом с двумя или менее знаками после запятой!");
                return false;
            }
            else if (price < 0)
            {
                MessageBox.Show("Цена должна быть положительным числом!");
                return false;
            }
            if (!int.TryParse(DiscountPercent_TB.Text, out int discount))
            {
                MessageBox.Show("Скидка должна быть целым числом!");
                return false;
            }
            if (StockQuantity_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите количество!");
                return false;
            }
            if (!int.TryParse(StockQuantity_TB.Text, out int Quantity))
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
            if (Image.Source == null || GetImage(Image.Source.ToString().Split('/').Last()) == System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images\\picture.png"))
            {
                MessageBox.Show("Введите изображение!");
                return false;
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

        private void PhotoPath_B_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog { Title = "Select a File", Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*" };


            if (openFileDialog.ShowDialog() == true)
            {
                Image.Source = new BitmapImage(new Uri(GetImage(openFileDialog.FileName.Split('\\').Last()), UriKind.RelativeOrAbsolute));
            }
        }

        public static string GetImage(string relpath)
        {
            string path = "";
            string basePath = AppDomain.CurrentDomain.BaseDirectory.Replace(@"bin\Debug\net9.0-windows\", "");
            try
            {
                string relativePath = "images\\" + relpath;
                path = System.IO.Path.Combine(basePath, relativePath);
                BitmapImage image = new BitmapImage(new Uri(path, UriKind.Relative));
                double height = image.Height;
            }
            catch
            {
                string relativePath = "images\\picture.png";
                path = System.IO.Path.Combine(basePath, relativePath);
            }
            return path;
        }
    }
}

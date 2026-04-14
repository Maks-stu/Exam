using System;
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
using Microsoft.Win32;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.ClassLibrary.DTOs;
using pgDataAccess.CRUDs.Services;
using WpfApp.Windows;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfApp
{
    public partial class MainWindow : Window
    {
        SCategories SCategories = new SCategories();
        SManufactures SManufactures = new SManufactures();
        SOrders SOrders = new SOrders();
        SOrderItems SOrderItems = new SOrderItems();
        SPickupPoints SPickupPoints = new SPickupPoints();
        SProducts SProducts = new SProducts();
        SRoles SRoles = new SRoles();
        SSuppliers SSuppliers = new SSuppliers();
        SUsers SUsers = new SUsers();

        CategoryRedactWindow CategoryRedactWindow = null;
        ManufactureRedactWindow ManufactureRedactWindow = null;
        OrderRedactWindow OrderRedactWindow = null;
        PickupPointRedactWindow PickupPointRedactWindow = null;
        ProductRedactWindow ProductRedactWindow = null;
        RoleRedactWindow RoleRedactWindow = null;
        SupplierRedactWindow SupplierRedactWindow = null;
        UserRedactWindow UserRedactWindow = null;

        List<Product> Products = new List<Product>();
        List<ProductDTO> ProductDTOs = new List<ProductDTO>();

        List<Order> Orders = new List<Order>();
        List<OrderDTO> OrderDTOs = new List<OrderDTO>();
        string SelectedSupplier = "Все поставщики";
        List<MenuItem> menuItems = new List<MenuItem>();

        public MainWindow()
        {
            InitializeComponent();
            ReWrite();
        }

        public void ReWrite()
        {
            Products_Table.Items.Clear();
            Order_Table.Items.Clear();
            Suppliers.Items.Clear();

            do
            {
                Products = SProducts.ReadAll();
            } while (Products == null);
            do
            {
                Orders = SOrders.ReadAll();
            } while (Orders == null);

            ProductDTOs.Clear();
            OrderDTOs.Clear();

            foreach (Product item in Products)
            {

                string path = ProductRedactWindow.GetImage(item.PhotoPath);
                string color = "#FFFFFF";
                string pricecolor = "";
                if (item.DiscountPercent > 15)
                {
                    color = "#2E8B57";
                }
                string newprice = "";
                string line = "";
                if (item.StockQuantity == 0)
                {
                    color = "#8AC8FF";
                }
                if (item.DiscountPercent > 0)
                {
                    pricecolor = "#FF0000";
                    line = "Strikethrough";
                    newprice = Math.Round((item.Price * (1 - double.Parse(item.DiscountPercent.ToString()) / 100)), 2).ToString();
                }
                Category category;
                Manufacture manufacture;
                Supplier supplier;
                do
                {
                    category = SCategories.ReadId(item.CategoryId);
                } while (category == null);
                do
                {
                    manufacture = SManufactures.ReadId(item.ManufacturerId);
                } while (manufacture == null);
                do
                {
                    supplier = SSuppliers.ReadId(item.SupplierId);
                } while (supplier == null);
                ProductDTOs.Add(new ProductDTO
                {
                    Article = item.Article,
                    BackGround = color,
                    Image = path,
                    Category = category.Name,
                    Name = item.Name,
                    Description = item.Description,
                    Manufacturer = manufacture.Name,
                    Supplier = supplier.Name,
                    OldPriceBG = pricecolor,
                    OldPriceLine = line,
                    OldPrice = item.Price.ToString(),
                    NewPrice = newprice,
                    Dimension = item.Unit.ToString(),
                    Count = item.StockQuantity.ToString(),
                    Discount = item.DiscountPercent.ToString()
                });

                if(Search.Text != "")
                {
                    List<ProductDTO> productDTOs_filter = ProductDTOs.Where(x =>
                    x.Name.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Category.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Description.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Dimension.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Discount.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Count.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Manufacturer.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Supplier.Contains(Search.Text, StringComparison.CurrentCultureIgnoreCase)
                    ).ToList();
                    ProductDTOs = productDTOs_filter;
                }
                if (Count.Header.ToString() == "˅ Количество товаров на складе")
                {
                    ProductDTOs.Sort((x, y) => int.Parse(x.Count) - int.Parse(y.Count));
                }
                else if (Count.Header.ToString() == "˄ Количество товаров на складе")
                {
                    ProductDTOs.Sort((y, x) => int.Parse(x.Count) - int.Parse(y.Count));
                }
                if (SelectedSupplier != "Все поставщики")
                {
                    List<ProductDTO> productDTOs_filter = ProductDTOs.Where(x => x.Supplier == SelectedSupplier).ToList();
                    ProductDTOs = productDTOs_filter;
                }
            }

            foreach (ProductDTO item in ProductDTOs)
            {
                Products_Table.Items.Add(item);
            }

            menuItems.Clear();
            Suppliers.Items.Clear();

            MenuItem menu0 = new MenuItem
            {
                Header = "Все поставщики",
            };
            menu0.Click += (s, e) =>
            {
                SelectedSupplier = menu0.Header.ToString();
                ReWrite();
            };
            menuItems.Add(menu0);
            Suppliers.Items.Add(menu0);

            foreach (var item in SSuppliers.ReadAll())
            {
                MenuItem menui = new MenuItem();
                menui.Header = item.Name;
                menui.Click += (s, e) =>
                {
                    SelectedSupplier = menui.Header.ToString();
                    ReWrite();
                };
                menuItems.Add(menui);
                Suppliers.Items.Add(menui);
            }
        }

        private void Search_Changed(object sender, TextChangedEventArgs e)
        {
            ReWrite();
        }

        public void ReWrite(Product product)
        {
            Order_Table.Items.Clear();

            Orders = SOrders.ReadAll();

            OrderDTOs.Clear();

            List<OrderItem> orderItems = SOrderItems.ReadAll().Where(x => x.ProductArticle == product.Article).ToList();

            foreach (Order item in Orders)
            {
                if (orderItems.Find(x => x.OrderId == item.Id) != null)
                    OrderDTOs.Add(new OrderDTO
                    {
                        Id = item.Id,
                        Article = product.Article,
                        Status = item.Status,
                        PickupPoint = SPickupPoints.ReadId(item.PickupPointId).Address,
                        OrderDate = item.OrderDate.ToString("d MMMM yyyy 'г.'", new System.Globalization.CultureInfo("ru-RU")),
                        DeliveryDate = item.DeliveryDate.ToString("d MMMM yyyy 'г.'", new System.Globalization.CultureInfo("ru-RU"))
                    });
            }

            foreach (OrderDTO item in OrderDTOs)
            {
                Order_Table.Items.Add(item);
            }
        }

        private void Products_Table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Products_Table.SelectedItem != null)
            {
                ReWrite(SProducts.ReadId(Products[Products_Table.SelectedIndex].Article));
            }
        }

        private void AddCategory(object sender, RoutedEventArgs e)
        {
            if (CategoryRedactWindow == null)
            {
                CategoryRedactWindow = new CategoryRedactWindow();
                if(CategoryRedactWindow.ShowDialog() == true)
                {
                    Category item = CategoryRedactWindow.GetData();
                    if (item != null)
                    {
                        SCategories.Create(item);
                    }
                }
                CategoryRedactWindow = null;
            }
            ReWrite();
        }

        private void AddManufacture(object sender, RoutedEventArgs e)
        {
            if (ManufactureRedactWindow == null)
            {
                ManufactureRedactWindow = new ManufactureRedactWindow();
                if(ManufactureRedactWindow.ShowDialog() == true)
                {
                    Manufacture item = ManufactureRedactWindow.GetData();
                    if (item != null)
                    {
                        SManufactures.Create(item);
                    }
                }
                ManufactureRedactWindow = null;
            }
            ReWrite();
        }

        private void AddOrder(object sender, RoutedEventArgs e)
        {
            if (OrderRedactWindow == null)
            {
                OrderRedactWindow = new OrderRedactWindow();
                if(OrderRedactWindow.ShowDialog() == true)
                {
                    Order item = OrderRedactWindow.GetData();
                    if (item != null)
                    {
                        SOrders.Create(item);
                    }
                }
                OrderRedactWindow = null;
            }
            ReWrite();
        }

        private void AddPickupPoint(object sender, RoutedEventArgs e)
        {
            if (PickupPointRedactWindow == null)
            {
                PickupPointRedactWindow = new PickupPointRedactWindow();
                if(PickupPointRedactWindow.ShowDialog() == true)
                {
                    PickupPoint item = PickupPointRedactWindow.GetData();
                    if (item != null)
                    {
                        SPickupPoints.Create(item);
                    }
                }
                PickupPointRedactWindow = null;
            }
            ReWrite();
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            if (ProductRedactWindow == null)
            {
                ProductRedactWindow = new ProductRedactWindow();
                if(ProductRedactWindow.ShowDialog() == true)
                {
                    Product item = ProductRedactWindow.GetData();
                    if (item != null)
                    {
                        SProducts.Create(item);
                    }
                }
                ProductRedactWindow = null;
            }
            ReWrite();
        }

        private void AddRole(object sender, RoutedEventArgs e)
        {
            if (RoleRedactWindow == null)
            {
                RoleRedactWindow = new RoleRedactWindow();
                if(RoleRedactWindow.ShowDialog()  == true)
                {
                    Role item = RoleRedactWindow.GetData();
                    if (item != null)
                    {
                        SRoles.Create(item);
                    }
                }
                RoleRedactWindow = null;
            }
            ReWrite();
        }

        private void AddSupplier(object sender, RoutedEventArgs e)
        {
            if (SupplierRedactWindow == null)
            {
                SupplierRedactWindow = new SupplierRedactWindow();
                if(SupplierRedactWindow.ShowDialog() == true)
                {
                    Supplier item = SupplierRedactWindow.GetData();
                    if (item != null)
                    {
                        SSuppliers.Create(item);
                    }
                }
                SupplierRedactWindow = null;
            }
            ReWrite();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (UserRedactWindow == null)
            {
                UserRedactWindow = new UserRedactWindow();
                if(UserRedactWindow.ShowDialog() == true)
                {
                    User item = UserRedactWindow.GetData();
                    if (item != null)
                    {
                        SUsers.Create(item);
                    }
                }
                UserRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactCategory(object sender, RoutedEventArgs e)
        {
            if (CategoryRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SCategories.ReadAll());
                if(choice.ShowDialog() == true)
                {
                    if (choice.GetData() != -1)
                    {
                        Category olditem = SCategories.ReadAll()[choice.GetData()];
                        CategoryRedactWindow = new CategoryRedactWindow(olditem);
                        if(CategoryRedactWindow.ShowDialog() == true)
                        {
                            Category newitem = CategoryRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SCategories.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                CategoryRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactManufacture(object sender, RoutedEventArgs e)
        {
            if (ManufactureRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SManufactures.ReadAll());
                if(choice.ShowDialog() == true)
                {
                    if (choice.GetData() != -1)
                    {
                        Manufacture olditem = SManufactures.ReadAll()[choice.GetData()];
                        ManufactureRedactWindow = new ManufactureRedactWindow(olditem);
                        if(ManufactureRedactWindow.ShowDialog() == true)
                        {
                            Manufacture newitem = ManufactureRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SManufactures.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                ManufactureRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactOrder(object sender, RoutedEventArgs e)
        {
            if (OrderRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SOrders.ReadAll());
                if(choice.ShowDialog()== true)
                {
                    if (choice.GetData() != -1)
                    {
                        Order olditem = SOrders.ReadAll()[choice.GetData()];
                        OrderRedactWindow = new OrderRedactWindow(olditem);
                        if(OrderRedactWindow.ShowDialog() == true)
                        {
                            Order newitem = OrderRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SOrders.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                OrderRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactPickupPoint(object sender, RoutedEventArgs e)
        {
            if (PickupPointRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SPickupPoints.ReadAll());
                if(choice.ShowDialog()== true)
                {
                    if (choice.GetData() != -1)
                    {
                        PickupPoint olditem = SPickupPoints.ReadAll()[choice.GetData()];
                        PickupPointRedactWindow = new PickupPointRedactWindow(olditem);
                        if(PickupPointRedactWindow.ShowDialog() == true)
                        {
                            PickupPoint newitem = PickupPointRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SPickupPoints.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                PickupPointRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactProduct(object sender, RoutedEventArgs e)
        {
            if (ProductRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SProducts.ReadAll());
                if(choice.ShowDialog() == true)
                {
                    if (choice.GetData() != -1)
                    {
                        Product olditem = SProducts.ReadAll()[choice.GetData()];
                        ProductRedactWindow = new ProductRedactWindow(olditem);
                        if(ProductRedactWindow.ShowDialog() == true)
                        {
                            Product newitem = ProductRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SProducts.Update(olditem.Article, newitem);
                            }
                        }
                    }
                }
                ProductRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactRole(object sender, RoutedEventArgs e)
        {
            if (RoleRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SRoles.ReadAll());
                if(choice.ShowDialog() == true)
                {
                    if (choice.GetData() != -1)
                    {
                        Role olditem = SRoles.ReadAll()[choice.GetData()];
                        RoleRedactWindow = new RoleRedactWindow(olditem);
                        if(RoleRedactWindow.ShowDialog()== true)
                        {
                            Role newitem = RoleRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SRoles.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                RoleRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactSupplier(object sender, RoutedEventArgs e)
        {
            if (SupplierRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SSuppliers.ReadAll());
                if(choice.ShowDialog()== true)
                {
                    if (choice.GetData() != -1)
                    {
                        Supplier olditem = SSuppliers.ReadAll()[choice.GetData()];
                        SupplierRedactWindow = new SupplierRedactWindow(olditem);
                        if(SupplierRedactWindow.ShowDialog()== true)
                        {
                            Supplier newitem = SupplierRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SSuppliers.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                SupplierRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactUser(object sender, RoutedEventArgs e)
        {
            if (UserRedactWindow == null)
            {
                ChoiceWindow choice = new ChoiceWindow(SUsers.ReadAll());
                if(choice.ShowDialog()== true)
                {
                    if (choice.GetData() != -1)
                    {
                        User olditem = SUsers.ReadAll()[choice.GetData()];
                        UserRedactWindow = new UserRedactWindow(olditem);
                        if(UserRedactWindow.ShowDialog()== true)
                        {
                            User newitem = UserRedactWindow.GetData();
                            if (newitem != null)
                            {
                                SUsers.Update(olditem.Id, newitem);
                            }
                        }
                    }
                }
                UserRedactWindow = null;
            }
            ReWrite();
        }

        private void DeleteCategory(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SCategories.ReadAll());
            if(choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Category item = SCategories.ReadAll()[choice.GetData()];
                        SCategories.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void DeleteManufacture(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SManufactures.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Manufacture item = SManufactures.ReadAll()[choice.GetData()];
                        SManufactures.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SOrders.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Order item = SOrders.ReadAll()[choice.GetData()];
                        SOrders.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void DeletePickupPoint(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SPickupPoints.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        PickupPoint item = SPickupPoints.ReadAll()[choice.GetData()];
                        SPickupPoints.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SProducts.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Product item = SProducts.ReadAll()[choice.GetData()];
                        SProducts.Delete(item.Article);
                    }
                }
            }
            ReWrite();
        }

        private void DeleteRole(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SRoles.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Role item = SRoles.ReadAll()[choice.GetData()];
                        SRoles.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void DeleteSupplier(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SSuppliers.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        Supplier item = SSuppliers.ReadAll()[choice.GetData()];
                        SSuppliers.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            ChoiceWindow choice = new ChoiceWindow(SUsers.ReadAll());
            if (choice.ShowDialog() == true)
            {
                if (choice.GetData() != -1)
                {
                    if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        User item = SUsers.ReadAll()[choice.GetData()];
                        SUsers.Delete(item.Id);
                    }
                }
            }
            ReWrite();
        }

        private void Count_Click(object sender, RoutedEventArgs e)
        {
            if (Count.Header.ToString().ToString() == "Количество товаров на складе")
            {
                Count.Header = "˅ Количество товаров на складе";
            }
            else if (Count.Header.ToString() == "˅ Количество товаров на складе")
            {
                Count.Header = "˄ Количество товаров на складе";
            }
            else if (Count.Header.ToString() == "˄ Количество товаров на складе")
            {
                Count.Header = "Количество товаров на складе";
            }
            ReWrite();
        }

        private void AddProductTable(object sender, RoutedEventArgs e)
        {
            if (ProductRedactWindow == null)
            {
                ProductRedactWindow = new ProductRedactWindow();
                if (ProductRedactWindow.ShowDialog() == true)
                {
                    Product item = ProductRedactWindow.GetData();
                    if (item != null)
                    {
                        SProducts.Create(item);
                    }
                }
                ProductRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactProductTable(object sender, RoutedEventArgs e)
        {
            if (ProductRedactWindow == null)
            {
                if (Products_Table.SelectedIndex != -1)
                {
                    Product olditem = SProducts.ReadId(ProductDTOs[Products_Table.SelectedIndex].Article);
                    ProductRedactWindow = new ProductRedactWindow(olditem);
                    if (ProductRedactWindow.ShowDialog() == true)
                    {
                        Product newitem = ProductRedactWindow.GetData();
                        if (newitem != null)
                        {
                            SProducts.Update(olditem.Article, newitem);
                        }
                    }
                }
                ProductRedactWindow = null;
            }
            ReWrite();
        }

        private void DeleteProductTable(object sender, RoutedEventArgs e)
        {
            if (Products_Table.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Product item = SProducts.ReadId(ProductDTOs[Products_Table.SelectedIndex].Article);
                    SProducts.Delete(item.Article);
                }
            }
            ReWrite();
        }

        private void AddOrderTable(object sender, RoutedEventArgs e)
        {
            if (OrderRedactWindow == null)
            {
                OrderRedactWindow = new OrderRedactWindow();
                if (OrderRedactWindow.ShowDialog() == true)
                {
                    Order item = OrderRedactWindow.GetData();
                    if (item != null)
                    {
                        SOrders.Create(item);
                    }
                }
                OrderRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactOrderTable(object sender, RoutedEventArgs e)
        {
            if (OrderRedactWindow == null)
            {
                if (Order_Table.SelectedIndex != -1)
                {
                    Order olditem = SOrders.ReadId(OrderDTOs[Order_Table.SelectedIndex].Id);
                    OrderRedactWindow = new OrderRedactWindow(olditem);
                    if (OrderRedactWindow.ShowDialog() == true)
                    {
                        Order newitem = OrderRedactWindow.GetData();
                        if (newitem != null)
                        {
                            SOrders.Update(olditem.Id, newitem);
                        }
                    }
                }
                OrderRedactWindow = null;
            }
            ReWrite();
        }

        private void DeleteOrderTable(object sender, RoutedEventArgs e)
        {
            if (Order_Table.SelectedIndex != -1)
            {
                if (MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Order item = SOrders.ReadId(OrderDTOs[Order_Table.SelectedIndex].Id);
                    SOrders.Delete(item.Id);
                }
            }
            ReWrite();
        }
    }
}
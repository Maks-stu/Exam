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
using pgDataAccess.ClassLibrary.DTOs;
using pgDataAccess.CRUDs.Services;

namespace WpfApp.Windows
{
    public partial class OrderRedactWindow : Window
    {
        SUsers SUsers = new SUsers(); SPickupPoints SPickupPoints = new SPickupPoints(); SProducts SProducts = new SProducts();
        List<User> clients; List<PickupPoint> pickupPoints; Order input;
        SOrderItems SOrderItems = new SOrderItems(); List<OrderItem> OrderItems = new List<OrderItem>();
        OrderItemRedactWindow OrderItemRedactWindow = null;
        List<OrderItemDTO> orderItemDTOs = new List<OrderItemDTO>();
        public OrderRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление заказов";
            Accept.Content = "Добавить";
            Status_CB.Items.Add("Новый");
            Status_CB.Items.Add("Завершен");
            ID_TB.Text = "0";
            this.clients = SUsers.ReadAll();
            this.pickupPoints = SPickupPoints.ReadAll();
            foreach (User item in clients)
            {
                Client_CB.Items.Add(item.FullName);
            }
            foreach (var item in pickupPoints)
            {
                PickupPoint_CB.Items.Add(item.Address);
            }
            Items_Table.Visibility = Visibility.Collapsed;
        }

        public OrderRedactWindow(Order input)
        {
            InitializeComponent();
            this.Title = "Редактирование заказов";
            Accept.Content = "Редактировать";
            Status_CB.Items.Add("Новый");
            Status_CB.Items.Add("Завершен");
            this.clients = SUsers.ReadAll();
            this.pickupPoints = SPickupPoints.ReadAll();
            this.OrderItems = SOrderItems.ReadAll();
            this.input = input;
            foreach (User item in clients)
            {
                Client_CB.Items.Add(item.FullName);
            }
            foreach (var item in pickupPoints)
            {
                PickupPoint_CB.Items.Add(item.Address);
            }
            ID_TB.Text = input.Id.ToString();
            Client_CB.SelectedItem = clients.Find(x => x.Id == input.ClientId).FullName;
            PickupPoint_CB.SelectedItem = pickupPoints.Find(x => x.Id == input.PickupPointId).Address;
            DeliveryDate_DT.SelectedDate = input.DeliveryDate.ToDateTime(TimeOnly.MinValue);
            Status_CB.SelectedItem = input.Status.ToString();
            PickupCode_TB.Text = input.PickupCode.ToString();
            ReWrite();
        }

        public Order? GetData()
        {
            if (Check())
            {
                Order output = new Order
                {
                    Id = int.Parse(ID_TB.Text),
                    ClientId = clients[Client_CB.SelectedIndex].Id,
                    PickupPointId = pickupPoints[PickupPoint_CB.SelectedIndex].Id,
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    DeliveryDate = DateOnly.FromDateTime(DeliveryDate_DT.SelectedDate.Value),
                    Status = Status_CB.SelectedItem.ToString(),
                    PickupCode = int.Parse(PickupCode_TB.Text)
                };
                if (int.Parse(ID_TB.Text) != 0)
                {
                    output.Id = int.Parse(ID_TB.Text);
                }
                return output;
            }
            return null;
        }

        public bool Check()
        {
            if (Client_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента!");
                return false;
            }
            if (PickupPoint_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите пункт выдачи!");
                return false;
            }
            if (Status_CB.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите статус!");
                return false;
            }
            if (DeliveryDate_DT.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату доставки!");
                return false;
            }
            if (!int.TryParse(PickupCode_TB.Text, out _))
            {
                MessageBox.Show("Код пункта должен быть целым цислом!");
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

        private void ReWrite()
        {
            Items_Table.Items.Clear();


            orderItemDTOs.Clear();

            OrderItems = SOrderItems.ReadAll().Where(x => x.OrderId == int.Parse(ID_TB.Text)).ToList();

            foreach (OrderItem item in OrderItems)
            {
                orderItemDTOs.Add(new OrderItemDTO
                {
                    OrderId = item.OrderId,
                    ProductArticle = item.ProductArticle,
                    ProductName = SProducts.ReadId(item.ProductArticle).Name+" ",
                    Quantity = item.Quantity+" " + SProducts.ReadId(item.ProductArticle).Unit,
                });
            }
            foreach (var item in orderItemDTOs)
            {
                Items_Table.Items.Add(item);
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            if(OrderItemRedactWindow == null)
            {
                OrderItemRedactWindow = new OrderItemRedactWindow(input);
                if(OrderItemRedactWindow.ShowDialog() == true)
                {
                    OrderItem item = OrderItemRedactWindow.GetData();
                    if (item != null)
                    {
                        if(SOrderItems.ReadAll().Find(x=> x.OrderId ==item.OrderId && x.ProductArticle == item.ProductArticle) == null)
                        {
                            Items_Table.Items.Add(item);
                            SOrderItems.Create(item);
                        }
                        else
                        {
                            MessageBox.Show("Такой элемент уже есть в списке!");
                        }
                    }
                }
                OrderItemRedactWindow = null;
            }
            ReWrite();
        }

        private void RedactItem(object sender, RoutedEventArgs e)
        {
            OrderItem olditem = OrderItems[Items_Table.SelectedIndex];
            if (OrderItemRedactWindow == null && Items_Table.SelectedIndex != -1)
            {
                OrderItemRedactWindow = new OrderItemRedactWindow(OrderItems[Items_Table.SelectedIndex],input);
                if(OrderItemRedactWindow.ShowDialog() == true)
                {
                    OrderItem item = OrderItemRedactWindow.GetData();
                    if (item != null)
                    {
                        if(olditem.OrderId == item.OrderId &&  item.ProductArticle == item.ProductArticle)
                        {
                            Items_Table.Items[Items_Table.SelectedIndex] = item;
                            SOrderItems.Update(olditem.OrderId, olditem.ProductArticle, item);
                        }
                        if (SOrderItems.ReadAll().Find(x => x.OrderId == item.OrderId && x.ProductArticle == item.ProductArticle) == null)
                        {
                            Items_Table.Items[Items_Table.SelectedIndex] = item;
                            SOrderItems.Update(olditem.OrderId, olditem.ProductArticle, item);
                        }
                        else
                        {
                            MessageBox.Show("Такой элемент уже есть в списке!");
                        }
                    }
                }
                OrderItemRedactWindow = null;
            }
            ReWrite();
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            OrderItem olditem = OrderItems[Items_Table.SelectedIndex];
            if (Items_Table.SelectedIndex != -1)
            {
                if(MessageBox.Show("Вы точно хотите удалить этот элемент?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Items_Table.Items.Remove(OrderItems[Items_Table.SelectedIndex]);
                    SOrderItems.Delete(olditem.OrderId, olditem.ProductArticle);
                }
            }
            ReWrite();
        }
    }
}

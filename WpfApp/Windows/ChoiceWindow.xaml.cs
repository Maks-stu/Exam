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
    public partial class ChoiceWindow : Window
    {
        public ChoiceWindow(List<Category> datas)
        {
            InitializeComponent();
            this.Title = "Выбор категории товаров";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.Name);
            }
        }

        public ChoiceWindow(List<Manufacture> datas)
        {
            InitializeComponent();
            this.Title = "Выбор производителей";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.Name);
            }
        }

        public ChoiceWindow(List<Order> datas)
        {
            InitializeComponent();
            SUsers sUsers = new SUsers();
            this.Title = "Выбор заказов";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(sUsers.ReadAll().Find(x=>x.Id==item.ClientId).FullName+" "+ item.OrderDate.ToString("d MMMM yyyy 'г.'", new System.Globalization.CultureInfo("ru-RU")));
            }
        }

        public ChoiceWindow(List<PickupPoint> datas)
        {
            InitializeComponent();
            this.Title = "Выбор пунктов выдачи";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.Address);
            }
        }

        public ChoiceWindow(List<Product> datas)
        {
            InitializeComponent();
            this.Title = "Выбор товаров";
            SCategories scategories = new SCategories();
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.Name+"("+scategories.ReadId(item.CategoryId).Name+")");
            }
        }

        public ChoiceWindow(List<Role> datas)
        {
            InitializeComponent();
            this.Title = "Выбор ролей пользователей";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.Name);
            }
        }

        public ChoiceWindow(List<Supplier> datas)
        {
            InitializeComponent();
            this.Title = "Выбор поставщиков";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.Name);
            }
        }

        public ChoiceWindow(List<User> datas)
        {
            InitializeComponent();
            this.Title = "Выбор пользователей";
            foreach (var item in datas)
            {
                Choice_CB.Items.Add(item.FullName);
            }
        }

        public int GetData()
        {
            return Choice_CB.SelectedIndex;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Choice_CB.SelectedIndex != -1)
                DialogResult = true;
        }

        private void Declaim_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

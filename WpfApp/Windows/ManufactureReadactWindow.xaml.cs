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

namespace WpfApp.Windows
{
    public partial class ManufactureRedactWindow : Window
    {
        public ManufactureRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление производителей";
            Accept.Content = "Добавить";
            ID_TB.Text = "0";
        }

        public ManufactureRedactWindow(Manufacture input)
        {
            InitializeComponent();
            this.Title = "Редактирование производителей";
            Accept.Content = "Редактировать";
            ID_TB.Text = input.Id.ToString();
            Name_TB.Text = input.Name;
        }

        public Manufacture? GetData()
        {
            if (Check())
            {
                Manufacture output = new Manufacture
                {
                    Name = Name_TB.Text
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
            if (Name_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите наименование!");
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
    }
}

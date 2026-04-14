using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Windows;
using pgDataAccess.ClassLibrary.Classes;
using pgDataAccess.CRUDs.Services;

namespace WpfApp.Windows
{
    public partial class UserRedactWindow : Window
    {
        SRoles SRoles = new SRoles();
        List<Role> roles;
        string password = "";
        public UserRedactWindow()
        {
            InitializeComponent();
            this.Title = "Добавление пользователей";
            Accept.Content = "Добавить";
            this.roles = SRoles.ReadAll();
            foreach (var item in roles)
            {
                Role_CB.Items.Add(item.Name);
            }
            ID_TB.Text = "0";
        }

        public UserRedactWindow(User input)
        {
            InitializeComponent();
            this.Title = "Редактирование пользователей";
            Accept.Content = "Редактировать";
            this.roles = SRoles.ReadAll();
            foreach (var item in roles)
            {
                Role_CB.Items.Add(item.Name);
            }
            Role_CB.SelectedItem = roles.Find(x => x.Id == input.RoleId).Name;
            ID_TB.Text = input.Id.ToString();
            Login_TB.Text = input.Login;
            PasswordHash_PB.Visibility = Visibility.Collapsed;
            password = input.PasswordHash;
            FullName_TB.Text = input.FullName;
        }

        public User? GetData()
        {
            User output;
            if (Check())
            {
                if(PasswordHash_PB.Visibility != Visibility.Collapsed)
                {
                    output = new User
                    {
                        Login = Login_TB.Text,
                        PasswordHash = Convert.ToBase64String(
                            SHA256.HashData(
                                System.Text.Encoding.UTF8.GetBytes(
                                    PasswordHash_PB.Password))),
                        FullName = FullName_TB.Text,
                        RoleId = roles[Role_CB.SelectedIndex].Id
                    };
                    if (int.Parse(ID_TB.Text) != 0)
                    {
                        output.Id = int.Parse(ID_TB.Text);
                    }
                }
                else
                {
                    output = new User
                    {
                        Login = Login_TB.Text,
                        PasswordHash = password,
                        FullName = FullName_TB.Text,
                        RoleId = roles[Role_CB.SelectedIndex].Id
                    };
                    if (int.Parse(ID_TB.Text) != 0)
                    {
                        output.Id = int.Parse(ID_TB.Text);
                    }
                }
                return output;
            }
            return null;
        }

        public bool Check()
        {
            if (FullName_TB.Text.Length == 0)
            {
                MessageBox.Show("Введите ФИО!");
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

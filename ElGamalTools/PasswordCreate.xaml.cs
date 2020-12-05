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

namespace ElGamalTools
{
    /// <summary>
    /// Логика взаимодействия для PasswordCreate.xaml
    /// </summary>
    public partial class PasswordCreate : Window
    {

        public string passwordF;

        public PasswordCreate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (password.Password.Length < 6 || repeatPassword.Password.Length < 6)
            {
                MessageBox.Show("Пароль должен быть больше 5 символов");
                return;
            }

            if(password.Password  == repeatPassword.Password)
            {
                passwordF = password.Password;
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Введенные пароли не совпадают");
            }

        }
    }
}

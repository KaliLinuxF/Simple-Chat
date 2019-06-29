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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

//MyDataBaseController MyData = new MyDataBaseController(@"DESKTOP-G5EG346\SQLEXPRESS", "SC_Users");
//DataTable dt_user = MyData.Select("SELECT * FROM [dbo].[tbUsers]"); // получаем данные из таблицы
//MyData.Select("INSERT INTO [dbo].[tbUsers] VALUES (1, 'test', 'test')"); // заносим запись в таблицу


namespace Simple_Chat
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        MyDataBaseController MyData = new MyDataBaseController(@"DESKTOP-G5EG346\SQLEXPRESS", "SC_Users");

        public string DBUserName { get; set; }
    

        public Login()
        {
           
            InitializeComponent();
            
        }

        private int setID()
        {
            DataTable dt_user = MyData.Select("SELECT * FROM [dbo].[tbUsers]");
            int temp = dt_user.Rows.Count;
            return temp++;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt_user = MyData.Select("SELECT * FROM [dbo].[tbUsers]");

            bool login = false;

            for (int i = 0; i < dt_user.Rows.Count; i++)
            {
                if((string)dt_user.Rows[i][1] == tbUserName.Text)
                {
                    if((string)dt_user.Rows[i][2] == tbPassword.Text)
                    {
                        login = true;
                        DBUserName = dt_user.Rows[i][1].ToString();
                        MainWindow main = new MainWindow(DBUserName);
                        main.Show();
                        this.Hide();
                    }
                }
            }

            if(!login)
            {
                MessageBox.Show("Неправельный логин, или пароль..");
            }
            
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt_user = MyData.Select("SELECT * FROM [dbo].[tbUsers]");

            int errorCode = 0;

            if (tbUserName.Text.Length >= 5 && tbPassword.Text.Length >= 5)
            {
                for (int i = 0; i < dt_user.Rows.Count; i++)
                {
                    if(tbUserName.Text == (string)dt_user.Rows[i][1])
                    {
                        errorCode = 1;
                    }
                }
            }
            else
            {
                MessageBox.Show("Минимальная длина логина и пароля - 6");
            }

            if(errorCode == 0)
            {
                DBUserName = tbUserName.Text;
                MyData.Select($@"INSERT INTO [dbo].[tbUsers] VALUES ({setID()}, '{tbUserName.Text}', '{tbPassword.Text}')");
                
                MainWindow main = new MainWindow(DBUserName);
                main.Show();
                this.Hide();
            }
            else if(errorCode == 1)
            {
                MessageBox.Show("Такое имя пользователя уже есть в базе данных!");
                tbUserName.Text = "";
            }

        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}

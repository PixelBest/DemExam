using DemExam.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
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

namespace DemExam.View
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;";
        Employee emp;

        public Login()
        {
            InitializeComponent();
        }

        private void Entry_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Employee> logins = GetEmployee();
            if (login_txb.Text != "" && password_txb.Password != "")
            {
                foreach (var item in logins)
                {
                    if (login_txb.Text == item.Login && password_txb.Password == item.Password)
                    {
                        emp = item;
                        UpdateEmploуee();
                        Orders orders = new Orders();
                        this.Close();
                        orders.ShowDialog();
                        return;
                    }
                }
                MessageBox.Show("Неправильно введены данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
                MessageBox.Show("Данные не введены", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Exit_Click(object sender, RoutedEventArgs e) =>
            Application.Current.Shutdown();

        public void UpdateEmploуee()
        {
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string test = DateTime.Now.ToString();
            string sql = $"UPDATE Employee SET ДатаВремяВхода = '{DateTime.Now.ToString()}' WHERE Id = {emp.Id}";
            OleDbCommand command = new OleDbCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public ObservableCollection<Employee> GetEmployee()
        {
            ObservableCollection<Employee> logins = new ObservableCollection<Employee>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM employee";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string login = reader.GetString(1);
                string password = reader.GetString(2);
                string fio = reader.GetString(3);

                Employee employee = new Employee();
                employee.Id = id;
                employee.Login = login;
                employee.Password = password;
                employee.FIO = fio;
                logins.Add(employee);
            }
            return logins;
        }
    }
}

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
    /// Логика взаимодействия для CreateOrder.xaml
    /// </summary>
    public partial class CreateOrder : Window
    {
        string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;";

        Orders orders;
        ObservableCollection<FL> FL;
        ObservableCollection<YL> YL;
        ObservableCollection<ServiceOTK> ListServiceOTK;
        public CreateOrder(Orders o)
        {
            InitializeComponent();
            orders = o;
            FL = o.FL;
            YL = o.YL;
            ListServiceOTK = o.ListServiceOTK;
            FillCmb();
        }
        private void Add_click(object sender, RoutedEventArgs e)
        {
            if (service_cmb.SelectedItem != null && position_cmb.SelectedItem != null && client_cmb.SelectedItem != null)
            {
                FullOrder newfullOrder = new FullOrder()
                {
                    Name = service_cmb.SelectedItem.ToString(),
                    Price = ListServiceOTK[service_cmb.SelectedIndex].Price,
                    Deadline = ListServiceOTK[service_cmb.SelectedIndex].Deadline,
                    AvgDeviation = ListServiceOTK[service_cmb.SelectedIndex].AvgDeviation,
                    Date = DateTime.Now,
                    StatusOrder = "Сборка",
                    StatusServiceInOrder = "В работе",
                    Client = client_cmb.SelectedItem.ToString()
                };
                AddToDB(newfullOrder);
                orders.ListFullOrder.Add(newfullOrder);
                orders.dtg.Items.Refresh();
            }
            else
                MessageBox.Show("Не все поля заполнены","Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void FillCmb()
        {
            List<string> service = new List<string>();
            foreach (var item in ListServiceOTK)
                service.Add(item.Name);
            service_cmb.ItemsSource = service;
            List<string> ylFl = new List<string>() { "ЮЛ", "ФЛ"};
            position_cmb.ItemsSource = ylFl;
        }

        private void Changed_cmb(object sender, SelectionChangedEventArgs e)
        {
            if(position_cmb.SelectedItem == "ЮЛ")
            {
                List<string> clients = new List<string>();
                foreach (var item in YL)
                    clients.Add(item.FIO);
                client_cmb.ItemsSource = clients;
            }
            else if(position_cmb.SelectedItem == "ФЛ")
            {
                List<string> clients = new List<string>();
                foreach (var item in FL)
                    clients.Add(item.FIO);
                client_cmb.ItemsSource = clients;
            }
        }

        public void AddToDB(FullOrder fullOrder)
        {
            List<int> id = new List<int>();
            foreach (var item in orders.ListOrders)
                id.Add(item.Id);
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO Orders VALUES ('{id.Max() + 1}','{fullOrder.Date}', '{ListServiceOTK[service_cmb.SelectedIndex].Id}', '{fullOrder.StatusOrder}', '{fullOrder.StatusServiceInOrder}', '{fullOrder.Deadline}', '{fullOrder.Price}', '{client_cmb.SelectedIndex + 1}', '{position_cmb.SelectedItem}')";
            OleDbCommand command = new OleDbCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Заказ успешно добавлен", "Успех", MessageBoxButton.OK);
            orders.ListOrders = orders.GetOrder();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e) =>
            this.Close();

    }
}

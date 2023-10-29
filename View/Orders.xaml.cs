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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;";
        ObservableCollection<Order> ListOrders;
        ObservableCollection<ServiceOTK> ListServiceOTK;
        ObservableCollection<FullOrder> ListFullOrder;
        public Orders()
        {
            InitializeComponent();
            ListOrders = GetOrder();
            ListServiceOTK = GetServiceOTK();
            ListFullOrder = GetFullOrder();
            dtg.ItemsSource = ListFullOrder;
        }




        public ObservableCollection<FullOrder> GetFullOrder()
        {
            ObservableCollection<FullOrder> list = new ObservableCollection<FullOrder>();
            foreach(Order item in ListOrders)
            {
                foreach(ServiceOTK item2 in ListServiceOTK)
                {
                    if(item.Service == item2.Id)
                    {
                        FullOrder full = new FullOrder();
                        full.Name = item2.Name ;
                        full.Price = item2.Price;
                        full.Deadline = item2.Deadline;
                        full.AvgDeviation = item2.AvgDeviation;
                        full.Date = item.Date;
                        full.StatusOrder = item.StatusOrder;
                        full.StatusServiceInOrder = item.StatusServiceInOrder;
                        list.Add(full);
                    }
                }
            }
            return list;
        }

        public ObservableCollection<Order> GetOrder()
        {
            ObservableCollection<Order> orders = new ObservableCollection<Order>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM Orders";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                DateTime date = reader.GetDateTime(1);
                int service = reader.GetInt32(2);
                string statusOrder = reader.GetString(3);
                string statusServiceInOrder = reader.GetString(4);
                string leadTime = reader.GetString(5);
                string priceOrder = reader.GetString(6);

                Order order = new Order();
                order.Date = date;
                order.Service = service;
                order.StatusOrder = statusOrder;
                order.StatusServiceInOrder = statusServiceInOrder;
                order.LeadTime = leadTime;
                order.PriceOrder = priceOrder;
                orders.Add(order);
            }
            return orders;
        }
        
        public ObservableCollection<ServiceOTK> GetServiceOTK()
        {
            ObservableCollection<ServiceOTK> listServiceOTK = new ObservableCollection<ServiceOTK>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM ServiceOTK";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string price = reader.GetString(2);
                string deadline = reader.GetString(3);
                string avgDeviation = reader.GetString(4);

                ServiceOTK serviceOTK = new ServiceOTK();
                serviceOTK.Id = id;
                serviceOTK.Name = name;
                serviceOTK.Price = price;
                serviceOTK.Deadline = deadline;
                serviceOTK.AvgDeviation = avgDeviation;
                listServiceOTK.Add(serviceOTK);
            }
            return listServiceOTK;
        }
    }
}

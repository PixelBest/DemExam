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
        public ObservableCollection<Order> ListOrders;
        public ObservableCollection<ServiceOTK> ListServiceOTK;
        public ObservableCollection<FullOrder> ListFullOrder;
        public ObservableCollection<FL> FL;
        public ObservableCollection<YL> YL;
        public Orders()
        {
            InitializeComponent();
            FL = GetFL();
            YL = GetYL();
            ListOrders = GetOrder();
            ListServiceOTK = GetServiceOTK();
            ListFullOrder = GetFullOrder();
            dtg.ItemsSource = ListFullOrder;
            (dtg.Columns[4] as DataGridTextColumn).Binding.StringFormat = "dd.MM.yyyy";
        }




        public ObservableCollection<FullOrder> GetFullOrder()
        {
            ObservableCollection<FullOrder> list = new ObservableCollection<FullOrder>();
            foreach(Order item in ListOrders)
                foreach(ServiceOTK item2 in ListServiceOTK)
                    if(item.Service == item2.Id)
                    {
                        FullOrder full = new FullOrder()
                        {
                            Name = item2.Name ,
                            Price = item2.Price,
                            Deadline = item2.Deadline,
                            AvgDeviation = item2.AvgDeviation,
                            Date = item.Date,
                            StatusOrder = item.StatusOrder,
                            StatusServiceInOrder = item.StatusServiceInOrder
                        };
                        if (item.ClientPosition == "ФЛ")
                        {
                            foreach (var fl in FL)
                                if (item.ClientId == fl.Id)
                                    full.Client = fl.FIO;
                        }
                        else
                            foreach(var yl in YL)
                                if (item.ClientId == yl.Id)
                                    full.Client = yl.FIO;
                        list.Add(full);
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
                int clientId = reader.GetInt32(7);
                string clientPosition = reader.GetString(8);

                Order order = new Order()
                {
                    Id = id,
                    Date = date,
                    Service = service,
                    StatusOrder = statusOrder,
                    StatusServiceInOrder = statusServiceInOrder,
                    LeadTime = leadTime,
                    PriceOrder = priceOrder,
                    ClientId = clientId,
                    ClientPosition = clientPosition
                };
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
        
        public ObservableCollection<YL> GetYL()
        {
            ObservableCollection<YL> listYL = new ObservableCollection<YL>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM YL";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                string company = reader.GetString(1);
                string adress = reader.GetString(2);
                string inn = reader.GetString(3);
                string r_s = reader.GetString(4);
                string bik = reader.GetString(5);
                string fioHead = reader.GetString(6);
                string fio = reader.GetString(7);
                string phone = reader.GetString(8);
                string e_mail = reader.GetString(9);
                string password = reader.GetString(10);

                YL yL = new YL()
                {
                    Id = id,
                    CompanyName = company,
                    Adress = adress,
                    INN = inn,
                    R_S = r_s,
                    BIK = bik,
                    FIOHead = fioHead,
                    FIO = fio,
                    Phone = phone,
                    E_mail = e_mail,
                    Password = password
                };
                listYL.Add(yL);
            }
            return listYL;
        }
        
        public ObservableCollection<FL> GetFL()
        {
            ObservableCollection<FL> listFL = new ObservableCollection<FL>();

            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string query = "SELECT * FROM FL";
            OleDbCommand command = new OleDbCommand(query, connection);
            OleDbDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                string e_mail = reader.GetString(1);
                string password = reader.GetString(2);
                string fio = reader.GetString(3);
                string dateOfBirth = reader.GetString(4);
                string pasport = reader.GetString(5);
                string phone = reader.GetString(6);

                FL fl = new FL()
                {
                    Id = id,
                    E_mail = e_mail,
                    Password = password,
                    FIO = fio,
                    DateOfBirth = dateOfBirth,
                    Pasport = pasport,
                    Phone = phone                
                };
                listFL.Add(fl);
            }
            return listFL;
        }

        private void CreateOrder_click(object sender, RoutedEventArgs e)
        {
            CreateOrder createOrder = new CreateOrder(this);
            createOrder.ShowDialog();
        }
    }
}

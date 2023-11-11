using DemExam.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для AddYl.xaml
    /// </summary>
    public partial class AddYl : Window
    {
        string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;";
        Orders orders;
        public AddYl(Orders o)
        {
            InitializeComponent();
            orders = o;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(nameCompany_txb.Text != "" && adres_txb.Text != "" && inn_txb.Text != "" && r_s_txb.Text != "" && bik_txb.Text != "" && fioHead_txb.Text != "" && fio_txb.Text != "" && phone_txb.Text != "" && email_txb.Text != "" && password_txb.Text != "")
            {
                YL newYl = new YL()
                {
                    CompanyName = nameCompany_txb.Text,
                    Adress = adres_txb.Text,
                    INN = inn_txb.Text,
                    R_S = r_s_txb.Text,
                    BIK = bik_txb.Text,
                    FIOHead = fioHead_txb.Text,
                    FIO = fio_txb.Text,
                    Phone = phone_txb.Text,
                    E_mail = email_txb.Text,
                    Password = password_txb.Text
                };
                AddNewYl(newYl);
                orders.YL.Add(newYl);
            }
        }

        public void AddNewYl(YL newYl)
        {
            List<int> id = new List<int>();
            foreach (var item in orders.YL)
                id.Add(item.Id);
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO YL VALUES ('{id.Max() + 1}','{newYl.CompanyName}', '{newYl.Adress}', '{newYl.INN}', '{newYl.R_S}', '{newYl.BIK}', '{newYl.FIOHead}', '{newYl.FIO}', '{newYl.Phone}', '{newYl.E_mail}', '{newYl.Password}')";
            OleDbCommand command = new OleDbCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Клиент успешно добавлен", "Успех", MessageBoxButton.OK);
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e) =>
            this.Close();

        private void nameCompany_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "[a-zA-Zа-яА-Я0-9\"']"))
                e.Handled = true;
        }

        private void adres_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, "[а-яА-Я0-9\"'.]"))
                e.Handled = true;
        }

        private void pasport_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[0-9]"))
                e.Handled = true;
        }

        private void fio_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[а-яА-Я]"))
                e.Handled = true;
        }

        private void email_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[a-zA-Z0-9@._]"))
                e.Handled = true;
        }

        private void phone_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[0-9+]"))
                e.Handled = true;
        }
    }
}

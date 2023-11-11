using DemExam.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.OleDb;
using System.Linq;
using System.Linq.Expressions;
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
    /// Логика взаимодействия для AddFl.xaml
    /// </summary>
    public partial class AddFl : Window
    {
        string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DB.accdb;";
        Orders order;
        public AddFl(Orders o)
        {
            InitializeComponent();
            order = o;
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if(email_txb.Text != "" && fio_txb.Text != "" && birth_txb.Text != "" && pasport_txb.Text != "" && phone_txb.Text != "" && password_txb.Text != "")
            {
                FL newFl = new FL()
                {
                    E_mail = email_txb.Text,
                    FIO = fio_txb.Text,
                    DateOfBirth = birth_txb.Text,
                    Pasport = pasport_txb.Text,
                    Phone = phone_txb.Text,
                    Password = password_txb.Text
                };
                AddNewFl(newFl);
                order.FL.Add(newFl);
            }
        }

        public void AddNewFl(FL newFl)
        {
            List<int> id = new List<int>();
            foreach (var item in order.FL)
                id.Add(item.Id);
            OleDbConnection connection = new OleDbConnection(connectionString);
            connection.Open();
            string sql = $"INSERT INTO FL VALUES ('{id.Max() + 1}','{newFl.E_mail}', '{newFl.Password}', '{newFl.FIO}', '{newFl.DateOfBirth}', '{newFl.Pasport}', '{newFl.Phone}')";
            OleDbCommand command = new OleDbCommand(sql, connection);
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Клиент успешно добавлен", "Успех", MessageBoxButton.OK);
            this.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e) =>
            this.Close();

        private void email_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[a-zA-Z0-9@._]"))
                e.Handled = true;
        }

        private void fio_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[а-яА-Я]"))
                e.Handled = true;
        }

        private void birth_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[0-9.]"))
                e.Handled = true;
        }

        private void pasport_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[0-9]"))
                e.Handled = true;
        }

        private void phone_txb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"[0-9+]"))
                e.Handled = true;
        }
    }
}

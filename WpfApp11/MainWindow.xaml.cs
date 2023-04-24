using Microsoft.IdentityModel.Protocols;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using Path = System.IO.Path;

namespace WpfApp11
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection = new SqlConnection("server = .\\SQLEXPRESS; database=image; integrated security = true");


        private string connectionString = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        
        byte[] imageData; // поле класса или переменная для хранения байтовой информации о фотографии
        private int selectedProductId = -1;
        

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                imgProductImage.Source = new BitmapImage(new Uri(dlg.FileName));
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selectedRow = dgProducts.SelectedItem as DataRowView;
            if (selectedRow != null)
            {
                selectedProductId = Convert.ToInt32(selectedRow["ProductKey"]);
                txtProductName.Text = selectedRow["ProductName"].ToString();
                byte[] imageData = (byte[])selectedRow["ProductImage"];
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = new MemoryStream(imageData);
                bi.EndInit();
                imgProductImage.Source = bi;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedProductId == -1)
            {
                // Insert new record
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO imageProduct (ProductName, ProductImage) VALUES (@ProductName, @ProductImage)", conn);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@ProductImage", GetImageBytes());
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            else
            {
                // Update existing record
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE imageProduct SET ProductName = @ProductName, ProductImage = @ProductImage WHERE ProductKey = @ProductKey", conn);
                    cmd.Parameters.AddWithValue("@ProductName", txtProductName.Text);
                    cmd.Parameters.AddWithValue("@ProductImage", GetImageBytes());
                    cmd.Parameters.AddWithValue("@ProductKey", selectedProductId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
                selectedProductId = -1;
            }
            LoadProducts();
        }

        private void LoadProducts()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM imageProduct", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dgProducts.DataContext = dataTable.DefaultView;
            }
        }

        private byte[] GetImageBytes()
        {
            if (imgProductImage.Source != null)
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapImage)imgProductImage.Source));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    return ms.ToArray();
                }
            }
            return null;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную запись в DataGrid
            DataRowView dataRow = (DataRowView)dgProducts.SelectedItem;

            // Проверяем, что запись выбрана
            if (dataRow != null)
            {
                // Получаем ключ выбранной записи
                int productKey = Convert.ToInt32(dataRow["ProductKey"]);

                // Создаем соединение с базой данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Создаем SQL-запрос на удаление записи по ключу
                    string sql = "DELETE FROM imageProduct WHERE ProductKey = @ProductKey";

                    // Создаем команду с параметром
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@ProductKey", productKey);

                        // Открываем соединение и выполняем команду
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                // Обновляем DataGrid
                LoadProducts();
            }
        }
    }
}

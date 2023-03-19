using Client.Enums;
using Client.Models;
using Client.Views;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Client;

public partial class MainWindow : Window
{
    public ObservableCollection<Car> Cars { get; set; }
    public ObservableCollection<string> Methods { get; set; }
    Command request;

    //Delete-Remove
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        Cars = new();
        request = new();
        Methods = new();
        AddMethods();
        Car car = new Car()
        {
            Id = 1,
            Make = "Bmw",
            Model = "M5",
            Year = 2010,
            VIN = "123",
            Color = "Red"
        };
        Cars.Add(car);
        using var client = new TcpClient("127.0.0.1", 45678);

        var serverStream = client.GetStream();
        var bw = new BinaryWriter(serverStream);
        var br = new BinaryReader(serverStream);

        
    }
    public void AddMethods()
    {
        Methods.Add(HttpMethods.Get.ToString());
        Methods.Add(HttpMethods.Delete.ToString());
        Methods.Add(HttpMethods.Put.ToString());
        Methods.Add(HttpMethods.Post.ToString());
    }

    private void MethodBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
    {
        if (MethodBox.SelectedItem.ToString() == "Post")
        {
            SearchBtn.Visibility = Visibility.Hidden;
            IdSearchertxtbox.Visibility = Visibility.Hidden;
            GridAdd.Visibility = Visibility.Visible;
            GridPut.Visibility = Visibility.Hidden;
            GridDelete.Visibility = Visibility.Hidden;
            GridGetT.Visibility = Visibility.Hidden;

        }
        else if (MethodBox.SelectedItem.ToString() == "Put")
        {

            SearchBtn.Visibility = Visibility.Hidden;
            IdSearchertxtbox.Visibility = Visibility.Hidden;
            GridPut.Visibility = Visibility.Visible;
            GridAdd.Visibility = Visibility.Hidden;
            GridDelete.Visibility = Visibility.Hidden;
            GridGetT.Visibility = Visibility.Visible;
        }
        else if (MethodBox.SelectedItem.ToString() == "Delete")
        {
            SearchBtn.Visibility = Visibility.Visible;
            GridDelete.Visibility = Visibility.Visible;
            GridPut.Visibility = Visibility.Hidden;
            GridAdd.Visibility = Visibility.Hidden;
            GridGetT.Visibility = Visibility.Hidden;
            IdSearchertxtbox.Visibility = Visibility.Visible;

        }
        else if (MethodBox.SelectedItem.ToString() == "Get")
        {
            SearchBtn.Visibility = Visibility.Visible;
            IdSearchertxtbox.Visibility = Visibility.Visible;
            GridGetT.Visibility = Visibility.Visible;
            GridPut.Visibility = Visibility.Hidden;
            GridDelete.Visibility = Visibility.Hidden;
            GridAdd.Visibility = Visibility.Hidden;
        }
    }

    private void SearchBtn_Click(object sender, RoutedEventArgs e)
    {
        var id = string.IsNullOrEmpty(IdSearchertxtbox.Text) ? 0 : int.Parse(IdSearchertxtbox.Text);
        if (MethodBox.SelectedItem == "Get")
        {
            request = new Command()
            {
                Method = HttpMethods.Get,
                Car = new Car { Id = id }
            };
        }
        else if (MethodBox.SelectedItem == "Remove")
        {
            if (id == 0)
            {
                MessageBox.Show("Id must be integer value and id > 0", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            };
            request = new Command()
            {
                Method = HttpMethods.Delete,
                Car = new Car { Id = id }
            };
        }
    }

    private void ListCars_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (GridPut.Visibility != Visibility.Visible) return;
        if (sender is not ListViewItem item) return;
        if (ListCars.SelectedItem == null) return;
        var car = (Car)ListCars.SelectedItem;
        ModifyView view = new(car);
        view.ShowDialog();

    }

    private void SaveCancel_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            if (btn.Name == "CancelBtn")
            {
                Maketxtbox.Text = string.Empty;
                Modeltxtbox.Text = string.Empty;
                VINtxtbox.Text = string.Empty;
                Yearxtbox.Text = string.Empty;
                Colortxtbox.Text = string.Empty;
            }
            else if (btn.Name == "SaveBtn")
            {
                request = new Command()
                {
                    Method = HttpMethods.Post,
                    Car = new Car() { Make = Maketxtbox.Text, Model = Modeltxtbox.Text, VIN = VINtxtbox.Text, Color = Colortxtbox.Text, Year = ushort.Parse(Yearxtbox.Text) }
                };
            }
        }
    }
}
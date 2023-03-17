using Client.Enums;
using Client.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Client;

public partial class MainWindow : Window
{
    public ObservableCollection<Car> Cars { get; set; }
    public ObservableCollection<string> Methods { get; set; }

    //Post-Add
    //Put-
    //Delete-Remove
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        Cars = new();
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
            GridGetT.Visibility = Visibility.Hidden;
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
        //Cars.Clear();
        //foreach (var item in Cars)
        //{
        //    //Cars -> DeserializedList change
        //    if (IdSearchertxtbox.Text == item.Id.ToString())
        //        Cars.Add(item);
        //}

        //if(Cars.Count == 0) {
        //    //foreach DeserializedList
        //    foreach (var item in Cars)
        //    {
        //        Cars.Add(item);
        //    }
        //}
    }

    private void ListCars_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if(sender is ListViewItem item)
            MessageBox.Show("Test");
    }
}

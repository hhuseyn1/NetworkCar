using Client.Enums;
using Client.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client;

public partial class MainWindow : Window
{
    public ObservableCollection<Car> Cars { get; set; }
    public ObservableCollection<string> Methods { get; set; }

    //Post-Add
    //Get-null ? all : id
    //Put-
    //Delete-Remove
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        Cars = new();
        Methods = new();
        AddMethods();
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
            GridAdd.Visibility = Visibility.Visible;
            GridPut.Visibility = Visibility.Hidden;
            GridDelete.Visibility = Visibility.Hidden;
            GridGetT.Visibility = Visibility.Hidden;

        }
        else if (MethodBox.SelectedItem.ToString() == "Put")
        {
            GridPut.Visibility = Visibility.Visible;
            GridAdd.Visibility = Visibility.Hidden;
            GridDelete.Visibility = Visibility.Hidden;
            GridGetT.Visibility = Visibility.Hidden;
        }
        else if (MethodBox.SelectedItem.ToString() == "Delete")
        {
            GridDelete.Visibility = Visibility.Visible;
            GridPut.Visibility = Visibility.Hidden;
            GridAdd.Visibility = Visibility.Hidden;
            GridGetT.Visibility = Visibility.Hidden;
        }
        else if (MethodBox.SelectedItem.ToString() == "Get")
        {
            GridGetT.Visibility = Visibility.Visible;
            GridPut.Visibility = Visibility.Hidden;
            GridDelete.Visibility = Visibility.Hidden;
            GridAdd.Visibility = Visibility.Hidden;
        }
    }
}

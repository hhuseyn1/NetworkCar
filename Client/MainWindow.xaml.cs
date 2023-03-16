using Client.Enums;
using Client.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace Client;

public partial class MainWindow : Window
{
    ObservableCollection<Car> Cars { get; set; }
    ObservableCollection<string> Methods { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        Cars= new ();
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

}

using Client.Models;
using System.Windows;
using System.Windows.Controls;

namespace Client.Views;

public partial class ModifyView : Window
{
    public ModifyView(Car car)
    {
        InitializeComponent();
    }
    private void SaveCancel_Click(object sender, RoutedEventArgs e)
    {
        if(sender is Button btn)
        {
            if (btn.Name == "SaveBtn")
            {

            }
            else if (btn.Name == "SaveBtn")
            {

            }
        }
    }
}

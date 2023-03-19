using Client.Models;
using System.Windows;
using System.Windows.Controls;

namespace Client.Views;

public partial class ModifyView : Window
{
    public ModifyView(Car car)
    {
        InitializeComponent();
        Idtxtbox.Text = car.Id.ToString();
        Maketxtbox.Text = car.Make;
        Modeltxtbox.Text = car.Model;
        VINtxtbox.Text = car.VIN;
        Colortxtbox.Text = car.Color;
        Yearxtbox.Text = car.Year.ToString();
    }
    private void SaveCancel_Click(object sender, RoutedEventArgs e)
    {
        if(sender is Button btn)
        {
            if (btn.Name == "SaveBtn")
            {

            }
            else if (btn.Name == "CancelBtn")
            {
                this.Close();
            }
        }
    }
}

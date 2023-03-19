using Client.Enums;
using Client.Models;
using System.Windows;
using System.Windows.Controls;

namespace Client.Views;

public partial class ModifyView : Window
{
    Command request;
    public ModifyView(Car car)
    {
        InitializeComponent();
        request = new Command();
        Idtxtbox.Text = car.Id.ToString();
        Maketxtbox.Text = car.Make;
        Modeltxtbox.Text = car.Model;
        VINtxtbox.Text = car.VIN;
        Colortxtbox.Text = car.Color;
        Yearxtbox.Text = car.Year.ToString();
    }
    private void SaveCancel_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button btn)
        {
            if (btn.Name == "SaveBtn")
            {
                request = new Command()
                {
                    Method = HttpMethods.Post,
                    Car = new Car() { Id = int.Parse(Idtxtbox.Text), VIN = VINtxtbox.Text, Color = Colortxtbox.Text, Make = Maketxtbox.Text, Model = Modeltxtbox.Text, Year = ushort.Parse(Yearxtbox.Text) }
                };
            }
            else if (btn.Name == "CancelBtn")
            {
                this.Close();
            }
        }
    }
}

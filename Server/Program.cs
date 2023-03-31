using Newtonsoft.Json;
using Server.Models;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using JsonSerializer = System.Text.Json.JsonSerializer;

var ip = IPAddress.Parse("127.0.0.1");
var port = 45678;

var listener = new TcpListener(ip, port);

listener.Start(1);
Console.WriteLine("Listening...");
var dirInfo = new DirectoryInfo(@"..\..\..\Data\CarData.json");
var jsonData = File.ReadAllText(dirInfo.FullName);
var cars = JsonSerializer.Deserialize<List<Car>>(jsonData);
List<Car> Cars = new();
List<Car> ShowCars = new(); //For return Car List
foreach (Car car in cars)
    Cars.Add(car);

while (true)
{
    var client = await listener.AcceptTcpClientAsync();
    var clientStream = client.GetStream();
    var br = new BinaryReader(clientStream);
    var bw = new BinaryWriter(clientStream);
    Console.WriteLine($"{client.Client.RemoteEndPoint} are connected...");
    await Task.Run(async () =>
    {
        while (true)
        {
            bw.Write(Cars.Count.ToString());
            foreach (var item in Cars)
            {
                var request = JsonSerializer.Serialize<Car>(item);
                bw.Write(request);
            }
            var text = br.ReadString();
            var text2 = br.ReadString();
            var car = ConvertToCar(text2);
            int id = car.Id;
            try
            {

            switch (text)
            {
                case "Get":
                    GetByIdAsync(id);
                    break;
                case "Remove":
                    Delete(id);
                    break;
                case "Put":
                    Update(car);
                    break;
                case "Post":
                    Add(car);
                    break;
                default:
                    break;
            }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }

            foreach (var item in ShowCars)
            {
                var request = JsonSerializer.Serialize<Car>(item);
                bw.Write(request);
            }
        }
    });

}

Car ConvertToCar(string text)
{
    Car car = new();
    car.Id = int.Parse(text.Split(' ')[0]);
    car.Make = text.Split(' ')[1];
    car.Model = text.Split(' ')[2];
    car.Year = ushort.Parse(text.Split(' ')[3]);
    car.VIN = text.Split(' ')[4];
    car.Color = text.Split(' ')[5];
    return car;
}

async Task GetByIdAsync(int Id)
{
    ShowCars.Clear();
    if (Id == 0)
        foreach (var item in Cars)
            ShowCars.Add(item);
    else
        foreach (var item in Cars)
            if (item.Id == Id)
                ShowCars.Add(item);
}


async Task<bool> Delete(int Id)
{
    bool result;
    string jsonData = await File.ReadAllTextAsync("CarData.json");

    List<Car?> Cars = JsonConvert.DeserializeObject<List<Car?>>(jsonData);

    Car? objectToRemove = Cars.FirstOrDefault(c => c.Id == Id);
    result = Cars.Remove(objectToRemove);


    string updatedJson = JsonConvert.SerializeObject(Cars, Formatting.Indented);

    File.WriteAllText("CarData.json", updatedJson);
    return result;
}

async Task<bool> Add(Car car)
{
    try
    {
        string jsonData = await File.ReadAllTextAsync("CarData.json");
        List<Car?> Cars = JsonConvert.DeserializeObject<List<Car?>>(jsonData);
        Cars.Add(car);
        string updatedJson = JsonConvert.SerializeObject(Cars, Formatting.Indented);
        File.WriteAllText("CarData.json", updatedJson);
    }
    catch (Exception ex)
    {
        return false;
    }
    return true;
}


async Task<bool> Update(Car car)
{
    try
    {
        string jsonData = await File.ReadAllTextAsync("CarData.json");
        List<Car?> Cars = JsonConvert.DeserializeObject<List<Car?>>(jsonData);
        Car? objectToUpdate = Cars.FirstOrDefault(c => c.Id == car.Id);
        if (objectToUpdate != null)
        {
            objectToUpdate.Make = car.Make;
            objectToUpdate.Model = car.Model;
            objectToUpdate.VIN = car.VIN;
            objectToUpdate.Year = car.Year;
            objectToUpdate.Color = car.Color;
        }

        string updatedJson = JsonConvert.SerializeObject(Cars, Formatting.Indented);

        File.WriteAllText("CarData.json", updatedJson);
    }
    catch (Exception ex)
    {
        return false;
    }
    return true;
}
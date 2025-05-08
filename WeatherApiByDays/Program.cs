using System.Security.Cryptography.X509Certificates;
using BussinessLogic;
using DatabseCall;
namespace WeatherApiByDays
{
    public class Program
    {
       
        static void Main(string[] args)
        {
            ApiClass obj = new ApiClass();
            Console.WriteLine("1) Insert the Record \n2)Display all Inserted Record \n3)Displaying the Records by city name and Days \nEnter any key to exit: ");
            Console.WriteLine("Enter the options: ");
            int ch = int.Parse(Console.ReadLine());
            switch(ch)
            {
                case 1:
                    {
                        Console.Write("Enter the Latitiude: ");
                        decimal _latiude = decimal.Parse(Console.ReadLine());
                        Console.Write("Enter the Longitude: ");
                        decimal _longitude = decimal.Parse(Console.ReadLine());
                        obj.GetCordinates(_latiude, _longitude);
                        try
                        {
                            obj.Runapi().GetAwaiter().GetResult();
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                    break;
                case 2:
                    {
                       
                        DatabseCall.ADO.ALLDisplay();
                    }
                    break;
                case 3:
                    {
                        Console.WriteLine("Enter the city Name: ");
                        string cityname = Console.ReadLine();

                        Console.WriteLine("Enter the days: ");
                        int days = int.Parse(Console.ReadLine());
                        
                        DatabseCall.ADO.EachDayDisplay(cityname, days);
                    }
                    break;
                 default:
                    Console.WriteLine("Exited");
                    break;
            }
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DatabseCall;
namespace BussinessLogic
{
    public class ApiClass
    {

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public void GetCordinates(decimal latiitude, decimal longitude)
        {
            Latitude = latiitude;
            Longitude = longitude;
        }

        public async Task Runapi()
        {
            try
            {
                string api = $"https://api.openweathermap.org/data/2.5/forecast?lat={Latitude}&lon={Longitude}&appid=13a48e4faa2082e26303d0266852b57a";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, api);


                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var otp = (await response.Content.ReadAsStringAsync());

                var data = JsonSerializer.Deserialize<Rootobject>(otp);

                if (data != null)
                {

                    DateTime csunrise = DateTimeOffset.FromUnixTimeSeconds(data.city.sunrise).DateTime;
                    string cRecordDate = csunrise.ToString("yyyy-MM-dd");
                    string citysunrise = csunrise.ToString("hh:mm:ss");
                    DateTime csunset = DateTimeOffset.FromUnixTimeSeconds(data.city.sunset).DateTime;
                    string citysunset = csunset.TimeOfDay.ToString();


                    ADO.CInsert(data.city.id, data.city.name, data.city.country, Latitude, Longitude, citysunrise, citysunset, cRecordDate);

                    foreach (var i in data.list)//List of all record of 5 days
                    {
                        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(i.dt).DateTime;
                        string RecordDate = dateTime.ToString("yyyy-MM-dd");
                        string RecordTime = dateTime.ToString("hh:mm:ss");
                        ADO.WInsert(i.weather[0].id,data.city.id, RecordDate,i.main.temp, i.weather[0].main, i.weather[0].description,RecordTime);
                        
                    }
                   
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }



        }
    }
}

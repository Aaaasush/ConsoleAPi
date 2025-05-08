using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime.InteropServices;
namespace DatabseCall
{
    public class ADO
    {
        
      
        public static void CInsert(int cid,string cname,string country,decimal latitude,decimal longitude,string csunrise ,string csunset,string cdate)
        {
            try
            {
               // data.city.id, data.city.name, data.city.country,Lati,Longi,citysunrise,citysunset,cityDate
                SqlConnection conn = new SqlConnection("Data Source=SUSHMIT\\SQLEXPRESS;Database=WeatherDB;Integrated Security=SSPI");
                SqlCommand cmd = new SqlCommand($"Insert into dbo.city(cityid,cityname,latitude,longitude,country,sunrise,sunset,RecordDate) values({cid},'{cname}','{latitude}','{longitude}','{country}','{csunrise}','{csunset}','{cdate}')", conn);
                conn.Open();
                var st = conn.State;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Saved");
                }
                else
                {
                    Console.WriteLine("Not Saved");
                }
            }
            catch (Exception ex)
            {

                throw;
            }
           

        }
        public static void WInsert(int weatherid,int cid, string RecordDate, float temperature, string weathermain, string weatherdes, string recordtime)
        {
            try
            {
                // data.city.id, data.city.name, data.city.country,Lati,Longi,citysunrise,citysunset,cityDate
                SqlConnection conn = new SqlConnection("Data Source=SUSHMIT\\SQLEXPRESS;Database=WeatherDB;Integrated Security=SSPI");
                SqlCommand cmd = new SqlCommand($"Insert into dbo.weather(weatherid,cid,RecordDate,RecordTime,temperature,weatherMain,weatherDescription) values({weatherid},{cid},'{RecordDate}','{recordtime}',{temperature},'{weathermain}','{weatherdes}')", conn);
                conn.Open();
                var st = conn.State;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine("Saved");
                }
                else
                {
                    Console.WriteLine("Not Saved");
                }
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public static void ALLDisplay()
        {
            try
            {
                SqlDataReader dr;
                SqlConnection conn = new SqlConnection("Data Source=SUSHMIT\\SQLEXPRESS;Database=WeatherDB;Integrated Security=SSPI");
                SqlCommand cmd = new SqlCommand("select w.weatherid,c.cityname,w.RecordDate,w.RecordTime,w.temperature,w.weatherMain,w.weatherDescription,c.latitude,c.longitude,c.country,c.sunrise,c.sunset from weather w inner join city c on w.cid=c.cityid", conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                int nrows = dr.FieldCount;
                while (dr.Read())
                {
                    Console.WriteLine("Weather ID\tCity Name\tDate\tTime\tTemperature\tWeather\tWeather Description\tLatitude\tLongitude\tSun Risetime\tSun Time\tCountry");
                    for (int i = 0; i < nrows; i++)
                    {
                        Console.WriteLine($"{dr["weatherid"]}\t{dr["cityname"]}\t{dr["RecordDate"]}\t{dr["RecordTime"]}\t{dr["temperature"]}\t{dr["weatherMain"]}\t{dr["weatherDescription"]}\t{dr["latitude"]}\t{dr["longitude"]}\t{dr["sunrise"]}\t{dr["sunset"]}\t{dr["country"]}");

                    }
                }
                
                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
            

        }
        public static void DayDisplay(string name,int day)
        {
            try
            {
                SqlDataReader dr;
                SqlConnection conn = new SqlConnection("Data Source=SUSHMIT\\SQLEXPRESS;Database=WeatherDB;Integrated Security=SSPI");
                SqlCommand cmd = new SqlCommand($"select w.weatherid,c.cityname,w.RecordDate,w.RecordTime,w.temperature,w.weatherMain,w.weatherDescription,c.latitude,c.longitude,c.country,c.sunrise,c.sunset from weather w inner join city c on w.cid=c.cityid where w.RecordDate between c.RecordDate and DATEADD(day,{day-1} ,c.RecordDate ) and c.cityname like'%{name}%'\r\n", conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                int nrows = dr.FieldCount;
                while (dr.Read())
                {
                    Console.WriteLine("Weather ID\tCity Name\tDate\tTime\tTemperature\tWeather\tWeather Description\tLatitude\tLongitude\tSun Risetime\tSun Time\tCountry");
                    for (int i = 0; i < nrows; i++)
                    {
                        Console.WriteLine($"{dr["weatherid"]}\t{dr["cityname"]}\t{dr["RecordDate"]}\t{dr["RecordTime"]}\t{dr["temperature"]}\t{dr["weatherMain"]}\t{dr["weatherDescription"]}\t{dr["latitude"]}\t{dr["longitude"]}\t{dr["sunrise"]}\t{dr["sunset"]}\t{dr["country"]}");

                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }


        }
        public static void EachDayDisplay(string name, int day)
        {
            try
            {
                SqlDataReader dr;
                SqlConnection conn = new SqlConnection("Data Source=SUSHMIT\\SQLEXPRESS;Database=WeatherDB;Integrated Security=SSPI");
                SqlCommand cmd = new SqlCommand($"select * from(select Rank()over (partition by w.RecordDate order by w.Temperature) as p,w.weatherid,w.cid,w.RecordDate,w.RecordTime,w.temperature,w.weatherMain,w.weatherDescription,c.cityname,c.latitude,c.longitude,c.country,c.sunrise,c.sunset from weather w inner join city c on w.cid=c.cityid where w.RecordDate between c.RecordDate and DATEADD(day, {day-1},c.RecordDate ) and c.cityname like'%{name}%' )s  where s.p =1 ", conn);
                conn.Open();
                dr = cmd.ExecuteReader();
                //int nrows = day;
                while (dr.Read())
                {
                    Console.WriteLine("Weather ID\tCity Name\tDate\tTime\tTemperature\tWeather\tWeather Description\tLatitude\tLongitude\tSun Risetime\tSun Time\tCountry");
                        Console.WriteLine($"{dr["weatherid"]}\t{dr["cityname"]}\t{dr["RecordDate"]}\t{dr["RecordTime"]}\t{dr["temperature"]}\t{dr["weatherMain"]}\t{dr["weatherDescription"]}\t{dr["latitude"]}\t{dr["longitude"]}\t{dr["sunrise"]}\t{dr["sunset"]}\t{dr["country"]}");

                  
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}

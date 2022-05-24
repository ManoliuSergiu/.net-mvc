using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using WebApplication2.Models;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public String WeatherDetail(String city)
        {
            string key = "57f296c5b223ce3e5880dd155bcb38da";
            string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&cnt=1&APPID={1}", city, key);
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
                WeatherModel.RawData weatherInfo = JsonConvert.DeserializeObject<WeatherModel.RawData>(json);
                WeatherModel.ResultViewModel rslt = new WeatherModel.ResultViewModel();

                rslt.Country = weatherInfo.sys.country;
                rslt.City = weatherInfo.name;
                rslt.Lat = Convert.ToString(weatherInfo.coord.lat);
                rslt.Lon = Convert.ToString(weatherInfo.coord.lon);
                rslt.Description = weatherInfo.weather[0].description;
                rslt.Humidity = Convert.ToString(weatherInfo.main.humidity);
                rslt.Temp = Convert.ToString(weatherInfo.main.temp);
                rslt.TempFeelsLike = Convert.ToString(weatherInfo.main.feels_like);
                rslt.TempMax = Convert.ToString(weatherInfo.main.temp_max);
                rslt.TempMin = Convert.ToString(weatherInfo.main.temp_min);
                rslt.WeatherIcon = weatherInfo.weather[0].icon;

                //Converting OBJECT to JSON String   
                var jsonstring = JsonConvert.SerializeObject(rslt);

                //Return JSON string.  
                return jsonstring;
            }
        }
        [HttpPost]
        public String GetCountryImage( int x, int y, string countryCode)
        {
            return string.Format("https://flagcdn.com/{0}x{1}/{2}.png", x, y, countryCode.ToLower());
        }

    }
}

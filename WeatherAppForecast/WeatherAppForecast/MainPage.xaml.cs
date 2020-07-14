using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppForecast.Helper;
using WeatherAppForecast.Models;
using Xamarin.Forms;

namespace WeatherAppForecast
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            GetWeatherInfo();
           // GetForecast();
        }

        private async void GetWeatherInfo()
        {
            // OpenWheater Api
            //var url = $"https://api.openweathermap.org/data/2.5/weather?q=Ciudad%20Aut%C3%B3noma%20de%20Buenos%20Aires&units=metric&APPID=********";

            // Metaweather Api
            var url = $"https://www.metaweather.com/api/location/468739";
            var result = await ApiCaller.Get(url);

            if (result.Successfull)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<Root>(result.Response);

                    // OpenWeatherMap
                    // iconImg.Source=$"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    //  iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    //  cityTxt.Text = weatherInfo.name.ToUpper();
                    //  temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    //  humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    //  pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    //  windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    //  cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";


                    descriptionTxt.Text = weatherInfo.consolidated_weather[0].weather_state_name.ToUpper();
                    iconImg.Source = $"https://www.metaweather.com/static/img/weather/png/{weatherInfo.consolidated_weather[0].weather_state_abbr}.png";
                    cityTxt.Text = weatherInfo.title.ToUpper();
                    temperatureTxt.Text = weatherInfo.consolidated_weather[0].the_temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.consolidated_weather[0].humidity}%";
                    pressureTxt.Text = $"{weatherInfo.consolidated_weather[0].air_pressure.ToString("0")} mbar";
                    windTxt.Text = $"{weatherInfo.consolidated_weather[0].wind_speed.ToString("0")} mph";
                    cloudinessTxt.Text = $"{weatherInfo.consolidated_weather[0].visibility.ToString("0")} miles";
                    DateTime dt = new DateTime();
                    dt = DateTime.Now;
                    dateTxt.Text = dt.ToString("dddd,MMM,dd").ToUpper();

                    //Forecast for metaweater API only



                    dayOneTxt.Text =    DateTime.Parse(weatherInfo.consolidated_weather[1].applicable_date).ToString("dddd");
                    dateOneTxt.Text =   DateTime.Parse(weatherInfo.consolidated_weather[1].applicable_date).ToString("dd MMM");
                    iconOneImg.Source = $"https://www.metaweather.com/static/img/weather/png/{weatherInfo.consolidated_weather[1].weather_state_abbr}.png";
                    tempOneTxt.Text =   weatherInfo.consolidated_weather[1].the_temp.ToString("0");

                    dayTwoTxt.Text =    DateTime.Parse(weatherInfo.consolidated_weather[2].applicable_date).ToString("dddd");
                    dateTwoTxt.Text =   DateTime.Parse(weatherInfo.consolidated_weather[2].applicable_date).ToString("dd MMM");
                    iconTwoImg.Source = $"https://www.metaweather.com/static/img/weather/png/{weatherInfo.consolidated_weather[2].weather_state_abbr}.png";
                    tempTwoTxt.Text =   weatherInfo.consolidated_weather[2].the_temp.ToString("0");

                    dayThreeTxt.Text =   DateTime.Parse(weatherInfo.consolidated_weather[3].applicable_date).ToString("dddd");
                    dateThreeTxt.Text =  DateTime.Parse(weatherInfo.consolidated_weather[3].applicable_date).ToString("dd MMM");
                    iconThreeImg.Source = $"https://www.metaweather.com/static/img/weather/png/{weatherInfo.consolidated_weather[3].weather_state_abbr}.png";
                    tempThreeTxt.Text = weatherInfo.consolidated_weather[3].the_temp.ToString("0");

                    dayFourTxt.Text =   DateTime.Parse(weatherInfo.consolidated_weather[4].applicable_date).ToString("dddd"); 
                    dateFourTxt.Text =  DateTime.Parse(weatherInfo.consolidated_weather[4].applicable_date).ToString("dd MMM");
                    iconFourImg.Source = $"https://www.metaweather.com/static/img/weather/png/{weatherInfo.consolidated_weather[4].weather_state_abbr}.png";
                    tempFourTxt.Text = weatherInfo.consolidated_weather[4].the_temp.ToString("0");


                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message,"OK");
                }
                

            }
            else
            {
                await DisplayAlert("Weather Info", "The app cant't contect with the server", "OK");
            }

        }


        // Forecast display for Openweather only
        private async void GetForecast()
        {

            var url = $"http://api.openweathermap.org/data/2.5/forecast?q=Ciudad%20Aut%C3%B3noma%20de%20Buenos%20Aires&appid=*****&units=metric";
            var result = await ApiCaller.Get(url);

            if (result.Successfull)
            {
                try
                {
                    var forcastInfo = JsonConvert.DeserializeObject<ForecastInfo>(result.Response);

                    List<List> allList = new List<List>();

                    foreach (var list in forcastInfo.list)
                    {
                        //var date = DateTime.ParseExact(list.dt_txt, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                        var date = DateTime.Parse(list.dt_txt);

                        if (date > DateTime.Now && date.Hour == 0 && date.Minute == 0 && date.Second == 0)
                            allList.Add(list);
                    }

                    dayOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dddd");
                    dateOneTxt.Text = DateTime.Parse(allList[0].dt_txt).ToString("dd MMM");
                    iconOneImg.Source = $"http://openweathermap.org/img/wn/{allList[0].weather[0].icon}@2x.png";
                    tempOneTxt.Text = allList[0].main.temp.ToString("0");

                    dayTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dddd");
                    dateTwoTxt.Text = DateTime.Parse(allList[1].dt_txt).ToString("dd MMM");
                    iconTwoImg.Source = $"http://openweathermap.org/img/wn/{allList[1].weather[0].icon}@2x.png";
                    tempTwoTxt.Text = allList[1].main.temp.ToString("0");

                    dayThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dddd");
                    dateThreeTxt.Text = DateTime.Parse(allList[2].dt_txt).ToString("dd MMM");
                    iconThreeImg.Source = $"http://openweathermap.org/img/wn/{allList[2].weather[0].icon}@2x.png";
                    tempThreeTxt.Text = allList[2].main.temp.ToString("0");

                    dayFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dddd");
                    dateFourTxt.Text = DateTime.Parse(allList[3].dt_txt).ToString("dd MMM");
                    iconFourImg.Source = $"http://openweathermap.org/img/wn/{allList[3].weather[0].icon}@2x.png";
                    tempFourTxt.Text = allList[3].main.temp.ToString("0");

                }
                catch (Exception ex)
                {
                    await DisplayAlert("Weather Info", ex.Message, "OK");
                }
            }
            else
            {
                await DisplayAlert("Weather Info", "No forecast information found", "OK");
            }
        }


    }
}

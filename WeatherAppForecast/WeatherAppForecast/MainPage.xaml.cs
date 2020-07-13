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
            GetForecast();
        }

        private async void GetWeatherInfo()
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q=Ciudad%20Aut%C3%B3noma%20de%20Buenos%20Aires&units=metric&APPID=22f57fc688942f931c869aad5115812f";

            var result = await ApiCaller.Get(url);

            if (result.Successfull)
            {
                try
                {
                    var weatherInfo = JsonConvert.DeserializeObject<WeatherInfo>(result.Response);
                    iconImg.Source=$"http://openweathermap.org/img/wn/{weatherInfo.weather[0].icon}@2x.png";
                    descriptionTxt.Text = weatherInfo.weather[0].description.ToUpper();
                  //  iconImg.Source = $"w{weatherInfo.weather[0].icon}";
                    cityTxt.Text = weatherInfo.name.ToUpper();
                    temperatureTxt.Text = weatherInfo.main.temp.ToString("0");
                    humidityTxt.Text = $"{weatherInfo.main.humidity}%";
                    pressureTxt.Text = $"{weatherInfo.main.pressure} hpa";
                    windTxt.Text = $"{weatherInfo.wind.speed} m/s";
                    cloudinessTxt.Text = $"{weatherInfo.clouds.all}%";



                    DateTime dt = new DateTime();
                    dt = DateTime.Now;
                    dateTxt.Text = dt.ToString("dddd,MMM,dd").ToUpper();


                }
                catch (Exception ex)
                {
                    
                }
                

            }
            else
            {
                await DisplayAlert("App info", "The app cant't contect with the server", "OK");
            }

        }



        private async void GetForecast()
        {
            var url = $"http://api.openweathermap.org/data/2.5/forecast?q=Ciudad%20Aut%C3%B3noma%20de%20Buenos%20Aires&appid=22f57fc688942f931c869aad5115812f&units=metric";
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

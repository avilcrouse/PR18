using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace PR18
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }


        public string Translate(string word)
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=en&tl=ru&dt=t&q={HttpUtility.UrlEncode(word)}";
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }

        string Str(string str)
        {
            return str;
        }

        private async void ButtonResult_Clicked(object sender, EventArgs e)
        {
            staclLayout.Children.Clear();
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    HttpClient client = new HttpClient();
                    HttpResponseMessage response = await client.GetAsync($"https://rickandmortyapi.com/api/character/{entryNumber.Text.Replace(".", "")}");
                    HttpContent responseContent = response.Content;
                    string json = await responseContent.ReadAsStringAsync();
                    RicAndMorty ricAndMorty = JsonConvert.DeserializeObject<RicAndMorty>(json);
                    staclLayout.Children.Add(new Image() { Source = ImageSource.FromUri(new Uri(ricAndMorty.image)), HeightRequest = 400, WidthRequest = 400 });
                    staclLayout.Children.Add(new Label() { Text = "ID: " + Translate(ricAndMorty.id.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Имя: " + Translate(ricAndMorty.name.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Статус: " + Translate(ricAndMorty.status.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Вид: " + Translate(ricAndMorty.species.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Тип: " + Translate(ricAndMorty.type.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Пол: " + Translate(ricAndMorty.gender.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Происхождение: " + Translate(ricAndMorty.origin.name.ToString()), FontSize = 20 });
                    staclLayout.Children.Add(new Label() { Text = "Местоположение: " + Translate(ricAndMorty.location.name.ToString()), FontSize = 20 });

                }
                catch (Exception ex)
                {
                    var error = new Label() { Text = ex.Message, TextColor = Color.Red };
                    staclLayout.Children.Add(error);
                }

            }
        }
    }
}

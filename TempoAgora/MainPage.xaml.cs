using System.Threading.Tasks;
using TempoAgora.Models;
using TempoAgora.Services;

namespace TempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void button_clicked(object sender, EventArgs e)
        {
            await BuscarPrevisao();
        }

        private async Task BuscarPrevisao()
        {
            try
            {
                string cidade = txt_cidade.Text?.Trim();

                if (string.IsNullOrEmpty(cidade))
                {
                    lbl_res.Text = "❗ Por favor, digite o nome da cidade.";
                    return;
                }

                Tempo? t = await DataService.GetPrevisao(cidade);

                if (t != null)
                {
                    lbl_res.Text = $"📍 Cidade: {cidade}\n\n" +

                                   $"🌤️ Clima: {t.description}\n\n" +

                                   $"🗺️ Localização:\n" +
                                   $"- Latitude: {t.lat}\n" +
                                   $"- Longitude: {t.lon}\n\n" +

                                   $"🌞 Sol:\n" +
                                   $"- Nascer: {t.sunrise}\n" +
                                   $"- Pôr:    {t.sunset}\n\n" +

                                   $"🌡️ Temperaturas:\n" +
                                   $"- Máxima: {t.temp_max}°C\n" +
                                   $"- Mínima: {t.temp_min}°C\n\n" +

                                   $"💨 Vento: {t.speed} m/s\n" +
                                   $"👁️ Visibilidade: faz {t.visibility} m";
                }
                else
                {
                    lbl_res.Text = "⚠️ Cidade não encontrada. Verifique o nome e tente novamente.";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de conexão", "Você está sem internet.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
            }
        }
    }
}
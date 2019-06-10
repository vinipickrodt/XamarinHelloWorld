using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        static Random random = new Random(42);
        static int bomba = random.Next(1, 4);
        static int score = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        public async void ButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;

            btn1.BackgroundColor = btn2.BackgroundColor = btn3.BackgroundColor = new Button().BackgroundColor;

            if (button.Text == bomba.ToString())
            {
                button.BackgroundColor = Color.Red;
                var t = Android.Widget.Toast.MakeText(Android.App.Application.Context, $"Bomba explodiu. \nSua pontuação foi {score}.", Android.Widget.ToastLength.Short);
                score = 0;
                t.Show();
                //await DisplayAlert($"GAME OVER", $"Bomba explodiu. \nSua pontuação foi {score}", "Tentar de novo");
            }
            else
            {
                button.BackgroundColor = Color.Yellow;
                score++;
                //await DisplayAlert("Parabéns", $"Bomba desarmada! Sua pontuação é {score}.", "Continuar");
            }

            lblScore.Text = "Pontuação: " + score.ToString();

            bomba = random.Next(1, 4);
        }
    }
}

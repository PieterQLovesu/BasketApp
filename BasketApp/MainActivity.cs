using Android.App;
using Android.Widget;
using Android.OS;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace BasketApp
{
    [Activity(Label = "BasketApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        string Winner = null;
        int D4nteScoreRes = 0;
        int PieterScoreRes = 0;
        string WhenWePlayed = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.AddScore);

          

            SeekBar PieterScore = FindViewById<SeekBar>(Resource.Id.PieterScore);
            TextView PieterScoreView = FindViewById<TextView>(Resource.Id.PieterScoreView);

            SeekBar D4nteScore = FindViewById<SeekBar>(Resource.Id.D4nteScore);
            TextView D4nteScoreView = FindViewById<TextView>(Resource.Id.D4nteScoreView);


            //  Get Pieter's ProgressBar result
            PieterScore.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                if (e.FromUser)
                {
                    PieterScoreRes = e.Progress;
                    PieterScoreView.Text = "Wynik: " + PieterScoreRes;
                }
            };

            //  Get D4nte's ProgressBar result
            D4nteScore.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
            {
                if (e.FromUser)
                {
                    D4nteScoreRes = e.Progress;
                    D4nteScoreView.Text = "Wynik: " + D4nteScoreRes;
                }
            };


            // Day-Month-Year
            WhenWePlayed = Convert.ToString(DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year);

            // Set winner
            RadioButton D4nteRadio = FindViewById<RadioButton>(Resource.Id.D4nte);
            D4nteRadio.Click += (sender, e) =>
            {
                if (D4nteRadio.Checked == true)
                    Winner = "D4nte";
            };

            RadioButton PieterRadio = FindViewById<RadioButton>(Resource.Id.Pieter);
            PieterRadio.Click += (sender, e) =>
            {
                if (PieterRadio.Checked == true)
                    Winner = "Pieter";
            };


            Button SendButton = FindViewById<Button>(Resource.Id.Send);
            //  Send data to DB
            SendButton.Click += SendButton_Click;

            Button ShowResults = FindViewById<Button>(Resource.Id.ShowResults);
            ShowResults.Click += (sender, e) =>
            {
                SetContentView(Resource.Layout.ShowResults);
            };


            Button ExitButton = FindViewById<Button>(Resource.Id.Exit);
            // Exit program
            ExitButton.Click += (sender, e) =>
            {
                Finish();
            };
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            ConnectDB db = new ConnectDB(this);
            db.Connect();
            db.AddResult(Winner, D4nteScoreRes, PieterScoreRes, WhenWePlayed);
            db.Disconnect();
        }
    }
}


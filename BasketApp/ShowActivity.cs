using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BasketApp
{
    [Activity(Label = "BasketApp")]
    public class ShowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ShowResults);

            ConnectDB Database = new ConnectDB(this);
            Database.Connect();

            TextView D4nteWins = FindViewById<TextView>(Resource.Id.D4nteWins);
            Database.DisplayWinsCount(D4nteWins ,"D4nte");

            TextView PieterWins = FindViewById<TextView>(Resource.Id.PieterWins);
            Database.DisplayWinsCount(PieterWins, "Pieter");

            TextView AllScores = FindViewById<TextView>(Resource.Id.AllScores);
            AllScores.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
            Database.ShowResults(AllScores);

            Database.Disconnect();
        }
    }
}
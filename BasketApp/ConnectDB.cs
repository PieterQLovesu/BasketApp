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
using MySql.Data.MySqlClient;
using System.Data;

namespace BasketApp
{
    class ConnectDB
    {
        private string strConnection = "Server=sql11.freemysqlhosting.net;" +   //  Server
                                                    "Database=sql11173533;" +   //  DB
                                                    "Uid=sql11173533;" +        //  UserName
                                                    "Pwd=kDj7kP5Rax;";          //  Password
        private Context NameOfActivity;
        private MySqlConnection Database = new MySqlConnection();

        public ConnectDB(Context View)
        {
            NameOfActivity = View;
        }

        public void Connect()
        {
            try
            {
                Database.ConnectionString = strConnection;
                Database.Open();
            }
            catch(MySqlException ex)
            {
                Toast.MakeText(NameOfActivity, ex.ToString(), ToastLength.Long).Show();
            }
        }

        public void Disconnect()
        {
            if (Database.State == ConnectionState.Open)
            {
                try
                {
                    Toast.MakeText(NameOfActivity, "Dodano", ToastLength.Long).Show();
                    Database.Close();
                }
                catch(MySqlException ex)
                {
                    Toast.MakeText(NameOfActivity, ex.ToString(), ToastLength.Long).Show();
                    throw;
                }
            }
        }

        public void AddResult(string Winner, int D4nteScoreRes, int PieterScoreRes, string WhenWePlayed)
        {
            try
            {
                MySqlCommand cmdDB;
                cmdDB = Database.CreateCommand();
                cmdDB.CommandText = "INSERT INTO Basket(Winner, D4nteScore, PieterScore, Date) VALUES(@Winner, @D4nteScore, @PieterScore, @Date)";
                cmdDB.Parameters.AddWithValue("@Winner", Winner);
                cmdDB.Parameters.AddWithValue("@D4nteScore", D4nteScoreRes);
                cmdDB.Parameters.AddWithValue("@PieterScore", PieterScoreRes);
                cmdDB.Parameters.AddWithValue("@Date", WhenWePlayed);
                cmdDB.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                Toast.MakeText(NameOfActivity, ex.ToString(), ToastLength.Long).Show();
                throw;
            }
        }

        public void DisplayWinsCount(TextView PlaceToDisplay, string PlayerName)
        {
            try
            {
                MySqlCommand cmdDB;
                cmdDB = Database.CreateCommand();
                cmdDB.CommandText = "SELECT COUNT(*) FROM Basket WHERE Winner='"+ PlayerName +"';";
                MySqlDataReader WinsCount = cmdDB.ExecuteReader();
                while (WinsCount.Read())
                {
                    PlaceToDisplay.Text = PlayerName + ": " + WinsCount.GetString(0);
                }
                if(!WinsCount.IsClosed)
                    WinsCount.Close();
            }
            catch (MySqlException ex)
            {
                Toast.MakeText(NameOfActivity, ex.ToString(), ToastLength.Long).Show();
                throw;
            }         
        }

        public void ShowResults(TextView ShowRes)
        {

            try
            {
                MySqlCommand cmdDB;
                cmdDB = Database.CreateCommand();
                cmdDB.CommandText = "SELECT * FROM Basket";
                MySqlDataReader WinsCount = cmdDB.ExecuteReader();

                ShowRes.Text = "\t\tZwyciężca\t\tD4nte\t\tPieter\t\t\tData\n";

                while (WinsCount.Read())
                {
                    ShowRes.Text += "\t\t\t" + WinsCount.GetString(1) + 
                                    "\t\t\t\t" + LessThanTen(WinsCount.GetString(2)) + 
                                    "\t\t\t\t" + LessThanTen(WinsCount.GetString(3)) + 
                                    "\t\t\t" + WinsCount.GetString(4) + "\n";

                }
                if (!WinsCount.IsClosed)
                    WinsCount.Close();
            }
            catch (MySqlException ex)
            {
                Toast.MakeText(NameOfActivity, ex.ToString(), ToastLength.Long).Show();
                throw;
            }
        }

        private string LessThanTen(string Points)
        {
            char idk = '0';

            for (int i = 0; i < 10; i++)
            {
                if(Points[0] == idk && Points.Length == 1)
                {
                    Points += " ";
                    break;
                }

                ++idk;
            }

            return Points;
        }
    }
}
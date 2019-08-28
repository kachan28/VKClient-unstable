using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using VkNet;
using System.Data.SQLite;
using System.IO;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace VKCrypto
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static VkApi api = Login.api;
        public static string myname = api.Account.GetProfileInfo().FirstName;
        public static bool chatloaded = false;

        public MainWindow()
        {
            //Utils.VKManage.VKSignal Signaller = new Utils.VKManage.VKSignal();
            InitializeComponent();

            SQLiteConnection.CreateFile("Current_Dialogs.sqlite");

            using (SQLiteConnection CrFile_Connection = new SQLiteConnection("Data Source=Current_Dialogs.sqlite;"))
            {
                CrFile_Connection.Open();
                string sql = "create table active_users (id int, pubkey text, simkey text)";
                SQLiteCommand command = new SQLiteCommand(sql, CrFile_Connection);
                command.ExecuteNonQuery();
                CrFile_Connection.Close();
            }
        }

        private void Frame_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            var ta = new ThicknessAnimation();
            ta.Duration = TimeSpan.FromSeconds(0.5);
            ta.DecelerationRatio = 0.5;
            ta.To = new Thickness(0, 0, 0, 0);
            if (e.NavigationMode == NavigationMode.New)
            {
                ta.From = new Thickness(500, 0, 0, 0);
            }
            else if (e.NavigationMode == NavigationMode.Back)
            {
                ta.From = new Thickness(0, 0, 500, 0);
            }
            (e.Content as Page).BeginAnimation(MarginProperty, ta);
        }

        private void Message_Button_Click(object sender, RoutedEventArgs e)
        {
            Pages.NavigationService.Navigate(new Chat_with_List());
            Welcome.Content = "";
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string filename = "Current_Dialogs.sqlite";
            if (File.Exists(filename))
            {
                try
                {
                    File.Delete(filename);
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
            else
            {
                MessageBox.Show("File is not exist");
            }
        }
    }
}

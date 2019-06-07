using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Security.Cryptography;
using VkNet;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;

namespace VKCrypto
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static VkApi api = Login.api;
        public static string myname = api.Account.GetProfileInfo().FirstName;
        public MainWindow()
        {
            InitializeComponent();
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

    }
}

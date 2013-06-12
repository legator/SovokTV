using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SovokStoreApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private const string VAULT_RESOURCE = "Sovok.tv WinApp";
        private PasswordVault vault = new PasswordVault();

        public Login()
        {
            this.InitializeComponent();
            LoadUserCredential();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void LoadUserCredential()
        {
            try
            {
                var creds = vault.FindAllByResource(VAULT_RESOURCE).FirstOrDefault();
                if (creds != null)
                {
                    string UserName = creds.UserName;
                    string Password = vault.Retrieve(VAULT_RESOURCE, UserName).Password;
                    login(UserName,Password,"");
                }
            }
            catch(Exception) 
            {
                // this exception likely means that no credentials have been stored
            }
        }

        private async void Login_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(LoginBox.Text) || String.IsNullOrEmpty(PassBox.Password))
                {
                    throw new System.InvalidOperationException("Put all data");
                } else 
                {
                    login(LoginBox.Text,PassBox.Password,CodeBox.Password);
                    vault.Add(new PasswordCredential(VAULT_RESOURCE, LoginBox.Text, PassBox.Password));
                }
            }
            catch (Exception ex)
            {
                var mess = new MessageDialog("Error to login\n");
                mess.ShowAsync();
            }
        }

        private async void login(string user,string pass,string code)
        {
            try
            {
                LoginPanel.Visibility = Visibility.Collapsed;
                WaitPanel.Visibility = Visibility.Visible;
                ActionButton.Visibility = Visibility.Collapsed;
                collapsedActionButton.Visibility = Visibility.Visible;

                await App.ViewModel.Login(user, pass, code);
                await App.ViewModel.GetChannel();
                await App.ViewModel.GetFavoriteChannels();
                await App.ViewModel.GetStreamServer();
                App.ViewModel.UpdateTitle();
                this.Frame.Navigate(typeof(PlayerPage));
            }
            catch (Exception ex)
            {
                LoginPanel.Visibility = Visibility.Visible;
                WaitPanel.Visibility = Visibility.Collapsed;
                ActionButton.Visibility = Visibility.Visible;
                collapsedActionButton.Visibility = Visibility.Collapsed;
                var mess = new MessageDialog("Error to login\n"+ex.Message);
                mess.ShowAsync();
            }
        }

        private void GoToSovokSite(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("http://www.sovok.tv"));
        }

        private void GoToGitHub(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("https://github.com/legator"));
        }

        private void GoToTwitter(object sender, RoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri("https://twitter.com/_legator"));
        }
    }
}

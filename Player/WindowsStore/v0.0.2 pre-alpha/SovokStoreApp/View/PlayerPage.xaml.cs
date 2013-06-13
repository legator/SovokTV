using SovokAPI.Class;
using SovokStoreApp.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using Windows.System;
using SovokStoreApp.View;
using Windows.Security.Credentials;
using Windows.UI.Notifications;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace SovokStoreApp
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class PlayerPage : SovokStoreApp.Common.LayoutAwarePage
    {
        private ObservableCollection<GroupChannel> _groups;
        private int index = -1;
        private int ind = -1;
        private bool showlist = true;
        private double volume = 0;
        private string quality_stream = null;


        /// <summary>
        Rect _windowBounds;
        double _settingsWidth = 346;
        Popup _settingsPopup;
        /// </summary>

        public PlayerPage()
        {
            this.InitializeComponent();
            this.DataContext = App.ViewModel;
            Grouped();
            SettingsPane.GetForCurrentView().CommandsRequested += SettingsCommandsRequested;

            _windowBounds = Window.Current.Bounds;
        }

        #region side settings bar
        private void SettingsCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand about_cmd = new SettingsCommand("about", "About", (x) =>
            {
                _settingsPopup = new Popup();
                _settingsPopup.Closed += OnPopupClosed;
                Window.Current.Activated += OnWindowActivated;
                _settingsPopup.IsLightDismissEnabled = true;
                _settingsPopup.Width = _settingsWidth;
                _settingsPopup.Height = _windowBounds.Height;

                SettingsAboutView mypane = new SettingsAboutView();
                //SimpleSettingsNarrow mypane = new SimpleSettingsNarrow();
                mypane.Width = _settingsWidth;
                mypane.Height = _windowBounds.Height;

                _settingsPopup.Child = mypane;
                _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);
                _settingsPopup.IsOpen = true;
            });

            args.Request.ApplicationCommands.Add(about_cmd);

            SettingsCommand logout_cmd = new SettingsCommand("logout", "Log out", (x) =>
            {
                var vault = new PasswordVault();
                vault.Remove(vault.Retrieve("Sovok.tv WinApp", App.ViewModel.UserAccount.login));
                bool nav = Frame.Navigate(typeof(Login));
                App.ViewModel = new Model.MainViewModel();
            });

            args.Request.ApplicationCommands.Add(logout_cmd);

            SettingsCommand settings_cmd = new SettingsCommand("settings", "Settings", (x) =>
            {
                _settingsPopup = new Popup();
                _settingsPopup.Closed += OnPopupClosed;
                Window.Current.Activated += OnWindowActivated;
                _settingsPopup.IsLightDismissEnabled = true;
                _settingsPopup.Width = _settingsWidth;
                _settingsPopup.Height = _windowBounds.Height;

                SettingsView mypane = new SettingsView();
                mypane.Width = _settingsWidth;
                mypane.Height = _windowBounds.Height;

                _settingsPopup.Child = mypane;
                _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
                _settingsPopup.SetValue(Canvas.TopProperty, 0);
                _settingsPopup.IsOpen = true;
            });
            args.Request.ApplicationCommands.Add(settings_cmd);
        }

        private void OnWindowActivated(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.Deactivated)
            {
                _settingsPopup.IsOpen = false;
            }
        }

        void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }
        #endregion

        private void Grouped()
        {
            _groups = new ObservableCollection<GroupChannel>();
            foreach (ChannelGroup item in App.ViewModel.UserAccount.channel_group)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Name = item.name;
                cbi.Content = item.name;
                ChCategory.Items.Add(cbi);
                ind++;
                var g = new GroupChannel{ GroupName = item.name};
                foreach (Channel it in item.Channels)
                {
                    Channel c = new Channel();
                    c = it;
                    //c.icon = "http://sovok.tv" + c.icon;
                    g.Channels.Add(c);
                }
                _groups.Add(g);
            }
            ComboBoxItem fcbi = new ComboBoxItem();
            fcbi.Content = fcbi.Name = "Favorite";
            ChCategory.Items.Add(fcbi);
            ind++;
            CList.Source = _groups;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }

        private async void ChangeActiveChannel(object sender, SelectionChangedEventArgs e)
        {
            Channel c = new Channel();
            index = ChannelListView.SelectedIndex;
            if (index<=-1)
            {
                return;
            }
            c = (Channel)ChannelListView.SelectedItems[0];
            App.ViewModel.Active_Channel = c;
            App.ViewModel.UpdateTitle();

            App.ViewModel.EpgTimeStart = c.epg_start.AddHours(App.ViewModel.Time).Hour + ":" + c.epg_start.AddHours(App.ViewModel.Time).Minute;
            App.ViewModel.EpgTimeEnd = c.epg_end.AddHours(App.ViewModel.Time).Hour + ":" + c.epg_end.AddHours(App.ViewModel.Time).Minute;

            var dif = (c.epg_end.AddHours(App.ViewModel.Time) - c.epg_start.AddHours(App.ViewModel.Time)).TotalMinutes;
            var dif1 = (DateTime.Now - c.epg_start.AddHours(App.ViewModel.Time)).TotalMinutes;
            App.ViewModel.EpgProgress = Math.Round(dif1/dif * 100);

            OpenStream(c.id);
            ChannelProcced(c);
            OpenEPG(c.id);
        }

        private async Task OpenStream(string id)
        {
            string url = await App.ViewModel.OpenChannel(id);
            try
            {
                Uri u = new Uri(url.Replace("http/ts", "http://ts").Trim());
                App.ViewModel.StreamURL = u;
                medialayer.Source = u;
                medialayer.Play();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.StackTrace + App.ViewModel.StreamURL);
                messageDialog.ShowAsync();
            }
        }

        private async Task OpenEPG(string id)
        {
            List<Programs> lp = new List<Programs>();
            await App.ViewModel.GetEpg();
            lp = App.ViewModel.EPG;
            ChannelEpgView.ItemsSource = lp;
        }

        private void ChangeChannelCategory(object sender, SelectionChangedEventArgs e)
        {
            ShowCategory(ChCategory.SelectedIndex);
        }

        private void ShowCategory(int index)
        {
            _groups.Clear();
            if (index == ind)
            {
                var fg = new GroupChannel { GroupName = "Favorite" };
                foreach (FavoriteChannel item in App.ViewModel.UserAccount.favorite_channel)
                {
                    Channel fc = new Channel();
                    foreach (СhannelItem it in App.ViewModel.ChannelList)
                    {
                        if (item.id_channel == it.ChannelId)
                        {
                            fc.epg_end = it.epg_end;
                            fc.epg_progname = it.Epg;
                            fc.epg_start = it.epg_start;
                            fc.icon = it.Image_Source.ToString();
                            fc.name = it.ChannelName;
                            break;
                        }
                    }
                    fg.Channels.Add(fc);
                }
                _groups.Add(fg);    
            }
            else
            {
                if (index<=0 || index>=App.ViewModel.UserAccount.channel_group.Count)
                {
                    index = 0;
                }
                try
                {
                    ChannelGroup item = App.ViewModel.UserAccount.channel_group[index];
                    var g = new GroupChannel { GroupName = item.name };
                    foreach (Channel it in item.Channels)
                    {
                        Channel c = new Channel();
                        c = it;
                        g.Channels.Add(c);
                    }
                    _groups.Add(g);
                }catch(Exception ex)
                {
                    var mes = new MessageDialog(index+"\n"+App.ViewModel.UserAccount.channel_group.Count);
                    mes.ShowAsync();
                }
            }
            CList.Source = _groups;
        }

        private void PrevChannel(object sender, RoutedEventArgs e)
        {
            --index;
            if (index <= -1)
            {
                index = ChannelListView.Items.Count - 1;
            }
            ChannelListView.SelectedIndex = index;
        }

        private void NextChannel(object sender, RoutedEventArgs e)
        {
            ++index;
            if (index >= ChannelListView.Items.Count)
            {
                index = 0;
            }
            ChannelListView.SelectedIndex = index;
        }

        private void ShowHideChannelList(object sender, RoutedEventArgs e)
        {
            App.ViewModel.IsList = !App.ViewModel.IsList;
            App.ViewModel.IsEpg = false; EpgButton.IsChecked = false;
            HideRightBar();
            if (App.ViewModel.IsList)
            {
                SideBar.Visibility = Visibility.Visible;
                ChannelListView.Visibility = Visibility.Visible;
                HideListButton.Visibility = Visibility.Visible;
                ChCategory.Visibility = Visibility.Visible;
            }
        }

        private void ChannelProcced(Channel c)
        {
            foreach (FavoriteChannel item in App.ViewModel.UserAccount.favorite_channel)
            {
                if (c.id==item.id_channel)
                {
                    App.ViewModel.IsFavorite = true;
                    break;
                }
            }
        }

        private void FavoriteChannel(object sender, RoutedEventArgs e)
        {
            App.ViewModel.AddDelFavoriteChannel(App.ViewModel.Active_Channel.id);
            if (App.ViewModel.IsFavorite)
            {
                App.ViewModel.IsFavorite = false;
            } App.ViewModel.IsFavorite = true;
        }

        private void PlayStopStream(object sender, RoutedEventArgs e)
        {
            if (App.ViewModel.IsPlay)
            {
                medialayer.Stop();
            }
            else
            {
                medialayer.Play();
            }
            App.ViewModel.IsPlay = !App.ViewModel.IsPlay;
        }

        private void VolumeMuteClick(object sender, RoutedEventArgs e)
        {
            if (App.ViewModel.IsMute)
            {
                App.ViewModel.IsMute = false;
                App.ViewModel.Volume = volume;
            }
            else
            {
                volume = App.ViewModel.Volume;
                App.ViewModel.IsMute = true;
                App.ViewModel.Volume = 0;
            }
        }

        private void HideChannelList(object sender, RoutedEventArgs e)
        {
            HideRightBar();
            EpgButton.IsChecked = false;
            List.IsChecked = false;
        }

        private void ShowHideEpg(object sender, RoutedEventArgs e)
        {
            App.ViewModel.IsEpg = !App.ViewModel.IsEpg;
            App.ViewModel.IsList = false; List.IsChecked = false;
            HideRightBar();
            if (App.ViewModel.IsEpg)
            {
                SideBar.Visibility = Visibility.Visible;
                ChannelEpgView.Visibility = Visibility.Visible;
                HideListButton.Visibility = Visibility.Visible;
                EpgText.Visibility = Visibility.Visible;
            }
        }

        private void HideRightBar()
        {
            SideBar.Visibility = Visibility.Collapsed;
            ChCategory.Visibility = Visibility.Collapsed;
            ChannelListView.Visibility = Visibility.Collapsed;
            HideListButton.Visibility = Visibility.Collapsed;
            ChannelEpgView.Visibility = Visibility.Collapsed;
            EpgText.Visibility = Visibility.Collapsed;
        }
    }
}

using SovokTV.Common;
using SovokTV.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Credentials;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SovokTV
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayerPage :  LayoutAwarePage
    {
        private double volume = 0;
        private int index = -1;
        Rect _windowBounds;
        double _settingsWidth = 346;
        Popup _settingsPopup;

        public PlayerPage()
        {
            this.InitializeComponent();
            this.DataContext = App.ViewModel;
            SettingsPane.GetForCurrentView().CommandsRequested += SettingsCommandsRequested;
            _windowBounds = Window.Current.Bounds;
            ShowChannelsView();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void SettingsCommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            SettingsCommand channels_cmd = new SettingsCommand("Channels", "Channels", (x) =>
            {
                ShowChannelsView();
            });
            args.Request.ApplicationCommands.Add(channels_cmd);

            SettingsCommand epg_cmd = new SettingsCommand("Epg", "EPG", (x) =>
            {
                ShowEPGView();
            });
            args.Request.ApplicationCommands.Add(epg_cmd);

            SettingsCommand settings_cmd = new SettingsCommand("Settings", "Settings", (x) =>
            {
                ShowSettingsView();
            });
            args.Request.ApplicationCommands.Add(settings_cmd);

            SettingsCommand linc_cmd = new SettingsCommand("Licence", "Privacy Policy", (x) =>
            {
                ShowLicenceView();
            });
            args.Request.ApplicationCommands.Add(linc_cmd);

            SettingsCommand about_cmd = new SettingsCommand("About", "About", (x) =>
            {
                ShowAboutView();
            });
            args.Request.ApplicationCommands.Add(about_cmd);

            SettingsCommand logout_cmd = new SettingsCommand("logout", "Log out", (x) =>
            {
                var vault = new PasswordVault();
                vault.Remove(vault.Retrieve("Sovok.tv WinApp", App.ViewModel.UserAccount.login));
                bool nav = Frame.Navigate(typeof(Login));
                App.ViewModel = new Model.MainViewModel();
            });
        }

        private void ShowLicenceView()
        {
            _settingsPopup = new Popup();
            _settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _settingsPopup.IsLightDismissEnabled = true;
            _settingsPopup.Width = _settingsWidth;
            _settingsPopup.Height = _windowBounds.Height;

            LicenceView mypane = new LicenceView();
            mypane.Width = _settingsWidth;
            mypane.Height = _windowBounds.Height;

            _settingsPopup.Child = mypane;
            _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
            _settingsPopup.SetValue(Canvas.TopProperty, 0);
            _settingsPopup.IsOpen = true;
        }

        private void ShowSettingsView()
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
        }

        private void ShowEPGView()
        {
            _settingsPopup = new Popup();
            _settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _settingsPopup.IsLightDismissEnabled = true;
            _settingsPopup.Width = _settingsWidth;
            _settingsPopup.Height = _windowBounds.Height;

            EpgView mypane = new EpgView();
            mypane.Width = _settingsWidth;
            mypane.Height = _windowBounds.Height;

            _settingsPopup.Child = mypane;
            _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
            _settingsPopup.SetValue(Canvas.TopProperty, 0);
            _settingsPopup.IsOpen = true;
        }

        private void ShowChannelsView()
        {
            _settingsPopup = new Popup();
            _settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _settingsPopup.IsLightDismissEnabled = true;
            _settingsPopup.Width = _settingsWidth;
            _settingsPopup.Height = _windowBounds.Height;

            ChannelsView mypane = new ChannelsView();
            mypane.Width = _settingsWidth;
            mypane.Height = _windowBounds.Height;

            _settingsPopup.Child = mypane;
            _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
            _settingsPopup.SetValue(Canvas.TopProperty, 0);
            _settingsPopup.IsOpen = true;
        }

        private void ShowAboutView()
        {
            _settingsPopup = new Popup();
            _settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            _settingsPopup.IsLightDismissEnabled = true;
            _settingsPopup.Width = _settingsWidth;
            _settingsPopup.Height = _windowBounds.Height;

            AboutView mypane = new AboutView();
            mypane.Width = _settingsWidth;
            mypane.Height = _windowBounds.Height;

            _settingsPopup.Child = mypane;
            _settingsPopup.SetValue(Canvas.LeftProperty, _windowBounds.Width - _settingsWidth);
            _settingsPopup.SetValue(Canvas.TopProperty, 0);
            _settingsPopup.IsOpen = true;
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
            UnCheck();
        }

        private void PrevChannel(object sender, RoutedEventArgs e)
        {
            --App.ViewModel.SelectedChannelIndex;
            if (App.ViewModel.SelectedChannelIndex <= -1)
            {
                App.ViewModel.SelectedChannelIndex = App.ViewModel.ChanneLisrRange - 1;
            }
            App.ViewModel.ChangeChannel();
        }

        private void NextChannel(object sender, RoutedEventArgs e)
        {
            ++App.ViewModel.SelectedChannelIndex;
            if (App.ViewModel.SelectedChannelIndex >= App.ViewModel.ChanneLisrRange)
            {
                App.ViewModel.SelectedChannelIndex = 0;
            }
            App.ViewModel.ChangeChannel();
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

        private void FavoriteChannel(object sender, RoutedEventArgs e)
        {
            App.ViewModel.AddDelFavoriteChannel(App.ViewModel.Active_Channel.id);
            if (App.ViewModel.IsFavorite)
            {
                App.ViewModel.IsFavorite = false;
            } App.ViewModel.IsFavorite = true;
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

        private void FailedToLoadStream(object sender, ExceptionRoutedEventArgs e)
        {
            App.ViewModel.IsPlay = false;
        }

        private void ShowHideChannelList(object sender, RoutedEventArgs e)
        {
            App.ViewModel.IsList = !App.ViewModel.IsList;
            App.ViewModel.IsEpg = false; EpgButton.IsChecked = false;
            HideRightBar();
            if (App.ViewModel.IsList)
            {
                ShowChannelsView();
            }
        }

        private void ShowHideEpg(object sender, RoutedEventArgs e)
        {
            App.ViewModel.IsEpg = !App.ViewModel.IsEpg;
            App.ViewModel.IsList = false; List.IsChecked = false;
            HideRightBar();
            if (App.ViewModel.IsEpg)
            {
                ShowEPGView();
            }
        }

        private void UnCheck()
        {
            App.ViewModel.IsList = false; List.IsChecked = false;
            App.ViewModel.IsEpg = false; EpgButton.IsChecked = false;
        }

        private void HideRightBar()
        {
            this.Focus(FocusState.Programmatic);
        }
    }
}

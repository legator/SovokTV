using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace SovokTV.View
{
    public sealed partial class SnapViewControl : UserControl
    {
        private double volume = 0;
        private int index = -1;

        public SnapViewControl()
        {
            this.InitializeComponent();
            this.DataContext = App.ViewModel;
        }

        private void PrevChannel(object sender, RoutedEventArgs e)
        {
            --App.ViewModel.SelectedChannelIndex;
            if (App.ViewModel.SelectedChannelIndex <= -1)
            {
                App.ViewModel.SelectedChannelIndex = App.ViewModel.ChanneLisrRange - 1;
            }
        }

        private void NextChannel(object sender, RoutedEventArgs e)
        {
            ++App.ViewModel.SelectedChannelIndex;
            if (App.ViewModel.SelectedChannelIndex >= App.ViewModel.ChanneLisrRange)
            {
                App.ViewModel.SelectedChannelIndex = 0;
            }
        }

        private void PlayStop(object sender, RoutedEventArgs e)
        {
            if (App.ViewModel.IsPlay)
            {
                MiniMediaPlayer.Stop();
            }
            else
            {
                MiniMediaPlayer.Play();
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

        private void Mute(object sender, RoutedEventArgs e)
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
    }
}

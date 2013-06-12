using SovokAPI.Class;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SovokStoreApp.View
{
    public sealed partial class SnapViewControl : UserControl
    {
        private ObservableCollection<GroupChannel> _groups;
        private int index = -1;
        private bool showlist = true;
        private double volume = 0;
        private string quality_stream = null;

        public SnapViewControl()
        {
            this.InitializeComponent();
            this.DataContext = App.ViewModel;
            Grouped();
        }

        private void Grouped()
        {
            _groups = new ObservableCollection<GroupChannel>();
            foreach (ChannelGroup item in App.ViewModel.UserAccount.channel_group)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Name = item.name;
                cbi.Content = item.name;
                ChCategory.Items.Add(cbi);

                var g = new GroupChannel { GroupName = item.name };
                foreach (Channel it in item.Channels)
                {
                    Channel c = new Channel();
                    c = it;
                    c.icon = "http://sovok.tv" + c.icon;
                    g.Channels.Add(c);
                }
                _groups.Add(g);
            }
            //var fg = new GroupChannel { GroupName = "Favorite" };
            ComboBoxItem fcbi = new ComboBoxItem();
            fcbi.Content = fcbi.Name = "Favorite";
            ChCategory.Items.Add(fcbi);
            /*foreach (FavoriteChannel item in App.ViewModel.UserAccount.favorite_channel)
            {
                Channel fc = new Channel();
                foreach (СhannelItem it in App.ViewModel.ChannelList)
                {
                    if (item.id_channel==it.ChannelId)
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
            _groups.Add(fg);*/
            CList.Source = _groups;
        }

        private void ChangeChannelCategory(object sender, SelectionChangedEventArgs e)
        {
            ShowCategory(ChCategory.SelectedIndex);
        }

        private void ShowCategory(int index)
        {
            _groups.Clear();
            if (index == 15)
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
                ChannelGroup item = App.ViewModel.UserAccount.channel_group[index];
                var g = new GroupChannel { GroupName = item.name };
                foreach (Channel it in item.Channels)
                {
                    Channel c = new Channel();
                    c = it;
                    g.Channels.Add(c);
                }
                _groups.Add(g);
            }
            CList.Source = _groups;
        }

        private async void ChangeActiveChannel(object sender, SelectionChangedEventArgs e)
        {
            Channel c = new Channel();
            index = ChannelListView.SelectedIndex;
            if (index <= -1)
            {
                return;
            }
            c = (Channel)ChannelListView.SelectedItems[0];
            App.ViewModel.Active_Channel = c;
            OpenStream(c.id);
            ChannelProcced(c);
            OpenEPG(c.id);

            ChCategory.Visibility = Visibility.Collapsed;
            ChannelListView.Visibility = Visibility.Collapsed;
            HideListButton.Visibility = Visibility.Collapsed;
            showlist = false;
        }

        private async Task OpenStream(string id)
        {
            App.ViewModel.StreamURL = await App.ViewModel.OpenChannel(id);
            try
            {
                Uri url = new Uri((App.ViewModel.StreamURL).Replace("http/ts", "http://ts").Trim());
                MiniMediaPlayer.Source = url;
                MiniMediaPlayer.Play();
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog(ex.StackTrace);
                messageDialog.ShowAsync();
            }
        }

        private void ChannelProcced(Channel c)
        {
            foreach (FavoriteChannel item in App.ViewModel.UserAccount.favorite_channel)
            {
                if (c.id == item.id_channel)
                {
                    App.ViewModel.IsFavorite = true;
                    break;
                }
            }
            foreach (ChannelGroup item in App.ViewModel.UserAccount.channel_group)
            {
                if (item.name.IndexOf("HD") != -1)
                {
                    foreach (Channel it in item.Channels)
                    {
                        if (it.name.IndexOf(c.name) != -1)
                        {
                            if (it.id != c.id)
                            {
                                //QualityButton.Content = "HD";
                                quality_stream = it.id;
                                break;
                            }
                        }
                    }
                    break;
                }
                else continue;
            }
        }

        private void OpenEPG(string id)
        { }

        private void PalyStop(object sender, RoutedEventArgs e)
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

        private void NextChannel(object sender, RoutedEventArgs e)
        {
            ++index;
            if (index >= ChannelListView.Items.Count)
            {
                index = 0;
            }
            ChannelListView.SelectedIndex = index;
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

        private void ShowHideChannelList(object sender, RoutedEventArgs e)
        {
            showlist = !showlist;
            if (showlist)
            {
                ChannelListView.Visibility = Visibility.Visible;
                HideListButton.Visibility = Visibility.Visible;
                ChCategory.Visibility = Visibility.Visible;
            }
            else
            {
                ChCategory.Visibility = Visibility.Collapsed;
                ChannelListView.Visibility = Visibility.Collapsed;
                HideListButton.Visibility = Visibility.Collapsed;
            }
        }

        private void ChangeQuality(object sender, RoutedEventArgs e)
        {
            /*if (QualityButton.Content == "HD")
            {
                if (String.IsNullOrEmpty(quality_stream))
                {
                    string ind = quality_stream;
                    quality_stream = App.ViewModel.Active_Channel.id;
                    QualityButton.Content = "SD";
                    OpenStream(ind);
                }
            }
            */
        }
    }
}

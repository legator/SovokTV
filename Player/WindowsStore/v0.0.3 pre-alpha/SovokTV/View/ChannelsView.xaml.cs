using SovokAPI.Class;
using SovokTV.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace SovokTV
{
    public sealed partial class ChannelsView : UserControl
    {
        private ObservableCollection<GroupChannel> _groups;
        private int ind = -1;

        public ChannelsView()
        {
            this.InitializeComponent();

            this.DataContext = App.ViewModel;
            Grouped();

            ChannelListView.Height = Window.Current.Bounds.Height - 100;
            ChannelListView.SelectedIndex = App.ViewModel.SelectedChannelIndex;
        }
        /// <summary>
        /// Back to chams
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Hide(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
            {
                ((Popup)this.Parent).IsOpen = false;
            }
            SettingsPane.Show();
        }
        /// <summary>
        /// Grouped chennels
        /// </summary>
        private void Grouped()
        {
            _groups = new ObservableCollection<GroupChannel>();
            foreach (ChannelGroup item in App.ViewModel.UserAccount.channel_group)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Name = item.name;
                cbi.Content = item.name;
                ind++;
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
            ComboBoxItem fcbi = new ComboBoxItem();
            fcbi.Content = fcbi.Name = "Favorite";
            ind++;
            CList.Source = _groups;
            App.ViewModel.ChanneLisrRange = ChannelListView.Items.Count;
        }

        private async void ChangeActiveChannel(object sender, SelectionChangedEventArgs e)
        {
            Channel c = new Channel();
           
            if (ChannelListView.SelectedIndex <= -1)
                return;
            if (ChannelListView.SelectedIndex == App.ViewModel.channel_index)
                return;
            
            App.ViewModel.SelectedChannelIndex = ChannelListView.SelectedIndex;

            App.ViewModel.ChangeChannel();
        }

    }
}

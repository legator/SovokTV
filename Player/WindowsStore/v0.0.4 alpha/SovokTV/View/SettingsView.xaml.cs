using SovokAPI.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ApplicationSettings;
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
    public sealed partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            this.InitializeComponent();
            this.DataContext = App.ViewModel;
            foreach (Streamers item in App.ViewModel.StreamServer)
            {
                ComboBoxItem boxitem = new ComboBoxItem();
                boxitem.Content = item.id;
                boxitem.Name = item.name;
                Stream.Items.Add(boxitem);
            }
            set_value();
        }

        private void set_value()
        {
            BufferValue.Text = App.ViewModel.UserAccount.setting.Buffer;
            DeinterlaceValue.Text = App.ViewModel.UserAccount.setting.Deinterlace;
            int i = -1;
            foreach (ComboBoxItem item in TimeZone.Items)
            {
                i++;
                if (item.Content.ToString() == App.ViewModel.UserAccount.setting.TimeZone)
                {
                    break;
                }
            }
            TimeZone.SelectedIndex = i;
            i = -1;
            foreach (ComboBoxItem item in Stream.Items)
            {
                i++;
                if (item.Content.ToString() == App.ViewModel.UserAccount.setting.StreamServer)
                {
                    break;
                }
            }
            Stream.SelectedIndex = i;
        }

        private void HideSettings(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
            {
                ((Popup)this.Parent).IsOpen = false;
            }
            SettingsPane.Show();
        }

        private void ChangedTimeZone(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)TimeZone.SelectedItem;
            if (item.Content.ToString() == App.ViewModel.UserAccount.setting.TimeZone)
            {
                return;
            }
            App.ViewModel.SetTimeZone(item.Content.ToString());
        }

        private void ChangeStream(object sender, SelectionChangedEventArgs e)
        {
            Streamers st = App.ViewModel.StreamServer[Stream.SelectedIndex];
            if (st.id == App.ViewModel.UserAccount.setting.TimeZone)
            {
                return;
            }
            App.ViewModel.SetStreamServer(Convert.ToInt32(st.id));
        }
    }
}

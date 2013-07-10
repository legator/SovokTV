using SovokAPI.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    public sealed partial class EpgView : UserControl
    {
        public EpgView()
        {
            this.InitializeComponent();
            if (App.ViewModel.Active_Channel != null)
                OpenEPG(App.ViewModel.Active_Channel.id);

            ChannelEpgView.Height = Window.Current.Bounds.Height - 100;
        }

        private void HideSettings(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
            {
                ((Popup)this.Parent).IsOpen = false;
            }
            SettingsPane.Show();
        }

        private async Task OpenEPG(string id)
        {
            List<Programs> lp = new List<Programs>();
            await App.ViewModel.GetEpg();
            lp = App.ViewModel.EPG;
            ChannelEpgView.ItemsSource = lp;
        }
    }
}

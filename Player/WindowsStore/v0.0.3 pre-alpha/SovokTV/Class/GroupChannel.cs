using SovokAPI.Class;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovokTV.Class
{
    public class GroupChannel
    {
        public string GroupName { get; set; }
        public ObservableCollection<Channel> Channels { get; set; }

        public GroupChannel()
        {
            Channels = new ObservableCollection<Channel>();
        }
    }
}

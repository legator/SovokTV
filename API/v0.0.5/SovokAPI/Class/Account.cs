using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovokAPI.Class
{
    public class Account
    {
        public string sid { get; set; }
        public string sid_name { get; set; }
        public string login { get; set; }
        public string balance { get; set; }
        public List<Service> services { get; set; }
        public string protect_code { get; set; }
        public Setting setting { get; set; }
        public List<ChannelGroup> channel_group { get; set; }
        public List<FavoriteChannel> favorite_channel { get; set; }
    }
}

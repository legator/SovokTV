using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovokAPI.Class
{
    public class СhannelItem
    {
        public string CategoryName { get; set; }
        public string ChannelId { get; set; }
        public string ChannelName { get; set; }
        public Uri Image_Source { get; set; }
        public DateTime epg_start { get; set; }
        public DateTime epg_end { get; set; }
        public int Progress { get; set; }
        public string Epg { get; set; }
    }
}

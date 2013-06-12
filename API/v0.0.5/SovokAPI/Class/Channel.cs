using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SovokAPI.Class
{
    public class Channel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string is_video { get; set; }
        public string is_protected { get; set; }
        public string icon { get; set; }
        public string epg_progname { get; set; }
        public DateTime epg_start { get; set; }
        public DateTime epg_end { get; set; }
    }
}

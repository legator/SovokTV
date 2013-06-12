using SovokAPI;
using SovokAPI.Class;
using SovokStoreApp.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace SovokStoreApp.Model
{
    public class MainViewModel : ViewModelBase
    {
        private restAPI api = new restAPI();
        public TileHelper tile = new TileHelper();

        public Account UserAccount
        {
            get
            {
                if (api.Account != null)
                    return api.Account;
                else return null;
            }
            set
            {
                if (api.Account == value)
                    return;
                api.Account = value;
                base.RaisePropertyChanged("UserAccount");
            }
        }

        private Channel active_channel;
        public Channel Active_Channel
        {
            get { return this.active_channel; }
            set
            {
                if (this.active_channel == value)
                {
                    return;
                }
                this.active_channel = value;
                base.RaisePropertyChanged("Active_Channel");
            }
        }

        private List<СhannelItem> channel_list;
        public List<СhannelItem> ChannelList
        {
            get { return this.channel_list; }
            set
            {
                if (this.channel_list == value) return;
                this.channel_list = value;
                base.RaisePropertyChanged("ChannelList");
            }
        }

        private List<Streamers> streamers;
        public List<Streamers> StreamServer
        {
            get { return streamers; }
            set 
            {
                if(this.streamers==value) return;
                this.streamers = value;
                base.RaisePropertyChanged("StreamServer");
            }
        }

        private string stream_url;
        public string StreamURL
        {
            get { return this.stream_url; }
            set
            {
                if (this.stream_url == value) return;
                this.stream_url = value;
                base.RaisePropertyChanged("StreamURL");
            }
        }

        private bool isplay = false;
        public bool IsPlay
        {
            get
            {
                return this.isplay;
            }
            set
            {
                bool flag = this.isplay != value;
                if (flag)
                {
                    this.isplay = value;
                    base.RaisePropertyChanged("IsPlay");
                }
            }
        }

        private bool ismute = false;
        public bool IsMute
        {
            get
            {
                return this.ismute;
            }
            set
            {
                bool flag = this.ismute != value;
                if (flag)
                {
                    this.ismute = value;
                    base.RaisePropertyChanged("IsMute");
                }
            }
        }

        private bool isfavorite = false;
        public bool IsFavorite
        {
            get
            {
                return this.isfavorite;
            }
            set
            {
                bool flag = this.isfavorite != value;
                if (flag)
                {
                    this.isfavorite = value;
                    base.RaisePropertyChanged("IsFavorite");
                }
            }
        }

        private bool islist = true;
        public bool IsList
        {
            get { return this.islist; }
            set 
            {
                if (this.islist = value) return;
                this.islist = value;
                base.RaisePropertyChanged("IsList");
            }
        }

        private bool isepg = false;
        public bool IsEpg
        {
            get { return this.isepg; }
            set
            {
                if (this.isepg = value) return;
                this.isepg = value;
                base.RaisePropertyChanged("IsEpg");
            }
        }

        private string timestart;
        public string EpgTimeStart
        {
            get { return this.timestart; }
            set 
            {
                if(this.timestart==value) return;
                this.timestart = value;
                base.RaisePropertyChanged("EpgTimeStart");
            }
        }

        private string timeend;
        public string EpgTimeEnd
        {
            get { return this.timeend; }
            set
            {
                if (this.timeend == value) return;
                this.timeend = value;
                base.RaisePropertyChanged("EpgTimeEnd");
            }
        }

        private double epgprogress;
        public double EpgProgress
        {
            get { return this.epgprogress; }
            set
            {
                if (this.epgprogress == value) return;
                this.epgprogress = value;
                base.RaisePropertyChanged("EpgProgress");
            }
        }

        private Double volume = 50;
        public Double Volume
        {
            get { return this.volume; }
            set
            {
                if (this.volume == value) return;
                this.volume = value;
                base.RaisePropertyChanged("Volume");
            }
        }

        private List<Programs> epg;
        public List<Programs> EPG
        {
            get { return this.epg; }
            set
            {
                if (this.epg == value) return;
                this.epg = value;
            }
        }

        private List<Programs> epg3;
        public List<Programs> EPG3
        {
            get { return this.epg3; }
            set
            {
                if (this.epg3 == value) return;
                this.epg3 = value;
            }
        }

        private List<EpgNext> epg2;
        public List<EpgNext> EPG2
        {
            get { return this.epg2; }
            set
            {
                if (this.epg2 == value) return;
                this.epg2 = value;
            }
        }

        private List<EpgNext> epgnext;
        public List<EpgNext> EpgNext
        {
            get { return this.epgnext; }
            set
            {
                if (this.epgnext == value) return;
                this.epgnext = value;
            }
        }

        public double Time
        {
            get { return api.Time; }
        }
        
        public async Task Login(string login, string pass, string code)
        {
            await api.Auth(login, pass, code);
        }

        public async Task GetChannel()
        {
            await api.GetChannelList();
            List<СhannelItem> channels = new List<СhannelItem>();
            foreach (ChannelGroup gitem in api.Account.channel_group)
            {
                for (int i = 0; i < gitem.Channels.Count; i++)
                {
                    СhannelItem ch = new СhannelItem();
                    ch.CategoryName = gitem.name;
                    //
                    string address = "http://sovok.tv" + gitem.Channels[i].icon;
                    ch.Image_Source = new Uri(address);
                    ch.ChannelName = gitem.Channels[i].name;
                    ch.Epg = gitem.Channels[i].epg_progname;
                    ch.ChannelId = gitem.Channels[i].id;

                    try
                    {
                        ch.epg_start = gitem.Channels[i].epg_start;
                        ch.epg_end = gitem.Channels[i].epg_end;
                    }
                    catch (System.Exception ex)
                    {
                    }
                    channels.Add(ch);
                }
            }
            channel_list = channels;
        }

        public async Task GetFavoriteChannels()
        {
            await api.GetFavorites();
        }

        public async Task<string> OpenChannel(string id)
        {
            string url = "";
            try
            {
                string u = await api.GetUrl(id);
                url = u.Split(' ')[0];
            }
            catch (Exception er)
            {
                
            }
            stream_url = url;
            //IsFavoriteChannel(id);
            //IsMD_HD_Sream(id);
            return url;
            //open_epg(id);
        }

        public void AddDelFavoriteChannel(string id)
        {
            api.SetFavoriteChannel(id);
        }

        public async Task GetStreamServer()
        {
            streamers = await api.GetStreamers();
        }

        public async Task SetStreamServer(int streamer)
        {
            await api.Setting(streamer);
        }

        public async Task SetTimeZone(string data)
        {
            string response = await api.Setting(data);    
        }

        public async Task GetEpg()
        {
            DateTime dt= new DateTime();
            dt = DateTime.Now;
            string [] dts = new string[3];
            dts[2] = dt.Year.ToString()[2] + "" + dt.Year.ToString()[3];
            if (dt.Month.ToString().Length == 1)
            {
                dts[1] = "0" + dt.Month.ToString();
            }
            else dts[1] = dt.Month.ToString();
            if (dt.Day.ToString().Length == 1)
            {
                dts[0] = "0" + dt.Day.ToString();
            }
            else dts[0] = dt.Day.ToString();
            epg = await api.Epg(active_channel.id,dts[0]+dts[1]+dts[2]);
        }

        public void UpdateTitle()
        {
           
            if (active_channel!=null)
            {
                tile.UpdateTile(active_channel.icon,active_channel.name,active_channel.epg_progname);
            }
            /*if (UserAccount.favorite_channel.Count!=0)
            {
                while (!String.IsNullOrEmpty(UserAccount.login))
                {
                    foreach (FavoriteChannel fc in UserAccount.favorite_channel)
                    {
                        foreach (СhannelItem item in channel_list)
                        {
                            if (item.ChannelId == fc.id_channel)
                            {
                                tile.UpdateTile("", item.ChannelName, item.Epg);
                                //Sleep(3000);
                                break;
                            }
                        }
                    }
                }
            }*/
        }

        private static void Sleep(int ms)
        {
            new System.Threading.ManualResetEvent(false).WaitOne(ms);
        }
    }
}

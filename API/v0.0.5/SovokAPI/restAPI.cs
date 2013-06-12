using SovokAPI.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace SovokAPI
{
    public class restAPI
    {
        private Account account;
        public Account Account
        {
            get { return this.account; }
            set
            {
                if (this.account == value) return;
                this.account = value;
            }
        }

        private string url;
        public string Url
        {
            get { return this.url; }
            set
            {
                if (this.url == value) return;
                this.url = value;
            }
        }

        private double time;
        public double Time
        {
            get { return this.time; }
            set { 
                if(this.time == value) return;
                this.time = value;
            }
        }

        public async Task Auth(string user, string pass, string protect_code)
        {
            try
            {
                HttpWebRequest request2 = WebRequest.CreateHttp(@"http://api.sovok.tv/v2.0/xml/login?login=" + user + "&pass=" + pass);
                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string endAsync = await streamReader.ReadToEndAsync();
                string str = endAsync;
                login_processing(str);
                account.protect_code = protect_code;
                GetChannelList();
                await GetSettings();
            }
            catch (Exception ex) { throw new System.InvalidOperationException("Authorization error\n"+ex.Message); }
        }

        private void login_processing(string data)
        {
            try
            {
                account = new Account();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNodeList list = doc.GetElementsByTagName("response");
                for (int i = 0; i < list.Count; i++)
                {
                    account.sid = doc.GetElementsByTagName("sid")[i].InnerText;
                    account.sid_name = doc.GetElementsByTagName("sid_name")[i].InnerText;
                    DateTime t = ConvertFromUnixTimestamp(Convert.ToDouble(doc.GetElementsByTagName("servertime")[i].InnerText));
                    DateTime d = new DateTime();
                    d = DateTime.Now;
                    time = Math.Round((d - t).TotalHours);
                }
                XmlNodeList alist = doc.GetElementsByTagName("account");
                for (int i = 0; i < alist.Count; i++)
                {
                    account.login = doc.GetElementsByTagName("login")[i].InnerText;
                    account.balance = doc.GetElementsByTagName("balance")[i].InnerText;
                }
                List<Service> ls = new List<Service>();
                XmlNodeList ilist = doc.GetElementsByTagName("item");
                for (int i = 0; i < ilist.Count; i++)
                {
                    try
                    {
                        Service s = new Service();
                        s.id = doc.GetElementsByTagName("id")[i].InnerText;
                        s.type = doc.GetElementsByTagName("type")[i].InnerText;
                        s.name = doc.GetElementsByTagName("name")[i].InnerText;
                        s.expire = doc.GetElementsByTagName("expire")[i].InnerText;
                        ls.Add(s);
                    }
                    catch (Exception) { }
                }
                account.services = ls;
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Login processing error");
            }
        }

        public async Task<Account> GetAccount()
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get user information");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/account?" + account.sid_name + "=" + account.sid);
                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string endAsync = await streamReader.ReadToEndAsync();
                string str = endAsync;
                login_processing(str);
                return account;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get account information");
            }
        }

        public async void LogOut()
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to log out. You are not logged");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/logout?" + account.sid_name + "=" + account.sid);
                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                using (responseStream)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string endAsync = await streamReader.ReadToEndAsync();
                        string str = endAsync;
                    }
                }
                account = null;
            }
            catch (Exception) { throw new System.InvalidOperationException("Error to logout"); }
        }

        public async Task GetChannelList()
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get channel list. You are not logged");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/channel_list?" + account.sid_name + "=" + account.sid);
                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                using (responseStream)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string endAsync = await streamReader.ReadToEndAsync();
                        string str = endAsync;
                        channellist_processing(str);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException(ex.Message);
            }
        }

        private void channellist_processing(string data)
        {
            List<Channel> cl = new List<Channel>();
            List<ChannelGroup> lg = new List<ChannelGroup>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xe in doc.DocumentElement.SelectNodes("//channels"))
                {
                    string s = "<root>" + xe.ParentNode.GetXml() + "</root>";
                    XmlDocument pdoc = new XmlDocument();
                    pdoc.LoadXml(s);
                    ChannelGroup g = new ChannelGroup();
                    List<Channel> lc = new List<Channel>();
                    g.id = pdoc.DocumentElement.SelectSingleNode("//id").InnerText;
                    g.name = pdoc.DocumentElement.SelectSingleNode("//name").InnerText;
                    g.color = pdoc.DocumentElement.SelectSingleNode("//color").InnerText;
                    g.id = pdoc.DocumentElement.SelectSingleNode("//id").InnerText;
                    XmlDocument cdoc = new XmlDocument();
                    cdoc.LoadXml("<rt>" + pdoc.DocumentElement.SelectSingleNode("//channels").GetXml() + "</rt>"); 
                    foreach (XmlElement lnks in cdoc.DocumentElement.SelectNodes("//item"))
                    {
                        Channel c = new Channel();
                        XmlDocument d = new XmlDocument();
                        d.LoadXml("<r>" + lnks.GetXml() + "</r>");
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//id"))
                        {
                            c.id = lnk.InnerText;
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//name"))
                        {
                            c.name = lnk.InnerText;
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//is_video"))
                        {
                            c.is_video = lnk.InnerText;
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//icon"))
                        {
                            c.icon = lnk.InnerText;
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//epg_progname"))
                        {
                            c.epg_progname = lnk.InnerText;
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//epg_start"))
                        {
                            if (!String.IsNullOrEmpty(lnk.InnerText))
                            {
                                double dt = Convert.ToDouble(lnk.InnerText);
                                c.epg_start = ConvertFromUnixTimestamp(dt);
                            }
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//epg_end"))
                        {
                            if (!String.IsNullOrEmpty(lnk.InnerText))
                            {
                                double dt = Convert.ToDouble(lnk.InnerText);
                                c.epg_end = ConvertFromUnixTimestamp(dt);
                            }
                        }
                        lc.Add(c);
                    }
                    g.Channels = lc;
                    lg.Add(g);
                }
                account.channel_group = lg;
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error to get list of channels");
            }
        }

        public async Task GetFavorites()
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get favotrite channel list. You are not logged");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/favorites?" + account.sid_name + "=" + account.sid);

                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string endAsync = await streamReader.ReadToEndAsync();
                string str = endAsync;
                fav_processing(str);
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException(ex.StackTrace);
            }
        }

        private void fav_processing(string data)
        {
            try
            {
                List<FavoriteChannel> fchannels = new List<FavoriteChannel>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    FavoriteChannel fav = new FavoriteChannel();
                    XmlDocument idoc = new XmlDocument();
                    idoc.LoadXml("<root>" + xitem.GetXml() + "</root>");
                    foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//channel_id"))
                    {
                        fav.id_channel = link.InnerText;
                    }
                    fchannels.Add(fav);
                }
                account.favorite_channel = fchannels;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get favorite channel list");
            }
        }

        public async void SetFavoriteChannel(string id_channel)
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to set favotrite channel. You are not logged");
                try
                {
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/favorites_set?cid=" + id_channel + "&" + account.sid_name + "=" + account.sid);

                    WebResponse responseAsync = await request2.GetResponseAsync();
                    WebResponse webResponse = responseAsync;
                    Stream responseStream = webResponse.GetResponseStream();
                    using (responseStream)
                    {
                        using (StreamReader streamReader = new StreamReader(responseStream))
                        {
                            string endAsync = await streamReader.ReadToEndAsync();
                            string str = endAsync;
                            setfav_processing(str);
                        }
                    }
                }
                catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private void setfav_processing(string data)
        {
            try
            {
                List<FavoriteChannel> lf = new List<FavoriteChannel>();
                lf = account.favorite_channel;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//message"))
                {
                    Boolean isdel = false;
                    string id_channel = "";
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.GetXml() + "</root>");
                    foreach (XmlElement titem in mdoc.DocumentElement.SelectNodes("//text"))
                    {
                        if (titem.InnerText == "Favorite channel was set")
                        {
                            isdel = false;
                        }
                        else isdel = true;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//channel"))
                    {
                        id_channel = link.InnerText;
                    }
                    if (isdel)
                    {
                        foreach (FavoriteChannel far in lf)
                        {
                            if (far.id_channel == id_channel)
                            {
                                lf.Remove(far);
                                break;
                            }
                        }
                    }
                    else
                    {
                        FavoriteChannel fav = new FavoriteChannel();
                        fav.id_channel = id_channel;
                        lf.Add(fav);
                    }
                }
                account.favorite_channel = lf;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to set/delete favorite channel");
            }
        }

        public async Task<string> GetUrl(string id)
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get channel stream. You are not logged");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/get_url?cid=" + id + "&protect_code=" + account.protect_code + "&" + account.sid_name + "=" + account.sid);

                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                using (responseStream)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string endAsync = await streamReader.ReadToEndAsync();
                        string str = endAsync;
                        return url_processing(str);
                    }
                }
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private string url_processing(string data)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNodeList list = doc.GetElementsByTagName("response");
                for (int i = 0; i < list.Count; i++)
                {
                    XmlElement xurl = (XmlElement)doc.GetElementsByTagName("url")[i];
                    url = xurl.InnerText;
                }
                if (url == "protected")
                {
                    throw new System.InvalidOperationException("Protected channel. You need to enter protected code");
                }
                else return url;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get channel url");
            }
        }

        public async Task<List<Streamers>> GetStreamers()
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get stream server list. You are not logged");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/streamers?" + account.sid_name + "=" + account.sid);

                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                using (responseStream)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string endAsync = await streamReader.ReadToEndAsync();
                        string str = endAsync;
                        return streamer_processing(str);
                    }
                }
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<Streamers> streamer_processing(string data)
        {
            try
            {
                List<Streamers> ls = new List<Streamers>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    Streamers stream = new Streamers();
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.GetXml() + "</root>");
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//id"))
                    {
                        stream.id = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//name"))
                    {
                        stream.name = link.InnerText;
                    }
                    ls.Add(stream);
                }
                return ls;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to load streamers");
            }
        }

        public async Task<string> Setting(string timezone)
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to set time zone. You are not logged");
            try
            {
                timezone = timezone.Replace("+", "%2b");
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings_set?timezone=" + timezone + "&" + account.sid_name + "=" + account.sid);

                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                using (responseStream)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string endAsync = await streamReader.ReadToEndAsync();
                        string str = endAsync;
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(str);
                        string ids = "";
                        foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                        {
                            XmlDocument mdoc = new XmlDocument();
                            mdoc.LoadXml("<root>" + xitem.GetXml() + "</root>");
                            foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//value"))
                            {
                                ids = link.InnerText;
                            }
                        }
                        timezone = ids;
                        return ids;
                    }
                }
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to set time zone");
            }
        }

        public async Task Setting(int streamers)
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to set stream server. You are not logged");
                try
                {
                    HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings_set?streamer=" + streamers + "&" + account.sid_name + "=" + account.sid);

                    WebResponse responseAsync = await request2.GetResponseAsync();
                    WebResponse webResponse = responseAsync;
                    Stream responseStream = webResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);

                    string endAsync = await streamReader.ReadToEndAsync();
                    string str = endAsync;

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(str);
                    string ids = "";
                    foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                    {
                        XmlDocument mdoc = new XmlDocument();
                        mdoc.LoadXml("<root>" + xitem.GetXml() + "</root>");
                        foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//value"))
                        {
                            ids = link.InnerText;
                        }
                    }
                    //streamer = Convert.ToInt32(ids);
                    //return Convert.ToInt32(ids);
                }
                catch (Exception)
                {
                    throw new System.InvalidOperationException("Error to set stream server");
                }
        }

        public async Task GetSettings()
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get settings. You are not logged");

            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings?" + account.sid_name + "=" + account.sid);

                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);

                string endAsync = await streamReader.ReadToEndAsync();
                string str = endAsync;
                Setting set = new Class.Setting();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//settings"))
                {
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.GetXml() + "</root>");
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//streamer"))
                    {
                        set.StreamServer = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//timezone"))
                    {
                        set.TimeZone = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//buffer"))
                    {
                        set.Buffer = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//deinterlace"))
                    {
                        set.Deinterlace = link.InnerText;
                    }
                }
                account.setting = set;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get settings");
            }
        }

        public async Task<List<Programs>> Epg(string id, string date)
        {
            if (account.login == null) throw new System.InvalidOperationException("Error to get stream server list. You are not logged");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/epg?cid=" + id + "&day=" + date + "&" + account.sid_name + "=" + account.sid);

                WebResponse responseAsync = await request2.GetResponseAsync();
                WebResponse webResponse = responseAsync;
                Stream responseStream = webResponse.GetResponseStream();
                using (responseStream)
                {
                    using (StreamReader streamReader = new StreamReader(responseStream))
                    {
                        string endAsync = await streamReader.ReadToEndAsync();
                        string str = endAsync;
                        return epg_processing(str);
                    }
                }
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<Programs> epg_processing(string data)
        {
            try
            {
                List<Programs> list_p = new List<Programs>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    Programs p = new Programs();
                    p.progname = xitem.GetElementsByTagName("progname")[0].InnerText;
                    p.t_start = xitem.GetElementsByTagName("t_start")[0].InnerText;
                    p.ut_start = xitem.GetElementsByTagName("ut_start")[0].InnerText;
                    list_p.Add(p);
                }
                return list_p;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get epg");
            }
        }

        public DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}

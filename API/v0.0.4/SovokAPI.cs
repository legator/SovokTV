using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Xml;

namespace SovoktvAPI
{
    public class SovokAPI
    {
        public Account acc = new Account();

        public void auth(string user,string pass, string protect_code)
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/login?login=" + user + "&pass=" + pass);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                debug_login(sr2.ReadToEnd());
                acc.protect_code = protect_code;
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private void debug_login(string data)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNodeList list = doc.GetElementsByTagName("response");
                for (int i = 0; i < list.Count; i++)
                {
                    acc.sid = ((XmlElement)doc.GetElementsByTagName("sid")[i]).InnerText;
                    acc.sid_name = ((XmlElement)doc.GetElementsByTagName("sid_name")[i]).InnerText;
                }
                XmlNodeList alist = doc.GetElementsByTagName("account");
                for (int i = 0; i < alist.Count; i++)
                {
                    acc.login = ((XmlElement)doc.GetElementsByTagName("login")[i]).InnerText;
                    acc.balance = ((XmlElement)doc.GetElementsByTagName("balance")[i]).InnerText;
                }
                List<Service> ls = new List<Service>();
                XmlNodeList ilist = doc.GetElementsByTagName("item");
                for (int i = 0; i < ilist.Count; i++)
                {
                    try
                    {
                        Service s = new Service();
                        s.id = ((XmlElement)doc.GetElementsByTagName("id")[i]).InnerText;
                        s.type = ((XmlElement)doc.GetElementsByTagName("type")[i]).InnerText;
                        s.name = ((XmlElement)doc.GetElementsByTagName("name")[i]).InnerText;
                        s.expire = ((XmlElement)doc.GetElementsByTagName("expire")[i]).InnerText;
                        ls.Add(s);
                    }catch(Exception){}
                }
                acc.services = ls;
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Login error+\n"+ex.StackTrace); 
            }
        }

        public void account()
        {
            if (acc.login != null)
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/account?" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                debug_login(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        public void logout()
        {
            if (acc.login!=null)
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/logout?" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                string s = sr2.ReadToEnd();
                acc.sid = acc.sid_name = acc.login = null;
            }
            catch (Exception) { throw new System.InvalidOperationException("Error to logout"); }
        }

        public void channel_list()
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/channel_list?" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                debug_channel( sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private void debug_channel(string data)
        {
            List<Channel> cl = new List<Channel>();
            List<Group> lg = new List<Group>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xe in doc.DocumentElement.SelectNodes("//channels"))
                {
                    string s = "<root>"+xe.ParentNode.InnerXml+"</root>";
                    XmlDocument pdoc = new XmlDocument();
                    pdoc.LoadXml(s);
                    Group g = new Group();
                    List<Channel> lc = new List<Channel>();
                    g.id = pdoc.DocumentElement.SelectSingleNode("//id").InnerText;
                    g.name = pdoc.DocumentElement.SelectSingleNode("//name").InnerText;
                    g.color = pdoc.DocumentElement.SelectSingleNode("//color").InnerText;
                    g.id = pdoc.DocumentElement.SelectSingleNode("//id").InnerText;
                    XmlDocument cdoc = new XmlDocument();
                    cdoc.LoadXml("<rt>"+pdoc.DocumentElement.SelectSingleNode("//channels").InnerXml+"</rt>");
                    foreach (XmlElement lnks in cdoc.DocumentElement.SelectNodes("//item"))
                    {
                        Channel c = new Channel();
                        XmlDocument d = new XmlDocument();
                        d.LoadXml("<r>"+lnks.InnerXml+"</r>");
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
                            c.epg_start = lnk.InnerText;
                        }
                        foreach (XmlElement lnk in d.DocumentElement.SelectNodes("//epg_end"))
                        {
                            c.epg_end = lnk.InnerText;
                        }
                        lc.Add(c);
                    }
                    g.Channels = lc;
                    lg.Add(g);
                }
                acc.channel_group = lg;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get channel list"); 
            }
        }

        public string get_url(string id)
        {
            if (acc.login == null) return null;
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/get_url?cid=" + id + "&protect_code=" + acc.protect_code + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return geturl(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private string geturl(string data)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                string url = "";
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
                throw new System.InvalidOperationException("Error to get channel url or protected channel\nYou need to enter protected code");
            }
        }

        public List<Programs> epg(string id, string date)
        {
            if (acc.login == null) return null;
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/epg?cid=" + id + "&day=" + date + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return debug_epg(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<Programs> debug_epg(string data)
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
                    p.ut_end = xitem.GetElementsByTagName("ut_end")[0].InnerText;
                    list_p.Add(p);
                }
                return list_p;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to get epg");
            }
        }

        public List<Programs> epg3(string id, string datetime, string period)
        {
            if (acc.login == null) return null;
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/epg?cid=" + id + "&dtime=" + datetime + "&period=" + period + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return debug_epg(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        public List<Epg2> epg2(List<Channel> channel_list)
        {
            string ids = "";
            foreach (Channel item in channel_list)
            {
                ids = ids + "," + item.id;
            }
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/epg_next2?cids=" + ids + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return debug_epg2(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<Epg2> debug_epg2(string data)
        {
            try
            {
                List<Epg2> list_en = new List<Epg2>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    Epg2 en = new Epg2();
                    XmlDocument idoc = new XmlDocument();
                    idoc.LoadXml("<root>"+xitem.InnerXml+"</root>");
                    foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//chid"))
                    {
                        en.id = link.InnerText;
                    }
                    foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//progname"))
                    {
                        en.progname = link.InnerText;
                    }
		            foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//end"))
                    {
                        en.end = link.InnerText;
                    }
		            foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//start"))
                    {
                        en.start = link.InnerText;
                    }
                    list_en.Add(en);
                }
                return list_en;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to load epg2");
            }
        }

        public List<EpgNext> epg_next(string id)
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/epg_next?cid=" + id + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return debug_epg_next(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<EpgNext> debug_epg_next(string data)
        {
            try
            {
                List<EpgNext> list_en = new List<EpgNext>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    EpgNext en = new EpgNext();
                    XmlDocument idoc = new XmlDocument();
                    idoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
                    foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//ts"))
                    {
                        en.ts = link.InnerText;
                    }
                    foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//progname"))
                    {
                        en.progname = link.InnerText;
                    }
                    list_en.Add(en);
                }
                return list_en;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to load epg next");
            }
        }

        public string set_settings(string pin1, string pin2)
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings_set?pin1=" + pin1 + "&pin2=" + pin2 + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return sr2.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException(ex.Message);
            }
        }

        public int set_settings(int streamers)
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings_set?streamer=" + streamers + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sr2.ReadToEnd());
                string ids = "";
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//value"))
                    {
                        ids = link.InnerText;
                    }
                }
                return Convert.ToInt32(ids);
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error to set stream server");
            }
        }

        public void error_log(string data)
        {
            TextWriter tw;
            string fn = "data.txt";
            if (File.Exists(fn))
            {
                tw = new StreamWriter(fn, true);
            }
            else tw = new StreamWriter(fn);
            tw.WriteLine(data);
            tw.Close();
        }

        public string set_settings(string timezone)
        {
            try
            {
                timezone = timezone.Replace("+", "%2b");
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings_set?timezone=" + timezone + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sr2.ReadToEnd());
                string ids = "";
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//value"))
                    {
                        ids = link.InnerText;
                    }
                }
                return ids;
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException(ex.Message);
            }
        }

        public Setting set_settings(string id_channel, string ratio, string buffer, string deinterlace)
        {
            string r = id_channel + ":" + ratio;
            //r = r.Replace(":","%3a");
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings_set?ratio=" + r + "&buffer=" + buffer + "&" + "deinterlace=" + deinterlace + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sr2.ReadToEnd());
                Setting sch = new Setting();
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
                    string name = "";
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//name"))
                    {
                        name = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//value"))
                    {
                        switch (name)
                        {
                            case "ratio":
                                sch.ratio = link.InnerText;
                                break;
                            case "buffer":
                                sch.buffer = link.InnerText;
                                break;
                            case "deinterlace":
                                sch.deinterlace = link.InnerText;
                                break;
                            default:
                                break;
                        }
                    }
                }
                return sch;
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        public Setting setting()
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/settings?" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(sr2.ReadToEnd());
                Setting set = new Setting();
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//settings"))
                {
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//streamer"))
                    {
                        set.streamer = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//timezone"))
                    {
                        set.timezone = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//buffer"))
                    {
                        set.buffer = link.InnerText;
                    }
                    foreach (XmlElement link in mdoc.DocumentElement.SelectNodes("//deinterlace"))
                    {
                        set.deinterlace = link.InnerText;
                    }
                }
                return set;
            }
            catch (Exception ex)
            {
                throw new System.InvalidOperationException("Error to load settings");
            }
        }

        public void favorites()
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/favorites?" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                acc.favorite_channel = debug_fav(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<Favorite> debug_fav(string data)
        {
            try
            {
                List<Favorite> lf = new List<Favorite>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//item"))
                {
                    Favorite fav = new Favorite();
                    XmlDocument idoc = new XmlDocument();
                    idoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
                    foreach (XmlElement link in idoc.DocumentElement.SelectNodes("//channel_id"))
                    {
                        fav.id_channel = link.InnerText;
                    }
                    lf.Add(fav);
                }
                return lf;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("rror to get favorite channel list");
            }
        }

        public void favorites_set(string id_channel)
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/favorites_set?cid=" + id_channel + "&" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                debug_fav_set(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private void debug_fav_set(string data)
        {
            try
            {
                List<Favorite> lf = new List<Favorite>();
                lf = acc.favorite_channel;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (XmlElement xitem in doc.DocumentElement.SelectNodes("//message"))
                {
                    Boolean isdel = false;
                    string id_channel = "";
                    XmlDocument mdoc = new XmlDocument();
                    mdoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
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
                        foreach (Favorite far in lf)
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
                        Favorite fav = new Favorite();
                        fav.id_channel = id_channel;
                        lf.Add(fav);
                    }
                }
                acc.favorite_channel = lf;
            }
            catch (Exception)
            {
                throw new System.InvalidOperationException("Error to set/delete favorite channel"); 
            }
        }

        public List<Streamers> streamers()
        {
            try
            {
                HttpWebRequest request2 = (HttpWebRequest)WebRequest.Create(@"http://api.sovok.tv/v2.0/xml/streamers?" + acc.sid_name + "=" + acc.sid);
                StreamReader sr2 = new StreamReader(request2.GetResponse().GetResponseStream());
                return debug_stream(sr2.ReadToEnd());
            }
            catch (Exception ex) { throw new System.InvalidOperationException(ex.Message); }
        }

        private List<Streamers> debug_stream(string data)
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
                    mdoc.LoadXml("<root>" + xitem.InnerXml + "</root>");
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
    }

    public class Group
    {
        public string id { get; set; }
        public string name { get; set; }
        public string color { get; set; }
        public List<Channel> Channels { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string is_video { get; set; }
        public string is_protected { get; set; }
        public string icon { get; set; }
        public string epg_progname { get; set; }
        public string epg_start { get; set; }
        public string epg_end { get; set; }
    }

    public class Account
    {
        public string sid { get; set; }
        public string sid_name { get; set; }
        public string login { get; set; }
        public string balance { get; set; }
        public List<Service> services { get; set; }
        public string protect_code { get; set; }
        public string timezone { get; set; }
        public string buffer { get; set; }
        public List<Group> channel_group { get; set; }
        public List<Favorite> favorite_channel { get; set; }
    }

    public class Service
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string expire { get; set; }
    }

    public class EpgNext
    {
        public string id { get; internal set; }
        public string ts { get; internal set; }
        public string progname { get; internal set; }
	    public string start { get; internal set; }
	    public string end { get; internal set; }
    }

    public class Epg2:EpgNext
    {
        public string start { get; internal set; }
        public string end { get; internal set; }
    }

    public class Programs
    {
        public string progname { get; internal set; }
        public string ut_start { get; internal set; }
        public string ut_end { get; internal set; }
        public string t_start { get; internal set; }
    }

    public class Favorite
    {
        public string id_channel { get; set; }
    }

    public class Streamers
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Setting
    {
        public string ratio { get; set; }
        public string streamer { get; set; }
        public string timezone { get; set; }
        public string buffer { get; set; }
        public string deinterlace { get; set; }
    }
}

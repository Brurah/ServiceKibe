using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace DashService.Classes
{
    
    public class XmlTempo
    {
        public string title { get; set; }
        public string description { get; set; }
    }
    //[Serializable()]
    //public class itm
    //{
    //    [System.Xml.Serialization.XmlElement("title")]
    //    public string title { get; set; }
    //    [System.Xml.Serialization.XmlElement("description")]
    //    public string description { get; set; }
    //    public string link { get; set; }
    //    public string author { get; set; }
    //    public string pubDate { get; set; }
    //}

}
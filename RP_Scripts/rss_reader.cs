using System.Collections.Generic;
using UnityEngine;
using System.Xml;

namespace Phoenix_Kiber_Ogurchik
{
    public class rss_reader : MonoBehaviour
    {
        XmlTextReader rssReader;
        XmlDocument rssDoc;
        XmlNode nodeRss;
        XmlNode nodeChannel;
        XmlNode nodeItem;
        public channel rowNews;

        public struct channel
        {
            public string title;
            public string link;
            public string description;
            public List<items> item;
        }

        public struct items
        {
            public string title;
            public string category;
            public string creator;
            public string guid;
            public string link;
            public string pubDate;
            public string description;
        }

        public rss_reader(string feedURL)
        {
            rowNews = new channel();
            rowNews.item = new List<items>();
            rssReader = new XmlTextReader(feedURL);
            rssDoc = new XmlDocument();

            rssDoc.Load(rssReader);
            if (rssDoc != null)
            {
                for (int i = 0; i < rssDoc.ChildNodes.Count; i++)
                {
                    if (rssDoc.ChildNodes[i].Name == "rss")
                    {
                        nodeRss = rssDoc.ChildNodes[i];
                    }
                }
            }

            if (nodeRss != null)
            {
                for (int i = 0; i < nodeRss.ChildNodes.Count; i++)
                {
                    if (nodeRss.ChildNodes[i].Name == "channel")
                    {
                        nodeChannel = nodeRss.ChildNodes[i];
                    }
                }
            }

            if (rowNews.title != null)
            {
                rowNews.title = nodeChannel["title"].InnerText;
            }
            if (rowNews.link != null)
            {
                rowNews.link = nodeChannel["link"].InnerText;
            }
            if (rowNews.description != null)
            {
                rowNews.description = nodeChannel["description"].InnerText;
            }

            if (nodeChannel != null)
            {
                for (int i = 0; i < nodeChannel.ChildNodes.Count; i++)
                {
                    if (nodeChannel.ChildNodes[i].Name == "item")
                    {
                        nodeItem = nodeChannel.ChildNodes[i];
                        items itm = new items();
                        if (nodeItem.InnerXml.Contains("title"))
                        {
                            itm.title = nodeItem["title"].InnerText;
                        }
                        if (nodeItem.InnerXml.Contains("link"))
                        {
                            itm.link = nodeItem["link"].InnerText;
                        }
                        if (nodeItem.InnerXml.Contains("category"))
                        {
                            itm.category = nodeItem["category"].InnerText;
                        }
                        if (nodeItem.InnerXml.Contains("dc:creator"))
                        {
                            itm.creator = nodeItem["dc:creator"].InnerText;
                        }
                        if (nodeItem.InnerXml.Contains("guid"))
                        {
                            itm.guid = nodeItem["guid"].InnerText;
                        }
                        if (nodeItem.InnerXml.Contains("pubDate"))
                        {
                            itm.pubDate = nodeItem["pubDate"].InnerText;
                        }
                        if (nodeItem.InnerXml.Contains("description"))
                        {
                            itm.description = nodeItem["description"].InnerText;
                        }
                        rowNews.item.Add(itm);
                    }
                }
            }
        }
    }
}

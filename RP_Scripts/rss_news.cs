using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Phoenix_Kiber_Ogurchik
{
    public class rss_news : MonoBehaviour
    {
        public Transform parent;

        public NewsData newsData;

        rss_reader rdr;
        string text;
        int imgLoadCount;

        public void GetNews()
        {
            rdr = new rss_reader("http://www.rsreu.ru/component/ninjarsssyndicator/?feed_id=1&format=raw");

            foreach (rss_reader.items itm in rdr.rowNews.item)
            {
                GameObject obj = Instantiate(newsData.TextObject, parent);
                text = "<b>" + itm.title + "</b>" + "\n" + itm.description;

                obj.GetComponent<Text>().text = text;
                text = null;
            }

            rdr = null;
        }
    }

    [System.Serializable]
    public class NewsData
    {
        public GameObject TextObject;
        public GameObject ImageObject;
    }
}

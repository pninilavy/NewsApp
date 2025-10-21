using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.ServiceModel.Syndication;
using System.Xml;
using RssNewsApp.Models;

namespace RssNewsApp.Repository
{
    public class RssRepository
    {
        private readonly string _rssUrl = "https://www.globes.co.il/webservice/rss/rssfeeder.asmx/FeederNode?iID=2";
        private readonly ObjectCache _cache = MemoryCache.Default;
        private readonly string _cacheKey = "GlobesRssCache";

        public List<NewsItem> GetNews(bool forceRefresh = false)
        {
            //שליפת רשימת החדשות מהזכרון
            var news = _cache.Get(_cacheKey) as List<NewsItem>;
            if (news != null && !forceRefresh)
                return news;
            //אם לא קיים יצירת החדשות מחדש 
            var items = new List<NewsItem>();

            try
            {

                //XmlReader קריאת החדשות בעזרת  
                using (var reader = XmlReader.Create(_rssUrl))
                {
                    var feed = SyndicationFeed.Load(reader);
                    if (feed != null)
                    {
                        int index = 0;
                        foreach (var entry in feed.Items)
                        {
                            var desc = entry.Summary != null ? entry.Summary.Text : "";
                            //הוספת הכתבה לרשימה עם כל הנתונים
                            items.Add(new NewsItem
                            {
                                Id = (++index).ToString(),
                                Title = entry.Title.Text,
                                Description = desc,
                                Link = entry.Links.FirstOrDefault()?.Uri.ToString(),
                                PublishDate = entry.PublishDate.LocalDateTime
                            });
                        }

                        // מיון לפי תאריך מהחדש לישן
                        items = items.OrderByDescending(x => x.PublishDate).ToList();
                    }
                }

                // שמירה בזכרןו לחצי שעה
                _cache.Set(_cacheKey, items, DateTimeOffset.Now.AddMinutes(30));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return items;
        }
        //id קבלת כתבה לפי מזהה 
        public NewsItem GetNewsById(string id)
        {
            var items = GetNews();
            return items.FirstOrDefault(x => x.Id == id);
        }
    }
}

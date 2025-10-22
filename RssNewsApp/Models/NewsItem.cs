using System;

namespace RssNewsApp.Models
{
    //מחלקה לחדשה בודדת
    public class NewsItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime PublishDate { get; set; }
    }
}

using System;
using System.Web.Script.Serialization;
using System.Web.Services;
using RssNewsApp.Repository;

namespace RssNewsApp.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        private readonly RssRepository _repo = new RssRepository();
        //טעינת החדשות בזמן טעינת העמוד
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadNews();
        }
        //"טעינת החדשות בזמן לחיצה על כפתור "רענן חדשות 
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadNews(true);
        }
        //Repeater טעינת החדשות והצגתם ע''י ה 
        private void LoadNews(bool refresh = false)
        {
            var news = _repo.GetNews(refresh);
            rptNews.DataSource = news;
            rptNews.DataBind();
        }
        //קבלת הכתבה
        [WebMethod]
        public static string GetArticle(string id)
        {
            var repo = new RssRepository();
            var item = repo.GetNewsById(id);
            if (item == null)
                return null;

            var json = new JavaScriptSerializer().Serialize(item);
            return json;
        }
    }
}

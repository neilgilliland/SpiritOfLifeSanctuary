using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;

namespace SpiritOfLifeSanctuary.Controllers
{
    public class FacebookController : Controller
    {
        //public ActionResult Index()
        //{
            //string appId = "222952734385999";
            //string appSecret = "6496cb452f350412e7ae73921ca4fc1b";
            //return View();
        //}
    
        static string[] suffix = new string[] {"st","nd","rd","th","th","th","th","th","th","th","th","th","th","th","th","th","th","th","th","th","st","nd","rd","th","th","th","th","th","th","th","st"};
          
        public ActionResult Events()
        {
            string url = "https://graph.facebook.com/218470858212744/events?access_token=AAADKxjqI508BABCYtUZAzPCJHhRUIONZCSEsZBv63iGLe1v2T75HZCyeBTYz2gF0epXASjjhmFSrHowtQslLf0ZBNqxomxcIZD";

            dynamic json = _getJson(url);

            StringBuilder html = new StringBuilder();

            html.Append( "<ul class='event-list'>" );

            foreach( var e in json.data )
            {
                html.Append( "<li>" );
                html.Append( string.Format("<a href='/Navigate/Event/{0}'>",e.id ) );
                html.Append( "<p class='event-title'>");
                html.Append( e.name );
                html.Append( "</p>");
                html.Append( "<p class='event-date'>");
                html.Append( NiceDate((DateTime)e.start_time) );
                html.Append( "</p>");
                html.Append( "</a>");
                html.Append( "</li>" );
            }
      
            html.Append( "</ul>" );

            return new ContentResult { Content = html.ToString()};
        }

        public static dynamic Event( string id )
        {
            string url = "https://graph.facebook.com/"+id+"/?access_token=AAADKxjqI508BABCYtUZAzPCJHhRUIONZCSEsZBv63iGLe1v2T75HZCyeBTYz2gF0epXASjjhmFSrHowtQslLf0ZBNqxomxcIZD";
            return _getJson(url);            
        }

        private static dynamic _getJson( string url )
        {
            var wc = new WebClient();
            
            Stream data = wc.OpenRead( url );
            StreamReader reader = new StreamReader(data);
            string json = reader.ReadToEnd();
            
            dynamic result = JsonConvert.DeserializeObject(json);

            return result;
        }

        public static string NiceDate( DateTime dt )
        {
            return string.Format( "{0}{1} {2}", dt.ToString( "dddd d"), suffix[dt.Day],  dt.ToString( "MMMM") );
        }

    }
}

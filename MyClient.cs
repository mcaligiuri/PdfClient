using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PdfClient
{
   [ComVisible(true)]
    public interface IMyClient
    {
        bool SendGridViste(string nome, string cartella, string inizio, string fine, string stato, string sito, string voto, string commento,string lang,string pdf);
        bool SendGridFuture(string nome, string stato, string priorita, string commento, string lang, string pdf);
        bool DeleteMyFile(string nome);
    }
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class MyClient:IMyClient
    {
        public bool SendGridViste(string nome, string cartella, string inizio, string fine, string stato, string sito, string voto, string commento, string lang, string pdf)
        {
            int lan;
            if (lang == "ITALIANO")
                lan = 1;
            else
                lan = 2;
            string url = "http://localhost/generaReportPDF/index.php?tab=1&lang=" + lan+"&nome="+pdf;
            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection()
                {
                    { "nome", nome },
                    { "cartella", cartella },
                    { "inizio", inizio },
                    { "fine", fine },
                    { "stato", stato },
                    { "sito", sito },
                    { "voto", voto },
                    { "commento", commento }
                };
                string pagesource = Encoding.UTF8.GetString(client.UploadValues(url, data));
                if (pagesource == null)
                    return false;
            }
            
            return true;
        }
        public bool SendGridFuture(string nome, string stato, string priorita, string commento, string lang, string pdf)
        {
            int lan;
            if (lang == "ITALIANO")
                lan = 1;
            else
                lan = 2;
            
            string url = "http://localhost/generaReportPDF/index.php?tab=2&lang=" + lan + "&nome=" + pdf;
            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection()
                {
                    { "nome", nome },
                    { "priorita", priorita },
                    { "stato", stato },
                    { "commento", commento }
                };
                string pagesource = Encoding.UTF8.GetString(client.UploadValues(url, data));
                if (pagesource == null)
                    return false;
            }
            return true;
        }
        public bool DeleteMyFile(string nome)
        {
            string url = "http://localhost/generaReportPDF/generaReportPDF/core.php";
            using (WebClient client = new WebClient())
            {
                NameValueCollection data = new NameValueCollection()
                {
                    { "file", nome }
                };
                string pagesource = Encoding.UTF8.GetString(client.UploadValues(url, data));
                if (pagesource == null)
                    return false;
            }
            return true;
        }
    }
}

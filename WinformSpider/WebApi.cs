using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinformSpider
{
    public class WebApi
    { 
        private MyWebClient _client;
        private MyWebClient client
        {
            get
            {
                if (_client == null)
                {
                    _client = new MyWebClient();
                    string userAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
                    _client.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
                }

                return _client;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryString">arg1=a&arg2=b</param>
        /// <returns></returns>
        public string Post(string url , string queryString)
        {
            string postString = queryString;//这里即为传递的参数，可以用工具抓包分析，也可以自己分析，主要是form里面每一个name都要加进来  
            byte[] postData = Encoding.UTF8.GetBytes(postString);//编码，尤其是汉字，事先要看下抓取网页的编码方式  
            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            byte[] responseData = client.UploadData(url, "POST", postData);//得到返回字符流  
            string srcString = Encoding.UTF8.GetString(responseData);//解码  
            return srcString;
        }

        public string Get(string url)
        {
            string html = client.DownloadString(url);
            return html;
        }


    }

    public class MyWebClient : WebClient
    { 
        public static CookieContainer Cookies = new CookieContainer();

        public int TimeoutSeconds { get; set; } = 30;

        public WebRequest Request { get; set; }

        public int RequestConentLength;

        protected override WebRequest GetWebRequest(Uri address)
        { 
            HttpWebRequest request = base.GetWebRequest(address) as HttpWebRequest;
            
            if (request != null)
            {
                request.Method = "Post";
                request.CookieContainer = Cookies;
                request.Timeout = 1000 * TimeoutSeconds;
                request.ContentLength = RequestConentLength;
            }
 
            Request = request;
            return request;
        }

        public WebResponse Response { get; set; }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            this.Response = base.GetWebResponse(request);
            return this.Response;
        }
    }
}

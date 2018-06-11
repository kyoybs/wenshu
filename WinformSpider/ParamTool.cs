using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WinformSpider
{
    public class ParamTool
    {
        private MyWebClient GetClient()
        {
            return new MyWebClient();
        }
             
        public string GetGuid()
        {
            var guid = GuidSub() + GuidSub() + "-" + GuidSub() + "-" + GuidSub() + GuidSub() + "-" + GuidSub() + GuidSub() + GuidSub(); //CreateGuid();
            return guid;
        }

        private string GuidSub()
        {
            //9eddfc9f-5eb8-87adef05-894e40ff4a9b
            return (Convert.ToInt32((1 + Random()) * 0x10000) | 0).ToString("X").Substring(1);
        }

        private double Random()
        {
            return new Random().NextDouble();
        }

        private string userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/22.0.1207.1 Safari/537.1";
        //:Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36


        public string GetNumber(string guid)
        {
            string codeUrl = "http://wenshu.court.gov.cn/ValiCode/GetCode"; //http://wenshu.court.gov.cn
            var web = this.GetClient();
            web.Headers.Clear();
            web.Headers.Add(System.Net.HttpRequestHeader.Host, "wenshu.court.gov.cn");
            web.Headers.Add("Origin", "http://wenshu.court.gov.cn");
            web.Headers.Add(System.Net.HttpRequestHeader.Referer, "http://wenshu.court.gov.cn");
            web.Headers.Add("X-Requested-With", "XMLHttpRequest");
            web.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            web.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            web.Headers.Add(HttpRequestHeader.Accept, "*/*");
            web.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            web.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9");
            //web.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            string param = $"guid={guid}";
            web.RequestConentLength = param.Length;
            string num = web.UploadString(codeUrl, "POST", param);//得到返回字符流  
            return num;
        }

        private Dictionary<string, string> _ShortFilters;

        private Dictionary<string, string> ShortFilters
        {
            get
            {
                if (_ShortFilters == null)
                {
                    _ShortFilters = new Dictionary<string, string> {
                        { "全文检索", "QWJS" }, { "案件名称", "AJMC" },
                        { "法院名称", "FYMC"}, { "案件类型", "AJLX"},
                        { "文书类型", "WSLX"},  { "审判人员", "SPRY"},
                        { "律所", "LS"}, { "案由", "AY"},
                        { "案号", "AH"}, { "法院层级", "FYCJ"},
                        { "审判程序", "SPCX"},   { "当事人", "DSR"},
                        { "律师", "LAWYER"},   { "法院地域", "FYDY"},
                        { "上传日期", "SCRQ"}}; 
                }
                return _ShortFilters; 
            }
        }

        string Referer1;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="number"></param>
        /// <param name="wenshuType">判决书 </param>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetVjkl5(string guid , string number , DateTime date )
        {
            //http://wenshu.court.gov.cn/list/list/?sorttype=1&number=WTVMELMJ&guid=e7a193ca-4e19-27028efe-ff78ab495436&conditions=searchWord+1+AJLX++%E6%A1%88%E4%BB%B6%E7%B1%BB%E5%9E%8B:%E5%88%91%E4%BA%8B%E6%A1%88%E4%BB%B6&conditions=searchWord+1+WSLX++%E6%96%87%E4%B9%A6%E7%B1%BB%E5%9E%8B:%E5%88%A4%E5%86%B3%E4%B9%A6&conditions=searchWord++CPRQ++%E8%A3%81%E5%88%A4%E6%97%A5%E6%9C%9F:2018-05-31%20TO%202018-05-31
            //http://wenshu.court.gov.cn/list/list/?sorttype=1&number=WTVMELMJ&guid=e7a193ca-4e19-27028efe-ff78ab495436&conditions=searchWord 1 AJLX  案件类型:刑事案件&conditions=searchWord 1 WSLX  文书类型:判决书&conditions=searchWord  CPRQ  裁判日期:2018-05-31 TO 2018-05-31
            string datestr = date.ToString("yyyy-MM-dd");
            string url = "http://wenshu.court.gov.cn/list/list/?sorttype=1";
            url += $"&number={number}&guid=e7a193ca-4e19-27028efe-ff78ab495436&conditions=searchWord 1 AJLX  案件类型:刑事案件&conditions=searchWord 1 WSLX  ";
            string wenshuType = "判决书";
            url += $"文书类型:{wenshuType}&conditions=searchWord  CPRQ  裁判日期:{datestr} TO {datestr}";
            Referer1 = url;

            var web = this.GetClient();
            web.Headers.Clear();
            web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            web.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            web.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            web.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            web.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            web.Headers.Add(HttpRequestHeader.Host, "wenshu.court.gov.cn");
        
            //web.Headers.Add("Proxy-Connection" , "keep-alive");
            web.Headers.Add("Upgrade-Insecure-Requests", "keep-alive");
            web.RequestConentLength =0;
            web.DownloadString(url);
            var cookies =MyWebClient.Cookies.GetCookies(web.Request.RequestUri);
            var ck = cookies["vjkl5"];
            return ck?.Value;
        }

        public string GetVl5x(string vjkl5)
        {
            string root = Directory.GetCurrentDirectory();
            StringBuilder js = new StringBuilder();
            js.AppendLine(File.ReadAllText(Path.Combine(root,"sha1.js")));
            js.AppendLine(File.ReadAllText(Path.Combine(root, "md5.js")));
            js.AppendLine(File.ReadAllText(Path.Combine(root, "base64.js")));
            js.AppendLine(File.ReadAllText(Path.Combine(root, "vl5x.js")));
            string val = CallJs($"vl5x('{vjkl5}')", js.ToString());
            return val;
        }
 
        public string CallJs(string jsCall , string jsFunctions)
        {
            Type obj = Type.GetTypeFromProgID("ScriptControl");
            if (obj == null) return null;
            object ScriptControl = Activator.CreateInstance(obj);
            obj.InvokeMember("Language", BindingFlags.SetProperty, null, ScriptControl, new object[] { "JavaScript" });
            //string js = "function time(a, b, msg){ var sum = a + b; return new Date().getTime() + ': ' + msg + ' = ' + sum }";
            obj.InvokeMember("AddCode", BindingFlags.InvokeMethod, null, ScriptControl, new object[] { jsFunctions });

            //return obj.InvokeMember("Eval", BindingFlags.InvokeMethod, null, ScriptControl, new object[] { "time(3, 5, '3 + 5')" }).ToString();
            return obj.InvokeMember("Eval", BindingFlags.InvokeMethod, null, ScriptControl, new object[] { jsCall }).ToString();
        }

        public string GetList(DateTime date , int pageIndex, int pageSize, string orderDirection="asc", string orderBy= "审判程序")
        {
            string datestr = date.ToString("yyyy-MM-dd");
            string guid = GetGuid();
            string number = GetNumber(guid);
            string vjkl5 = GetVjkl5(guid, number, date);
            string vl5x = GetVl5x(vjkl5);

            string url = "http://wenshu.court.gov.cn/List/ListContent";

            var web = this.GetClient();
            web.Headers.Clear();
            web.Headers.Add(HttpRequestHeader.Accept, "*/*");
            web.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            web.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.9");
            //web.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            web.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            web.Headers.Add(HttpRequestHeader.Host, "wenshu.court.gov.cn");
            web.Headers.Add("Origin", "http://wenshu.court.gov.cn");
            //web.Headers.Add("Proxy-Connection", "keep-alive"); 
            web.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            web.Headers.Add("X-Requested-With", "XMLHttpRequest");
            web.Headers.Add(HttpRequestHeader.Referer, WebUtility.UrlEncode( Referer1));
             
            //web.
            string param = $"案件类型:刑事案件,文书类型:判决书,裁判日期:{datestr} TO {datestr}";
            string postData = $"Param={param}&Index={pageIndex}&Page={pageSize}&Order=法院层级&Direction=asc&vl5x={vl5x}&number={number}&guid={guid}";
  
            string html = web.Post(url, postData, false );
            return html;
        }

    }
}

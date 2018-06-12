using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
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
            web.Proxy = _proxy;
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

            web.Proxy = _proxy;
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

            web.Proxy = _proxy;


            string html = web.Post(url, postData, false );
            return html;
        }

        public void Download(string caseId, string caseName, DateTime date)
        {
            // conditions : WebUtility.UrlEncode 案件类型为刑事案件且文书类型为判决书且裁判日期为2018-06-01 TO 2018-06-01
            // docIds : e5eda16f-bf8e-4b71-8968-a8f200d7a792|汪某某、廖某某故意伤害罪二审刑事判决书|2018-06-01
            // keyCode : ""

            string coditions = WebUtility.UrlEncode("案件类型为刑事案件且文书类型为判决书且裁判日期为2018-06-01 TO 2018-06-01");
            string docIds = $"{caseId}|{caseName}|{date.ToString("yyyy-MM-dd")}";
            string keyCode = "";
            //Referer: http://wenshu.court.gov.cn/list/list/?sorttype=1&number=KHC8W9HV&guid=542fbd32-2a4b-59e04802-01dbe7a80284&conditions=searchWord+1+AJLX++%E6%A1%88%E4%BB%B6%E7%B1%BB%E5%9E%8B:%E5%88%91%E4%BA%8B%E6%A1%88%E4%BB%B6&conditions=searchWord+1+WSLX++%E6%96%87%E4%B9%A6%E7%B1%BB%E5%9E%8B:%E5%88%A4%E5%86%B3%E4%B9%A6&conditions=searchWord++CPRQ++%E8%A3%81%E5%88%A4%E6%97%A5%E6%9C%9F:2018-06-01%20TO%202018-06-01

            var url = "/CreateContentJS/CreateListDocZip.aspx?action=1";
             
            var web = this.GetClient();
            web.Headers.Clear();
            //web.Headers.Add("Origin", "http://wenshu.court.gov.cn"); 
            //web.Headers.Add("X-Requested-With", "XMLHttpRequest");
            web.Headers.Add(HttpRequestHeader.UserAgent, userAgent);
            web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");//采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可  
            web.Headers.Add(HttpRequestHeader.Accept, "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
            web.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            web.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8,zh-TW;q=0.7,zh-HK;q=0.5,en-US;q=0.3,en;q=0.2");
            web.Headers.Add(HttpRequestHeader.Referer, WebUtility.UrlEncode(Referer1));
            web.Headers.Add(HttpRequestHeader.Host, "wenshu.court.gov.cn");

            //web.Headers.Add(HttpRequestHeader.Connection, "keep-alive");
            string param = $"conditions={coditions}&docIds={docIds}&keyCode={keyCode}";
            web.RequestConentLength = param.Length;
            web.Proxy = _proxy;
            var buffer = web.UploadData(url, "POST", Encoding.UTF8.GetBytes( param));//得到返回字符流  
            string fileName = WebUtility.UrlDecode(web.Response.Headers["Content-Disposition"]).Replace("attachment;filename=","");
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), date.ToString("yyyy-MM-dd"), fileName);
            File.WriteAllBytes(filePath, buffer);

            string sql = "UPDATE DayLog SET DownloadCount = DownloadCount+1 WHERE CaseDate=@CaseDate ";
            CaseDB.Create().Execute(sql, new { CaseDate = date });

        }

        public void ParseList(string json, DateTime date)
        {
            json = json.Trim('"').Replace("\\\"", "\"");
            var dics = JsonConvert.DeserializeObject<JObject[]>(json);
 
            if (dics.Length > 0)
            {
                this.DayCount = Convert.ToInt32(dics[0]["Count"]);
                CaseDB.Create().InsertDayLog(date, this.DayCount);
            }

            Form1.MainForm.ShowState($"本页共有{DayCount}个文档， 即将开始下载...");

            Thread.Sleep(10000);

            for (int i = 1; i < dics.Length; i++)
            {
                Form1.MainForm.ShowState($"本页共有{DayCount}个文档， 即将开始下载 {1}...");
                Case caseInfo = new Case();
                caseInfo.CaseId = dics[i]["文书ID"].ToString();
                caseInfo.UnpubReason = dics[i]["不公开理由"].ToString();
                caseInfo.CaseType = dics[i]["案件类型"].ToString();
                caseInfo.CaseDate = Convert.ToDateTime( dics[i]["裁判日期"].ToString());
                caseInfo.CaseName = dics[i]["案件名称"].ToString();
                caseInfo.CaseRule = dics[i]["审判程序"].ToString();
                caseInfo.CaseCode = dics[i]["案号"].ToString();
                caseInfo.CourtName = dics[i]["法院名称"].ToString();

                var db = CaseDB.Create();
                if(db.GetCase(caseInfo.CaseId) == null)
                    db.InsertCase(caseInfo);

                this.Download(caseInfo.CaseId, caseInfo.CaseName, date);

               
                Thread.Sleep(10000);

            }
 
        }

        private WebProxy _proxy;
        public void SetProxy(string host, int port)
        {
            _proxy = new WebProxy(host, port);
        }

        public int DayCount { get; set; }

        public DateTime Date { get; set; }

        public int PageCount
        {
            get
            {
                return (DayCount - 1) / 20 + 1;
            }
        }

        public int PageIndex { get; set; }

        public int DocIndex { get; set; }

        

    }
}

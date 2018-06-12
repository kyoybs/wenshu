using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformSpider
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MainForm = this;
        }

        public static Form1 MainForm { get; set; }

        protected void RunOnUI(Action action)
        {
            Invoke(action);
        }

        protected void RunOnBg(Action action)
        {
            Task.Factory.StartNew(action);
        }

        private void btnGet_Click(object sender, EventArgs e)
        {

        }

        public void ShowState(string stateName)
        {
            RunOnUI(() =>{
                lblState.Text = stateName;
            }) ;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            ParamTool pt = new ParamTool();
            //pt.SetProxy("60.177.224.188", 18118);
            this.ShowState("正在下载列表...");
            DateTime date = new DateTime(2018, 6, 1);
            string json = pt.GetList(date, 1, 20);
            this.ShowState("正在解析列表...");
            pt.ParseList(json, date);
        }


        private void btnParseJson_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "list.txt");
            var json = File.ReadAllText(path, Encoding.UTF8);

            var dics = JsonConvert.DeserializeObject<JObject[]>(json);
            int count = 0;
            if (dics.Length > 0)
            {
                count = Convert.ToInt32(dics[0]["Count"]);
            }
            if (dics.Length > 1)
            {
                string str = dics[1]["文书ID"].ToString();
                //不公开理由
                //案件类型
                //裁判日期
                //案件名称
                //文书ID
                //审判程序
                //案号
                //法院名称 
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        }

        

        private void btnGet_Click(object sender, EventArgs e)
        {
            
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            ParamTool pt = new ParamTool();
            var html = pt.GetList(new DateTime(2018, 6, 1), 1, 20);
            txtResult.Text = html;
        }
    }
}

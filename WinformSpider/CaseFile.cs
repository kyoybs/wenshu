using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformSpider
{
    [Table("CaseFile")]
    public class CaseFile
    {
        /// <summary>
        /// 文书ID
        /// </summary>
        public string CaseId { get; set;}

        /// <summary>
        /// 不公开理由
        /// </summary>
        public string UnpubReason { get; set; }

        public string CaseType { get; set; }

        public DateTime CaseDate { get; set; }

        public string CaseName { get; set; }

        /// <summary>
        /// 审判程序
        /// </summary>
        public string CaseRule { get; set; }

        /// <summary>
        /// 案号
        /// </summary>
        public string CaseCode { get; set; }

        public string CourtName { get; set; }
    }
}

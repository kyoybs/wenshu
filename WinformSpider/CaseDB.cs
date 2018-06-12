using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace WinformSpider
{
    class CaseDB
    {
        public static CaseDB Create()
        {
            CaseDB db = new CaseDB();
            db.ConnectString = ConfigurationManager.ConnectionStrings["main"].ConnectionString; 
            return db;
        }

        private string ConnectString;

        public void InsertCase(Case caseInfo)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                conn.Insert<Case>(caseInfo);
            }
        }

        public void Execute(string sql, object param)
        {
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                conn.Execute(sql, param);
            }
        }

        public void InsertDayLog(DateTime date, int dayCount)
        {
            string strdate = date.ToString("yyyy-MM-dd");
            string sql = $@"IF NOT EXISTS(SELECT * FROM [dbo].[DayLog] WHERE [CaseDate]='{strdate}')
INSERT INTO [dbo].[DayLog] ([CaseDate], TotalCount, [DownloadCount])
VALUES('{strdate}', {dayCount}, 0)";
            CaseDB.Create().Execute(sql, null);
        }


        public Case GetCase(string caseId)
        {
            string sql = @"SELECT TOP 1 * FROM Case WHERE CaseID=@caseId";
            using (SqlConnection conn = new SqlConnection(ConnectString))
            {
                return conn.QueryFirstOrDefault<Case>(sql, new { caseId });
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;

namespace ShipIt.Repositories
{
    public class ConnectionHelper
    {
        public static string GetConnectionString()
        {
            var dbname = ConfigurationManager.AppSettings["RDS_DB_NAME"];

            if (dbname == null)
            {
                return ConfigurationManager.ConnectionStrings["MyPostgres"].ConnectionString;
            };

            var username = ConfigurationManager.AppSettings["RDS_USERNAME"];
            var password = ConfigurationManager.AppSettings["RDS_PASSWORD"];
            var hostname = ConfigurationManager.AppSettings["RDS_HOSTNAME"];
            var port = ConfigurationManager.AppSettings["RDS_PORT"];

            return "Server=" + hostname + ";Port=" + port + ";Database=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}

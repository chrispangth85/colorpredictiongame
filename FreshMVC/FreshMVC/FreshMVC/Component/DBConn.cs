using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace FreshMVC.Component
{
    public class DBConn
    {
        private static string localConnectionString = "Data Source=localhost; Initial Catalog=ColorPrediction; uid=sa; password=reallyStrongPwd123; Connect Timeout=30000";       

        private readonly static string ConfigurationFilePath = @"C:\Config\ConfigurationColorPrediction.xml";

        public static string connString
        {
            get
            {                
                if (File.Exists(ConfigurationFilePath))
                {
                    XmlDocument configDoc = new XmlDocument();
                    configDoc.Load(ConfigurationFilePath);

                    XmlNode databaseNode = configDoc.SelectSingleNode("/Configurations/DatabaseList/Database[Name='ColorPrediction']");
                    //XmlNode databaseNode = configDoc.SelectSingleNode("/Configurations/DatabaseList/Database[Name='BVAShare']");
                    if (databaseNode != null)
                    {
                        return databaseNode.SelectSingleNode("ConnectionString").InnerText;
                    }                    
                }

                return localConnectionString;

            }
        }


        public static SqlConnection GetConnection()
        {
            if (File.Exists(ConfigurationFilePath))
            {
                XmlDocument configDoc = new XmlDocument();
                configDoc.Load(ConfigurationFilePath);

                XmlNode databaseNode = configDoc.SelectSingleNode("/Configurations/DatabaseList/Database[Name='ColorPrediction']");                
                if (databaseNode != null)
                {
                    localConnectionString = databaseNode.SelectSingleNode("ConnectionString").InnerText;
                }

                return new SqlConnection(localConnectionString);
            }

            return new SqlConnection(localConnectionString);
        }
    }
}

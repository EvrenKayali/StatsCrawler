using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SoccerStatistics
{
    public static class XmlHelper
    {
        static XmlDocument xml;
        public static void GetNodes()
        {
            xml = new XmlDocument();
            xml.Load(AppDomain.CurrentDomain.BaseDirectory + "\\teams.xml");
            XmlNodeList xnLinst = xml.SelectNodes("/ArrayOfTeam/Team");

            foreach (XmlNode xmlNode in xnLinst)
            {
                Console.WriteLine(xmlNode["Competition"].InnerText);
            }
        }
    }
}

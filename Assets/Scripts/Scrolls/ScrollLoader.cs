using UnityEngine;
using System.Xml;

namespace Scrolls
{
    public static class ScrollLoader
    {
        #region Fields

        private static XmlDocument _scrollDataXml;

        public const string _path = "XML/Scrolls";

        #endregion

        #region Loading

        public static void LoadXML()
        {
            if (_scrollDataXml != null)
            {
                return;
            }

            TextAsset xmlTextAsset = Resources.Load<TextAsset>(_path);
            _scrollDataXml = new XmlDocument();
            _scrollDataXml.LoadXml(xmlTextAsset.text);
        }

        #endregion

        #region Find Scrolls

        public static XmlNodeList FindAllScrolls()
        {
            LoadXML();

            Debug.Log("Finding all scrolls");
            XmlNodeList scrolls = _scrollDataXml.SelectNodes("/ScrollCollection/Scrolls/Scroll");

            return scrolls;
        }

        public static XmlNode FindScrollWithName(string scrollName)
        {
            LoadXML();

            XmlNode curNode = _scrollDataXml.SelectSingleNode("/ScrollCollection/Scrolls/Scroll[@Name='" + scrollName + "']");

            return curNode;
        }

        #endregion

        #region Get Nodes

        public static string GetDisplayName(string scrollName)
        {
            LoadXML();

            XmlNode scroll = FindScrollWithName(scrollName);
            string displayName = scroll["DisplayName"].InnerText;

            return displayName;
        }

        public static string GetDescription(string scrollName)
        {
            LoadXML();

            XmlNode scroll = FindScrollWithName(scrollName);
            string description = scroll["Description"].InnerText;

            return description;
        }

        public static int GetCost(string scrollName)
        {
            LoadXML();
            
            XmlNode scroll = FindScrollWithName(scrollName);
            int.TryParse(scroll["Cost"].InnerText, out int cost);

            return cost;
        }

        #endregion
    }
}
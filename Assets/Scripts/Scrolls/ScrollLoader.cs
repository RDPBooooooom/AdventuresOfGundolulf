using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Scrolls
{
    public static class ScrollLoader
    {
        public const string path = "XML/Scrolls";

        private static XmlDocument scrollDataXml;

        public static void LoadXML()
        {
            if (scrollDataXml != null)
            {
                return;
            }

            TextAsset xmlTextAsset = Resources.Load<TextAsset>(path);
            scrollDataXml = new XmlDocument();
            scrollDataXml.LoadXml(xmlTextAsset.text);
        }

        public static XmlNodeList FindAllScrolls()
        {
            LoadXML();

            Debug.Log("Finding all scrolls");
            XmlNodeList scrolls = scrollDataXml.SelectNodes("/ScrollCollection/Scrolls/Scroll");

            return scrolls;
        }

        public static XmlNode FindScrollWithName(string scrollName)
        {
            LoadXML();

            XmlNode curNode = scrollDataXml.SelectSingleNode("/ScrollCollection/Scrolls/Scroll[@Name='" + scrollName + "']");

            return curNode;
        }

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
    }
}
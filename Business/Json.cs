namespace Business
{
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    using Newtonsoft.Json;

    public static class Json
    {
        /// <summary>
        /// Convert xml to json.
        /// </summary>
        public static string SerializeXmlNode(string xml)
        {
            XElement doc = XElement.Parse(xml);
            doc.Descendants().Where(e => string.IsNullOrEmpty(e.Value)).Remove();
            return JsonConvert.SerializeObject(doc);
        }
    }
}

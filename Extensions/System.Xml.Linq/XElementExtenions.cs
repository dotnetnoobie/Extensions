using System.Linq;

namespace System.Xml.Linq
{
    public static class XElementExtenions
    {
        public static string GetLocalName(this XElement element, string name)
        {
            return element?.Elements()?.FirstOrDefault(i => i.Name.LocalName == name)?.Value;
        }

        public static string GetLocalName(this XElement element, string name, string attribute)
        {
            return element?.Elements()?.FirstOrDefault(i => i.Name.LocalName == name)?.Attribute(attribute)?.Value;
        }

        public static string GetAttribute(this XElement element, string attribute)
        {
            return element.Attribute(attribute)?.Value;
        }
    }
}
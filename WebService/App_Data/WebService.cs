using System.Threading;
using System.Web.Services;
using System.Xml;
using Business;

using WebServices.Extension;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://gguidici.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{
    [WebMethod, TraceExtension]
    public int Fibonacci(int n)
    {
        Thread.Sleep(2000);
        return Business.Fibonacci.GetFibNumber(n);
    }

    [WebMethod, TraceExtensionAttribute]
    public string XmlToJson(string xml)
    {
        try
        {
            return Json.SerializeXmlNode(xml);
        }
        catch (XmlException)
        {
            return "Bad Xml format";
        }
    }
}

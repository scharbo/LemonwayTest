// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TraceExtension .cs" company="">
//   http://devblog.avesse.net/2012/07/07/soap-extension-log4net-traceasmx-web-service/
// </copyright>
// <summary>
//   Defines the TraceExtension type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebServices.Extension
{
    using System;
    using System.IO;
    using System.Text;
    using System.Web.Services.Protocols;
    using System.Xml.Linq;

    using log4net;

    // Define a SOAP Extension that traces the SOAP request and SOAP
    // response for the XML Web service method the SOAP extension is
    // applied to.
    public class TraceExtension : SoapExtension
    {
        private static readonly ILog logger = LogManager.GetLogger("My Service");

        private Stream oldStream;

        private Stream newStream;

        // Save the Stream representing the SOAP request or SOAP response into
        // a local memory buffer.
        public override Stream ChainStream(Stream stream)
        {
            oldStream = stream;
            newStream = new MemoryStream();
            return newStream;
        }

        public override object GetInitializer(LogicalMethodInfo methodInfo, SoapExtensionAttribute attribute)
        {
            return methodInfo.Name;
        }

        public override object GetInitializer(Type WebServiceType)
        {
            return WebServiceType.Name;
        }

        public override void Initialize(object initializer)
        {
        }

        public override void ProcessMessage(SoapMessage message)
        {
            switch (message.Stage)
            {
                case SoapMessageStage.BeforeSerialize:
                    break;
                case SoapMessageStage.AfterSerialize:
                    WriteOutput(message);
                    break;
                case SoapMessageStage.BeforeDeserialize:
                    WriteInput(message);
                    break;
                case SoapMessageStage.AfterDeserialize:
                    break;
            }
        }

        public void WriteOutput(SoapMessage message)
        {
            var soapString = (message is SoapServerMessage) ? "SoapResponse" : "SoapRequest";
            var header = soapString + ": " + message.MethodInfo.Name + "\n";

            if (message.Exception != null)
            {
                Log(header, newStream, message.Exception);
                return;
            }

            Log(header, newStream);

            Copy(newStream, oldStream);
        }

        public void WriteInput(SoapMessage message)
        {
            Copy(oldStream, newStream);

            string soapString = (message is SoapServerMessage) ? "SoapRequest" : "SoapResponse";
            var header = soapString + ": " + message.MethodInfo.Name + "\n";

            Log(header, newStream);
        }

        private void Log(string header, Stream stream, Exception e = null)
        {
            var sb = new StringBuilder();
            var w = new StringWriter(sb);

            stream.Position = 0;
            Copy(stream, w);
            var msg = sb.ToString();
            try
            {
                //Since we're looking at SOAP, parse the XML so it gets formatted nicely.
                var log = header + XElement.Parse(msg.Trim());
                if (e == null) logger.Info(log);
                else logger.Error(log, e);
            }
            catch (Exception) //message is not valid xml
            {
                if (e == null) logger.Info(header + msg);
                else logger.Error(header + msg, e);
            }

            stream.Position = 0;
        }

        private void Copy(Stream from, TextWriter to)
        {
            var reader = new StreamReader(from);
            to.WriteLine(reader.ReadToEnd());
            to.Flush();
        }

        private void Copy(Stream from, Stream to)
        {
            TextReader reader = new StreamReader(from);
            TextWriter writer = new StreamWriter(to);
            writer.WriteLine(reader.ReadToEnd());
            writer.Flush();
        }
    }
}
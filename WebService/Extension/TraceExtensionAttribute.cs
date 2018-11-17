namespace WebServices.Extension
{
    using System;
    using System.Web.Services.Protocols;

    [AttributeUsage(AttributeTargets.Method)]
    public class TraceExtensionAttribute : SoapExtensionAttribute
    {
        public override Type ExtensionType
        {
            get
            {
                return typeof(TraceExtension);
            }
        }

        public override int Priority { get; set; }
    }
}
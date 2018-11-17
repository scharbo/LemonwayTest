namespace Business.Tests
{
    using System.Xml;

    using Business;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class JsonTests
    {
        [TestMethod, ExpectedException(typeof(XmlException))]
        public void SerializeXmlNode_NonXmlValue__ExpectedXmlException()
        {
            Json.SerializeXmlNode("Something");
        }

        [TestMethod, ExpectedException(typeof(XmlException))]
        public void SerializeXmlNode_EmptyValue__ExpectedXmlException()
        {
            Json.SerializeXmlNode("");
        }

        [TestMethod, ExpectedException(typeof(XmlException))]
        public void SerializeXmlNode_IncorrectXmlFormat_ExpectedXmlException()
        {
            Json.SerializeXmlNode("<foo>hello</bar>");
        }

        [TestMethod]
        public void SerializeXmlNode_SimpleXmlFormat_ReturnsGoodJson()
        {
            Assert.AreEqual(@"{""foo"":""bar""}", Json.SerializeXmlNode("<foo>bar</foo>"));
        }

        [TestMethod]
        public void SerializeXmlNode_EmptyXmlFormat_ReturnsNullInJson()
        {
            Assert.AreEqual(@"{""INT_MSG"":null}",Json.SerializeXmlNode("<INT_MSG/>"));
        }

        [TestMethod]
        public void SerializeXmlNode_SimpleXmlWithEmptyFormat_ReturnsGoodJson()
        {
            Assert.AreEqual(@"{""TRANS"":{""MLABEL"":""501767XXXXXX6700""}}", Json.SerializeXmlNode("<TRANS><INT_MSG/><MLABEL>501767XXXXXX6700</MLABEL></TRANS>"));
        }

        [TestMethod]
        public void SerializeXmlNode_ComplexXmlFormat_ReturnsGoodJson()
        {
            Assert.AreEqual(
                @"{""TRANS"":{""HPAY"":{""ID"":""103"",""STATUS"":""3"",""EXTRA"":{""IS3DS"":""0"",""AUTH"":""031183""},""MLABEL"":""501767XXXXXX6700"",""MTOKEN"":""project01""}}}",
                Json.SerializeXmlNode(
                    "<TRANS><HPAY><ID>103</ID><STATUS>3</STATUS><EXTRA><IS3DS>0</IS3DS><AUTH>031183</AUTH></EXTRA><INT_MSG/><MLABEL>501767XXXXXX6700</MLABEL><MTOKEN>project01</MTOKEN></HPAY></TRANS>"));
        }
    }
}

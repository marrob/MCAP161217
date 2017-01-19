
namespace Konvolucio.MCAP161217.Device.UnitTest
{
    using Device;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_OnlineExceptions
    {
        const string PortName = "COM7";
        MainDevice _thg = new MainDevice();

        [TestFixtureSetUp]
        public void Setup()
        {
            _thg = new MainDevice();
            _thg.Open(PortName);
        }

        [TestFixtureTearDown]
        public void Close()
        {
            _thg.Dispose();
        }

        [Test]
        public void _0001_InavlidAttributeId()
        {
            Assert.Catch(() =>
                {
                    byte[] attrValue;
                     _thg.Attributes.AttributeRead(Services.SID_ATTR_READ, 0x00000001, out attrValue);
                },                   
                "SPPC2_NRC_ATTR_BAD_ID Service is:0x2");

        }
    }
}

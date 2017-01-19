
namespace Konvolucio.MCAP161217.Device.UnitTest
{
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_Offline
    {

        [Test]
        public void _0001_Attr()
        {
            byte[] tx;
            Attributes.MakeRequest(out tx, Services.SID_ATTR_READ, Attributes.ATTR_DEVICE_NAME, null);
            Assert.AreEqual("0x07,0x00,0x02,0x03,0x00,0x00,0x81,0x72,0xFF", Tools.ByteArrayToCStyleString(tx));
        }

        [Test]
        public void _0002_Attr()
        {
            byte[] rx = new byte[] { 0x0E,0x00,0x42,0x4D,0x54,0x48,0x47,0x31,0x35,0x31,0x32,0x31,0x35,0x00,0x50,0xFD };
            byte[] strBytes;
            Attributes.ParseResponse(rx, Services.SID_ATTR_READ, out strBytes);
            Assert.AreEqual("0x4D,0x54,0x48,0x47,0x31,0x35,0x31,0x32,0x31,0x35,0x00", Tools.ByteArrayToCStyleString(strBytes));
        }
    }
}

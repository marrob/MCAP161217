
namespace Konvolucio.MCAP161217.Device.UnitTest
{
    using System;
    using Device;
    using NUnit.Framework;

    class UnitTest_Cheksum
    {
        [Test]
        public void _0001_Cheksum()
        {
            byte[] data = new byte[] { 0x03, 0x00, 0x01 };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xfffb, cks);
        }

        [Test]
        public void _0002_Cheksum()
        {
            byte[] data = new byte[] { 0xFF, 0xFF, 0x01 };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xfe00, cks);
        }

        [Test]
        public void _0003_Cheksum()
        {
            byte[] data = new byte[] { 0xFF };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xff00, cks);
        }

        [Test]
        public void _0004_Cheksum()
        {
            byte[] data = new byte[] { 0x00 };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xffff, cks);
        }

        [Test]
        public void _0005_Cheksum()
        {
            byte[] data = new byte[] { 0x07, 0x00, 0x02, 0x03, 0x00, 0x00, 0x81, 0x72, 0xFF };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xFE01, cks);
        }

        [Test]
        public void _0006_Cheksum()
        {
            byte[] data = new byte[] { 0x0E, 0x00, 0x42, 0x4D, 0x54, 0x48, 0x47, 0x31, 0x35, 0x31, 0x32, 0x31, 0x35, 0x00, 0x50, 0xFD };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xFC03, cks);
        }

        [Test]
        public void _0007_Cheksum()
        {
            byte[] data = new byte[] { /*0xFE,*/ 0x0B, 0x00, 0x63, 0x04, 0x00, 0xA4, 0x02, 0x0C, 0x02, 0x00, 0x02/*, 0xD7, 0xFE*/ };
            UInt16 cks = Sppc2.Checksum(data, 0, data.Length);
            Assert.AreEqual(0xfed7, cks);
        }
    }
}

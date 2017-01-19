
namespace Konvolucio.MCAP161217.Device.UnitTest
{
    using System;
    using System.Text;
    using Device;
    using NUnit.Framework;

    [TestFixture]
    class UnitTest_CRC
    {     
        [Test]
        public void _0001_Calc_For_HelloWorld()
        {
            /*0x48,0x65,0x6C,0x6C,0x6F,0x20,0x57,0x6F,0x72,0x6C,0x64 -> CRC:0x70C3*/
            string input = "Hello World";
            byte[] array = Encoding.ASCII.GetBytes(input);
            UInt16 crc = Tools.CalcCrc16Ansi(0,array);
            Assert.AreEqual(0x70C3, crc);
        }
    }
}

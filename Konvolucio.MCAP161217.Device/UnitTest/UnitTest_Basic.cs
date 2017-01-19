

namespace Konvolucio.MCAP161217.Device.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using System.IO.Ports;

    [TestFixture]
    class UnitTest_Basic
    {
        SerialPort _sp;

        [TestFixtureSetUp]
        public void Setup()
        {
            _sp = new SerialPort("COM7", 115200, Parity.None, 8, StopBits.One);
            _sp.ReadTimeout = 1000;
            _sp.Open();
        }

        [TestFixtureTearDown]
        public void Close()
        {

            if (_sp != null && _sp.IsOpen)
            {
                _sp.Close();
                _sp = null;
            }
        }

        [Test]
        public void _0003_BadFrameLength()
        {
            byte[] data = new byte[] { 0x0FE, 0x01, 0x02 };
            _sp.Write(data, 0, data.Length);
        }

        [Test]
        public void _0004_BadFrameCKS()
        {
            byte[] data = new byte[] { 0xFE, 0x03, 0x00, 0x00, 0x04, 0x00 };
            _sp.Write(data, 0, data.Length);
            System.Threading.Thread.Sleep(20);
            byte[] rx = new byte[_sp.BytesToRead];
            _sp.Read(rx, 0, rx.Length);
            Assert.AreEqual("0xFE,0x05,0x00,0x7F,0x02,0x00,0x79,0xFF", Tools.ByteArrayToCStyleString(rx));

        }

        [Test]
        public void _0005_StartField_Passed()
        {
            System.Threading.Thread.Sleep(400);
            byte[] tx = new byte[] {0xFE, 0x07, 0x00, 0x02, 0x03, 0x00, 0x00, 0x81, 0x72, 0xFF };
            _sp.Write(tx, 0, tx.Length);
            System.Threading.Thread.Sleep(20);
            byte[] rx = new byte[_sp.BytesToRead];
            _sp.Read(rx, 0, rx.Length);
            Assert.AreEqual("0xFE,0x12,0x00,0x42,0x03,0x00,0x00,0x81,0x4D,0x54,0x48,0x47,0x31,0x35,0x31,0x32,0x31,0x35,0x00,0xC8,0xFC", Tools.ByteArrayToCStyleString(rx));
        }

        [Test]
        public void _0006_StartFieldWithGabrage()
        {
            //                         GB0   SF    LEN0  LEN1  DAT0  DAT1  DAT2  DAT3  DAT4  CKS0  CKS1
            byte[] tx = new byte[] { 0x01, 0xFE, 0x07, 0x00, 0x02, 0x03, 0x00, 0x00, 0x81, 0x72, 0xFF };
            _sp.Write(tx, 0, tx.Length);
            System.Threading.Thread.Sleep(20);
            byte[] rx = new byte[_sp.BytesToRead];
            _sp.Read(rx, 0, rx.Length);
            Assert.AreEqual("0xFE,0x12,0x00,0x42,0x03,0x00,0x00,0x81,0x4D,0x54,0x48,0x47,0x31,0x35,0x31,0x32,0x31,0x35,0x00,0xC8,0xFC", Tools.ByteArrayToCStyleString(rx));

        }

        [Test]
        public void _0007_StartFieldWithBeforeGabrage()
        {
            //                         GB0   GB01  SF    LEN0  LEN1  DAT0  DAT1  DAT2  DAT3  DAT4  CKS0  CKS1
            byte[] data = new byte[] { 0x02, 0x01, 0xFE, 0x07, 0x00, 0x02, 0x03, 0x00, 0x00, 0x81, 0x72, 0xFF };
            _sp.Write(data, 0, data.Length);
            System.Threading.Thread.Sleep(20);
            byte[] rx = new byte[_sp.BytesToRead];
            _sp.Read(rx, 0, rx.Length);
            Assert.AreEqual("0xFE,0x12,0x00,0x42,0x03,0x00,0x00,0x81,0x4D,0x54,0x48,0x47,0x31,0x35,0x31,0x32,0x31,0x35,0x00,0xC8,0xFC", Tools.ByteArrayToCStyleString(rx));
        }

        [Test]
        public void _0008_StartFieldWithAfterBeforeGabrage()
        {
            //                         GB0   GB01  SF    LEN0  LEN1  DAT0  DAT1  DAT2  DAT3  DAT4  CKS0  CKS1  GB0   GB01
            byte[] data = new byte[] { 0x02, 0x01, 0xFE, 0x07, 0x00, 0x02, 0x03, 0x00, 0x00, 0x81, 0x72, 0xFF, 0x02, 0x01, };
            _sp.Write(data, 0, data.Length);
            System.Threading.Thread.Sleep(20);
            byte[] rx = new byte[_sp.BytesToRead];
            _sp.Read(rx, 0, rx.Length);
            Assert.AreEqual("0xFE,0x12,0x00,0x42,0x03,0x00,0x00,0x81,0x4D,0x54,0x48,0x47,0x31,0x35,0x31,0x32,0x31,0x35,0x00,0xC8,0xFC", Tools.ByteArrayToCStyleString(rx));
        }

        [Test]
        public void _0009_ExtFunctionRepeatAuthDataMoveResponse()
        {
            byte[] data = new byte[] { 0xFE, 0x04, 0x00, 0x30, 0x03, 0xC8, 0xFF};
            _sp.Write(data, 0, data.Length);
            System.Threading.Thread.Sleep(20);

            var startU8 = new byte[1];
            _sp.Read(startU8, 0, startU8.Length);
            var lengthU16 = new byte[2];
            _sp.Read(lengthU16, 0, lengthU16.Length);
            int length = BitConverter.ToUInt16(lengthU16, 0);
            var response = new byte[length];
            _sp.Read(response, 0, length);

            startU8 = new byte[1];
            _sp.Read(startU8, 0, startU8.Length);
            lengthU16 = new byte[2];
            _sp.Read(lengthU16, 0, lengthU16.Length);
            length = BitConverter.ToUInt16(lengthU16,0);
            response = new byte[length];
            _sp.Read(response, 0, length);
        }
    }
}

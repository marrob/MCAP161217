
namespace Konvolucio.MTOO180701.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;


    [TestFixture]
    class UnitTest
    {
        [Test]
        public void SuffixFormatter_0001()
        {
            
        }

        [Test]
        public void SuffixFormatter_0002()
        {
            Assert.AreEqual("5.00 p", SuffixFormatter.Suffix(5E-12, 2));
            Assert.AreEqual("5.00 m", SuffixFormatter.Suffix(5E-3, 2));
            Assert.AreEqual("1.00 u", SuffixFormatter.Suffix(1E-6, 2));
            Assert.AreEqual("1.00 m", SuffixFormatter.Suffix(0.001, 2));
            Assert.AreEqual("0.00", SuffixFormatter.Suffix(0, 2));
            Assert.AreEqual("0.1 K", SuffixFormatter.Suffix(100, 1));
            Assert.AreEqual("5.00 K", SuffixFormatter.Suffix(5000, 2));
        }

        [Test]
        public void SuffixFormatter_0003()
        { 
            Assert.AreEqual("5.00 M", string.Format(new SuffixFormatter(), "{0:sfx}", 5000000));
            Assert.AreEqual("5.00 ", string.Format(new SuffixFormatter(), "{0:sfx}", 5));
            Assert.AreEqual("5.00 K", string.Format(new SuffixFormatter(), "{0:sfx}", 5000));
            Assert.AreEqual("6.00 K", string.Format(new SuffixFormatter(), "{0:sfx}", 5999));
        }
    }
}


namespace Konvolucio.MTOO180701.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using System.ComponentModel;
    using System.Reflection;


    [TestFixture]
    class UnitTest_PropertyPlus
    {
        class testClass
        {
            [System.ComponentModel.Description("Hello World")]
            public double SmapleProperty { get; set; }
        }
        [Test]
        public void GetPropertyDescriptionTest()
        {
            var tc = new testClass();
            Assert.AreEqual("Hello World", PropertyPlus.GetPropertyDescription(tc, "SmapleProperty"));

        }

        [Test]
        public void GetPropertyDescriptionTest_2()
        {
            var tc = new testClass();
            Assert.AreEqual("Hello World", PropertyPlus.GetPropertyDescription(tc, PropertyPlus.GetPropertyName(() => tc.SmapleProperty)));

        }

    }

}

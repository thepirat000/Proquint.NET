using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Proquint;

namespace UnitTest
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Proquint_IPs()
        {
            var quads = new Dictionary<string, string>
            {
                {"127.0.0.1", "lusab-babad"},
                {"63.84.220.193", "gutih-tugad"},
                {"63.118.7.35", "gutuk-bisog"},
                {"140.98.193.141", "mudof-sakat"},
                {"64.255.6.200", "haguz-biram"},
                {"128.30.52.45", "mabiv-gibot"},
                {"147.67.119.2", "natag-lisaf"},
                {"212.58.253.68", "tibup-zujah"},
                {"216.35.68.215", "tobog-higil"},
                {"216.68.232.21", "todah-vobij"},
                {"198.81.129.136", "sinid-makam"},
                {"12.110.110.204", "budov-kuras"}
            };
            foreach (var q in quads)
            {
                var i = ToInt(q.Key);
                var qu = new Quint32(i);
                var i2 = (uint)qu;
                Assert.AreEqual(q.Value, qu.ToString());
                Assert.AreEqual(i, i2);
            }
        }

        [TestMethod]
        public void Proquint_int()
        {
            var q0 = (Quint32)(int)0;
            var qm1 = (Quint32)(int)(-1);
            var qmin = (Quint32)int.MinValue;
            var qmax = (Quint32)int.MaxValue;
            Assert.AreEqual("babab-babab", q0.ToString());
            Assert.AreEqual("zuzuz-zuzuz", qm1.ToString());
            Assert.AreEqual("mabab-babab", qmin.ToString());
            Assert.AreEqual("luzuz-zuzuz", qmax.ToString());
        }

        [TestMethod]
        public void Proquint_Random()
        {
            var a = Quint32.NewQuint();
            var b = Quint32.NewQuint();
            Assert.AreEqual(11, a.ToString().Length);
            Assert.AreEqual(11, b.ToString().Length);
            Assert.AreNotEqual(a, b);
        }

        [TestMethod]
        public void Quint_Test()
        {
            var q = new Quint32(123456);
            uint i = (uint) q;
            string s = (string)q;
            var q2 = new Quint32(s);
            var q3 = new Quint32(123499);
            Assert.AreEqual((uint)123456, i);
            Assert.AreEqual((uint)123456, (uint)q2);
            Assert.IsTrue(q.Equals(q2));
            Assert.IsTrue(q.Equals((uint)123456));
            Assert.IsTrue(q == q2);
            Assert.IsTrue(q <= q2);
            Assert.IsFalse(q != q2);
            Assert.IsTrue(q3 > q2);
            Assert.IsTrue(q3 >= q2);
        }

        private uint ToInt(string addr)
        {
            return (uint)IPAddress.NetworkToHostOrder((int)IPAddress.Parse(addr).Address);
        }
    }
}

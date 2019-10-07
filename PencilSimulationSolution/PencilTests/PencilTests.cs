using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {
        [TestMethod]
        public void PencilTipDurabilityIs20()
        {
            int durability = 20;
            Pencil pencil = new Pencil(durability);
            Assert.AreEqual(durability, pencil.tip);
        }

          [TestMethod]
          public void PencilWriteReturnsString()
        {
            string input = "Test";
            Pencil pencil = new Pencil(20);
            Assert.AreEqual("Test", pencil.write(input));

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {

        public static int tipDurability = 20;
        public static Pencil pencil = new Pencil(tipDurability);

        [TestMethod]
        public void PencilTipDurabilityIs20()
        {
            Assert.AreEqual(20, pencil.tip);
        }

        [TestMethod]
        public void PencilWriteReturnsString()
        {
            string input = "Test";
            
            Assert.AreEqual("Test", pencil.write(input));

        }

        
    }
}

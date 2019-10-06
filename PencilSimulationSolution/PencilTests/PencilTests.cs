using Microsoft.VisualStudio.TestTools.UnitTesting;
using PencilLib;

namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {
        [TestMethod]
        public void PencilDurabilityIs20()
        {
            int durability = 20;
            Pencil pencil = new Pencil(durability);
            Assert.AreEqual(durability, pencil.durability);
        }
    }
}

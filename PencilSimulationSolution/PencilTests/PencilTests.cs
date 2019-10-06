using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace PencilTests
{
    [TestClass]
    public class PencilTests
    {
        [TestMethod]
        public void IsThisAPencilTest()
        {
            Pencil pencilTest = new Pencil();
            Assert.IsInstanceOfType(pencilTest, Pencil);
        }
    }
}

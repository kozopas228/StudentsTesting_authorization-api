using Authorization_Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Authorization_API.UnitTests
{
    [TestClass]
    class ServicesUTest
    {
        [TestMethod]
        public void Hesh_string_In_b80467f35b449736162b64cbaa3a2a2d_Out()
        {
            string stringInput = "string";
            string stringOutput = "b80467f35b449736162b64cbaa3a2a2d";
            string stringResult;

            stringResult = Hashing.GetHashString(stringInput);

            Assert.AreEqual(stringOutput, stringResult);
        }
    }
}

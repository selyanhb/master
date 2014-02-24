using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimAvatar;
using UnityEngine;
using System.Collections.Generic;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestInitTransformer()
        {
            Transformer transformer = new Transformer(@"C:\Users\Tlatoc\Downloads\Motion\Motion\fast_kick.drf");
            Assert.AreEqual(258753, transformer.initFrame);
        }

        [TestMethod]
        public void TestGetBoneRot()
        {
            Transformer transformer = new Transformer(@"C:\Users\Tlatoc\Downloads\Motion\Motion\fast_kick.drf");
            List<Vector3> bonesRot = transformer.getBoneRot(transformer.initFrame);
            Assert.AreEqual(-5.9987f, bonesRot[0].y);
        }
    }
}

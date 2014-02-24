using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimAvatar;
using System.Collections.Generic;
using UnityEngine;

namespace TestpAppl
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestParseFrame()
        {
            int result = ARTParser.parseFrame("qsdfqfr 525851");
            Assert.AreEqual(525851, result);
        }

        [TestMethod]
        public void TestParseFrameError()
        {
            string error = "";
            try
            {
                ARTParser.parseFrame("qsdfqfd 525851");
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            Assert.AreNotEqual(error, "");
        }

        [TestMethod]
        public void TestParseTime()
        {
            double result = ARTParser.parseTime("ts 34224.513654");
            Assert.AreEqual(34224.513654, result);
        }

        [TestMethod]
        public void TestParseTimeVirgule()
        {
            double result = ARTParser.parseTime("ts 34224,513654");
            Assert.AreEqual(34224.513654, result);
        }

        [TestMethod]
        public void TestParseTimeError()
        {
            string error = "";
            try
            {
                ARTParser.parseFrame("tl 34224.513654");
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            Assert.AreNotEqual("", error);
        }

        [TestMethod]
        public void TestParseData()
        {
            List<Bone> bones = ARTParser.parseData("6dj 1 1 [0 1][0 1.000][-8.0677 -173.6933 995.9617 -0.0000 0.0000 -5.9987][0.994524 -0.104505 0.000000 0.104505 0.994524 -0.000000 0.000000 0.000000 1.000000]");
            float result = bones[0].Mat[0, 1];
            Assert.IsTrue((-0.104505 - 0.000001) < result && result < (-0.104505 + 0.000001));
        }

        [TestMethod]
        public void TestParseData2()
        {
            List<Bone> bones = ARTParser.parseData("6dj 1 1 [0 20][1 1.000][-8.0677 -173.6933 995.9617 -0.0000 0.0000 -5.9987][0.994524 -0.104505 0.000000 0.104505 0.994524 -0.000000 0.000000 0.000000 1.000000]");
            float result = bones[0].Mat[0, 1];
            Assert.IsTrue((-0.104505 - 0.000001) < result && result < (-0.104505 + 0.000001));
        }

        [TestMethod]
        public void TestParseDataErrorSizeVector()
        {
            string error = "";
            try
            {
                List<Bone> bones = ARTParser.parseData("6dj 1 1 [0 20][1 1.000][-8.0677 -173.6933 995.9617 -0.0000 -5.9987][0.994524 -0.104505 0.000000 0.104505 0.994524 -0.000000 0.000000 0.000000 1.000000]");
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            Assert.AreNotEqual("", error);
        }

        [TestMethod]
        public void TestParseDataErrorSizeMat()
        {
            string error = "";
            try
            {
                List<Bone> bones = ARTParser.parseData("6dj 1 1 [0 20][1 1.000][-8.0677 -173.6933 995.9617 -0.0000 0.0000 -5.9987][0.994524 -0.104505  0.104505 0.994524 -0.000000 0.000000 0.000000 1.000000]");
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            Assert.AreNotEqual("", error);
        }

        [TestMethod]
        public void TestParse()
        {
            float result;
            List<Bone> bones = null;
            try
            {
                bones = ARTParser.parse(@"C:\Users\Tlatoc\Downloads\Motion\Motion\fast_kick.drf")[259371];
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }

            result = bones[3].Mat[0, 1];

            Assert.IsTrue((-0.088390 - 0.000001) < result && result < (-0.088390 + 0.000001));
        }

        [TestMethod]
        public void TestParseDirectoryNameError()
        {
            string error = "";
            try
            {
                Dictionary<Int32, List<Bone>> bones = ARTParser.parse(@"motion\fichierintrouvable.drf");
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            Assert.AreNotEqual("", error);
        }


    }
}

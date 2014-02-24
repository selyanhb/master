using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimAvatar;
using UnityEngine;

namespace TestAnimator
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AvatarAnimator animator = new AvatarAnimator();
            animator.Start();
        }
    }
}

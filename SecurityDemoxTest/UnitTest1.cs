using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using NUnit.Framework;
using SecurityDemoX.Module.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecurityDemoxTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var a = ReflectionHelper.GetInterfaceHierarchy(typeof(IPartyRoleType));
        }




    }
}
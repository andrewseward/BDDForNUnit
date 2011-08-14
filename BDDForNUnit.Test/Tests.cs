using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDDForNUnit.Attributes;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [BDDTestFixture]
    public class Tests
    {
        private string _doAThing;

        [Given]
        public void MyGiven()
        {
            _doAThing = "LALALLAA";
        }

        [When]
        public void MyWhen()
        {
            Assert.Pass(_doAThing);

        }

        [Then]
        public void MyThen()
        {
            Assert.Pass(_doAThing);

        }
    }

    [TestFixture]
    public class StandardTests
    {
    }
}

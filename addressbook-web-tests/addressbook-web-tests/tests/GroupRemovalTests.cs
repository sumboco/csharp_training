using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            //prepare
            GroupData createGroup = new GroupData("2qwe123");
            createGroup.Header = "tew31";
            createGroup.Footer = "ew124";
            app.Group.IsElementGroupAndCreate(createGroup);

            //action
            app.Group.Remove(1);
        }

    }
}

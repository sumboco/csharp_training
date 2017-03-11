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
    public class GroupCreationTests : AuthTestBase
    {
       
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("2qwe");
            group.Header = "tew";
            group.Footer = "ew";

            app.Group.Create(group);
        }

    }
}

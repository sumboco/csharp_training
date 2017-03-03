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
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            SelectContact(2);
            DeleteContact();
            CloseAlertDeleteContact();
            Logout();
        }

    }
}

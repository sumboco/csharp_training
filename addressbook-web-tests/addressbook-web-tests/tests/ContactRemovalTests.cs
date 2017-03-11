using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : AuthTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {

            //prepare
            ContactData createContact = new ContactData("Ivan3", "Ivanov");
            app.Contact.IsElementContactAndCreate(createContact);

            //action
            app.Contact.Remove(2);
        }

    }
}

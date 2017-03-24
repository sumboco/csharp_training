using System;
using System.Text;
using System.Threading;
using System.Collections.Generic;
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
            List<ContactData> oldContacts = app.Contact.GetContactList();
            app.Contact.Remove(0);
            app.Navigator.OpenHomePage();
            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.RemoveAt(0);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

    }
}

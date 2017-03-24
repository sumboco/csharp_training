using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {

        [Test]
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("Ivan3", "Ivanov");

            List<ContactData> oldContacts = app.Contact.GetContactList();
            app.Contact.Create(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]
        public void BadNameContactCreationTest()
        {
            ContactData contact = new ContactData("a'a", "Ivanov");

            List<ContactData> oldContacts = app.Contact.GetContactList();
            app.Contact.Create(contact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            //oldContacts.Add(contact);

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

        }
    }
}

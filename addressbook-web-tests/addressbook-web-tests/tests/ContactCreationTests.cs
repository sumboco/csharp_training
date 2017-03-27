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
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 2; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(30), GenerateRandomString(30))
                {
                    Address = "kasldooglbvgfs",
                    HomePhome = "234()23423",
                    MobilePhome = "(32)2323-230230-43",
                    WorkPhome = "2342-3456",
                    Email = "test@test.test",
                    Email2 = "helloo@word.test",
                    Email3 = "mxcvjkfudf@kdslasd.test"

                });
            }

            return contacts;
        }

        [Test, TestCaseSource("RandomContactDataProvider")]
        public void ContactCreationTest(ContactData contact)
        {
            /*ContactData contact = new ContactData("Ivan3", "Ivanov")
            {
                Address = "kasldooglbvgfs",
                HomePhome = "234()23423",
                MobilePhome = "(32)2323-230230-43",
                WorkPhome = "2342-3456",
                Email = "test@test.test",
                Email2 = "helloo@word.test",
                Email3 = "mxcvjkfudf@kdslasd.test"
            };*/

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

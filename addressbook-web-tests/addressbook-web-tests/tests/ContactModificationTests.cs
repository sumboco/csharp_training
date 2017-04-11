using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {

        [Test]
        public void ContactModificationTest()
        {

            //prepare
            ContactData createContact = new ContactData("Ivan3", "Ivanov");
            app.Contact.IsElementContactAndCreate(createContact);

            //action
            ContactData editContact = new ContactData("newIvan3", "newIvanov");
            List<ContactData> oldContacts = app.Contact.GetContactList();

            app.Contact.Modify(0, editContact);

            List<ContactData> newContacts = app.Contact.GetContactList();
            oldContacts[0].FirstName = editContact.FirstName;
            oldContacts[0].LastName = editContact.LastName;

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

        }

        [Test]
        public void ContactModificationDBTest()
        {

            //prepare
            ContactData createContact = new ContactData("Ivan3", "Ivanov");
            app.Contact.IsElementContactAndCreate(createContact);

            //action
            ContactData editContact = new ContactData("newIvan3", "newIvanov");
            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModification = oldContacts[14];

            app.Contact.Modify(toBeModification, editContact);

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[14].FirstName = editContact.FirstName;
            oldContacts[14].LastName = editContact.LastName;

            oldContacts.Sort();
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);

        }
    }
}
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : AuthTestBase
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
    }
}
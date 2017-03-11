using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


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
            app.Contact.Modify(2, editContact);

        }
    }
}
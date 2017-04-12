using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class AddingContactToGroupTests : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(group.GetContacts()).First();

            app.Contact.AddContactToGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }

        [Test]
        public void TestRemovalContactToGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            if (oldList.Count > 0)
            {
                ContactData contact = oldList[0];
                                
                app.Contact.RemoveContactFromGroup(contact, group);
                
                List<ContactData> newList = group.GetContacts();
                oldList.Remove(contact);
                newList.Sort();
                oldList.Sort();

                Assert.AreEqual(oldList, newList);
            }
            else
            {
                System.Console.Out.WriteLine("Группа не содержит контактов");
            }
        }
    }
}

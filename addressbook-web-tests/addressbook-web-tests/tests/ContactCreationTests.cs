using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
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
                    HomePhone = "234()23423",
                    MobilePhone = "(32)2323-230230-43",
                    WorkPhone = "2342-3456",
                    Email = "test@test.test",
                    Email2 = "helloo@word.test",
                    Email3 = "mxcvjkfudf@kdslasd.test"

                });
            }

            return contacts;
        }
        public static IEnumerable<ContactData> ContactDataFromCsvFile()
        {
            List<ContactData> contacts = new List<ContactData>();
            string[] lines = File.ReadAllLines(@"contacts.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                contacts.Add(new ContactData()
                {
                    FirstName = parts[0],
                    LastName = parts[1],
                    Address = parts[2],
                    HomePhone = parts[3],
                    MobilePhone = parts[4],
                    WorkPhone = parts[5],
                    Email = parts[6],
                    Email2 = parts[7],
                    Email3 = parts[8]
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {

            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {

            return JsonConvert.DeserializeObject<List<ContactData>>
                (File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]
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

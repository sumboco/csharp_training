using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactInformationTests : AuthTestBase
    {
        [Test]
        public void TestContactInformationMainPage()
        {
            ContactData fromTable = app.Contact.GetContactInforamitonFromTable(0);
            ContactData fromForm = app.Contact.GetContactInforamitonFromEditForm(0);

            //verivication
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmail, fromForm.AllEmail);
            Assert.AreEqual(fromTable.AllPhone, fromForm.AllPhone);
        }
        [Test]
        public void TestContactInformationProfilePage()
        {
            ContactData fromProfile = app.Contact.GetContactInformationFromProfile(0);
            ContactData fromForm = app.Contact.GetContactInforamitonFromEditForm(0);
            //app.Contact.GetContactInformationFromProfile(0);
            //System.Console.Out.Write("143");
            Assert.AreEqual(fromProfile, fromForm);
            Assert.AreEqual(fromProfile.AllProfile, fromForm.AllProfile);
        }
    }
}

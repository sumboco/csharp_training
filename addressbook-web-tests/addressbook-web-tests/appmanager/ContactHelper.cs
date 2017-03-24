using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {

        private List<ContactData> contactCache = null;

        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactPage();
            return this;
        }


        public ContactHelper Modify(int p, ContactData editContact)
        {
            SelectContactModification(p);
            FillContactForm(editContact);
            SubmitContactModification();
            ReturnToContactPage();
            return this;
        }

        public ContactHelper Remove(int p)
        {
            SelectContact(p);
            DeleteContact();
            CloseAlertDeleteContact();
            return this;
        }

        public ContactHelper ReturnToContactPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContactModification(int p)
        {
            driver.FindElement(By.XPath("//tbody/tr[" + (p + 2) + "]/td[8]/a/img")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("//tr[" + (index + 2) + "]/td[1]/input")).Click();
            return this;
        }


        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }

        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper CloseAlertDeleteContact()
        {
            Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            return this;
        }

        public ContactHelper DeleteContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public void IsElementContactAndCreate(ContactData createContact)
        {
            manager.Navigator.OpenHomePage();
            if (!IsElementPresent(By.XPath("//tbody/tr[2]")))
            {
                Create(createContact);
            }
        }

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name = 'entry']"));
                for (int i = 0; i < elements.Count(); i++)
                //foreach (IWebElement element in elements)
                {
                    IWebElement fistNameElement = driver.FindElement(By.CssSelector("tr:nth-child(" + (i + 2) + ")[name = 'entry'] td:nth-child(3)"));
                    IWebElement lastNameElement = driver.FindElement(By.CssSelector("tr:nth-child(" + (i + 2) + ")[name = 'entry'] td:nth-child(2)"));
                    //contacts.Add(new ContactData(element.Text));
                    contactCache.Add(new ContactData(fistNameElement.Text, lastNameElement.Text));
                }
            }
            
            return new List<ContactData>(contactCache);
        }

    }
}

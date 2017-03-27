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
using System.Collections;

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

        public ContactHelper OpenProfile(int index)
        {
            driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("home"), contact.HomePhome);
            Type(By.Name("mobile"), contact.MobilePhome);
            Type(By.Name("work"), contact.WorkPhome);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
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

        public ContactData GetContactInforamitonFromTable(int index)
        {
            IList<IWebElement> cell = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cell[1].Text;
            string firstName = cell[2].Text;
            string address = cell[3].Text;
            string allEmail = cell[4].Text;
            string allPhone = cell[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllPhone = allPhone,
                AllEmail = allEmail
            };
        }

        public ContactData GetContactInforamitonFromEditForm(int index)
        {
            manager.Navigator.OpenHomePage();
            SelectContactModification(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhome = homePhone,
                MobilePhome = mobilePhone,
                WorkPhome = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3
            };
        }

        public ContactData GetContactInformationFromProfile(int index)
        {
            manager.Navigator.OpenHomePage();
            OpenProfile(index);
            
            //получаем данные из контент из профиля
            string contactProfile = driver.FindElement(By.Id("content")).Text;
            
            // преобразуем в массив
            string[] arrayContactProfile = contactProfile.Split('\n');
            
            //выибираем первую строчку, где Имя и Фамилия
            string fioContactProfile = arrayContactProfile[0].Trim('\r');
            string[] arrayFioContactProfile = fioContactProfile.Split();
            string firstname = arrayFioContactProfile[0];
            string lastname = arrayFioContactProfile[1];

            //собираем новый контент профиля без первой строчки(Имени Фамилии)
            string newContactProfile = "";
            for (int i = 1; i < arrayContactProfile.Length; i++)
            {
                newContactProfile = newContactProfile + arrayContactProfile[i] + "\n";
            }
            string allProfile = newContactProfile.Trim('\n');

            return new ContactData(firstname, lastname)
            {
                AllProfile = allProfile
            };
        }

    }
}

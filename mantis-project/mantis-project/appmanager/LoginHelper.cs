using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_project
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }

                Logout();
            }

            Type(By.Name("username"), account.Username);
            Type(By.Name("password"), account.Password);
            
            driver.FindElement(By.CssSelector("input[value='Войти']")).Click();
        }

        public void Logout()
        {
            if (IsLoggedIn())
            {
                driver.FindElement(By.CssSelector("span.user-info")).Click();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("a[href = '/mantisbt/logout_page.php']")));
                driver.FindElement(By.CssSelector("a[href = '/mantisbt/logout_page.php']")).Click();
            }
        }

        public bool IsLoggedIn()
        {
            return IsElementPresent(By.CssSelector("span.user-info"));
        }

        public bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn()
                && driver.FindElement(By.CssSelector("span.user-info")).Text == "(" + account.Username + ")";
        }
    }
}

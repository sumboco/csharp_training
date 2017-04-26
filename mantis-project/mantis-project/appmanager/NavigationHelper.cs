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
    public class NavigationHelper : HelperBase
    {
        
        public NavigationHelper(ApplicationManager manager) : base(manager)
        {
        }
        public void OpenManagement()
        {
            driver.FindElement(By.CssSelector("a[href = '/mantisbt/manage_overview_page.php']")).Click();
        }
    }
}

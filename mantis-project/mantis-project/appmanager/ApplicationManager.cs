using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_project
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver(new FirefoxBinary("C:\\Program Files\\Mozilla FirefoxESR\\firefox.exe"), new FirefoxProfile());
            baseURL = "http://localhost:9080/";
            Auth = new LoginHelper(this);
            Navigator = new NavigationHelper(this);
            ManagementMenu = new ManagementMenuHelper(this);
            ProjectManager = new ProjectManagementHelper(this);
            API = new APIHelper(this);

        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = "http://localhost:9080/mantisbt/login_page.php";
                app.Value = newInstance;
                
            }
            return app.Value;
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public LoginHelper Auth { get; set; }
        public NavigationHelper Navigator { get; set; }
        internal ManagementMenuHelper ManagementMenu { get; set; }
        internal ProjectManagementHelper ProjectManager { get; private set; }
        public APIHelper API { get; set; }
    }
}

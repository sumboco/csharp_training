using System;
using System.Text;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    public class TestBase
    {

        protected ApplicationManager app;

        [SetUp]
        public void SetupTest()
        {
            app = ApplicationManager.GetInstance();

        }

        [TearDown]
        public void TeardownTest()
        {
            //app.Logout.Logout();
        }
    }
}

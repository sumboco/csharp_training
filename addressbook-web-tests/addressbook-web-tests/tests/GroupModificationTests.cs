using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : TestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("aghasd");
            newData.Header = "asdfas";
            newData.Footer = "egdsd";

            app.Group.Modify(1, newData);
        }

    }
}
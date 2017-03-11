using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : AuthTestBase
    {

        [Test]
        public void GroupModificationTest()
        {
            //prepare
            GroupData createGroup = new GroupData("2qwe123");
            createGroup.Header = "tew31";
            createGroup.Footer = "ew124";
            app.Group.IsElementGroupAndCreate(createGroup);

            //action
            GroupData newData = new GroupData("aghasd");
            newData.Header = null;
            newData.Footer = null;
            app.Group.Modify(1, newData);
        }

    }
}
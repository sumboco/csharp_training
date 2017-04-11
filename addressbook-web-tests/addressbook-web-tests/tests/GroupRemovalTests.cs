using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {

        [Test]
        public void GroupRemovalTest()
        {
            //prepare
            GroupData createGroup = new GroupData("2qwe123");
            createGroup.Header = "tew31";
            createGroup.Footer = "ew124";

            app.Group.IsElementGroupAndCreate(createGroup);

            //action
            List<GroupData> oldGroups = app.Group.GetGroupList();

            app.Group.Remove(0);

            List<GroupData> newGroups = app.Group.GetGroupList();

            oldGroups.RemoveAt(0);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupRemovalDBTest()
        {
            //prepare
            GroupData createGroup = new GroupData("2qwe123");
            createGroup.Header = "tew31";
            createGroup.Footer = "ew124";

            app.Group.IsElementGroupAndCreate(createGroup);

            //action
            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData toBeReoved = oldGroups[0];
            app.Group.Remove(toBeReoved);

            List<GroupData> newGroups = GroupData.GetAll();

            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupData group in newGroups)
            {
                Assert.AreNotEqual(group.Id, toBeReoved.Id);
            }
        }

    }
}

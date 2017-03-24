using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
       
        [Test]
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("2qwe");
            group.Header = "tew";
            group.Footer = "ew";

            List<GroupData> oldGroups = app.Group.GetGroupList();

            app.Group.Create(group);

            List<GroupData> newGroups = app.Group.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            oldGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void BadNameGroupCreationTest()
        {
            GroupData group = new GroupData("a'a");
            group.Header = "tew";
            group.Footer = "ew";

            List<GroupData> oldGroups = app.Group.GetGroupList();

            app.Group.Create(group);

            List<GroupData> newGroups = app.Group.GetGroupList();
            //oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

    }
}

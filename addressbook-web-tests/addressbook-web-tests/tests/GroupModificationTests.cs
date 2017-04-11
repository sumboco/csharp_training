using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Collections.Generic;


namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupModificationTests : GroupTestBase
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

            List<GroupData> oldGroups = app.Group.GetGroupList();
            app.Group.Modify(0, newData);

            List<GroupData> newGroups = app.Group.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void GroupModificationDBTest()
        {
            //prepare
            GroupData createGroup = new GroupData("2qwe123");
            createGroup.Header = "tew31";
            createGroup.Footer = "ew124";
            app.Group.IsElementGroupAndCreate(createGroup);

            //action
            GroupData newData = new GroupData("a2gha3s1d");
            newData.Header = null;
            newData.Footer = null;

            List<GroupData> oldGroups = GroupData.GetAll();
            GroupData oldData = oldGroups[0];

            app.Group.Modify(oldData, newData);

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);

            foreach (GroupData group in newGroups)
            {
                if(group.Id == oldData.Id)
                {
                    Assert.AreEqual(newData.Name, group.Name);
                }
            }
        }

    }
}
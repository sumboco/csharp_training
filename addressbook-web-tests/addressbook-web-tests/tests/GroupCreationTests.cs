using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i< 2; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)

                });
            }

            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach(string l in lines)
            {
                string [] parts = l.Split(',');
                groups.Add(new GroupData()
                {
                    Name = parts[0],
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {

            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {

            return JsonConvert.DeserializeObject<List<GroupData>>
                (File.ReadAllText(@"groups.json"));
        }

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationTest(GroupData group)
        {
            /*GroupData group = new GroupData("2qwe");
            group.Header = "tew";
            group.Footer = "ew";*/

            List<GroupData> oldGroups = app.Group.GetGroupList();

            app.Group.Create(group);

            List<GroupData> newGroups = app.Group.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
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

        [Test, TestCaseSource("GroupDataFromJsonFile")]
        public void GroupCreationDBTest(GroupData group)
        {
            List<GroupData> oldGroups = GroupData.GetAll();

            app.Group.Create(group);

            List<GroupData> newGroups = GroupData.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]
        public void TestDBGroupsTime()
        {
            DateTime start = DateTime.Now;
            List<GroupData> fromUi = app.Group.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupData> fromDb = GroupData.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }

        [Test]
        public void TestDBConnectivity()
        {
            foreach (ContactData contact in GroupData.GetAll()[0].GetContacts())
            {
                System.Console.Out.WriteLine(contact);
            }
        }

    }
}

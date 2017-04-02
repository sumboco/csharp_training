using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebAddressbookTests;

namespace addressbook_test_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            string typeDate = args[0];
            int count = Convert.ToInt32(args[1]);
            StreamWriter writer = new StreamWriter(args[2]);
            string format = args[3];

            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i<count; i++)
            {
                groups.Add(new GroupData(TestBase.RandomString(10))
                {
                    Header = TestBase.RandomString(10),
                    Footer = TestBase.RandomString(10)
                });
            }

            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactData()
                {
                    FirstName = TestBase.RandomString(10),
                    LastName = TestBase.RandomString(10),
                    Address = TestBase.RandomString(10),
                    HomePhone = TestBase.RandomDigital(10),
                    MobilePhone = TestBase.RandomDigital(10),
                    WorkPhone = TestBase.RandomDigital(10),
                    Email = TestBase.RandomString(10),
                    Email2 = TestBase.RandomString(10),
                    Email3 = TestBase.RandomString(10),
                    AllEmail = "",
                    AllPhone = "",
                    AllProfile = ""
                });
            }

            if (typeDate == "groups")
            {
                if (format == "csv")
                {
                    WriteGroupsToCsvFile(groups, writer);
                }

                else if (format == "xml")
                {
                    WriteGroupsToXmlFile(groups, writer);
                }
                else if (format == "json")
                {
                    WriteGroupsToJsonFile(groups, writer);
                }

                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }
            }
            else if (typeDate == "contacts")
            {
                if (format == "csv")
                {
                    WriteContactsToCsvFile(contacts, writer);
                }

                else if (format == "xml")
                {
                    WriteContactsToXmlFile(contacts, writer);
                }
                else if (format == "json")
                {
                    WriteContactsToJsonFile(contacts, writer);
                }

                else
                {
                    System.Console.Out.Write("Unrecognized format" + format);
                }
            }
            else
            {
                System.Console.Out.Write("Input type data 'groups' or 'contacts'");
            }
            writer.Close();
        }

        static void WriteGroupsToCsvFile(List<GroupData> groups, StreamWriter writer)
        {
            foreach (GroupData group in groups)
            {
                writer.WriteLine(String.Format("{0},{1},{2}",
                    group.Name, group.Header, group.Footer));
            }
        }

        static void WriteGroupsToXmlFile(List<GroupData> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupData>)).Serialize(writer, groups);
        }

        static void WriteGroupsToJsonFile(List<GroupData> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToCsvFile(List<ContactData> contacts, StreamWriter writer)
        {
            foreach (ContactData contact in contacts)
            {
                writer.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                    contact.FirstName, contact.LastName, contact.Address, contact.HomePhone,
                    contact.MobilePhone, contact.WorkPhone, contact.Email, contact.Email2, contact.Email3));
            }
        }

        static void WriteContactsToXmlFile(List<ContactData> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactData>)).Serialize(writer, contacts);
        }

        static void WriteContactsToJsonFile(List<ContactData> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }

    }
}

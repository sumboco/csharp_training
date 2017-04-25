using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using OpaqueMail.Net;

namespace mantis_tests
{
    [TestFixture]
    public class UnitTest1 //: TestBase
    {
        [Test]
        public void TestMethod1()
        {
            /*AccountData account = new AccountData()
            {
                Name = "user",
                Password = "password"
            };
            Assert.IsFalse(app.James.Verify(account));
            app.James.Add(account);
            Assert.IsTrue(app.James.Verify(account));
            app.James.Delete(account);
            Assert.False(app.James.Verify(account));*/

            for (int i = 0; i < 20; i++)
            {
                Pop3Client pop3 = new Pop3Client("localhost", 110, "user1", "password", false);
                pop3.Connect();
                pop3.Authenticate();
                if (pop3.GetMessageCount() > 0)
                {
                    ReadOnlyMailMessage message = pop3.GetMessage(1);
                    string body = message.Body;
                    pop3.DeleteMessage(1);
                    System.Console.Out.WriteLine(body);
                }
                else
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }
        }
    }
}

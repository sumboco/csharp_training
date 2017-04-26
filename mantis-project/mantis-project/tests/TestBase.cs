using System;
using System.Text;
using System.Threading;
using NUnit.Framework;
using System.Linq;

namespace mantis_project
{
    public class TestBase
    {

        public bool PERFORM_LONG_UI_CHECKS = true;
        protected ApplicationManager app;

        [TestFixtureSetUp]
        public void SetupApplicationManager()
        {
            app = ApplicationManager.GetInstance();

        }

        public static Random rnd = new Random();

        //Генерируются любые из таблицы ASCII символы
        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                //builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 223)));
                builder.Append(Convert.ToChar(32 + rnd.Next(0, 90)));

            }
            return builder.ToString();
        }

        //Генерируются только заданные символы
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdeafhijklmnopqrtsuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }

        //Генерируются только цифры как символы
        public static string RandomDigital(int length)
        {
            const string chars = "1234567890";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[rnd.Next(s.Length)]).ToArray());
        }


        [TearDown]
        public void TeardownTest()
        {
            //app.Auth.Logout();
        }
    }
}

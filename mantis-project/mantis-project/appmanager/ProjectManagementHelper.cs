using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace mantis_project
{
    class ProjectManagementHelper : HelperBase
    {
        public ProjectManagementHelper(ApplicationManager manager) : base(manager)
        {
        }

        public void CreateProject(ProjectData project)
        {
            manager.Navigator.OpenManagement();
            manager.ManagementMenu.OpenProjectManagement();
            InitProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            
        }

        internal void RemoveProject(int v)
        {
            manager.Navigator.OpenManagement();
            manager.ManagementMenu.OpenProjectManagement();
            SelectProject(v);
            DeleteProject();
            CloseAlertDeleteProject();
        }


        internal void IsElementProjectAndCrate(ProjectData project)
        {
            manager.Navigator.OpenManagement();
            manager.ManagementMenu.OpenProjectManagement();
            if(!IsElementPresent(By.XPath("//tr[1]/td/a")))
            {
                CreateProject(project);
            }
        }

        private void SubmitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input[value='Добавить проект']")).Click();
            //new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElement(By.CssSelector("input[value='создать новый проект']")));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("input[value='создать новый проект']")));
        }

        private void FillProjectForm(ProjectData project)
        {
            Type(By.Name("name"), project.ProjectName);
            Type(By.Name("description"), project.ProjectDescription);
        }

        private void InitProjectCreation()
        {
            driver.FindElement(By.CssSelector("input[value='создать новый проект']")).Click();
        }

        private void CloseAlertDeleteProject()
        {
            driver.FindElement(By.CssSelector("input[value='Удалить проект']")).Click();
        }

        private void DeleteProject()
        {
            driver.FindElement(By.CssSelector("input[value='Удалить проект']")).Click();
        }

        private void SelectProject(int v)
        {
            driver.FindElement(By.XPath("//tr[" + (v + 1) + "]/td/a")).Click();
        }
        public List<ProjectData> GetProjectList()
        {
            List<ProjectData> projects = new List<ProjectData>();
            manager.Navigator.OpenManagement();
            manager.ManagementMenu.OpenProjectManagement();
            ICollection<IWebElement> elements = driver.FindElements(By.XPath("//div[2]/div[2]/div[1]/div/table/tbody/tr"));
            foreach (IWebElement element in elements)
            {
                ProjectData project = new ProjectData();
                project.ProjectName = element.FindElement(By.TagName("a")).Text;
                project.ProjectDescription = element.FindElement(By.XPath("//td[5]")).Text;
                projects.Add(project);
            }
            return projects;
        }
    }
}

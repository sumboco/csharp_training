using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_project
{
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData()
            {
                ProjectName = RandomString(10),
                ProjectDescription = RandomString(20)
            };

            List<ProjectData> oldProjects = app.ProjectManager.GetProjectList();
            app.ProjectManager.CreateProject(project);

            List<ProjectData> newProjects = app.ProjectManager.GetProjectList();
            oldProjects.Add(project);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);

            app.Auth.Logout();
        }

        [Test]
        public void APIProjectCreationTest()
        {
            ProjectData project = new ProjectData()
            {
                ProjectName = RandomString(10),
                ProjectDescription = RandomString(20)
            };

            List<ProjectData> oldProjects = app.API.APIGetProjectList(new AccountData("usernew", "password"));
            app.ProjectManager.CreateProject(project);

            List<ProjectData> newProjects = app.API.APIGetProjectList(new AccountData("usernew", "password"));
            oldProjects.Add(project);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);

            app.Auth.Logout();
        }
    }
}

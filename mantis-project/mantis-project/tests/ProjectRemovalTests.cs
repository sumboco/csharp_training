using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;

namespace mantis_project
{
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            //prepare
            ProjectData project = new ProjectData()
            {
                ProjectName = RandomString(10),
                ProjectDescription = RandomString(20)
            };
            app.ProjectManager.IsElementProjectAndCrate(project);

            //action
            List<ProjectData> oldProjects = app.ProjectManager.GetProjectList();
            app.ProjectManager.RemoveProject(0);

            List<ProjectData> newProjects = app.ProjectManager.GetProjectList();
            oldProjects.RemoveAt(0);

            oldProjects.Sort();
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);

            app.Auth.Logout();
        }
    }
}

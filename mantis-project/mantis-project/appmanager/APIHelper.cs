using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_project
{
    public class APIHelper : HelperBase
    {
        private string baseUrl;

        public APIHelper(ApplicationManager manager) : base(manager){ }

        public void CreateNewProject(AccountData account, ProjectData proj)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = proj.ProjectName;
            project.description = proj.ProjectDescription;
            client.mc_project_add(account.Username, account.Password, project);
        }

        public List<ProjectData> APIGetProjectList(AccountData account)
        {
            List<ProjectData> projects = new List<ProjectData>();
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            //elements = new Mantis.ProjectData();
            Mantis.ProjectData[] elements = client.mc_projects_get_user_accessible(account.Username, account.Password);
            //Mantis.ProjectData[] elements = client.mc_projects_get_user_accessible("administrator", "root");

            foreach (Mantis.ProjectData element in elements)
            {
                ProjectData project = new ProjectData();
                project.ProjectName = element.name;
                project.ProjectDescription = element.description;
                projects.Add(project);
            }
            return projects;
        }

        internal void APIIsElementProjectAndCrate(ProjectData project)
        {
            if (APIGetProjectList(new AccountData("usernew", "password")) == null)
            {
                CreateNewProject(new AccountData("usernew", "password"), project);
            }
        }
    }
}

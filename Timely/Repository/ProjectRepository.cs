using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timely.DatabaseContext;
using Timely.Models;

namespace Timely.Repository
{
    public class ProjectRepository
    {
        private readonly TimeContext _timeContext;
        public ProjectRepository(TimeContext timeContext)
        {
            _timeContext = timeContext;
        }

        public IEnumerable<Project> GetProjects()
        {
            return _timeContext.Projects.ToList();
        }

        public Project GetProject(int projectId)
        {
            return _timeContext.Projects.FirstOrDefault(project => project.ProjectId.Equals(projectId));
        }

        public void StartProject(Project project)
        {
            project.StartTime = DateTime.Now;
            _timeContext.Projects.Add(project);
            _timeContext.SaveChanges();
        }

        public Project EndProject(int projectId)
        {
            var project = GetProject(projectId);
            
            if (project != null)
            {
                project.EndTime = DateTime.Now;
                TimeSpan dur=(TimeSpan)(project.EndTime - project.StartTime);
                project.Duration = dur.TotalHours.ToString();
                _timeContext.SaveChanges();
            }
            return project;
        }

        public void AddName(Project project)
        {
            var projectToChange = GetProject(project.ProjectId);
            if (projectToChange != null)
            {
                projectToChange.ProjectName = project.ProjectName;
                projectToChange.EndTime = EndProject(project.ProjectId).EndTime;
                projectToChange.Duration = EndProject(project.ProjectId).Duration;

                _timeContext.SaveChanges();
            }

        }
    }
}

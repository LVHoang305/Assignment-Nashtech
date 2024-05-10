using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public interface IProjectsService
    {
        public Task<ActionResult<IEnumerable<Projects>>> GetProjects();
        public Projects GetProject(int id);
        public Projects CreateProjects(ProjectsDTO projects);
        public void UpdateProjects(int id, ProjectsDTO projects);
        public void DeleteProjects(int id);
        public ValidationResult ValidationProjects(int id, ProjectsDTO projects);

    }
}
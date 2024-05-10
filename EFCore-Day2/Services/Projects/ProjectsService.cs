using EFCore.Data;
using EFCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly EFCoreContext _context;
        private readonly IValidationService _validationService;

        public ProjectsService(EFCoreContext context, IValidationService validationService)
        {
            _context = context;
            _validationService = validationService;

        }
        /// <summary>
        /// Get all Departments Service
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
        {
            return  await _context.Projects.ToListAsync();
        }

        /// <summary>
        /// Get 1 Department Service
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Projects GetProject(int id)
        {
            var project = _context.Projects.Find(id);

            return project;
        }

        /// <summary>
        /// Update Service
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        public void UpdateProjects(int id, ProjectsDTO projects)
        {
            var project = _context.Projects.Find(id);
            if (project != null)
            {
                project.Name = project.Name;
            }
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
            }
        }

        /// <summary>
        /// API Create
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPost]
        public Projects CreateProjects(ProjectsDTO projects)
        {
            var newProjects = new Projects
            {
                Name = projects.Name
            };
            _context.Projects.Add(newProjects);
            _context.SaveChanges();

            return newProjects;
        }

        /// <summary>
        /// API delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public void DeleteProjects(int id)
        {
            var projects = _context.Projects.Find(id);
            if (projects != null)
            {
                _context.Projects.Remove(projects);
                _context.SaveChanges();
            }
        }
        public ValidationResult ValidationProjects(int id, ProjectsDTO projects) 
        {
            ValidationResult validationResult = new ValidationResult
            {
                IsValid = true,
                Message = ""
            };

            ValidationResult validationIdResult = _validationService.ValidationId(id, 2);
            if (!validationIdResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationIdResult.Message;
            }

            ValidationResult validationNameResult = _validationService.ValidationName(projects.Name);
            if (!validationNameResult.IsValid)
            {
                validationResult.IsValid = false;
                validationResult.Message = validationResult.Message + validationNameResult.Message;
            }

            return validationResult;
        }
    }
}
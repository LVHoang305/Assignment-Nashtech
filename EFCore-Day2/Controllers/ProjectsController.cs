using Microsoft.AspNetCore.Mvc;
using EFCore.Data;
using EFCore.Models;
using EFCore.Services;

namespace EFCore.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly EFCoreContext _context;
        private readonly IProjectsService _projectsService;

        public ProjectsController(EFCoreContext context, IProjectsService projectsService)
        {
            _context = context;
            _projectsService = projectsService;
        }

        // GET: api/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Projects>>> GetProjects()
        {
            return await _projectsService.GetProjects();
        }


        /// <summary>
        /// Update
        /// </summary>
        /// <param name="id"></param>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProjects(int id, ProjectsDTO projects)
        {
            ValidationResult validationResult = _projectsService.ValidationProjects(id, projects);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _projectsService.UpdateProjects(id, projects);

            return CreatedAtAction(nameof(GetProjects), _context.Projects);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Projects>> CreateProjects(ProjectsDTO projects)
        {
            ValidationResult validationResult = _projectsService.ValidationProjects(1, projects);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            var newProjects = _projectsService.CreateProjects(projects);

            return CreatedAtAction(nameof(GetProjects), newProjects);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProjects(int id)
        {
            ValidationResult validationResult = _projectsService.ValidationProjects(id, new ProjectsDTO { Name = "A" });
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Message);
            }

            _projectsService.DeleteProjects(id);

            return CreatedAtAction(nameof(GetProjects), _context.Departments);
        }
    }
}

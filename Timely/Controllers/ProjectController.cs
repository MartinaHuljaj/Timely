using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timely.Models;
using Timely.Repository;

namespace Timely.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly ProjectRepository _projectRepository;
        public ProjectController(ProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IActionResult GetProjects()
        {
            try
            {
                var projects = _projectRepository.GetProjects();
                return Ok(projects);
            }
            catch (SystemException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public IActionResult StartProject([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();
            try
            {
                _projectRepository.StartProject(project);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{projectId:int}")]
        public IActionResult GetFlight(int projectId)
        {
            try
            {
                return Ok(_projectRepository.GetProject(projectId));
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public IActionResult AddName([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();
            try
            {
                _projectRepository.AddName(project);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{projectId:int}")]
        public IActionResult EndProject(int projectId)
        {
            try
            {
                _projectRepository.EndProject(projectId);
                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

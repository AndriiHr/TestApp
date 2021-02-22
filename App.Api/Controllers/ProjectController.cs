using System.Threading.Tasks;
using App.Application.Projects;
using App.Application.Projects.Commands;
using App.Application.Projects.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Api.Controllers
{
    [Route("api/project")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }


        [Route("")]
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
        {
            var projectDto = _mapper.Map<ProjectDto>(request);
            await _mediator.Send(new CreateProjectCommand(projectDto));

            return Ok();
        }

        [Route("projects")]
        [HttpGet]
        public async Task<IActionResult> GetRpojects([FromRoute] bool active = true)
        {
            var projects = await _mediator.Send(new GetProjectsQuery(active));

            return Ok(projects);
        }

        [Route("{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetProject([FromRoute] int projectId)
        {
            var project = await _mediator.Send(new GetProjectQuery(projectId));

            return Ok(project);
        }

        [Route("")]
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
        {
            await _mediator.Send(new UpdateProjectCommand(request.Project));

            return Ok();
        }


        [Route("{projectId}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProject([FromRoute] int projectId)
        {
            await _mediator.Send(new DeleteProjectCommand(projectId));

            return Ok();
        }

        [Route("{userId}/{projectId}")]
        [HttpPut]
        public async Task<IActionResult> AssignProjectToUser([FromRoute] int userId, [FromRoute] int projectId)
        {
            await _mediator.Send(new AssignUserToProjectCommand(projectId, userId));

            return Ok();
        }
    }
}
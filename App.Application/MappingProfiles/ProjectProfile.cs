using System.Collections.Generic;
using App.Application.Projects;
using App.Application.Projects.Commands;
using App.Domain.Projects;
using AutoMapper;

namespace App.Application.MappingProfiles
{
    public class ProjectProfile:Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
            
            CreateMap<List<Project>, List<ProjectDto>>();
            CreateMap<List<ProjectDto>, List<Project>>();

            CreateMap<CreateProjectRequest, ProjectDto>();
        }
    }
}
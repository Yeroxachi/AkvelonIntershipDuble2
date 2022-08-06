using System;
using System.Collections.Generic;
using System.Linq;
using AkvelonIntershipDuble2.Context;
using AkvelonIntershipDuble2.DTO;
using AkvelonIntershipDuble2.Entities;
using AkvelonIntershipDuble2.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AkvelonIntershipDuble2.Controllers
{
    [ApiController,Route("[Controller]")]
    public class ProjectController : Controller
    {
        private readonly ManagementContext _context;

        public ProjectController(ManagementContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [Route("Project/AddProject")]
        public ActionResult AddProject([FromBody] ProjectDto projectDto)
        {
            var project = new Project()
            {
                ProjectName = projectDto.ProjectName,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                Priority = projectDto.Priority,
                ProjectStatus = Enum.Parse<ProjectStatus>(projectDto.ProjectStatus)
            };
            
            _context.Projects.Add(project);
            _context.SaveChanges();
            var projectResponse = new ProjectResponse()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                ProjectStatus = project.ProjectStatus.ToString(),
            };
            
            return Ok(projectResponse);
        }

        [HttpDelete]
        [Route("DeleteProject/{id}")]
        public ActionResult DeleteProject([FromRoute] int id)
        {
            var project = _context.Projects.FirstOrDefault(project => project.ProjectId == id);
            if (project == null)
            {
                return NotFound("Not found by this id try with another next time");
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            return Ok("Chiki Puki");
        }

        [HttpPost]
        [Route("Project/EditProject/{id}")]
        public ActionResult EditProject([FromRoute] int id, [FromBody] ProjectDto projectDto)
        {
            var project = _context.Projects.FirstOrDefault(project => project.ProjectId == id);
            if (project == null)
            {
                return NotFound("Not found by this id try with another next time");
            }

            project.ProjectName = projectDto.ProjectName;
            project.StartDate = projectDto.StartDate;
            project.EndDate = projectDto.EndDate;
            project.Priority = projectDto.Priority;
            project.ProjectStatus = Enum.Parse<ProjectStatus>(projectDto.ProjectStatus);

            _context.Projects.Update(project);
            _context.SaveChanges();
            var projectResponse = new ProjectResponse()
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                ProjectStatus = project.ProjectStatus.ToString(),
            };
            return Ok(projectResponse);
        }
        
        [HttpGet]
        [Route("Project/ViewProject")]
        public List<ProjectResponse> ViewProjects()
        {
            List<ProjectResponse> allProjects = _context.Projects.Select(project => new ProjectResponse
            {
                ProjectId = project.ProjectId,
                ProjectName = project.ProjectName,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Priority = project.Priority,
                ProjectStatus = project.ProjectStatus.ToString()
            }).ToList();
            return allProjects;
        }

        [HttpGet]
        [Route("ViewAllTaskOfProject/{projectId}")]
        public ActionResult<List<ProjectTaskResponse>> AllTasksOfProject([FromRoute] int projectId)
        {
            var project = _context.Projects
                .Where(project => project.ProjectId == projectId)
                .Include(project => project.Tasks)
                .FirstOrDefault();

            if (project == null)
            {
                return NotFound();
            }
            
            return project.Tasks.Select(task =>new ProjectTaskResponse
            {
                ProjectId = task.Project.ProjectId,
                Priority = task.Priority,
                ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                TaskDescription = task.TaskDescription,
                TaskId = task.TaskId,
                TaskName = task.TaskName
            }).ToList();
        }

        [HttpGet]
        [Route("SortByParametr{byWhat}")]
        public ActionResult<List<ProjectResponse>> SortByParametr([FromRoute] string byWhat)
        {
            var projects = _context.Projects;
            if (projects == null)
            {
                return NotFound("No objects on Db check it pls");
            }
            if (byWhat == "Name")
            {
                 var allProjects = _context.Projects.Select(project => new ProjectResponse
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority,
                    ProjectStatus = project.ProjectStatus.ToString()
                }).OrderBy(response => response.ProjectName).ToList();
                 return Ok(allProjects);
            }
            if (byWhat == "StartDate")
            {
                var allProjects = _context.Projects.Select(project => new ProjectResponse
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority,
                    ProjectStatus = project.ProjectStatus.ToString()
                }).OrderBy(response => response.StartDate).ToList();
                return Ok(allProjects);
            }
            if (byWhat == "EndDate")
            {
                var allProjects = _context.Projects.Select(project => new ProjectResponse
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority,
                    ProjectStatus = project.ProjectStatus.ToString()
                }).OrderBy(response => response.EndDate).ToList();
                return Ok(allProjects);
            }
            if (byWhat == "Priority")
            {
                var allProjects = _context.Projects.Select(project => new ProjectResponse
                {
                    ProjectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    Priority = project.Priority,
                    ProjectStatus = project.ProjectStatus.ToString()
                }).OrderBy(response => response.Priority).ToList();
                return Ok(allProjects);
            }

            return NotFound("This Key Word Not Found");
        }
    }
}
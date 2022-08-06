using System;
using System.Collections.Generic;
using System.Linq;
using AkvelonIntershipDuble2.Context;
using AkvelonIntershipDuble2.DTO;
using AkvelonIntershipDuble2.Entities;
using AkvelonIntershipDuble2.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AkvelonIntershipDuble2.Controllers
{
    [ApiController,Route("[Controller]")]
    public class TaskController : Controller
    {
        private readonly ManagementContext _context;

        public TaskController(ManagementContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        [Route("Add")]
        public ActionResult AddTask([FromBody]TaskDto taskDto)
        {
           
            if (!_context.Projects.Any(project => project.ProjectId == taskDto.ProjectId))
            {
                return NotFound("Project by given id not found");
            }

            ProjectTask task = new ProjectTask
            {
                TaskName = taskDto.TaskName,
                TaskDescription = taskDto.Description,
                Priority = taskDto.Priority,
                ProjectTaskStatus = Enum.Parse<ProjectTaskStatus>(taskDto.TaskStatus),
                Project = _context.Projects.First(project => project.ProjectId == taskDto.ProjectId)
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();
            var taskResponse = new ProjectTaskResponse()
            {
                TaskName = task.TaskName,
                TaskId = task.TaskId,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority,
                ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                ProjectId = task.Project.ProjectId
            };

            return Ok(taskResponse);
        }
        
        [HttpDelete]
        [Route("DeleteTask/{id}")]
        public ActionResult DeleteTask([FromRoute]int id)
        {
            var task = _context.Tasks.FirstOrDefault(task => task.TaskId == id);

            if (task == null)
            {
                return NotFound("Not found by this id try with another next time");
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok("Chiki puki");
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult EditTask([FromRoute] int id, [FromBody] TaskDto taskDto)
        {
            var task = _context.Tasks.FirstOrDefault(task => task.TaskId == id);
            var project = _context.Projects.FirstOrDefault(project => project.ProjectId == taskDto.ProjectId);
            if(task == null)
            {
                return NotFound("Not found by this task id try with another next time");
            }
            if (project == null)
            {
                return NotFound("Not found by this project id try with another next time");
            }
            task.TaskName = taskDto.TaskName;
            task.Priority = taskDto.Priority;
            task.TaskDescription = taskDto.Description;
            task.ProjectTaskStatus = Enum.Parse<ProjectTaskStatus>(taskDto.TaskStatus);
            task.Project = project;
            
            _context.Tasks.Update(task);
            _context.SaveChanges();
            
            var taskResponse = new ProjectTaskResponse()
            {
                TaskName = task.TaskName,
                TaskId = task.TaskId,
                TaskDescription = task.TaskDescription,
                Priority = task.Priority,
                ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                ProjectId = task.Project.ProjectId
            };
            
            return Ok(taskResponse);
        }

        [HttpGet]
        [Route("Get")]
        public ActionResult<List<ProjectTaskResponse>> ViewTasks()
        {
            var allTasks = _context.Tasks.Select(task => new ProjectTaskResponse
            {
                Priority = task.Priority,
                ProjectId = task.Project.ProjectId,
                ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                TaskDescription = task.TaskDescription,
                TaskName = task.TaskName,
                TaskId = task.TaskId
            }).ToList();
            return Ok(allTasks);
        }
        
        [HttpGet]
        [Route("SortByParametr{byWhat}")]
        public ActionResult<List<ProjectTaskResponse>> SortByParametr([FromRoute] string byWhat)
        {
            var projects = _context.Projects;
            if (projects == null)
            {
                return NotFound("No objects in db on table Project tasks");
            }

            if(byWhat == "Name")
            {
                var allTasks = _context.Tasks.Select(task => new ProjectTaskResponse
                {
                    Priority = task.Priority,
                    ProjectId = task.Project.ProjectId,
                    ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                    TaskDescription = task.TaskDescription,
                    TaskName = task.TaskName,
                    TaskId = task.TaskId
                }).OrderBy(response => response.TaskName);
                return Ok(allTasks);
            }
            if (byWhat == "Prority")
            {
                var allTasks = _context.Tasks.Select(task => new ProjectTaskResponse
                {
                    Priority = task.Priority,
                    ProjectId = task.Project.ProjectId,
                    ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                    TaskDescription = task.TaskDescription,
                    TaskName = task.TaskName,
                    TaskId = task.TaskId
                }).OrderBy(response => response.Priority);
                return Ok(allTasks);
            }
            if (byWhat == "Description")
            {
                var allTasks = _context.Tasks.Select(task => new ProjectTaskResponse
                {
                    Priority = task.Priority,
                    ProjectId = task.Project.ProjectId,
                    ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                    TaskDescription = task.TaskDescription,
                    TaskName = task.TaskName,
                    TaskId = task.TaskId
                }).OrderBy(response => response.TaskDescription);
                return Ok(allTasks);
            }
            
            if (byWhat == "Status")
            {
                var allTasks = _context.Tasks.Select(task => new ProjectTaskResponse
                {
                    Priority = task.Priority,
                    ProjectId = task.Project.ProjectId,
                    ProjectTaskStatus = task.ProjectTaskStatus.ToString(),
                    TaskDescription = task.TaskDescription,
                    TaskName = task.TaskName,
                    TaskId = task.TaskId
                }).OrderBy(response => response.ProjectTaskStatus);
                return Ok(allTasks);
            }

            return NotFound("This Key Word Not Found");
        }
    }
}
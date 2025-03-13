using System;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApi.Services;
using TaskManagerApi.Models;

namespace TaskManagerApi.Controllers;


[ApiController]
[Route("api/task")]
public class TaskManagerController : ControllerBase
{
    private readonly ITaskManagerService _taskManagerService;

    public TaskManagerController(ITaskManagerService taskManagerService)
    {
        _taskManagerService = taskManagerService;
    }

    [HttpPost]
    public IActionResult AddTask(TaskItem task)
    {
        var result = _taskManagerService.AddTask(task);
        return Ok(result);
    }

    [HttpPut]
    public IActionResult EditTask(TaskItem task)
    {
        var result = _taskManagerService.EditTask(task);
        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetTask(Guid taskId)
    {
        var result = _taskManagerService.GetTask(taskId);
        return Ok(result);
    }

}

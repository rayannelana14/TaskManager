using Xunit;
using TaskManagerApi.Models;
using TaskManagerApi.Enums;
using TaskManagerApi.Services;
using TaskManagerApi.Exceptions;
using TaskManagerApi.Repository;
using FluentAssertions;
using System.Threading.Tasks;

namespace TaskManagerApi.Tests;

public class TaskManageTests
{
    [Fact]
    public void AddTask()
    {
        // Arrange
        var taskService = new TaskManagerService(new TaskManagerRepository());
        var task = new TaskItem { Id = Guid.NewGuid(), Title = "Task 1", Description = "Task 1 Description", Status = Status.Completed, CreationDate = DateTime.Now, Priority = Priority.High };

        // Act
        var result = taskService.AddTask(task);

        // Assert
        Assert.Equal(task.Id, result.Id);
    }

    [Fact]
    public void AddTask_WhenTaskAlreadyExist_ShouldThrowDuplicateTaskException()
    {
        //Arrange
        var taskService = new TaskManagerService(new TaskManagerRepository());
        var id = Guid.NewGuid();
        var task1 = new TaskItem { Id = id, Description = "Task 1", Status = Status.Completed, CreationDate = DateTime.Now, Priority = Priority.High };
        var task2 = new TaskItem { Id = id, Description = "Task 2", Status = Status.Pending, CreationDate = DateTime.Now, Priority = Priority.Low };

        //Act
        taskService.AddTask(task1);

        //Assert
        var exception = Assert.Throws<TaskAlreadyExistsException>(() => taskService.AddTask(task2));
        Assert.Equal($"The Task Id {task2.Id} already exists", exception.Message);
    }

    [Fact]
    public void EditTask()
    {
        //Arrange
        var taskService = new TaskManagerService(new TaskManagerRepository());
        var id = Guid.NewGuid();

        var task = new TaskItem { Id = id, Description = "Task to edit", Status = Status.Canceled, CreationDate = DateTime.Now, Priority = Priority.Low };

        taskService.AddTask(task);
        task.Status = Status.InProgress;

        //Act
        var result = taskService.EditTask(task);

        //Assert
        result.Should().BeEquivalentTo(task, options => options
            .Excluding(t => t.Status));
        Assert.Equal(Status.InProgress, result.Status);
    }

    [Fact]
    public void EditTask_WhenTaskDoesNotExist_ShoudlReturnAnException()
    {
        //Arrange
        var taskService = new TaskManagerService(new TaskManagerRepository());
        var task = new TaskItem { Id = Guid.NewGuid(), Description = "Task to edit", Status = Status.Canceled, CreationDate = DateTime.Now, Priority = Priority.Low };

        //Assert
        var exception = Assert.Throws<TaskDoesNotExistException>(() => taskService.EditTask(task));
        Assert.Equal($"The Task Id {task.Id} dosen't exists", exception.Message);
    }

    [Fact]
    public void GetTask()
    {
        //Arrange
        var taskService = new TaskManagerService(new TaskManagerRepository());
        var id = Guid.NewGuid();
        var task = new TaskItem { Id = id, Description = "Task to Get", Status = Status.Pending, CreationDate = DateTime.Now, Priority = Priority.Medium };

        //Act
        taskService.AddTask(task);
        var result = taskService.GetTask(id);

        //Assert
        Assert.Equal(result, task);
    }

    [Fact]
    public void GetTask_WhenTaskDoesNotExist_ShoudlReturnNull()
    {
        //Arrange
        var taskService = new TaskManagerService(new TaskManagerRepository());
        var id = Guid.NewGuid();

        //Act
        var result = taskService.GetTask(id);

        //Assert
        Assert.Null(result);
    }
}
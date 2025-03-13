using TaskManagerApi.Exceptions;
using TaskManagerApi.Models;
using TaskManagerApi.Repository;

namespace TaskManagerApi.Services;

public class TaskManagerService : ITaskManagerService
{
    private readonly ITaskManagerRepository _taskManagerRepository;

    public TaskManagerService(ITaskManagerRepository taskManagerRepository)
    {
        _taskManagerRepository = taskManagerRepository;
    }
    public TaskItem AddTask(TaskItem task)
    {
        if (_taskManagerRepository.TaskExists(task.Id))
        {
            throw new TaskAlreadyExistsException(task.Id);
        }

        _taskManagerRepository.AddTask(task);

        return task;
    }

    public TaskItem EditTask(TaskItem task)
    {
        if (!_taskManagerRepository.TaskExists(task.Id))
        {
            throw new TaskDoesNotExistException(task.Id);
        }
        _taskManagerRepository.AddTask(task);

        return task;
    }

    public TaskItem? GetTask(Guid taskId)
    {
        if (!_taskManagerRepository.TaskExists(taskId))
        {
            return null;
        }

        var result = _taskManagerRepository.GetTask(taskId);
        return result;
    }
}

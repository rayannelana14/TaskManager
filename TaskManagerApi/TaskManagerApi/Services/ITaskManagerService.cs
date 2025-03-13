using TaskManagerApi.Models;

namespace TaskManagerApi.Services;

public interface ITaskManagerService
{
    TaskItem AddTask(TaskItem task);

    TaskItem EditTask(TaskItem task);

    TaskItem? GetTask(Guid taskId);
}

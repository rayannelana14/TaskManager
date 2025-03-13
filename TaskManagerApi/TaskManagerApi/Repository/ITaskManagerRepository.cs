using TaskManagerApi.Models;

namespace TaskManagerApi.Repository;

public interface ITaskManagerRepository
{
    bool AddTask(TaskItem task);
    bool TaskExists(Guid taskId);

    TaskItem GetTask(Guid taskId);
}

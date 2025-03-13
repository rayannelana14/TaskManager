using System.Threading.Tasks;

namespace TaskManagerApi.Exceptions;

public class TaskAlreadyExistsException : Exception
{
    public Guid TaskId { get; }
    public TaskAlreadyExistsException() { }

    public TaskAlreadyExistsException(string message)
        : base(message) { }

    public TaskAlreadyExistsException(string message, Exception inner)
        : base(message, inner) { }

    public TaskAlreadyExistsException(Guid taskId)
       : base($"The Task Id {taskId} already exists")
    {
        TaskId = taskId;
    }
}

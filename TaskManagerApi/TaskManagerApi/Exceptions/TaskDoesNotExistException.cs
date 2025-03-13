namespace TaskManagerApi.Exceptions;

public class TaskDoesNotExistException : Exception
{
    public Guid TaskId { get; }
    public TaskDoesNotExistException() { }

    public TaskDoesNotExistException(string message)
        : base(message) { }

    public TaskDoesNotExistException(string message, Exception inner)
        : base(message, inner) { }

    public TaskDoesNotExistException(Guid taskId)
       : base($"The Task Id {taskId} dosen't exists")
    {
        TaskId = taskId;
    }
}

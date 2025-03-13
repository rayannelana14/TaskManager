using TaskManagerApi.Enums;

namespace TaskManagerApi.Models;
public struct TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Status Status { get; set; }
    public DateTime CreationDate { get; set; }
    public Priority Priority { get; set; }
}
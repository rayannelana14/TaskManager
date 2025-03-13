using TaskManagerApi.Models;
using System.Text.Json;
using System.Linq;

namespace TaskManagerApi.Repository;

public class TaskManagerRepository : ITaskManagerRepository
{
    private readonly string _filePath;

    public TaskManagerRepository()
    {
        string directory = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Database");
        Directory.CreateDirectory(directory);
        _filePath = Path.Combine(directory, "tasks.json");
    }

    public bool AddTask(TaskItem task)
    {
        var tasks = LoadTasks();
        tasks.Add(task);
        SaveTasks(tasks);

        return true;
    }

    public TaskItem GetTask(Guid taskId) 
    {
        var tasks = LoadTasks();
        return tasks.FirstOrDefault(t => t.Id == taskId);
    }

    public bool TaskExists(Guid taskId)
    {
        var tasks = LoadTasks();
        return tasks.Any(t => t.Id == taskId);
    }

    private List<TaskItem> LoadTasks()
    {
        if (!File.Exists(_filePath))
        {
            return new List<TaskItem>();
        }

        string json = File.ReadAllText(_filePath);
        var tasks = new List<TaskItem>();

        if (!string.IsNullOrEmpty(json))
        {
            tasks = JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }

        return tasks;
    }

    private void SaveTasks(List<TaskItem> tasks)
    {
        string json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}

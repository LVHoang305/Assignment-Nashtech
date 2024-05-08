using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASPNETCoreAPI_Day1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private static List<ASPNETCoreAPI_Day1.Models.Task> _tasks = new List<ASPNETCoreAPI_Day1.Models.Task>();

        [HttpGet("all")]
        public IActionResult GetAllTasks()
        {
            return Ok(_tasks);
        }

        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(ASPNETCoreAPI_Day1.Models.ViewTask task)
        {
            _tasks.Add(new ASPNETCoreAPI_Day1.Models.Task { Id = _tasks.Count + 1, Title = task.Title, IsCompleted = task.IsCompleted });
            var route = CreatedAtAction(nameof(GetTaskById), new { id = _tasks.Count }, task);
            return Ok(route);
        }

        [HttpPost("bulk")]
        public IActionResult CreateMultiTask(List<ASPNETCoreAPI_Day1.Models.ViewTask> tasks)
        {
            foreach (ASPNETCoreAPI_Day1.Models.ViewTask task in tasks)
            {
                _tasks.Add(new ASPNETCoreAPI_Day1.Models.Task { Id = _tasks.Count + 1, Title = task.Title, IsCompleted = task.IsCompleted });
            };
            //return NoContent();
            return CreatedAtAction(nameof(GetAllTasks), _tasks);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, ASPNETCoreAPI_Day1.Models.Task updatedTask)
        {
            var index = _tasks.FindIndex(t => t.Id == id);
            if (index == -1)
            {
                return NotFound();
            }
            _tasks[index] = updatedTask;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            _tasks.Remove(task);
            return NoContent();
        }

        [HttpDelete("bulk")]
        public IActionResult DeleteMultiTask(List<int> ids)
        {
            foreach (int id in ids)
            {
                var task = _tasks.Find(t => t.Id == id);
                if (task == null)
                {
                    return NotFound();
                }
                _tasks.Remove(task);
            }
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.Core.Models;
using SchoolManagement.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.Mvc.Controllers
{
    //web api 增删改查
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        //注入仓储服务, TodoItem的主键Id为long类型,仓储服务参数也需要对应一致
        private readonly IRepository<TodoItem, long> _todoItemRepository;
        public TodoItemController(IRepository<TodoItem, long> todoRepository)
        {
            this._todoItemRepository = todoRepository;
        }
        // GET:api/TodoItem
        [HttpGet]
        public async Task<ActionResult<List<TodoItem>>> GetTodo()
        { //获取所有的待办事项列表
            var models = await _todoItemRepository.GetAllListAsync();
            return models;
        }

        #region 根据Id获取待办事项
        // GET:api/TodoItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoItemRepository.FirstOrDefaultAsync(a => a.Id == id);
            if (todoItem == null)
            { //返回404状态码
                return NotFound();
            }
            return todoItem;
        }
        #endregion 根据Id获取待办事项
        #region 更新待办事项
        // PUT:api/TodoItem/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            await _todoItemRepository.UpdateAsync(todoItem);
            //返回状态码204
            return todoItem;
        }
        #endregion 更新待办事项
        #region 添加待办事项
        // POST:api/TodoItem
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _todoItemRepository.InsertAsync(todoItem);
            return todoItem;
        }

        #endregion 添加待办事项
        #region 删除指定Id的待办事项
        // DELETE:api/TodoItem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(int id)
        {
            var todoItem = await _todoItemRepository.FirstOrDefaultAsync(a => a.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            await _todoItemRepository.DeleteAsync(todoItem);
            return todoItem;
        }
        #endregion 删除指定Id的待办事项
    }
}

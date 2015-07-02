using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRedisChapter1
{
    public class Todo
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public int Order { get; set; }

        public bool Done { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            using (var redisManager = new PooledRedisClientManager("192.168.0.128:6379"))
            {
                using (var redis = redisManager.GetClient())
                {
                    var redisTodos = redis.As<Todo>();
                    var todo = new Todo
                    {
                        Id = redisTodos.GetNextSequence(),
                        Content = "Learn Redis",
                        Order = 1
                    };

                    redisTodos.Store(todo);

                    Todo savedTodo = redisTodos.GetById(todo.Id);
                    savedTodo.Done = true;
                    redisTodos.Store(savedTodo);

                }
            }
        }
    }
}

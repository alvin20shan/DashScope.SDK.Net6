using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashScope.SDK.Net6.Utils
{
    public abstract class Endpoint
    {
        public string Task { get; set; }

        public string TaskGroup { get; set; }

        public string Function { get; set; }

        internal Endpoint(string task, string taskgroup, string funciton)
        {
            Task = task;
            TaskGroup = taskgroup;
            Function = funciton;
        }

    }


    public class QwenEndpoint : Endpoint
    {
        public QwenEndpoint() : base("aigc", "text-generation", "generation")
        {
        }
    }

    public class EmbeddingsEndpoint : Endpoint
    {
        public EmbeddingsEndpoint() : base("embeddings", "text-embedding", "text-embedding")
        {
        }
    }
}

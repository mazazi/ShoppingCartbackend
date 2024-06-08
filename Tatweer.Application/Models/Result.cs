using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tatweer.Application.Models
{
    public class Result
    {
        public bool Succeeded { get; set; }
        public object Data { get; set; }
        public string[] Errors { get; set; }

        internal Result(bool succeeded, IEnumerable<string> erros)
        {
            Succeeded = succeeded;
            Errors = erros.ToArray();
        }

        internal Result(bool succeeded, object data, IEnumerable<string> erros)
        {
            Succeeded = succeeded;
            Data = data;
            Errors = erros.ToArray();
        }

        public static Result Success()
        {
            return new Result(true, new string[] { });
        }

        public static Result Success(object data)
        {
            return new Result(true, data, new string[] { });
        }

        public static Result Failure(params string[] errors)
        {
            return new Result(false, errors);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anMvp.Comman.Result
{
    public class Result
    {
        // Code 200 / Message success / Success bool 
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }

        public static Result Ok(string message = "success")
        {
            return new Result
            {
                Code = 200,
                Message = message,
                Success = true
            };
        }
        public static Result Fail(string message = "fial",int code = 500)
        {
            return new Result
            {
                Code = code,
                Message = message,
                Success = false
            };
        }
        
    }

    public class Result<T>
    {
        // Code 200 / Message success / Success bool /Data T /
        public int Code { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public T? Data { get; set; }

        public static Result<T> Ok(T data ,string message = "success")
        {
            return new Result<T>
            {
                Code = 200,
                Message = message,
                Success = true,
                Data = data
            };
        }
        public static Result<T> Fail(int code,string message = "fail")
        {
            return new Result<T>
            {
                Code = code,
                Message = message,
                Success = true,
                Data = default
            };
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagerAPI.Service.Models
{
    public class ServiceResponse<T>
    {
        public readonly T? Data;

        public readonly bool IsSuccess;

        public readonly string? Message;

        public ServiceResponse(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public ServiceResponse(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        public ServiceResponse()
        {
            IsSuccess = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects
{
    public class ApiResponse
    {
        public int StatusCode = 200;
        public string? Data { get; set; }

        public ApiResponse(int code, string? DataInput)
        {
            StatusCode = code;
            Data = DataInput;
        }

    }
}

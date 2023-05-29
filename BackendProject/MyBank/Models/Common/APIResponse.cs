using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Models.Common
{
    public class APIResponse
    {
        public APIResponse()
        {
        }
        public int Code { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
        public List<object> ListResult { get; set; }

        public bool isSuccess { get; set; }

        public APIResponse(bool isSuccess, int statusCode, string message, object payload)
        {
            this.isSuccess = isSuccess;
            this.Code = statusCode;
            this.Message = message;
            this.Result = payload;
        }

    }
}

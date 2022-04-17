using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Common.BaseResponse
{
    [Serializable]
    public class BaseResponseModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int StatusCode { get; set; }
    }
}

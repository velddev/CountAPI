using System;
using System.Collections.Generic;
using System.Text;

namespace CountAPI.Common
{
    class Response
    {
		int ErrorCode { get; set; }
		string Message { get; set; }

		public Response(int code, string message)
		{
			ErrorCode = code;
			Message = message;
		}
    }
}

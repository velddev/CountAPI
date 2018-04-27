using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountAPI.Results
{
    public class ErrorResult : JsonResult
    {
		public ErrorResult(int code, string message) : base(new
		{
			ErrorCode = code,
			Message = message
		})
		{ } 
    }
}

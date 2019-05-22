using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Models
{
    public class Result <T>
    {
        public int StatusCode { get; set; }
        public bool Correct { get; set; }
        public string Message { get; set; }
        public string FullStackTrace { get; set; }
        public T Data { get; set; }

        public Result ()
        {

        }
    }
}
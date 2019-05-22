using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Models
{
    public class Device
    {
        public string Id = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string IconName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public bool IsOn { get; set; }
        public string UserEmail { get; set; }

        public Device()
        {

        }
    }
}

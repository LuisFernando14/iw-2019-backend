using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TutorialMoya001.Repositories
{
    public interface IImagenesRepository
    {
        Task<string> SaveImage(string name, string contentType, Stream image);
    }
}

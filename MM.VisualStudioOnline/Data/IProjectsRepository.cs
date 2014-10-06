using System.Collections.Generic;
using System.Threading.Tasks;
using MM.VisualStudioOnline.Models;

namespace MM.VisualStudioOnline.Data
{
    internal interface IProjectsRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
    }
}
using global::GSMicroServices.Model;
using GSMicroServices.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMicroServices.Services
{
    public interface IPromptService
    {
        Task<IEnumerable<Prompt>> GetAll();
        Task<Prompt?> GetById(int id);
        Task<Prompt> Create(Prompt prompt);
        Task<bool> Update(int id, Prompt prompt);
        Task<bool> Delete(int id);
    }
}


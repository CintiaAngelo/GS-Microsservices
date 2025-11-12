
using GSMicroServices.Model;
using Microsoft.EntityFrameworkCore;
using GSMicroServices.Data;
using GSMicroServices.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSMicroServices.Services
{
    public class PromptService : IPromptService
    {
        private readonly PromptContext _context;

        public PromptService(PromptContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Prompt>> GetAll()
        {
            return await _context.Prompt.ToListAsync();
        }

        public async Task<Prompt?> GetById(int id)
        {
            return await _context.Prompt.FirstOrDefaultAsync(p => p.IdPrompt == id);
        }

        public async Task<Prompt> Create(Prompt prompt)
        {
            _context.Prompt.Add(prompt);
            await _context.SaveChangesAsync();
            return prompt;
        }

        public async Task<bool> Update(int id, Prompt prompt)
        {
            var existingPrompt = await _context.Prompt.FindAsync(id);
            if (existingPrompt == null)
                return false;

            existingPrompt.Nome = prompt.Nome;
            existingPrompt.Descricao = prompt.Descricao;
            existingPrompt.Versao = prompt.Versao;
            existingPrompt.DataCriacao = prompt.DataCriacao;
            existingPrompt.Autor = prompt.Autor;
            existingPrompt.TipoModelo = prompt.TipoModelo;
            existingPrompt.StatusPrompt = prompt.StatusPrompt;

            _context.Prompt.Update(existingPrompt);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var prompt = await _context.Prompt.FindAsync(id);
            if (prompt == null)
                return false;

            _context.Prompt.Remove(prompt);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}


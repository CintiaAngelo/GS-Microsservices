
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
        private readonly ILogger<PromptService> _logger;

        public PromptService(PromptContext context, ILogger<PromptService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Prompt>> GetAll()
        {
            try
            {
                return await _context.Prompt.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter lista de Prompts");
                throw new Exception("Ocorreu um erro ao buscar os prompts. Verifique o log para mais detalhes.");
            }
        }

        public async Task<Prompt?> GetById(int id)
        {
            try
            {
                return await _context.Prompt.FirstOrDefaultAsync(p => p.IdPrompt == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao buscar Prompt com Id {IdPrompt}", id);
                throw new Exception("Erro ao buscar o prompt especificado.");
            }
        }

        public async Task<Prompt> Create(Prompt prompt)
        {
            try
            {
                await _context.Prompt.AddAsync(prompt);
                await _context.SaveChangesAsync();
                return prompt;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro de banco de dados ao criar Prompt");
                throw new Exception("Erro ao salvar o prompt no banco de dados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao criar Prompt");
                throw new Exception("Erro inesperado ao criar o prompt.");
            }
        }

        public async Task<bool> Update(int id, Prompt prompt)
        {
            try
            {
                var existingPrompt = await _context.Prompt.FindAsync(id);
                if (existingPrompt == null)
                {
                    _logger.LogWarning("Tentativa de atualizar Prompt inexistente com Id {IdPrompt}", id);
                    return false;
                }

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
            catch (DbUpdateConcurrencyException dbEx)
            {
                _logger.LogError(dbEx, "Erro de concorrência ao atualizar Prompt com Id {IdPrompt}", id);
                throw new Exception("Erro de concorrência ao atualizar o prompt.");
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro de banco de dados ao atualizar Prompt com Id {IdPrompt}", id);
                throw new Exception("Erro ao atualizar o prompt no banco de dados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao atualizar Prompt com Id {IdPrompt}", id);
                throw new Exception("Erro inesperado ao atualizar o prompt.");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var prompt = await _context.Prompt.FindAsync(id);
                if (prompt == null)
                {
                    _logger.LogWarning("Tentativa de excluir Prompt inexistente com Id {IdPrompt}", id);
                    return false;
                }

                _context.Prompt.Remove(prompt);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Erro de banco de dados ao excluir Prompt com Id {IdPrompt}", id);
                throw new Exception("Erro ao excluir o prompt do banco de dados.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro inesperado ao excluir Prompt com Id {IdPrompt}", id);
                throw new Exception("Erro inesperado ao excluir o prompt.");
            }
        }
    }
}

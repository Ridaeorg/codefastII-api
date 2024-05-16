﻿using Codefast.Context;
using Codefast.Models;
using Codefast.Models.DTOs.ControleMataMata;
using Codefast.Models.DTOs.Equipe;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Codefast.Repository;

public class ControleMataMataRepository : BaseRepository, IControleMataMataRepository
{
    private readonly CodefastContext _context;

    public ControleMataMataRepository(CodefastContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<EquipeDTO>> GetEquipesParaFaseMataMata(int idTorneio)
    {
        return await _context.Equipes
            .Where(e => e.TorneioId == idTorneio && e.IsDesclassificado == false)
            .Select(eq => new EquipeDTO
            {
                Id = eq.Id,
                Nome = eq.Nome,
                NomeParticipantes = eq.NomeParticipantes,
                IsCredenciado = eq.IsCredenciado
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ControleMataMataDTO>> GetControleMataMataAsync(int idTorneio)
    {
        return await _context.ControleMataMatas
            .Where(c => c.Equipe.TorneioId == idTorneio && !c.Equipe.IsDesclassificado)
            .Select(eq => new ControleMataMataDTO
            {
                Id = eq.Id,
                StatusValidacao = eq.StatusValidacao,

                Equipe = new ControleMataMataEquipeDTO
                {
                    Id = eq.EquipeId,
                    Nome = eq.Equipe.Nome,
                    IsDesclassificado = eq.Equipe.IsDesclassificado,
                   
                }
            })
            .ToListAsync();
    }
    public async Task<IEnumerable<ControleMataMataDTO>> GetControleMataMataEmValidacaoAsync(int id)
    {
        return await _context.ControleMataMatas
                .Where(eq => eq.Equipe.TorneioId == id && !eq.Equipe.IsDesclassificado)
                .Select(eq => new ControleMataMataDTO
                {
                    Id = eq.Id,
                    StatusValidacao = eq.StatusValidacao,

                    Equipe = new ControleMataMataEquipeDTO
                    {
                        Id = eq.EquipeId,
                        Nome = eq.Equipe.Nome,
                        IsDesclassificado = eq.Equipe.IsDesclassificado
                    }
                }).Where(eq => eq.StatusValidacao == "Validando")
                .ToListAsync();
    }

    public async Task<ControleMataMata> GetControleMataMataByIdAsync(int id)
    {
        return await _context.ControleMataMatas
            .Include(c => c.Equipe)
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<SementeRodada> PreparaEtapaMataMata(int equipeId, int sementeRodadaId)
    {
        var equipe = await _context.Equipes
            .Include(e => e.SementeRodadas)
            .FirstOrDefaultAsync(e => e.Id == equipeId);

        var sementeRodada = await _context.SementeRodadas
        .FindAsync(sementeRodadaId);

        equipe.SementeRodadas.Add(sementeRodada);
        await _context.SaveChangesAsync();

        return sementeRodada;

    }

    public async Task<IEnumerable<ControleMataMata>> GetDisputaTerceiroLugarAsync(int id)
    {
        return await _context.ControleMataMatas
            .Include(c => c.Equipe)
            .Where(c => c.Equipe.TorneioId == id && c.DisputaTerceiroLugar)
            .Select(eq => new ControleMataMata
            {
                Id = eq.Id,
                StatusValidacao = eq.StatusValidacao,
                DisputaTerceiroLugar = eq.DisputaTerceiroLugar, 
                Equipe = new Equipe
                {
                    Id = eq.EquipeId,
                    Nome = eq.Equipe.Nome,
                    IsDesclassificado = eq.Equipe.IsDesclassificado
                }
            })
            .ToListAsync();
}
}

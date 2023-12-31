﻿using FileExplorer.DTOs;
using FileExplorer.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FileExplorer.IService
{
    public interface IFileEntityService
    {
        ICollection<FileEntityDTO> GetAllByType(string type);
        Task<ICollection<FileEntityDTO>> GetAllByTypeAsync(string type);
      
        ICollection<FileEntityDTO> GetAll();
        Task<ICollection<FileEntityDTO>> GetAllAsync();
        ICollection<FileEntityDTO> GetFilesByProjectId(int projectId);
        Task<ICollection<FileEntityDTO>> GetFilesByProjectIdAsync(int projectId);
        FileEntityDTO GetById(int Id);
        Task<FileEntityDTO> GetByIdAsync(int Id);
        FileEntityDTO GetByIdAsNoTracking(int Id);
        Task<FileEntityDTO> GetByIdAsNoTrackingAsync(int Id);
        Task<FileEntityDTO> CreateFileEntityDTODirectly(IFormFile file, FileEntityDTO fileEntityDTO);
        bool AddFileEntity(FileEntityDTO file);
        Task<bool> AddFileEntityAsync(FileEntityDTO file);
        Task<ICollection<FileEntityDTO>> SearchInRecords(string search);
        bool Update(FileEntityDTO file);
        Task<bool> UpdateAsync(FileEntityDTO file);
        bool RemoveFileEntity(FileEntityDTO file);
        Task<bool> RemoveFileEntityAsync(FileEntityDTO file);
        List<string> LastFivePaths();
        string ChangeBytesToString(byte[] fileInBytes);
        byte[] ChangeStringToByte(string file);


    }
}

using Entities;
using DTOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using Microsoft.EntityFrameworkCore.Metadata;

namespace Services
{
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepository;//_ categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDTO> getCategoryById(int id)// Changed to PascalCase
        {
            return _mapper.Map<Category, CategoryDTO>(await categoryRepository.getCategoryById(id));
        }
        public async Task<List<CategoryDTO>> GetAllCategories() // Changed to PascalCase
        {
            return _mapper.Map<List<Category>, List<CategoryDTO>>(await categoryRepository.getAllCategories());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;

namespace Service
{
    public interface ICategoriesService
    {
        Task<List<DropDownDto>> GetCategoriesDDAsync();
    }
}

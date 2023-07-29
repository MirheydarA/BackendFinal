using Business.Services.Abstract.User;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Userr
{
    public class MedicalDepartmentService : IMedicalDepartmentService
    {
        private readonly IMedicalRepository _medicalRepository;
        private readonly IDepartmentComponentRepository _departmentComponentRepository;

        public MedicalDepartmentService(IMedicalRepository medicalRepository, IDepartmentComponentRepository departmentComponentRepository)
        {
            _medicalRepository = medicalRepository;
            _departmentComponentRepository = departmentComponentRepository;
        }

        public async Task<List<DepartmentComponent>> GetDepartmentComponentsAsync()
        {
            var components = await _departmentComponentRepository.GetAllAsync();
            return components;
        }

        public async Task<List<Medical>> GetMedicalAsync()
        {
            var medicals  = await _medicalRepository.GetAllAsync();
            return medicals;
        }
    }
}

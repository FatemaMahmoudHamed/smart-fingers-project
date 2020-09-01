using AnamelTask.Data.DbModels;
using AnamelTask.ViewModel.Customer;
using AutoMapper;
using System.Linq;


namespace Lease.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Customer
            CreateMap<Customer, CustomerVM>().ReverseMap();
            #endregion


        }
    }
}

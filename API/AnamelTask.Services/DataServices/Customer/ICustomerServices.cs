using AnamelTask.Data.DbModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AnamelTask.ViewModel.Customer;
using AnamelTask.ViewModel.GeneralResponse;

namespace AnamelTask.Services.DataServices.Customers
{
    public interface ICustomerServices
    {
        Task<ResponseListVM<CustomerVM>> GetAll();
        Task<ResponseObjVM<CustomerVM>> CreateCustomer(CustomerVM contactVm);
        Task<ResponseObjVM<CustomerVM>> GetCustomerByID(int ContactId);
        Task<ResponseObjVM<CustomerVM>> UpdateCustomer(CustomerVM contactVm);
        Task<ResponseObjVM<string>> DeleteCustomer(int ContactId);


    }
}

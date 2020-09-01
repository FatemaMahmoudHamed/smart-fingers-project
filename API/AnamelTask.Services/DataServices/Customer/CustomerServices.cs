using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AnamelTask.Services.DataServices.Customers;
using AnamelTask.Repositories.DataRepositories.Customer;
using AnamelTask.Repositories.UserDataRepositories.UOW;
using AnamelTask.Data.DataContext;
using AnamelTask.UserData.DbModels;
using AnamelTask.Data.DbModels;
using AnamelTask.ViewModel.Customer;
using AnamelTask.ViewModel.GeneralResponse;

namespace AnamelTask.Services.DataServices.Customers
{
    public class CustomerServices : ICustomerServices
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork<TaskDbContext> _unitOfWork;
       
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;


        public CustomerServices(
            ICustomerRepository customerRepository, 
            IUnitOfWork<TaskDbContext> unitOfWork,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IMapper mapper

            )
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
           
        }

        public async Task<ResponseObjVM<CustomerVM>> CreateCustomer(CustomerVM customerVM)
        {
            if (customerVM == null)
            {
                return "Empty sent data";
            }
            else
            {
                var customer = _mapper.Map<Customer>(customerVM);
                await _customerRepository.AddAsync(customer);

                int save = await _unitOfWork.CommitAsync();
                if (save == 0)
                {
                    return "Customer can not created.";
                }
                return new ResponseObjVM<CustomerVM>(customerVM,"Customer has been created.");
            }
        }

        public async Task<ResponseObjVM<string>> DeleteCustomer(int customerID)
        {
            if (customerID == 0) 
            { 
                return "Empty sent data";
            }
            var customer = await _customerRepository.GetFirstAsync(x => x.ID == customerID);

            if (customer == null)
            {
                return "Customer is not exist.";
            }
            _customerRepository.Remove(customer);

            int save = await _unitOfWork.CommitAsync();
            if (save == 0)
            {
                return "Customer can not deleted.";
            }
            return new ResponseObjVM<string>(save.ToString(), "Customer has been deleted Successfully.");
        }

        public async Task<ResponseListVM<CustomerVM>> GetAll()
        {
            var customers= await _customerRepository.GetAllAsync();
            List<CustomerVM> customerList = new List<CustomerVM>();

            foreach (var customer in customers)
            {
                var customerVMItem = _mapper.Map<CustomerVM>(customer);
                customerList.Add(customerVMItem);
            }
            if (customers != null)
            {
                return new ResponseListVM<CustomerVM>(customerList, "Customer list has been got successfully.");
            }
            else
                return "Failed to get customers list.";
        }

        public async Task<ResponseObjVM<CustomerVM>> GetCustomerByID(int customerID)
        {
            var customer = await _customerRepository.GetAll().FirstOrDefaultAsync(x => x.ID == customerID);
            var customerVM = _mapper.Map<CustomerVM>(customer);
            if (customerVM != null)
            {
                return new ResponseObjVM<CustomerVM>(customerVM, "Customer has been got successfully.");
            }
            else
                return "Failed to get customer.";
        }

        public async Task<ResponseObjVM<CustomerVM>> UpdateCustomer(CustomerVM customerVM)
        {
            if (customerVM == null)
                return "Empty sent data.";

            var dbCustomer= await _customerRepository.GetFirstAsync(x => x.ID == customerVM.ID);
            if (dbCustomer == null)
            {
                return "Customer is not exist.";
            }
            
            var customer = _mapper.Map<Customer>(customerVM);
            _customerRepository.Update(customer);

            int save = await _unitOfWork.CommitAsync();
            if (save == 0)
            {
                return "Failed to update customer.";
            }
            return new ResponseObjVM<CustomerVM>(customerVM, "Customer has been updated successfully.");

        }

    }
}

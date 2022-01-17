using BookStoreManager.Interface;
using BookStoreModels;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository addressRepository;

        public AddressManager(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public int AddAddress(AddressModel addressModel)
        {
            try
            {
                return this.addressRepository.AddAddress(addressModel);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

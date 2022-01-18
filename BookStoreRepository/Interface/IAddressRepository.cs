using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        int AddAddress(AddressModel addressModel);

        int UpdateAddress(AddressModel addressModel);

        List<AddressModel> GetUserAddress(int userId);
    }
}
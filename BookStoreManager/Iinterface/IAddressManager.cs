using BookStoreModels;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        int AddAddress(AddressModel addressModel);

        int UpdateAddress(AddressModel addressModel);

        List<AddressModel> GetUserAddress(int userId);
    }
}
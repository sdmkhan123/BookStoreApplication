﻿using BookStoreModels;

namespace BookStoreManager.Interface
{
    public interface IAddressManager
    {
        int AddAddress(AddressModel addressModel);

        int UpdateAddress(AddressModel addressModel);
    }
}
﻿using BookStoreModels;

namespace BookStoreRepository.Interface
{
    public interface IAddressRepository
    {
        int AddAddress(AddressModel addressModel);

        int UpdateAddress(AddressModel addressModel);
    }
}
﻿using Mango.Services.EmailAPI.Message;
using Mango.Services.EmailAPI.Models.DTO;

namespace Mango.Services.EmailAPI.Services
{
    public interface IEmailService
    {
        Task EmailCartAndLog(CartDto cartDto);

        Task RegisterUserEmailAndLog(string email);

        Task LogOrderPlaced(RewardsMessage rewardsDto);
    }
}

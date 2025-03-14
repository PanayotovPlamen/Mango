﻿using Mango.Web.Models.DTO;

namespace Mango.Web.Services.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}

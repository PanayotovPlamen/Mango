﻿using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ResponseDto _response;
        private readonly IMapper _mapper;

        public CouponAPIController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();

                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponCode == code);

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Post([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);

                _db.Coupons.Add(obj);

                _db.SaveChanges();

                var options = new Stripe.CouponCreateOptions
                {
                    AmountOff = (long)(couponDto.DiscountAmount * 100),
                    Name = couponDto.CouponCode,
                    Currency = "usd",
                    Id = couponDto.CouponCode,
                };

                var service = new Stripe.CouponService();

                service.Create(options);

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPut]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);

                _db.Coupons.Update(obj);

                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpDelete]
        [Route("{id:int}")]
		[Authorize(Roles = "ADMIN")]
		public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(x => x.CouponId == id);

                _db.Coupons.Remove(obj);

                _db.SaveChanges();

                var service = new Stripe.CouponService();

                service.Delete(obj.CouponCode);

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;

                _response.Message = ex.Message;
            }

            return _response;
        }
    }
}

﻿using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) 
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
            .Coupons
            .FirstOrDefaultAsync(x => x.ProductName == request.ProductName) 
                     ?? new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

        logger.LogInformation("ProductName: {productName}, Amount: {amount}", coupon.ProductName, coupon.Amount);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
        
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is created. ProductName: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));

        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is updated. ProductName: {productName}", coupon.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext
                         .Coupons
                         .FirstOrDefaultAsync(x => x.ProductName == request.ProductName)
                     ?? throw new RpcException(new Status(StatusCode.NotFound, "Not Found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is deleted. ProductName: {productName}", coupon.ProductName);

        return new DeleteDiscountResponse { Success = true };
    }
}

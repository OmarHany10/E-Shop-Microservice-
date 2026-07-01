using Discount.gRPC.Data;
using Discount.gRPC.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.gRPC.Services
{
    public class DiscountServiec(DiscountDbContext dbcontext)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModule> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if (coupon is null)
                coupon = new Coupon() {Id=0, ProductName="No Discount", Description="", Amount=0 };

            var result = coupon.Adapt<CouponModule>();
            return result;
        }
        public override async Task<CouponModule> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            dbcontext.Add(coupon);
            await dbcontext.SaveChangesAsync();

            var result = coupon.Adapt<CouponModule>();
            return result;
        }
        public override async Task<CouponModule> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Rpc call"));

            dbcontext.Update(coupon);
            await dbcontext.SaveChangesAsync();

            var result = coupon.Adapt<CouponModule>();
            return result;
        }
        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbcontext.Coupons.FirstOrDefaultAsync(c => c.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "There is no coupon have this name"));

            dbcontext.Remove(coupon);
            await dbcontext.SaveChangesAsync();

            return new DeleteDiscountResponse() { IsDeleted=true};

        }
    }
}

﻿namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext)
        : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            //Delete Order entity from command object
            var orderId = OrderId.Of(command.OrderId);
            var order = await dbContext.Orders
                .FindAsync([orderId], cancellationToken: cancellationToken);

            if (order is null)
            {
                throw new OrderNotFoundException(command.OrderId);
            }

            dbContext.Orders.Remove(order);

            //save to database
            await dbContext.SaveChangesAsync(cancellationToken);

            //return result
            return new DeleteOrderResult(true);
        }
    }
}
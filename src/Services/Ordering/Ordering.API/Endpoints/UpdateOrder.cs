
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace Ordering.API.Endpoints
{
    public record UpdateOrderRequest(OrderDto Order);
    public record UpdateOrderResponse(bool IsSucces);
    public class UpdateOrder : ICarterModule
    {
        //Accept a UpdateOrderRequest
        //Maps the request to an UpdateOrderCommand
        //Send the command for processing
        //Return a success or an error response based on the outcome.
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/orders", async (UpdateOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateOrderCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateOrder")
            .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Order")
            .WithDescription("Update Order");
        }
    }
}

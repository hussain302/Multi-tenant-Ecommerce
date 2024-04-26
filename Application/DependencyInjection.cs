using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
        => services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
}
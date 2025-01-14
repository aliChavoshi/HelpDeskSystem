using AutoMapper;
using HelpDeskSystem.Entities;
using HelpDeskSystem.ViewModels.TicketsDto;
using System.Reflection;

namespace HelpDeskSystem.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //CreateMap<CreateTicketDto, Ticket>();
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly()); //All : IMapFrom
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var mapFromType = typeof(IMapFrom<>);

        var mappingMethodName = nameof(IMapFrom<object>.Mapping);

        bool HasInterface(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
        }

        var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

        Type[] argumentTypes = [typeof(Profile)];

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod(mappingMethodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(instance, [this]);
            }
            else
            {
                var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

                if (interfaces.Count <= 0) continue;
                foreach (var interfaceMethodInfo in interfaces.Select(@interface =>
                             @interface.GetMethod(mappingMethodName, argumentTypes)))
                    interfaceMethodInfo?.Invoke(instance, [this]);
            }
        }
    }
}
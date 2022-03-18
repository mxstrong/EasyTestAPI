using Autofac;
using EasyTestAPI.Core.Interfaces;
using EasyTestAPI.Core.Services;

namespace EasyTestAPI.Core;
public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}

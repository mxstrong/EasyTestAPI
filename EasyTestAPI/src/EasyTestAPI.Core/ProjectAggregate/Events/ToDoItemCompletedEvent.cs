using EasyTestAPI.Core.ProjectAggregate;
using EasyTestAPI.SharedKernel;

namespace EasyTestAPI.Core.ProjectAggregate.Events;
public class ToDoItemCompletedEvent : BaseDomainEvent
{
  public ToDoItem CompletedItem { get; set; }

  public ToDoItemCompletedEvent(ToDoItem completedItem)
  {
    CompletedItem = completedItem;
  }
}

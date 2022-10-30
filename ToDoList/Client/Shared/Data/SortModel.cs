namespace ToDoList.Client.Shared.Data;

public record SortModel<TModel>(Func<TModel, dynamic> Element, string Name);
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using OpenModular.DDD.Core.Domain.Entities;

namespace OpenModular.Module.Web.ModelBinderProviders;

internal class TypeIdBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Metadata.ModelType.BaseType == typeof(TypedIdValueBase))
        {
            return new BinderTypeModelBinder(typeof(TypeIdModelBinder));
        }

        return null;
    }
}

public class TypeIdModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var modelName = bindingContext.ModelName;
        var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);

        if (valueProviderResult == ValueProviderResult.None)
        {
            return Task.CompletedTask;
        }

        var value = valueProviderResult.FirstValue;

        if (string.IsNullOrEmpty(value))
        {
            return Task.CompletedTask;
        }

        var modelType = bindingContext.ModelType;
        var constructor = modelType.GetConstructor([typeof(string)]);

        if (constructor == null)
        {
            bindingContext.ModelState.TryAddModelError(modelName, $"No suitable constructor found for {modelType.Name}");
            return Task.CompletedTask;
        }

        try
        {
            var model = constructor.Invoke([value]);
            bindingContext.Result = ModelBindingResult.Success(model);
        }
        catch (Exception ex)
        {
            bindingContext.ModelState.TryAddModelError(modelName, ex.Message);
        }

        return Task.CompletedTask;
    }
}
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebAppMedicalAssistantMVC.Infrastructure
{
    public class CustomDateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            // Для объекта SimpleTypeModelBinder необходим сервис ILoggerFactory
            // Получаем его из сервисов
            ILoggerFactory loggerFactory = context.Services.GetRequiredService<ILoggerFactory>();
            IModelBinder binder = new CustomDateTimeModelBinder(new SimpleTypeModelBinder(typeof(DateTime), loggerFactory));
            return context.Metadata.ModelType == typeof(DateTime) ? binder : null;
        }
    }
}

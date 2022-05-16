using Microsoft.Extensions.DependencyInjection;

namespace DiscordBot.Console.Utils
{
    public class InterfaceUtils<T>
    {
        public IEnumerable<T> GetClasses()
        {
            var types = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .SelectMany(x => x.GetTypes())
                        .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                        .Select(x =>
                        {
                            return ActivatorUtilities.CreateInstance(Services.provider!, x);
                        })
                        .Cast<T>();

            if (!types.Any()) return Enumerable.Empty<T>();
            return types;
        }
    }
}

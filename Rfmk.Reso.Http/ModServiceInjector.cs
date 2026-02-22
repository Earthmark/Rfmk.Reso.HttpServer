namespace Rfmk.Reso.Http;

/// <summary>
/// The entry point for other mods to add themselves to the HTTP server.
/// </summary>
public static class ModServiceInjector
{
    private static readonly Lock InjectorLock = new();

    private static List<IResoHttpModule>? _modules = [];

    public static void RegisterHttpModule(this IResoHttpModule module)
    {
        lock (InjectorLock)
        {
            if (_modules == null)
            {
                throw new InvalidOperationException("The injectors have already been retrieved.");
            }

            _modules.Add(module);
        }
    }

    /// <summary>
    /// Retrieves the registered injectors, this can only be called once and should only be called by the HTTP runtime.
    /// </summary>
    /// <returns>The registered injectors</returns>
    /// <exception cref="InvalidOperationException">The injectors have already been taken</exception>
    public static List<IResoHttpModule> TakeInjectors()
    {
        lock (InjectorLock)
        {
            if (_modules == null)
            {
                throw new InvalidOperationException("The injectors have already been retrieved.");
            }

            return Interlocked.Exchange(ref _modules, null);
        }
    }
}

namespace Rfmk.Reso.Http;

/// <summary>
/// A module that adds a subpage to the HTTP server that is user-visible.
/// </summary>
public interface ISubPageHttpModule
{
    /// <summary>
    /// The route of the index page, this should start with a slash.
    /// </summary>
    string RoutePrefix { get; }
    
    /// <summary>
    /// The user visible name of the page, this shows up as link text.
    /// </summary>
    string PageName { get; }
}

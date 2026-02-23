namespace Rfmk.Reso.Http;

/// <summary>
/// A marker that if included in the dependency injector, means Resonite is available in the services and application domain.
///
/// This is added early in the process in a live service, and mods can use the existence of this marker for if development or live data should be used.
/// </summary>
public class ResoniteMarker;

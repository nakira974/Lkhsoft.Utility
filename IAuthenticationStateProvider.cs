namespace Lkhsoft.Utility;

/// <summary>
///     Type servant à fournir un accès à plate-forme d'authentifcation ASP.NET.Core.Identity
/// </summary>
public interface IAuthenticationStateProvider
{
    /// <summary>
    ///     Enregistre le token délivré par l'API dans un espace de stockage sécurisé et permet à l'utilisateur de naviguer au
    ///     sein de l'application
    /// </summary>
    /// <param name="token">GUID délivré par l'API si le couple samaccountname + mot de passe est approuvé par l'AD</param>
    /// <returns></returns>
    public Task Login(string token);


    /// <summary>
    ///     Supprime le token delivré par l'API de l'espace de stockage sécurisé et ne permet plus à l'utilisateur de naviguer
    ///     dans l'application
    /// </summary>
    /// <returns></returns>
    public Task Logout();
}

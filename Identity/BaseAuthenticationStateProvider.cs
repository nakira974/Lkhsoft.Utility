// ReSharper disable TemplateIsNotCompileTimeConstantProblem

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace Lkhsoft.Utility.Identity;

/// <summary>
///     Modèle de base pour les AuthenticationStateProvider contenant les prototypes des actions
///     d'authentification
/// </summary>
/// <typeparam name="T">Type d'une classe implémentant IAuthenticationStateProvider</typeparam>
public abstract class BaseAuthenticationStateProvider<T> : AuthenticationStateProvider, IAuthenticationStateProvider
{
    protected readonly ILogger<T> Logger;

    /// <inheritdoc />
    protected BaseAuthenticationStateProvider(ILogger<T> logger)
    {
        Logger = logger;
        Logger.LogInformation($"{typeof(T)} has been called");
    }

    /// <summary>
    ///     Enregistre le token délivré par l'API dans un espace de stockage sécurisé et permet à l'utilisateur de naviguer au
    ///     sein de l'application
    /// </summary>
    /// <param name="token">GUID délivré par l'API si le couple samaccountname + mot de passe est approuvé par l'AD</param>
    /// <returns></returns>
    public abstract Task Login(string token);


    /// <summary>
    ///     Supprime le token delivré par l'API de l'espace de stockage sécurisé et ne permet plus à l'utilisateur de naviguer
    ///     dans l'application
    /// </summary>
    /// <returns></returns>
    public abstract Task Logout();

    /// <summary>
    ///     Récupère le token situé dans le stockage sécurisé et check sa validité et authorise l'utilisateur à accèder à la
    ///     ressource cible
    /// </summary>
    /// <returns></returns>
    public abstract override Task<AuthenticationState> GetAuthenticationStateAsync();
}

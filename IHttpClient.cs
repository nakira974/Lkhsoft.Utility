namespace Lkhsoft.Utility;

/// <summary>
///     Type fournissant l'accès à un service de requêtes C.R.U.D en HTTP 1.1
/// </summary>
public interface IHttpClient
{
    /// <summary>
    ///     Exécute une requête GET et renvoi la résultat désérialisé
    /// </summary>
    /// <param name="uri">Url du serveur cible</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<T> GetResponse<T>(string uri);

    /// <summary>
    ///     Exécute une requête GET et renvoi un booléen pour confirmer si tout s'est bien déroulé
    /// </summary>
    /// <param name="uri">Url du serveur cible</param>
    /// <returns>Une confirmation que la requête s'est bien déroulée</returns>
    public Task<bool> GetResponse(string uri);

    /// <summary>
    ///     Exécute une requête POST avec un StringContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <returns>Une réponse HTTP qui ne provoque pas une exception lors du EnsureStatusCode</returns>
    public Task<HttpResponseMessage> PostEntity(string entity, string uri);

    /// <summary>
    ///     Exécute une requête POST avec un ByteArrayContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="byteArrayContent">Contenu binaire à poster, pouvant contenir des paramètres de la méthode dans les Headers </param>
    /// <param name="uri">Url du serveur cible</param>
    /// <returns>Une réponse HTTP qui ne provoque pas une exception lors du EnsureStatusCode</returns>
    public Task<HttpResponseMessage> PostEntity(ByteArrayContent? byteArrayContent, string uri);

    /// <summary>
    ///     Exécute une requête PATCH avec un StringContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<T> PatchEntity<T>(string entity, string uri);

    /// <summary>
    ///     Exécute une requête PATCH avec un ByteArrayContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="byteArrayContent">Contenu binaire à poster, pouvant contenir des paramètres de la méthode dans les Headers </param>
    /// <param name="uri">Url du serveur cible</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<T> PatchEntity<T>(ByteArrayContent byteArrayContent, string uri);

    /// <summary>
    ///     Exécute une requête PUT avec un StringContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <returns>Une réponse HTTP qui ne provoque pas une exception lors du EnsureStatusCode</returns>
    public Task<HttpResponseMessage> PutEntity(string entity, string uri);

    /// <summary>
    ///     Exécute une requête DELETE et renvoi un booléen pour confirmer si tout s'est bien déroulé
    /// </summary>
    /// <param name="uri">Url du serveur cible</param>
    /// <returns>Une confirmation que la requête s'est bien déroulée</returns>
    public Task<bool> DeleteEntity(string uri);
}
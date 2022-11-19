namespace Lkhsoft.Utility.WebServices;

/// <summary>
///     Type fournissant l'accès à un service de requêtes C.R.U.D en HTTP 1.1
/// </summary>
public interface IHttpClientBase
{
    /// <summary>
    ///     Exécute une requête GET et renvoi la résultat désérialisé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<HttpResponseMessage> GetEntity<T>(string entity, string uri, IDictionary<string, string>? additionalHeaders = null);
    

    /// <summary>
    ///     Exécute une requête POST avec un StringContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <returns>Une réponse HTTP qui ne provoque pas une exception lors du EnsureStatusCode</returns>
    public Task<HttpResponseMessage> PostEntity(string entity, string uri, IDictionary<string, string>? additionalHeaders = null);

    /// <summary>
    ///     Exécute une requête POST avec un ByteArrayContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="byteArrayContent">Contenu binaire à poster, pouvant contenir des paramètres de la méthode dans les Headers </param>
    /// <param name="uri">Url du serveur cible</param>
    /// <returns>Une réponse HTTP qui ne provoque pas une exception lors du EnsureStatusCode</returns>
    public Task<HttpResponseMessage> PostEntity(ByteArrayContent? byteArrayContent, string uri, IDictionary<string, string>? additionalHeaders = null);

    /// <summary>
    ///     Exécute une requête PATCH avec un StringContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="fieldValue">Nom du champs à modifier</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="fieldName">Valeur du champs à modifier</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<HttpResponseMessage> PatchEntity<T>(string fieldName, string fieldValue, string uri, IDictionary<string, string>? additionalHeaders = null);

    /// <summary>
    ///     Exécute une requête PATCH avec un ByteArrayContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="byteArrayContent">Contenu binaire à poster, pouvant contenir des paramètres de la méthode dans les Headers </param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<HttpResponseMessage> PatchEntity<T>(ByteArrayContent? byteArrayContent, string uri, IDictionary<string, string>? additionalHeaders = null);

    /// <summary>
    ///     Exécute une requête PUT avec un StringContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <returns>Une réponse HTTP qui ne provoque pas une exception lors du EnsureStatusCode</returns>
    public Task<HttpResponseMessage> PutEntity(string entity, string uri, IDictionary<string, string>? additionalHeaders = null);

    /// <summary>
    ///     Exécute une requête DELETE et renvoi un booléen pour confirmer si tout s'est bien déroulé
    /// </summary>
    /// <param name="entity">Entité à envoyer sérialisé</param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <returns>Une confirmation que la requête s'est bien déroulée</returns>
    public Task<HttpResponseMessage> DeleteEntity(string entity, string uri, IDictionary<string, string>? additionalHeaders = null);
    
    /// <summary>
    ///     Exécute une requête PATCH avec un ByteArrayContent et renvoi le HttpResponseMessage si tout s'est bien déroulé
    /// </summary>
    /// <param name="byteArrayContent">Contenu binaire à poster, pouvant contenir des paramètres de la méthode dans les Headers </param>
    /// <param name="uri">Url du serveur cible</param>
    /// <param name="additionalHeaders">Headers additionnels</param>
    /// <typeparam name="T">Type de retour attendu</typeparam>
    /// <returns>Une instance du type attendu</returns>
    public Task<HttpResponseMessage> DeleteEntity(ByteArrayContent? byteArrayContent, string uri, IDictionary<string, string>? additionalHeaders = null);
}
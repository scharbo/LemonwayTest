// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomRequestValidator.cs" >
//   https://stackoverflow.com/questions/3070642/web-service-does-not-accept-input
// </copyright>
// <summary>
//   (Ugly) fix for validation of xml parameter 
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WebServices.Validator
{
    using System.Web;
    using System.Web.Util;

    /// <summary>
    /// Validates the input based on some custom rules
    /// </summary>
    public class CustomRequestValidator : RequestValidator
    {
        /// <summary>
        /// Validates a string that contains HTTP request data.
        /// </summary>
        /// <param name="context">The context of the current request.</param>
        /// <param name="value">The HTTP request data to validate.</param>
        /// <param name="requestValidationSource">An enumeration that represents the source of request data that is being validated. The following are possible values for the enumeration:QueryStringForm CookiesFilesRawUrlPathPathInfoHeaders</param>
        /// <param name="collectionKey">The key in the request collection of the item to validate. This parameter is optional. This parameter is used if the data to validate is obtained from a collection. If the data to validate is not from a collection, <paramref name="collectionKey"/> can be null.</param>
        /// <param name="validationFailureIndex">When this method returns, indicates the zero-based starting point of the problematic or invalid text in the request collection. This parameter is passed uninitialized.</param>
        /// <returns>
        /// true if the string to be validated is valid; otherwise, false.
        /// </returns>
        protected override bool IsValidRequestString(
            HttpContext context,
            string value,
            RequestValidationSource requestValidationSource,
            string collectionKey,
            out int validationFailureIndex)
        {
            // Set a default value for the out parameter.
            validationFailureIndex = -1;

            // Allow the query-string key data to have a value that is formatted like XML.
            if ((requestValidationSource == RequestValidationSource.Form) && (collectionKey == "xml"))
            {
                return true;
            }

            return base.IsValidRequestString(
                context,
                value,
                requestValidationSource,
                collectionKey,
                out validationFailureIndex);
        }
    }
}

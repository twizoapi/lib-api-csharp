namespace TwizoAPI.Responses
{
    /**
    * This file is part of the Twizo C# API.
    * 
    * (c) Twizo - info@twizo.com
    * 
    * For the full copyright and license information, please view the LICENSE file that was distributed with this source code.
    */

    /// <summary>
    /// Abstract class with all rest status codes.
    /// </summary>
    public abstract class RestStatusCodes
    {
        //1xx Informational
        public const int REST_INFO_CONTINUE = 100;
        public const int REST_INFO_SWITCHING_PROTOCOLS = 101;
        public const int REST_INFO_PROCESSING = 102;

        //2xx Success
        public const int REST_SUCCESS_OK = 200;
        public const int REST_SUCCESS_CREATED = 201;
        public const int REST_SUCCESS_ACCEPTED = 202;
        public const int REST_SUCCESS_NON_AUTHORITATIVE_INFORMATION = 203;
        public const int REST_SUCCESS_NO_CONTENT = 204;
        public const int REST_SUCCESS_RESET_CONTENT = 205;
        public const int REST_SUCCESS_PARTIAL_CONTENT = 206;
        public const int REST_SUCCESS_MULTI_STATUS = 207;
        public const int REST_SUCCESS_ALREADY_REPORTED = 208;
        public const int REST_SUCCESS_IM_USED = 226;

        //3xx Redirection
        public const int REST_REDIR_MULTIPLE_CHOICES = 300;
        public const int REST_REDIR_MOVED_PERMANENTLY = 301;
        public const int REST_REDIR_FOUND = 302;
        public const int REST_REDIR_SEE_OTHER = 303;
        public const int REST_REDIR_NOT_MODIFIED = 304;
        public const int REST_REDIR_USE_PROXY = 305;
        public const int REST_REDIR_UNUSED = 306;
        public const int REST_REDIR_TEMPORARY_REDIRECT = 307;
        public const int REST_REDIR_PERMANENT_REDIRECT = 308;
        
        //4xx Client error
        public const int REST_CLIENT_ERROR_BAD_REQUEST = 400;
        public const int REST_CLIENT_ERROR_UNAUTHORIZED = 401;
        public const int REST_CLIENT_ERROR_PAYMENT_REQUIRED = 402;
        public const int REST_CLIENT_ERROR_FORBIDDEN = 403;
        public const int REST_CLIENT_ERROR_NOT_FOUND = 404;
        public const int REST_CLIENT_ERROR_METHOD_NOT_ALLOWED = 405;
        public const int REST_CLIENT_ERROR_NOT_ACCEPTABLE = 406;
        public const int REST_CLIENT_ERROR_PROXY_AUTHENTICATION_REQUIRED = 407;
        public const int REST_CLIENT_ERROR_REQUEST_TIMEOUT = 408;
        public const int REST_CLIENT_ERROR_CONFLICT = 409;
        public const int REST_CLIENT_ERROR_GONE = 410;
        public const int REST_CLIENT_ERROR_LENGTH_REQUIRED = 411;
        public const int REST_CLIENT_ERROR_PRECONDITION_FAILED = 412;
        public const int REST_CLIENT_ERROR_REQUEST_ENTITY_TOO_LARGE = 413;
        public const int REST_CLIENT_ERROR_REQUEST_URI_TOO_LONG = 414;
        public const int REST_CLIENT_ERROR_UNSUPPORTED_MEDIA_TYPE = 415;
        public const int REST_CLIENT_ERROR_REQUESTED_RANGE_NOT_SATISFIABLE = 416;
        public const int REST_CLIENT_ERROR_EXPECTATION_FAILED = 417;
        public const int REST_CLIENT_ERROR_I_AM_A_TEAPOT = 418;
        public const int REST_CLIENT_ERROR_MISDIRECTED_REQUEST = 421;
        public const int REST_CLIENT_ERROR_UNPROCESSABLE_ENTITY = 422;
        public const int REST_CLIENT_ERROR_LOCKED = 423;
        public const int REST_CLIENT_ERROR_FAILED_DEPENDENCY = 424;
        public const int REST_CLIENT_ERROR_RESERVED_FOR_WEBDAV_ADVANCED_COLLECTIONS_EXPIRED_PROPOSAL = 425;
        public const int REST_CLIENT_ERROR_UPGRADE_REQUIRED = 426;
        public const int REST_CLIENT_ERROR_PRECONDITION_REQUIRED = 428;
        public const int REST_CLIENT_ERROR_TOO_MANY_REQUESTS = 429;
        public const int REST_CLIENT_ERROR_REQUEST_HEADER_FIELDS_TOO_LARGE = 431;
        public const int REST_CLIENT_ERROR_UNAVAILABLE_FOR_LEGAL_REASONS = 451;

        //5xx Server error
        public const int REST_SERVER_ERROR_INTERNAL_SERVER_ERROR = 500;
        public const int REST_SERVER_ERROR_NOT_IMPLEMENTED = 501;
        public const int REST_SERVER_ERROR_BAD_GATEWAY = 502;
        public const int REST_SERVER_ERROR_SERVICE_UNAVAILABLE = 503;
        public const int REST_SERVER_ERROR_GATEWAY_TIMEOUT = 504;
        public const int REST_SERVER_ERROR_VERSION_NOT_SUPPORTED = 505;
        public const int REST_SERVER_ERROR_VARIANT_ALSO_NEGOTIATES_EXPERIMENTAL = 506;
        public const int REST_SERVER_ERROR_INSUFFICIENT_STORAGE = 507;
        public const int REST_SERVER_ERROR_LOOP_DETECTED = 508;
        public const int REST_SERVER_ERROR_NOT_EXTENDED = 510;
        public const int REST_SERVER_ERROR_NETWORK_AUTHENTICATION_REQUIRED = 511;
    }
}

namespace Api.Middleware
{
	using System;
	using System.Net;
	using System.Text;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.Extensions.Configuration;

	/// <summary>
	///     Middleware that is run on every API request
	///     Checks the provided Username/Password against a config
	/// </summary>
	public class BasicAuthMiddleware
	{
		/// <summary>
		///     The next
		/// </summary>
		private readonly RequestDelegate _next;

		/// <summary>
		/// The configuration
		/// </summary>
		private readonly IConfiguration _config;

		/// <summary>
		/// Initializes a new instance of the <see cref="BasicAuthMiddleware" /> class.
		/// </summary>
		/// <param name="next">The next.</param>
		/// <param name="config">The configuration.</param>
		public BasicAuthMiddleware(RequestDelegate next, IConfiguration config)
		{
			_next = next;
			_config = config;
		}

		/// <summary>
		///     Invokes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		public async Task Invoke(HttpContext context)
		{
			string authHeader = context.Request.Headers["Authorization"];

			//Check that auth header is there, and that it's the correct atuth type
			if (authHeader == null || !authHeader.StartsWith("Basic "))
			{
				context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				return;
			}

			//Get the encoded username and password, and decode them
			var encodedUsernameAndPassword = authHeader.Split(' ', 2)[1]?.Trim();
			var usernameAndPassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernameAndPassword));
			
			//Split the username and password out
			var username = usernameAndPassword.Split(':', 2)[0];
			var password = usernameAndPassword.Split(':', 2)[1];

			if (IsAuthorized(username, password))
			{
				await _next(context);
				return;
			}

			context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
		}

		/// <summary>
		///     Determines whether the credentials are valid
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>
		///     <c>true</c> if credentials are valid; otherwise, <c>false</c>.
		/// </returns>
		private bool IsAuthorized(string username, string password)
		{
			//Get the correct username and password from the config

			var basicAuthUserName = _config.GetSection("BasicAuth")["Username"];
			var basicAuthPassword = _config.GetSection("BasicAuth")["Password"];

			return username.Equals(basicAuthUserName, StringComparison.InvariantCultureIgnoreCase)
				&& password.Equals(basicAuthPassword);
		}
	}
}
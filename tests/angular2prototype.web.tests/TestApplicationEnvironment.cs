using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.IO;
using System.Net.Http;
using System.Reflection;

namespace angular2prototype.web.tests
{
	public static class TestApplicationEnvironment 
	{
		private const string _solutionName = "angular2prototype.sln";
		private static TestServer _server;

		static TestApplicationEnvironment()
		{
			InitTestServer(Path.Combine("src"));
		}

		internal static void InitTestServer(string solutionRelativeTargetProjectParentDir)
		{
			var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;
			var contentRoot = GetProjectPath(solutionRelativeTargetProjectParentDir, startupAssembly);

			var builder = new WebHostBuilder()
				.UseContentRoot(contentRoot)
				.ConfigureServices(InitializeServices)
				.UseEnvironment("Development")
				.UseStartup(typeof(Startup));

			_server = new TestServer(builder);

			Client = _server.CreateClient();
			Client.BaseAddress = new Uri("http://localhost");

		}

		public static HttpClient Client { get; internal set; }
		
		private static void InitializeServices(IServiceCollection services)
		{
			var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

			// Inject a custom application part manager. Overrides AddMvcCore() because that uses TryAdd().
			var manager = new ApplicationPartManager();
			manager.ApplicationParts.Add(new AssemblyPart(startupAssembly));

			manager.FeatureProviders.Add(new ControllerFeatureProvider());
			manager.FeatureProviders.Add(new ViewComponentFeatureProvider());

			services.AddSingleton(manager);
		}

		/// <summary>
		/// Gets the full path to the target project path that we wish to test
		/// </summary>
		/// <param name="solutionRelativePath">
		/// The parent directory of the target project.
		/// e.g. src, samples, test, or test/Websites
		/// </param>
		/// <param name="startupAssembly">The target project's assembly.</param>
		/// <returns>The full path to the target project.</returns>
		private static string GetProjectPath(string solutionRelativePath, Assembly startupAssembly)
		{
			// Get name of the target project which we want to test
			var projectName = startupAssembly.GetName().Name;

			// Get currently executing test project path
			var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;

			// Find the folder which contains the solution file. We then use this information to find the target
			// project which we want to test.
			var directoryInfo = new DirectoryInfo(applicationBasePath);
			do
			{
				var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, _solutionName));
				if (solutionFileInfo.Exists)
				{
					return Path.GetFullPath(Path.Combine(directoryInfo.FullName, solutionRelativePath, projectName));
				}

				directoryInfo = directoryInfo.Parent;
			}
			while (directoryInfo.Parent != null);

			throw new Exception($"Solution root could not be located using application root {applicationBasePath}.");
		}
	}

}

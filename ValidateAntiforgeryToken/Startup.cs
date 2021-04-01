using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ValidateAntiforgeryToken
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// このメソッドはランタイムによって呼び出されます。 このメソッドを使用して、コンテナーにサービスを追加します。
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews(options =>
			{
				// 全ての「POST, PUT, PATCH, DELETE」アクションに自動で ValidateAntiForgeryToken を付与。
				// 個別に除外したい場合は「IgnoreAntiforgeryToken」属性を指定すること
				// API では HTML 側にトークンを発行できないのでコントローラーに「IgnoreAntiforgeryToken」を指定する必要がある。
				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
			});
		}

		// このメソッドはランタイムによって呼び出されます。 このメソッドを使用して、HTTP要求パイプラインを構成します。
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// デフォルトのHSTS値は30日です。 運用シナリオではこれを変更することができます。https://aka.ms/aspnetcore-hsts を参照してください。
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
									name: "default",
									pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}

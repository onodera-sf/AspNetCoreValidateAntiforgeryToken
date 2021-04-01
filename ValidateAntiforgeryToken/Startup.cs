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

		// ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B ���̃��\�b�h���g�p���āA�R���e�i�[�ɃT�[�r�X��ǉ����܂��B
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews(options =>
			{
				// �S�ẮuPOST, PUT, PATCH, DELETE�v�A�N�V�����Ɏ����� ValidateAntiForgeryToken ��t�^�B
				// �ʂɏ��O�������ꍇ�́uIgnoreAntiforgeryToken�v�������w�肷�邱��
				// API �ł� HTML ���Ƀg�[�N���𔭍s�ł��Ȃ��̂ŃR���g���[���[�ɁuIgnoreAntiforgeryToken�v���w�肷��K�v������B
				options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
			});
		}

		// ���̃��\�b�h�̓����^�C���ɂ���ČĂяo����܂��B ���̃��\�b�h���g�p���āAHTTP�v���p�C�v���C�����\�����܂��B
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// �f�t�H���g��HSTS�l��30���ł��B �^�p�V�i���I�ł͂����ύX���邱�Ƃ��ł��܂��Bhttps://aka.ms/aspnetcore-hsts ���Q�Ƃ��Ă��������B
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

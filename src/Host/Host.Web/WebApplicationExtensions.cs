﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenModular.Host.Web.Options;
using OpenModular.Persistence;
using OpenModular.Persistence.DataSeeding;

namespace OpenModular.Host.Web;

public static class WebApplicationExtensions
{
    /// <summary>
    /// 执行数据库迁移
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication ExecuteDbMigration(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService<DbMigrationHandler>();
        handler.MigrateAsync().GetAwaiter().GetResult();

        return app;
    }

    /// <summary>
    /// 执行数据种子
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication ExecuteDataSeeding(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var executor = scope.ServiceProvider.GetRequiredService<IDataSeedingExecutor>();
        executor.ExecuteAsync().GetAwaiter().GetResult();

        return app;
    }

    /// <summary>
    /// 配置路基跟地址
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UsePathBase(this IApplicationBuilder app, WebHostOptions options)
    {
        var pathBase = options.BasePath;
        if (pathBase.NotNull())
        {
            //1、配置请求基础地址：
            app.Use((context, next) =>
            {
                context.Request.PathBase = pathBase;
                return next();
            });

            // 2、配置静态文件基地址：
            app.UsePathBase(pathBase);
        }

        return app;
    }

    /// <summary>
    /// 设置默认页为index.html
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseDefaultPage(this IApplicationBuilder app, WebHostOptions options)
    {
        //设置默认文档
        if (options.DefaultPage.NotNull())
        {
            var defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add(options.DefaultPage);
            app.UseDefaultFiles(defaultFilesOptions);
        }

        return app;
    }

    /// <summary>
    /// 设置默认目录
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseDefaultDir(this IApplicationBuilder app, WebHostOptions options)
    {
        if (options.DefaultDir.NotNull())
        {
            var rewriteOptions = new RewriteOptions().AddRedirect("^$", options.DefaultDir);
            app.UseRewriter(rewriteOptions);
        }

        return app;
    }


    /// <summary>
    /// 启用Banner图
    /// </summary>
    /// <param name="app"></param>
    /// <param name="appLifetime"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseBanner(this IApplicationBuilder app, IHostApplicationLifetime appLifetime, WebHostOptions options)
    {
        appLifetime.ApplicationStarted.Register(() =>
        {
            //显示启动Banner
            var customFile = Path.Combine(AppContext.BaseDirectory, "banner.txt");
            if (File.Exists(customFile))
            {
                try
                {
                    var lines = File.ReadAllLines(customFile);
                    foreach (var line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
                catch
                {
                    Console.WriteLine("banner.txt文件无效");
                }
            }
            else
            {
                ConsoleBanner(options);
            }
        });

        return app;
    }


    /// <summary>
    /// 启动后打印Banner图案
    /// </summary>
    private static void ConsoleBanner(WebHostOptions options)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine(@" ***************************************************************************************************************");
        Console.Write(@" *                                      ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(@"Startup successful, welcome to OpenModular ~");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(@"                                             *");
        Console.WriteLine(@" *                                                                                                             *");
        Console.Write(@" *                                          ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(@"URL：" + options.Urls);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(@"                                                 *");
        Console.WriteLine(@" *                                                                                                             *");
        Console.WriteLine(@" *                                                                                                             *");
        Console.WriteLine(@" ***************************************************************************************************************");
        Console.WriteLine();
    }
}
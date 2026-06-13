using Jasmine.Format.Demo.Modules;
using Spectre.Console;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Jasmine.Format.Demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.MarkupLine("[bold green]===== 生成 HTML 预览页面 =====[/]");
            AnsiConsole.WriteLine();

            var htmlBuilder = new StringBuilder();
            htmlBuilder.Append(@"
<!DOCTYPE html>
<html lang='zh-CN'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Jasmine.Format 演示</title>
    <style>
        body { font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif; padding: 40px; max-width: 1200px; margin: 0 auto; }
        .demo-section { margin-bottom: 40px; padding: 20px; border: 1px solid #eee; border-radius: 8px; background: #fafafa; }
        .demo-section h3 { margin-top: 0; color: #333; }
        .card { border: 1px solid #ccc; border-radius: 8px; padding: 15px; margin: 10px 0; }
        .card h4 { margin-top: 0; color: #333; }
        .code-block { background: #2d2d2d; color: #ccc; padding: 12px; border-radius: 4px; font-family: 'Consolas', 'Monaco', monospace; font-size: 12px; overflow-x: auto; }
        .output-box { border: 1px dashed #ccc; padding: 10px; margin-top: 5px; }
        
        /* Tab 导航样式 */
        .tabs { display: flex; gap: 4px; margin-bottom: 20px; flex-wrap: wrap; }
        .tab { 
            padding: 10px 20px; 
            border: 1px solid #ddd; 
            border-radius: 4px 4px 0 0; 
            cursor: pointer; 
            background: #f5f5f5; 
            transition: all 0.3s;
            font-size: 14px;
        }
        .tab:hover { background: #e8e8e8; }
        .tab.active { 
            background: #fff; 
            border-bottom: 1px solid #fff; 
            font-weight: bold;
            color: #0066cc;
        }
        .tab-content { 
            display: none; 
            border: 1px solid #ddd; 
            border-radius: 0 4px 4px 4px; 
            padding: 20px;
            background: #fff;
        }
        .tab-content.active { display: block; }
    </style>
</head>
<body>
    <h1>Jasmine.Format 演示</h1>
    
    <!-- Tab 导航 -->
    <div class='tabs'>");

            var modules = GetModules();
            
            foreach (var module in modules)
            {
                htmlBuilder.Append($@"
        <div class='tab {(module.Id == modules[0].Id ? "active" : "")}' onclick='switchTab(""{module.Id}"")'>{module.Name}</div>");
            }

            htmlBuilder.Append(@"
    </div>

    <!-- Tab 内容区域 -->");

            foreach (var module in modules)
            {
                htmlBuilder.Append($@"
    <div id='{module.Id}-content' class='tab-content {(module.Id == modules[0].Id ? "active" : "")}'>");
                module.Render(htmlBuilder);
                htmlBuilder.Append(@"
    </div>");
            }

            htmlBuilder.Append(@"
    <script>
        function switchTab(tabId) {
            // 隐藏所有 tab 内容
            var contents = document.querySelectorAll('.tab-content');
            contents.forEach(function(content) {
                content.classList.remove('active');
            });
            
            // 移除所有 tab 的 active 类
            var tabs = document.querySelectorAll('.tab');
            tabs.forEach(function(tab) {
                tab.classList.remove('active');
            });
            
            // 显示选中的 tab
            document.getElementById(tabId + '-content').classList.add('active');
            event.target.classList.add('active');
        }
    </script>
</body>
</html>");

            var outputPath = Path.Combine(Directory.GetCurrentDirectory(), "index.html");
            File.WriteAllText(outputPath, htmlBuilder.ToString(), Encoding.UTF8);

            AnsiConsole.MarkupLine($"[green]HTML 文件已生成:[/] [blue]{outputPath}[/]");
            AnsiConsole.WriteLine();

            int startingPort = 8080;
            int maxPort = 65535;
            int port = startingPort;
            HttpListener listener = null;

            while (port <= maxPort)
            {
                try
                {
                    listener = new HttpListener();
                    listener.Prefixes.Add($"http://localhost:{port}/");
                    listener.Start();
                    AnsiConsole.MarkupLine($"[green]预览服务器已启动:[/] [blue underline]http://localhost:{port}/[/]");

                    if (port != startingPort)
                    {
                        AnsiConsole.MarkupLine($"[yellow]提示:[/] 端口 [red]{startingPort}[/] 被占用，已自动切换到端口 [green]{port}[/]");
                    }

                    break;
                }
                catch (HttpListenerException)
                {
                    listener?.Close();
                    port++;
                }
            }

            if (listener == null || port > maxPort)
            {
                AnsiConsole.MarkupLine("[red]错误:[/] 无法找到可用端口，服务器启动失败");
                return;
            }

            try
            {
                Process.Start(new ProcessStartInfo($"http://localhost:{port}/") { UseShellExecute = true });
                AnsiConsole.MarkupLine("[green]正在打开浏览器...[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]打开浏览器失败:[/] {ex.Message}");
                AnsiConsole.MarkupLine($"请手动访问: [blue underline]http://localhost:{port}/[/]");
            }

            AnsiConsole.MarkupLine("[yellow]服务器将持续运行，按 Ctrl+C 停止...[/]");

            var htmlContent = htmlBuilder.ToString();
            while (true)
            {
                var context = listener.GetContext();
                var request = context.Request;
                var response = context.Response;
                var requestPath = request.Url.AbsolutePath;

                if (requestPath.StartsWith("/Resource/", StringComparison.OrdinalIgnoreCase))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), requestPath.TrimStart('/'));
                    
                    if (File.Exists(filePath))
                    {
                        var extension = Path.GetExtension(filePath).ToLowerInvariant();
                        response.ContentType = GetContentType(extension);
                        
                        using (var fs = File.OpenRead(filePath))
                        {
                            response.ContentLength64 = fs.Length;
                            fs.CopyTo(response.OutputStream);
                        }
                        
                        AnsiConsole.MarkupLine($"[gray]已处理静态文件:[/] {requestPath}");
                    }
                    else
                    {
                        response.StatusCode = 404;
                        var notFoundContent = Encoding.UTF8.GetBytes("404 - File not found");
                        response.ContentLength64 = notFoundContent.Length;
                        response.OutputStream.Write(notFoundContent, 0, notFoundContent.Length);
                        
                        AnsiConsole.MarkupLine($"[red]文件未找到:[/] {filePath}");
                    }
                }
                else
                {
                    response.ContentType = "text/html; charset=UTF-8";
                    var buffer = Encoding.UTF8.GetBytes(htmlContent);
                    response.ContentLength64 = buffer.Length;
                    response.OutputStream.Write(buffer, 0, buffer.Length);
                    
                    AnsiConsole.MarkupLine($"[gray]已处理请求:[/] {request.Url}");
                }
                
                response.Close();
            }
        }

        private static string GetContentType(string extension)
        {
            return extension switch
            {
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".bmp" => "image/bmp",
                ".webp" => "image/webp",
                ".mp4" => "video/mp4",
                ".ogg" => "audio/ogg",
                ".mp3" => "audio/mpeg",
                ".wav" => "audio/wav",
                _ => "application/octet-stream"
            };
        }

        private static List<IExampleModule> GetModules()
        {
            return new List<IExampleModule>
            {
                new FormatHelperModule(),
                new SpanModule(),
                new PModule(),
                new AModule(),
                new ImgModule(),
                new MediaModule(),
                new DivModule(),
                new ListModule(),
                new TableModule()
            };
        }
    }
}

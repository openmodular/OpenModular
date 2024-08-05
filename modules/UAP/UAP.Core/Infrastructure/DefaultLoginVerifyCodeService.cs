using OpenModular.Common.Utils.DependencyInjection;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace OpenModular.Module.UAP.Core.Infrastructure;

internal class DefaultLoginVerifyCodeService : ILoginVerifyCodeService, ISingletonDependency
{
    //颜色列表，用于验证码、噪线、噪点 
    private readonly Color[] _colors = new[] { Color.Black, Color.Red, Color.Blue, Color.Green, Color.Orange, Color.Brown, Color.Brown, Color.DarkBlue };

    private readonly StringHelper _stringHelper;
    private readonly ICacheProvider _cacheHandler;
    private readonly AdminCacheKeys _cacheKeys;

    public DefaultLoginVerifyCodeService(StringHelper stringHelper, ICacheProvider cacheHandler,  AdminCacheKeys cacheKeys)
    {
        _stringHelper = stringHelper;
        _cacheHandler = cacheHandler;
        _cacheKeys = cacheKeys;
    }

    public async Task<LoginVerifyCode> Create()
    {
        var code = _stringHelper.GenerateRandomNumber();

        var bytes = DrawVerifyCode(code);

        var id = Guid.NewGuid().ToString();

        await _cacheHandler.Set(_cacheKeys.VerifyCode(id), code, 5);

        return new LoginVerifyCode(id, "data:image/png;base64," + Convert.ToBase64String(bytes));
    }

    public async Task<bool> Verify(string id, string code)
    {
        if (id.IsNull() || code.IsNull())
            return false;

        var cacheCode = await _cacheHandler.Get(_cacheKeys.VerifyCode(id));
        if (cacheCode.IsNull())
            return false;

        if (!cacheCode.Equals(code))
            return false;

        return true;
    }

    /// <summary>
    /// 绘制验证码图片，返回图片的字节数组
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    private byte[] DrawVerifyCode(string code)
    {
        using var img = new Image<Rgba32>(4 + 16 * code.Length, 40);
        var font = new Font(SystemFonts.Families.First(), 16, FontStyle.Regular);
        var codeStr = code;
        img.Mutate(x =>
        {
            x.BackgroundColor(Color.WhiteSmoke);

            var r = new Random();

            //画噪线 
            for (var i = 0; i < 4; i++)
            {
                int x1 = r.Next(img.Width);
                int y1 = r.Next(img.Height);
                int x2 = r.Next(img.Width);
                int y2 = r.Next(img.Height);
                x.DrawLine(new SolidPen(_colors.RandomGet(), 1L), new PointF(x1, y1), new PointF(x2, y2));
            }

            //画验证码字符串 
            for (int i = 0; i < codeStr.Length; i++)
            {
                x.DrawText(codeStr[i].ToString(), font, _colors.RandomGet(), new PointF((float)i * 16 + 4, 8));
            }
        });

        using var stream = new MemoryStream();
        img.SaveAsPng(stream);
        return stream.GetBuffer();
    }
}
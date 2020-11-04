using System;
using System.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WolfyBot.Core.Helper
{
    public static class Extensions
    {
        public static async Task<JsonValue> ReadAsJsonAsync(this HttpContent content)
        {
            var txtContent = await content.ReadAsStringAsync().ConfigureAwait(false);
            var jObj = JsonObject.Parse(txtContent);
            return jObj;
        }

        public static string ToCamelCase(this string text) => $"{char.ToLower(text[0])}{text.Substring(1)}";

        public static string CleanPacket(string startwith, string endwith, string message)
        {
            while (!message.StartsWith(startwith)) //TODO improve this part
            {
                message = message.Remove(0, 1);
            }
            while (!message.EndsWith(endwith))
            {
                message = message.Remove(message.Length - 1, 1);
            }
            return message;
        }

        public static string ConcatCopy(this string str, int times)
        {
            var builder = new StringBuilder(str.Length * times);

            for (int i = 0; i < times; i++)
            {
                builder.Append(str);
            }

            return builder.ToString();
        }

        public static bool TryGetCoord(this int cellid, out float x, out float y) => TryGetCoord((short)cellid, out x, out y);

        public static bool TryGetCoord(this short cellid, out float x, out float y)
        {
            x = 0;
            y = 0;

            if (cellid < 0 || cellid > 560)
                return false;

            x = cellid % 14; // 14 = Map width
            y = (float)Math.Floor((float)cellid / 14);
            return true;
        }
    }
}
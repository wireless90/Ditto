using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ditto.Common.Spa
{
    public class Resources
    {
        private const string ResourcePrefix = "Ditto.Common.Spa.assets.";
        private const string DittoCssFile = "ditto.css";
        public static string GetCss()
        {
            var css = GetDittoResource(DittoCssFile);
            return css;
        }

        public static string GetHtml(DittoOptions dittoOptions)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var transform in dittoOptions.Transforms)
            {
                stringBuilder.Append($@"<li><a href=""{dittoOptions.Path+ "?key=" + transform.TransformName}"">{transform.TransformName}</a></li>");
            }

            var html = $@"<!DOCTYPE html>
                <html>
                    <head>
                        <meta charset=""UTF-8"">
                        <title>Ditto Transform</title>
                        <style>
                            
                        </style>
                    </head>
                    <body>
                        <div >
                            <h2>Please select a user to continue authentication.</h2>
                            <ul>
                                {stringBuilder}
                            </ul>
                        </div>
                    </body>
                </html>";

            return html;
        }

        private static string GetDittoResource(string resourceName)
        {
            var resource = string.Empty;

            var assembly = typeof(Resources).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream(
                ResourcePrefix + resourceName))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    resource = streamReader.ReadToEnd();
                }
            }

            return resource;
        }

        public static byte[] ReadResource(string resourceName)
        {;
            var assembly = typeof(Resources).GetTypeInfo().Assembly;

            using (var stream = assembly.GetManifestResourceStream(
                ResourcePrefix + resourceName))
            {
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
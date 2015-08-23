using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using RazorEngine.Templating;

namespace RazorTest.TEST_FILES
{
    public class TemplateManager : ITemplateManager
    {
      //  private static readonly string usingStatement = "@model dynamic" + Environment.NewLine;

        public ITemplateSource Resolve(ITemplateKey key)
        {
            // Resolve your template here (ie read from disk)
            // if the same templates are often read from disk you probably want to do some caching here.
            var relativePath = "";
            
            if (!key.Name.StartsWith("~"))
            {
                relativePath = @"~/views/TESTViews/" + key.Name + ".cshtml";
            }
            else
            {
                relativePath = (key.Name);
            }

            var serverPath = HttpContext.Current.Server.MapPath(relativePath);

            string templateHtml;

            try
            {
                //templateHtml = usingStatement + File.ReadAllText(serverPath);

                templateHtml = File.ReadAllText(serverPath);
            }
            catch (DirectoryNotFoundException)
            {
                templateHtml = "No folder named " + Path.GetDirectoryName(serverPath) + ". in other words add a folder named " + Path.GetDirectoryName(serverPath);
            }
            catch (FileNotFoundException)
            {
                templateHtml = "No template '" + Path.GetFileName(key.Name) + ". In other words add template named " + Path.GetFileName(key.Name) + ".cshtml in a folder called " + Path.GetDirectoryName(serverPath);
            }

            // Provide a non-null file to improve debugging
            return new LoadedTemplateSource(templateHtml, serverPath);
        }

        public ITemplateKey GetKey(string name, ResolveType resolveType, ITemplateKey context)
        {
            // If you can have different templates with the same name depending on the
            // context or the resolveType you need your own implementation here!
            // Otherwise you can just use NameOnlyTemplateKey.
            return new NameOnlyTemplateKey(name, resolveType, context);
        }

        public void AddDynamic(ITemplateKey key, ITemplateSource source)
        {
            // You can disable dynamic templates completely, but
            // then all convenience methods (Compile and RunCompile) with
            // a TemplateSource will no longer work (they are not really needed anyway).
            throw new NotImplementedException("dynamic templates are not supported!");
        }
    }
}
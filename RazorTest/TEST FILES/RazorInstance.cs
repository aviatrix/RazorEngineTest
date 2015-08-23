using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace RazorTest.TEST_FILES
{
    public static class RazorInstance
    {
        private static IRazorEngineService _instance;

        public static IRazorEngineService Instance
        {
            get
            {
                return _instance ?? (_instance = RazorEngineService.Create(new TemplateServiceConfiguration()
                {
                    Language = Language.CSharp,
                    EncodedStringFactory = new RawStringFactory(),
                    TemplateManager = new TemplateManager(),
                    DisableTempFileLocking = true,
                    CachingProvider = new InvalidatingCachingProvider(),
                    Namespaces = new HashSet<string>()
                    {
                        "System",
                        "System.Collections",
                        "System.Collections.Generic",
                        "System.Linq",
                        "System.Text"
                    }

                }));
            }
        }
    }
}
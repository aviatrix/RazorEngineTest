using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RazorTest.TEST_FILES;
using DynamicViewBag = RazorEngine.Templating.DynamicViewBag;
using RazorEngine.Templating;

namespace RazorTest.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            ViewBag.Title = "Home Page";

            var tw = new StringWriter();
             
            var viewKey = "Simpletest";

            var model = new SimpleModel()
            {
                Content = "fooo bar baz ",
                StringList = new List<string>()
                {
                    "foo",
                    "bar",
                    "baz",
                    "long text",
                }
            };


            var viewbag = new DynamicViewBag();

            try
            {
                RazorInstance.Instance.RunCompile(viewKey, tw, typeof(SimpleModel), model);
            
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException ex)
            {
                throw new Exception("There is no property called " + ex.Message.Split(' ').Last(), ex);
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return tw.ToString();
        }
    }
}

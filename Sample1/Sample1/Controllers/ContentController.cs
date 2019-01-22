using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sample1.Models;

namespace Sample1.Controllers
{
    /// <summary>
    /// 2019-01-22
    /// JXJ
    /// 控制器
    /// </summary>
    public class ContentController : Controller
    {
        private readonly Content contents;
        public ContentController(Microsoft.Extensions.Options.IOptions<Content> option)
        {
            contents = option.Value;
        }
        /// <summary>
        /// 首页显示
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View(new ContentViewModel { Contents = new List<Content> { contents } });
        }
    }
}
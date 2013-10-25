using Orchard.ContentManagement;
using Orchard.DisplayManagement;
using Orchard.Localization;
using Orchard.Mvc;
using Orchard.Settings;
using Orchard.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTest.Controllers
{
    [Themed]
    public class HomeController : Controller
    {
        private readonly IContentManager _contentManager;
        private readonly ISiteService _siteService;
        private dynamic Shape { get; set; }

        public HomeController(
            IContentManager contentManager,
            IShapeFactory shapeFactory,
            ISiteService siteService)
        {
            _contentManager = contentManager;
            _siteService = siteService;
            Shape = shapeFactory;
        }

        public ActionResult Simple()
        {
            return View();
        }

        public ActionResult Complex()
        {
            var contentItems = _contentManager.Query(VersionOptions.Published, "Page").List();
            var itemList = Shape.List(); // create list shape
            itemList.AddRange(
                contentItems.Select(x => 
                    _contentManager.BuildDisplay(x, "Summary") // create summary shape to display content
            ));
            var result = Shape.Content_ItemList(ItemList: itemList);
            return new ShapeResult(this, result);
        }
    }
}
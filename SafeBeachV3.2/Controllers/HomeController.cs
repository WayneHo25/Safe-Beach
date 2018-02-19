using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafeBeachV3._2.Models;

namespace SafeBeachV3._2.Controllers
{
    public class HomeController : Controller
    {
        private BeachInfoEntities db = new BeachInfoEntities();
        public ActionResult Index()
        {
            return View();
        }

        /*
            Function: This is the code for searching beaches by suburb name, beach name and postcode. 

            Input: search string, search parameter

            Output: 1. A list of beach items which match the search string
                    2. Null
    
            Notes: 1. The search parameter comes from the hidden input.
                   2. If the search result is empty, it will return the searchNull page to users.
        */
        [HttpPost]
        public ActionResult Search(string searchString, string search_param)
        {
            var beaches = from m in db.BeachInfoV3_1
                          select m;
            var choice = Request.Form["search_param"];
            if (choice == "suburbName")
            {
                beaches = beaches.Where(s => s.beachSuburb.Contains(searchString));
            }
            else
            {
                if (choice == "beachName")
                {
                    beaches = beaches.Where(s => s.beachName.Contains(searchString));
                }
                else
                {
                    beaches = beaches.Where(s => s.postcode.ToString().Contains(searchString));
                }
            }
            if (beaches.Count() > 0)
            {
                return View(beaches.OrderBy(s => s.beachName));
            }
            else
            {
                return View("SearchNull");
            }
        }

        /*
            Function: This is the code for searching beaches by safety filters. 

            Input: swim ability choice, wave height choice and patrol availability choice

            Output: 1. A list of beach items which match the search string
                    2. Null
    
            Notes: 1. If the users choose "can't" for swim ability, the wave height will always be low.
                   2. If the users choose "beginner" for swim ability, the wave height can be low or medium.
                   3. If the search result is empty, it will return the searchNull page to users.
        */
        public ActionResult Search2(string swimLevelChoice, string surfLevelChoice, string patroled)
        {
            var beaches = from m in db.BeachInfoV3_1
                          select m;
            var swimLevel = Request.Form["swimLevelChoice"];
            var surfLevel = Request.Form["surfLevelChoice"];
            var patrol = Request.Form["patroled"];
            if (!String.IsNullOrEmpty(swimLevel) & !String.IsNullOrEmpty(surfLevel) & !String.IsNullOrEmpty(patrol))
            {
                int a = int.Parse(swimLevel);
                int b = int.Parse(surfLevel);
                int c = int.Parse(patrol);
                if (c == 1)
                {
                    beaches = beaches.Where(s => s.patrol.Contains("Yes"));
                }
                else
                {
                    beaches = beaches.Where(s => s.patrol.Contains("No"));
                }
                if (a == 0)
                {
                    beaches = beaches.Where(s => s.surfLevel.Contains("Low"));
                }
                else
                {
                    if (a == 1)
                    {
                        if (b < 1)
                        {
                            beaches = beaches.Where(s => s.surfLevel.Contains("Low"));
                        }
                        else
                        {
                            beaches = beaches.Where(s => s.surfLevel.Contains("Medium"));
                        }
                    }
                    else
                    {
                        if (a == 2)
                        {
                            if (b == 0)
                            {
                                beaches = beaches.Where(s => s.surfLevel.Contains("Low"));
                            }
                            if (b == 1)
                            {
                                beaches = beaches.Where(s => s.surfLevel.Contains("Medium"));
                            }
                            if (b == 2)
                            {
                                beaches = beaches.Where(s => s.surfLevel.Contains("High"));
                            }
                        }
                    }
                }
            }
            if (beaches.Count() > 0)
            {
                return View("Search2", beaches.OrderBy(s => s.beachName));
            }
            else
            {
                return View("SearchNull");
            }
        }

        /*
            Function: This is the code for searching beaches by regions. 

            Input: region name

            Output: A list of beach items which match the search string
    
            Notes: 1. the search string is hard code in HTML and the search string must be in database. Therefore, the search result will not be empty.
        */
        public ActionResult Search3(string searchString)
        {
            var beaches = from m in db.BeachInfoV3_1
                          select m;
            beaches = beaches.Where(s => s.region.Contains(searchString));
            ViewBag.Message = searchString;
            return View(beaches.OrderBy(s => s.beachName));
        }

        /*
            Function: This is the code for returning SearchNull page. 

            Input: None

            Output: SeachNull page
    
            Notes: None
        */
        public ActionResult SearchNull()
        {
            return View();
        }

        /*
            Function: This is the code for returning Details page. 

            Input: The id of a certain beach

            Output: Details page

            Notes: None
        */
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BeachInfoV3_1 beach = db.BeachInfoV3_1.Find(id);
            if (beach == null)
            {
                return HttpNotFound();
            }
            return View(beach);
        }

        /*
            Function: This is the code for returning About page. 

            Input: None

            Output: About page

            Notes: None
        */
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /*
            Function: This is the code for returning Tips page. 

            Input: None

            Output: Tips page

            Notes: None
        */
        public ActionResult Tips1()
        {
            ViewBag.Message = "Your tips page.";

            return View();
        }
        public ActionResult Tips2()
        {
            ViewBag.Message = "Your tips page.";

            return View();
        }
        public ActionResult Tips3()
        {
            ViewBag.Message = "Your tips page.";

            return View();
        }
        public ActionResult Recommend()
        {
            ViewBag.Message = "Recommended beaches page.";

            return View();
        }
    }
}
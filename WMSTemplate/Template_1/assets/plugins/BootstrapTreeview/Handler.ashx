<%@ WebHandler Language="C#" Class="Handler" %>

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class Handler : IHttpHandler
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        using (DataClassesDatabaseDataContext db = new DataClassesDatabaseDataContext())
        {
            List<Sidebar> ListSidebar = new List<Sidebar>();

            ListSidebar.AddRange(db.TBMenus.Where(item => item.TBMenus.Count > 0).Select(item => new Sidebar
            {
                text = item.Nama,
                icon = item.Icon,
                selectable = false,
                backColor = "#eee",
                nodes = item.TBMenus.Count > 0 ? item.TBMenus.Select(item2 => new Sidebar
                {
                    text = item2.Nama,
                    href = item2.Url
                })
            .ToList() : null
            }));

            context.Response.Write(JsonConvert.SerializeObject(ListSidebar));
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public class Sidebar
    {
        public string text { get; set; }
        public string icon { get; set; }
        public string selectedIcon { get; set; }
        //public string color { get; set; }
        public string backColor { get; set; }
        public string href { get; set; }
        public bool selectable { get; set; }
        public State state { get; set; }
        public string[] tags { get; set; }
        public List<Sidebar> nodes { get; set; }
    }

    public class State
    {
        public bool disabled { get; set; }
        public bool expanded { get; set; }
        public bool selected { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Post_Model
/// </summary>

[Serializable]
public class Post_Model
{
    public string Judul { get; set; }
    public string Deskripsi { get; set; }
    public string Align { get; set; }
    public string Tags { get; set; }
}
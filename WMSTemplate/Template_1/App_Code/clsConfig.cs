using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public static class clsConfig
{
    #region Settings Properties
    /// <summary>
    ///  Max upload filesize in Mb
    /// </summary>
    public static int intMaxUploadSizeMb
    {
        get
        {
            return 100;
        }
    }

    /// <summary>
    ///  Allowed image file extensions
    /// </summary>
    public static string strAllowedImageExtensions
    {
        get
        {
            return "'jpg', 'jpeg', 'png', 'gif', 'bmp', 'tiff'";
        }
    }

    /// <summary>
    ///  Allowed image file extensions as an array
    /// </summary>
    public static string[] arrAllowedImageExtensions
    {
        get
        {
            return getArrayFromString(strAllowedImageExtensions);
        }
    }

    /// <summary>
    ///  Allowed document file extensions
    /// </summary>
    public static string strAllowedFileExtensions
    {
        get
        {
            return "'doc', 'docx', 'pdf', 'xls', 'xlsx', 'txt', 'csv','html','psd','sql','log','fla','xml','ade','adp','ppt','pptx'";
        }
    }

    /// <summary>
    ///  Allowed document file extensions as an array
    /// </summary>
    public static string[] arrAllowedFileExtensions
    {
        get
        {
            return getArrayFromString(strAllowedFileExtensions);
        }
    }

    /// <summary>
    ///  Allowed video file extensions
    /// </summary>
    public static string strAllowedVideoExtensions
    {
        get
        {
            return "'mov', 'mpeg', 'mp4', 'avi', 'mpg','wma'";
        }
    }

    /// <summary>
    ///  Allowed video file extensions as an array
    /// </summary>
    public static string[] arrAllowedVideoExtensions
    {
        get
        {
            return getArrayFromString(strAllowedVideoExtensions);
        }
    }

    /// <summary>
    ///  Allowed music file extensions
    /// </summary>
    public static string strAllowedMusicExtensions
    {
        get
        {
            return "'mp3', 'm4a', 'ac3', 'aiff', 'mid'";
        }
    }

    /// <summary>
    ///  Allowed music file extensions as an array
    /// </summary>
    public static string[] arrAllowedMusicExtensions
    {
        get
        {
            return getArrayFromString(strAllowedMusicExtensions);
        }
    }

    /// <summary>
    ///  Allowed misc file extensions
    /// </summary>
    public static string strAllowedMiscExtensions
    {
        get
        {
            return "'zip', 'rar'";
        }
    }

    /// <summary>
    ///  Allowed misc file extensions as an array
    /// </summary>
    public static string[] arrAllowedMiscExtensions
    {
        get
        {
            return getArrayFromString(strAllowedMiscExtensions);
        }
    }

    /// <summary>
    ///  All allowed file extensions
    /// </summary>
    public static string strAllowedAllExtensions
    {
        get
        {
            string strRet = "";

            if (strAllowedImageExtensions.Length > 0)
            {
                strRet = strAllowedImageExtensions;
            }
            if (strAllowedFileExtensions.Length > 0)
            {
                if (strRet.Length > 0)
                {
                    strRet += "," + strAllowedFileExtensions;
                }
                else
                {
                    strRet = strAllowedFileExtensions;
                }
            }
            if (strAllowedVideoExtensions.Length > 0)
            {
                if (strRet.Length > 0)
                {
                    strRet += "," + strAllowedVideoExtensions;
                }
                else
                {
                    strRet = strAllowedVideoExtensions;
                }
            }
            if (strAllowedMusicExtensions.Length > 0)
            {
                if (strRet.Length > 0)
                {
                    strRet += "," + strAllowedMusicExtensions;
                }
                else
                {
                    strRet = strAllowedMusicExtensions;
                }
            }
            if (strAllowedMiscExtensions.Length > 0)
            {
                if (strRet.Length > 0)
                {
                    strRet += "," + strAllowedMiscExtensions;
                }
                else
                {
                    strRet = strAllowedMiscExtensions;
                }
            }

            return strRet;
        }
    }

    /// <summary>
    /// Returns document root
    /// </summary>
    public static string strDocRoot
    {
        get
        {
            //return HttpContext.Current.Request.ServerVariables["APPL_PHYSICAL_PATH"].TrimEnd('\\');
            return HttpContext.Current.Server.MapPath("/").TrimEnd('\\');
        }
    }

    /// <summary>
    /// Returns the base url of the site
    /// </summary>
    public static string strBaseURL
    {
        get
        {
            return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority.TrimEnd('/');
        }
    }

    /// <summary>
    /// Returns the full upload drive path
    /// </summary>
    public static string strUploadPath
    {
        get
        {
            return strDocRoot + "\\" + ("upload\\files").TrimEnd('\\') + "\\";
        }
    }

    /// <summary>
    /// Returns the full thumb drive path
    /// </summary>
    public static string strThumbPath
    {
        get
        {
            return strDocRoot + "\\" + ("upload\\thumbs").TrimEnd('\\') + "\\";
        }
    }

    /// <summary>
    /// Returns the full upload url
    /// </summary>
    public static string strUploadURL
    {
        get
        {
            return strBaseURL + "/" + ("upload\\files").Replace('\\', '/');
        }
    }

    /// <summary>
    /// Returns the full thumb url
    /// </summary>
    public static string strThumbURL
    {
        get
        {
            return strBaseURL + "/" + ("upload\\thumbs").Replace('\\', '/');
        }
    }

    /// <summary>
    /// Returns the setting for allowing upload of file
    /// </summary>
    public static bool boolAllowUploadFile
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Returns the setting for allowing delete of file
    /// </summary>
    public static bool boolAllowDeleteFile
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Returns the setting for allowing creation of folder
    /// </summary>
    public static bool boolAllowCreateFolder
    {
        get
        {
            return true;
        }
    }

    /// <summary>
    /// Returns the setting for allowing delete of folder
    /// </summary>
    public static bool boolAllowDeleteFolder
    {
        get
        {
            return true;
        }
    }
    #endregion

    private static string[] getArrayFromString(string strInput)
    {
        string[] arrExt;
        string strTemp;

        //remove lead and trail single quotes so we can SPLIT the hell out of it
        strTemp = strInput.Trim('\'');
        arrExt = strTemp.Split(new string[] { "'", ",", "'" }, StringSplitOptions.RemoveEmptyEntries);

        return arrExt;
    }   // getArrayFromString

}   // class
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class plugins_EncryptDecrypt_Default : System.Web.UI.Page
{
    protected void ButtonEncrypt_Click(object sender, EventArgs e)
    {
        //Get the Input File Name and Extension.
        string fileName = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

        //Build the File Path for the original (input) and the encrypted (output) file.
        string input = Server.MapPath("~/Files/") + fileName + fileExtension;
        string output = Server.MapPath("~/Files/") + fileName + "_enc" + fileExtension;

        //Save the Input File, Encrypt it and save the encrypted file in output path.
        FileUpload1.SaveAs(input);
        EncryptDecrypt.Encrypt(input, output);

        Response.Write(FileUpload1.PostedFile.ContentType);
        //Download the Encrypted File.
        //Response.ContentType = FileUpload1.PostedFile.ContentType;
        //Response.Clear();
        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
        //Response.WriteFile(output);
        //Response.Flush();

        //Delete the original (input) and the encrypted (output) file.
        File.Delete(input);
        //File.Delete(output);

        Response.End();
    }

    protected void DecryptFile(object sender, EventArgs e)
    {
        //Get the Input File Name and Extension
        string fileName = Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
        string fileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

        //Build the File Path for the original (input) and the decrypted (output) file
        string input = Server.MapPath("~/Files/") + fileName + fileExtension;
        string output = Server.MapPath("~/Files/") + fileName + "_dec" + fileExtension;

        //Save the Input File, Decrypt it and save the decrypted file in output path.
        FileUpload1.SaveAs(input);
        EncryptDecrypt.Decrypt(input, output);

        //Download the Decrypted File.
        Response.Clear();
        Response.ContentType = FileUpload1.PostedFile.ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(output));
        Response.WriteFile(output);
        Response.Flush();

        //Delete the original (input) and the decrypted (output) file.
        File.Delete(input);
        File.Delete(output);

        Response.End();
    }
}
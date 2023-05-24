using AFEditor;
using System.Drawing;

if (args.Length == 0)
{
    Console.WriteLine("Argument not enough exception! 1: Input Image or Folder; 2: Output Folder;");
    Console.ReadLine();
    return;
}

var arg = args[0];
var output = args.Length > 1 ? args[1] : string.Empty;

if (File.Exists(arg))
{
    try
    {
        var image = Image.FromFile(arg);
        var newPath = Path.ChangeExtension(arg, "rip");
        if (!string.IsNullOrEmpty(output))
        {
            var fileName = Path.GetFileName(newPath);
            WriteImage(image, Path.Combine(output, fileName));
        }
        else
        {
            WriteImage(image, newPath);
        }
    }
    catch
    {
        Console.WriteLine($"Can't convert file {arg}");
    }
}
else if (Directory.Exists(arg))
{
    var directory = Directory.GetFiles(arg);
    if (string.IsNullOrEmpty(output))
    {
        output = Path.Combine(arg, "Converted");
    }

    if (!Directory.Exists(output))
    {
        Directory.CreateDirectory(output);
    }

    foreach (var file in directory)
    {
        try
        {
            var image = Image.FromFile(file);
            var newPath = Path.ChangeExtension(file, "rip");
            var fileName = Path.GetFileName(newPath);
            WriteImage(image, Path.Combine(output, fileName));
        }
        catch
        {
            Console.WriteLine($"Can't convert file {arg}");
        }
    }
}
else
{
    Console.WriteLine("Argument not image or folder! 1: Input Image or Folder; 2: Output Image or Folder;");
    Console.ReadLine();
}

void WriteImage(Image image, string pathToWrite)
{
    CBitWriter bw = new CBitWriter();

    CR6Ti.WriteTransparent((Bitmap)image, bw, (short)image.Width, (short)image.Height);

    using (FileStream file = new FileStream(pathToWrite, FileMode.Create, System.IO.FileAccess.Write))
    {
        bw.CopyToStream(file);
    }
}
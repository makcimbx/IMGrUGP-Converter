using AFEditor;
using System.Drawing;

if (args.Length == 0)
{
    Console.WriteLine("Не хватает аргументов! В качестве аргумента могут быть папка или изображение.");
    Console.ReadLine();
    return;
}

var arg = args[0];
if (File.Exists(arg))
{
    var image = Image.FromFile(arg);
    var newPath = Path.ChangeExtension(arg, "rip");
    WriteImage(image, newPath);
}
else if (Directory.Exists(arg))
{
    var directory = Directory.GetFiles(arg);
    foreach (var file in directory)
    {
        var image = Image.FromFile(file);
        var newPath = Path.ChangeExtension(file, "rip");
        WriteImage(image, newPath);
    }
}
else
{
    Console.WriteLine("Неправильные аргументы! В качестве аргумента могут быть папка или изображение.");
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
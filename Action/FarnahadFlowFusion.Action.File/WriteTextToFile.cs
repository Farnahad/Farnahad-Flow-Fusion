﻿using System.Text;
using FarnahadFlowFusion.Action.Main;
using FarnahadFlowFusion.Service.Scripting.CSharp;

namespace FarnahadFlowFusion.Action.File;

public class WriteTextToFile : IAction
{
    private readonly CSharpService _cSharpService;

    public string Name => "Write text to file";

    public ActionInput FilePtah { get; set; }
    public ActionInput TextToWrite { get; set; }
    public bool AppendNewLine { get; set; }
    public WriteTextToFileBase.IfFileExists IfFileExists { get; set; }
    public WriteTextToFileBase.Encoding Encoding { get; set; }

    public WriteTextToFile()
    {
        _cSharpService = new CSharpService();

        FilePtah = new ActionInput();
        TextToWrite = new ActionInput();
        AppendNewLine = true;
        IfFileExists = WriteTextToFileBase.IfFileExists.OverwriteExistingContent;
        Encoding = WriteTextToFileBase.Encoding.Unicode;
    }

    public async Task Execute(SandBox sandBox)
    {
        var filePtahValue = await _cSharpService.EvaluateActionInput<string>(sandBox, FilePtah);
        var textToWriteValue = await _cSharpService.EvaluateActionInput<string>(sandBox, TextToWrite);

        var encoding = Encoding switch
        {
            WriteTextToFileBase.Encoding.Ascii => global::System.Text.Encoding.ASCII,
            WriteTextToFileBase.Encoding.SystemDefault => global::System.Text.Encoding.Default,
            WriteTextToFileBase.Encoding.Unicode => global::System.Text.Encoding.Unicode,
            WriteTextToFileBase.Encoding.UnicodeBigEndian => global::System.Text.Encoding.BigEndianUnicode,
            WriteTextToFileBase.Encoding.UnicodeNoByteOrderMark => new UnicodeEncoding(false, true),
            WriteTextToFileBase.Encoding.Utf8 => global::System.Text.Encoding.UTF8,
            WriteTextToFileBase.Encoding.Utf8NoByteOrderMark => new UTF8Encoding(false),
            _ => global::System.Text.Encoding.UTF8
        };

        var fileInfo = new FileInfo(filePtahValue);

        if (AppendNewLine)
            textToWriteValue = textToWriteValue + Environment.NewLine;

        if ((fileInfo.Exists && IfFileExists == WriteTextToFileBase.IfFileExists.OverwriteExistingContent) ||
            fileInfo.Exists == false)
        {
            await global::System.IO.File.WriteAllTextAsync(filePtahValue, textToWriteValue, encoding);
        }
        else if (fileInfo.Exists && IfFileExists == WriteTextToFileBase.IfFileExists.AppendContent)
        {
            await global::System.IO.File.AppendAllTextAsync(filePtahValue, textToWriteValue, encoding);
        }
    }
}
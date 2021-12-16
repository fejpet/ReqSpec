using Markdig;
using System.IO;
using System;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;


MarkdownPipeline pipline;
string html;
string markdown;

markdown = File.ReadAllText("Readme.md");

pipline = new MarkdownPipelineBuilder()
  .Build();

html = Markdown.ToHtml(markdown, pipline);

pipline = new MarkdownPipelineBuilder()
  .UseAdvancedExtensions()
  .UseAutoIdentifiers()
  .Build();

html = Markdown.ToHtml(markdown, pipline);

var parser = new HtmlParser();
var document = parser.ParseDocument(html);

List<Tuple<string, string>> requirements = new List<Tuple<string, string>>();

foreach (IElement element in document.QuerySelectorAll("*").Where(item => item.ClassList.Contains("requirement")))
{
    requirements.Add(new Tuple<string, string>(element.Id, element.TextContent));
    Console.WriteLine($"'{element.TextContent}' id:'{element.Id}'");
    var small = document.CreateElement("small");
    small.TextContent = $"({element.Id})";
    element.InsertAfter(small);
}

foreach (IElement element in document.QuerySelectorAll("*").Where(item => item.TextContent.Equals("Table of Requirements")))
{
    var ul = document.CreateElement("ul");
    foreach (var item in requirements)
    {
        var li = document.CreateElement("li");
        var a = document.CreateElement("a");
        a.SetAttribute("href", $"#{item.Item1}");
        a.TextContent = $"{item.Item1} - {item.Item2}";
        li.AppendChild(a);
        ul.AppendChild(li);
    }
    element.InsertAfter(ul);
}

//https://github.com/AngleSharp/AngleSharp/blob/devel/docs/tutorials/03-Examples.md
File.WriteAllText(@"result.html", document.DocumentElement.OuterHtml);
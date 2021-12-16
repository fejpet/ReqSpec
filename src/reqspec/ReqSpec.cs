using Markdig;
using System.IO;
using System;
using AngleSharp.Dom;
using AngleSharp.Html.Parser;

namespace reqspec;
public class ReqSpec
{

    private IList<Tuple<string, string>> _requirements = new List<Tuple<string, string>>();
    private AngleSharp.Html.Dom.IHtmlDocument _document = null;

    public IEnumerable<Tuple<string, string>> Requirements
    {
        get
        {
            return _requirements;
        }
    }
    public ReqSpec Parse(string filename)
    {
        MarkdownPipeline pipline;
        string html;
        string markdown;

        markdown = File.ReadAllText(filename);

        pipline = new MarkdownPipelineBuilder()
          .Build();

        html = Markdown.ToHtml(markdown, pipline);

        pipline = new MarkdownPipelineBuilder()
          .UseAdvancedExtensions()
          .UseAutoIdentifiers()
          .Build();

        html = Markdown.ToHtml(markdown, pipline);

        var parser = new HtmlParser();

        _document = parser.ParseDocument(html);



        foreach (IElement element in _document.QuerySelectorAll("*").Where(item => item.ClassList.Contains("requirement")))
        {
            if (element != null)
            {
                _requirements.Add(new Tuple<string, string>(element.Id, element.TextContent));
                var small = _document.CreateElement("small");
                small.TextContent = $"({element.Id})";
                element.InsertAfter(small);
            }
        }

        foreach (IElement element in _document.QuerySelectorAll("*").Where(item => item.TextContent.Equals("Table of Requirements")))
        {
            var ul = _document.CreateElement("ul");
            foreach (var item in _requirements)
            {
                var li = _document.CreateElement("li");
                var a = _document.CreateElement("a");
                a.SetAttribute("href", $"#{item.Item1}");
                a.TextContent = $"{item.Item1} - {item.Item2}";
                li.AppendChild(a);
                ul.AppendChild(li);
            }
            element.InsertAfter(ul);
        }
        return this;
    }
    public string Result
    {
        get
        {
            return _document.DocumentElement.OuterHtml;
        }
    }
    public void WriteResult(string filename)
    {
        if (_document != null)
        {
            File.WriteAllText(filename, _document.DocumentElement.OuterHtml);
        }
    }

}

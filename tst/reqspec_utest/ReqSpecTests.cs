using reqspec;
using System.Linq;
using NUnit.Framework;

namespace reqspec_utest;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEmptyReqSpec()
    {
        var reqspec = new ReqSpec();

        Assert.False(reqspec.Requirements.Any());
    }

    [Property("REQ", "REQ_1")]
    [Test]
    public void TestNoControlInFileReqSpec()
    {
        var reqspec = new ReqSpec();
        reqspec.Parse(@"Sample01.md");
        Assert.False(reqspec.Requirements.Any());
        Assert.That(reqspec.Result, Is.EqualTo("<html><head></head><body><h1 id=\"title\">Title</h1>\n<p>Nothing in this file.</p>\n</body></html>"));
    }

    [Property("REQ", "REQ_4")]
    [Test]
    public void TestRequirementAddedReqSpec()
    {
        var reqspec = new ReqSpec();
        reqspec.Parse(@"Sample02.md");
        Assert.That(reqspec.Requirements.Any());

        Assert.That(reqspec.Requirements.Any(item => item.Item1 == "REQ_1"));
        Assert.That(reqspec.Requirements.Any(item => item.Item1 == "REQ_1.1"));
        Assert.That(reqspec.Requirements.Any(item => item.Item1 == "REQ_2"));
    }
}
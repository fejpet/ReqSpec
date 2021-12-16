using parsereqspec;

var reqspec = new ReqSpec();

reqspec.Parse("Readme.md");

reqspec.WriteResult(@"result.html");

foreach (var item in reqspec.Requirements)
{
    Console.WriteLine($"{item.Item1} - {item.Item2}");
}
//https://github.com/AngleSharp/AngleSharp/blob/devel/docs/tutorials/03-Examples.md

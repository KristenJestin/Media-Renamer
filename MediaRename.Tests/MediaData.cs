using MediaRenamer.Models.Medias;
using Newtonsoft.Json;
using System.Reflection;
using System.Xml.Linq;

namespace MediaRename.Tests
{
    public class Tests
    {
        private IEnumerable<string> inputs;
        private IEnumerable<MediaParserResult> outputs;

        [SetUp]
        public async Task Setup()
        {
            var directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fileInput = Path.Combine(directoryName, "data", $"media-data.json");
            var fileOutput = Path.Combine(directoryName, "data", $"media-data.output.json");

            var inputContent = await File.ReadAllTextAsync(fileInput);
            var outputContent = await File.ReadAllTextAsync(fileOutput);
            inputs = JsonConvert.DeserializeObject<IEnumerable<string>>(inputContent);
            outputs = JsonConvert.DeserializeObject<IEnumerable<MediaParserResult>>(outputContent);
        }

        [Test]
        public void Test1()
        {
            for (var i = 0; i < inputs.Count(); i++)
            {
                // Arrange
                var input = inputs.ElementAt(i);
                var output = outputs.ElementAt(i);

                // Act
                var result = MediaParser.Default.Parse(input);

                // Assert
                Assert.That(result.Title, Is.EqualTo(output.Title));
            }
        }
    }
}
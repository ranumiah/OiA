using System.IO;
using System.Reflection;
using NUnit.Framework;

namespace OiA.Tests
{
    [TestFixture]
    public class ReadFileTests
    {
        string testDirectory = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"..\..\..\..\TestData"));

        [Test]
        public void ReadFileDataTest()
        {
            ReadFile readFile = new ReadFile();

            var testFile = Path.Combine(testDirectory, "Aquatic Flowers.jpg");
            var fileDetails = readFile.GetFileDetails(testFile);

            Assert.That(fileDetails.Name, Is.EqualTo("Aquatic Flowers.jpg"));
            Assert.That(fileDetails.Extension, Is.EqualTo(".jpg"));
            Assert.That(fileDetails.Length, Is.EqualTo(453438));
            Assert.That(fileDetails.FullName, Is.EqualTo(testFile));
        }

        [Test]
        public void CreateFileMd5Hash()
        {
            string file = Path.Combine(testDirectory, "Aquatic Flowers.jpg");
            var readAllBytes = File.ReadAllBytes(file);

            var hashFile = new HashFile();
            var md5Hash = hashFile.GenerateHash(readAllBytes);

            Assert.That(md5Hash, Is.EqualTo("35D25CDEB2EB861F6DEC063E62FC78A4"));
        }

        [Test]
        public void CreateFileSha256Hash()
        {
            var file = Path.Combine(testDirectory, "Aquatic Flowers.jpg");
            var readAllBytes = File.ReadAllBytes(file);
            var hashFile = new HashFile();
            var sha256Hash = hashFile.GenerateHash(readAllBytes, HashType.SHA256);

            Assert.That(sha256Hash, Is.EqualTo("34B6919D75D14DE1D8EF71F0ECB288FF47CAA37A475EF9063C0B42A75409A443"));
        }

        [Test]
        public void CreateFileSha512Hash()
        {
            var file = Path.Combine(testDirectory, "Aquatic Flowers.jpg");
            var readAllBytes = File.ReadAllBytes(file);
            var hashFile = new HashFile();
            var sha256Hash = hashFile.GenerateHash(readAllBytes, HashType.SHA512);

            Assert.That(sha256Hash, Is.EqualTo("7B9B401EFD2575EB1734F39CB93B74273632378B05E37B3C8DC9EE0F75BE901B0BE1EE63EA04476A72163F76B04A18C3136C30D4538D0AA38FCEAA780E3CCB43"));
        }
    }
}

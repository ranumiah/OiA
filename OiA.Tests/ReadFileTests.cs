using System;
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

            FileDetail fileDetails = readFile.GetFileDetails(Path.Combine(testDirectory, "Aquatic Flowers.jpg"));

            Assert.That(fileDetails.FileName, Is.EqualTo("Aquatic Flowers.jpg"));
            Assert.That(fileDetails.FileExtension, Is.EqualTo(".jpg"));
            Assert.That(fileDetails.FileLength, Is.EqualTo(453438));
            Assert.That(fileDetails.FileFullName, Is.EqualTo(@"C:\code\OiA\TestData\Aquatic Flowers.jpg"));
            Assert.That(fileDetails.FileLastWriteTimeUtc, Is.EqualTo(new DateTime(2015,09,21,11,28,40)));
        }

        [Test]
        public void CreateFileMd5Hash()
        {
            var md5Hash = HashFile.GenerateMd5Hash(Path.Combine(testDirectory, "Aquatic Flowers.jpg"));

            Assert.That(md5Hash, Is.EqualTo("E? <??R\\!N%?=ZO?"));
        }

        [Test]
        public void CreateFileSha256Hash()
        {
            var sha256Hash = HashFile.GenerateSHA256String(Path.Combine(testDirectory, "Aquatic Flowers.jpg"));

            Assert.That(sha256Hash, Is.EqualTo("0EA9E367888A6A8C6BEF55C5B10223346C9F8126F631CB39B61C974517D57CE8"));
        }

        [Test]
        public void CreateFileSha512Hash()
        {
            var sha256Hash = HashFile.GenerateSHA512String(Path.Combine(testDirectory, "Aquatic Flowers.jpg"));

            Assert.That(sha256Hash, Is.EqualTo("08696184B3DF7AF07EA1373831E83EA4F85AA5619FB0A99FFA50BA56A46B79862192426ABE9F4E600FCEC6649A8A5F0B1EB2DF5B1202CECF4A309C77E2BD9F12"));
        }
    }
}

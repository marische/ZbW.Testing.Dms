using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.UnitTests
{
    [TestFixture]
    internal class FileServiceTest
    {
        [Test]
        public void MetaDataItem_Serialize_Success()
        {
            //arrange
            var sut = new FileService();
            var MetaDataStub = A.Fake<MetadataItem>();
            var XmlTestableMock = A.Fake<SerializeTestable>();

            //act
            var result = sut.SerializeMetadataItem(XmlTestableMock, MetaDataStub);

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void MetaDataItem_Deserialize_Success()
        {
            //arrange
            var sut = new FileService();
            String path = "Pfad";
            var XmlTestableMock = A.Fake<SerializeTestable>();

            //act
            var result = sut.DeserializeMetadataItem(XmlTestableMock, path);

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void File_GenerateContentFileName_CorrectName()
        {
            //arrange
            var documentId = new Guid("382c74c3-721d-4f34-80e5-57657b6cbc27");
            var extension = ".pdf";
            var sut = new FileService();

            //act
            var result = sut.GetContentFileName(documentId, extension);

            //assert
            Assert.That(result, Is.EqualTo(documentId + "_Content" + extension));
        }

        [Test]
        public void File_GenerateMetaDataFileName_CorrectName()
        {
            //arrange
            var documentId = new Guid("382c74c3-721d-4f34-80e5-57657b6cbc27");
            var sut = new FileService();

            //act
            var result = sut.GetMetadataFileName(documentId);

            //assert
            Assert.That(result, Is.EqualTo(documentId + "_Metadata.xml"));
        }

        [Test]
        public void File_OpenFile_Success()
        {
            //arrange
            var sut = new FileService();
            var MetaDataStub = A.Fake<MetadataItem>();
            var FileTestableMock = A.Fake<SerializeTestable>();


            //act
            sut.OpenFile(FileTestableMock, MetaDataStub);

            //assert
            A.CallTo(() => FileTestableMock.OpenFile(MetaDataStub)).MustHaveHappenedOnceExactly();
        }

    }
}

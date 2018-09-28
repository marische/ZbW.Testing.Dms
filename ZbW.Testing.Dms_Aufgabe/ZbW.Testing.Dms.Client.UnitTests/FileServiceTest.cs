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
}
}

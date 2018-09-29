using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using NUnit.Framework.Internal;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.ViewModels;

namespace ZbW.Testing.Dms.Client.UnitTests
{
    [TestFixture()]
    class MetadataItemTest
    {
        [Test]
        public void DocumentDetail_AddFile_Success()
        {
            //arrange
            var serializeTestableMock = A.Fake<SerializeTestable>();
            //var metadataItem = A.Fake<IMetadataItem>();
            var sut = new DocumentDetailViewModel("User", null);
            var metadataItem = sut.CreateMetadataItem(serializeTestableMock);
            metadataItem._searchService._directoryTestable = A.Fake<DirectoryTestable>();

            sut.Bezeichnung = "Testfile";
            sut.ValutaDatum = new DateTime(2015, 12, 30, 00, 0, 0);
            sut.SelectedTypItem = "Quittungen";
            sut.Stichwoerter = "";
            sut.IsRemoveFileEnabled = false;
            sut._filePath = "Testpfad";

            //act
            metadataItem.AddFile(sut.CreateMetadataItem(serializeTestableMock), false);

            //assert
            Assert.That(metadataItem.Testergebnis, Is.Not.Null);
        }
    }
}

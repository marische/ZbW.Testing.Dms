using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Model;
using ZbW.Testing.Dms.Client.Services;
using ZbW.Testing.Dms.Client.ViewModels;

namespace Zbw.Testing.Dms.Client.Integrationstests
{
    [TestFixture()]
    class DocumentDetailModelTest
    {
        private const string BENUTZER = "Testuser";
        private const string TESTPFAD = "Tespfad";

        [Test]
        public void DocumentDetail_Search_Success()
        {
            //arrange
            var sut = new DocumentDetailViewModel(BENUTZER, null);

            //act
            var result = sut.CmdDurchsuchen.CanExecute();

            //assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void DocumentDetail_DocumentSelected_ReturnTrue()
        {
            //arrange
            var sut = new DocumentDetailViewModel(BENUTZER, null);
            sut._filePath = TESTPFAD;
            
            //act
            var result = sut.isDocumentSelected();

            //assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void DocumentDetail_HasAllRequiredFields_ReturnTrue()
        {
            //arrange
            var sut = new DocumentDetailViewModel(BENUTZER, null);
            sut.Bezeichnung = "Testfile";
            sut.ValutaDatum = DateTime.Now;
            sut.SelectedTypItem = "Quittungen";

            //act
            var result = sut.hasAllRequiredFieldSet();

            //assert
            Assert.That(result, Is.True);

        }

        [Test]
        public void DocumentDetail_CreateMetadataItem_Success()
        {
            //arrange
            var sut = new DocumentDetailViewModel(BENUTZER, null);
            sut.Bezeichnung = "Testfile";
            sut.ValutaDatum = DateTime.Now;
            sut.SelectedTypItem = "Quittungen";
            sut.Stichwoerter = "";
            sut.IsRemoveFileEnabled = false;
            sut._filePath = "Testpfad";
            var SerializeTestableMock = A.Fake<SerializeTestable>();

            //act
            var result = sut.CreateMetadataItem(SerializeTestableMock);

            //assert
            Assert.That(result, Is.Not.Null);

        }

        
    }
}

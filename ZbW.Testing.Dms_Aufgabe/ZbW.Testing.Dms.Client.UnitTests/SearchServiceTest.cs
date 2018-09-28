using System;
using System.Collections.Generic;
using FakeItEasy;
using NUnit.Framework;
using ZbW.Testing.Dms.Client.Services;

namespace ZbW.Testing.Dms.Client.UnitTests
{
    [TestFixture]
    class SearchServiceTests
    {
        private String path = "Testpfad";
        private String[] FolderPaths = new[] {"Testpfad1", "Testpfad2"};

        [Test]
        public void FolderPath_GetAllFolder_Success()
        {
            //arrange
            var sut = new SearchService();
            var directoryTestableMock = A.Fake<DirectoryTestable>();

            //act
            var result = sut.GetAllFolderPaths(directoryTestableMock, path);

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void FolderPath_GetAllXmlPath_Success()
        {
            //arrange
            var sut = new SearchService();
            var directoryTestableMock = A.Fake<DirectoryTestable>();

            //act
            var result = sut.GetAllXmlPaths(directoryTestableMock, path);

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void FolderPath_GetAllRepoDir_Success()
        {
            //arrange
            var sut = new SearchService();
            var directoryTestableMock = A.Fake<DirectoryTestable>();

            //act
            var result = sut.GetRepositoryDir(directoryTestableMock);

            //assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void FolderPath_GetAllMetadataItems_Success()
        {
            //arrange
            var sut = new SearchService();
            var directoryTestableMock = A.Fake<DirectoryTestable>();

            //act
            var result = sut.GetAllMetadataItems(directoryTestableMock);

            //assert
            Assert.That(result, Is.Not.Null);
        }
    }
}

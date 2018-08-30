﻿using DWC_A;
using Xunit;

namespace Tests
{
    public class ArchiveTests
    {
        private const string archiveFileName = "./resources/dwca-vascan-v37.5.zip";

        [Fact]
        public void ShouldOpenCoreFile()
        {
            using (var archive = new Archive(archiveFileName))
            {
                foreach(var row in archive.CoreFile.Rows)
                {
                    Assert.NotNull(row[0]);
                }
            }
        }

        [Fact]
        public void ShouldReturnDescriptionExtensionFile()
        {
            using (var archive = new Archive(archiveFileName))
            {
                var descriptionFile = archive.Extensions["description.txt"];
                Assert.NotEmpty(descriptionFile.Rows);
            }
        }

    }
}
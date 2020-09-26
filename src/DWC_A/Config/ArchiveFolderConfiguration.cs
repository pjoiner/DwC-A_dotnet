namespace DwC_A.Config
{
    /// <summary>
    /// Configuration for the internal ArchiveFolder class
    /// </summary>
    public class ArchiveFolderConfiguration
    {
        /// <summary>
        /// Path to extract files to
        /// </summary>
        public string OutputPath { get; set; } = null;
        /// <summary>
        /// Set to true to delete the OutputPath and files when the ArchiveReader is disposed
        /// </summary>
        public bool ShouldCleanup { get; set; } = true;
    }
}

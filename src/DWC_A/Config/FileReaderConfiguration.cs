namespace DwC_A.Config
{
    /// <summary>
    /// Used to set configuration options for file readers
    /// </summary>
    public class FileReaderConfiguration
    {
        /// <summary>
        /// Set the size of the FileReader buffer
        /// </summary>
        /// <default>65536 bytes</default>
        public int BufferSize { get; set; } = 65536;
    }
}

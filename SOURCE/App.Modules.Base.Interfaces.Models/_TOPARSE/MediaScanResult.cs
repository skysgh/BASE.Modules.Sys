namespace App.Modules.Base.Substrate.Models.Messages
{
    /// <summary>
    /// Model of the response from a malware check.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="malewareDetectedFlag"></param>
    /// <param name="malwareScanResults"></param>
    public class MediaMalwareScanResult(bool malewareDetectedFlag, string malwareScanResults)
    {

        /// <summary>
        /// 
        /// </summary>
        public bool LatestScanMalwareDetected { get; set; } = malewareDetectedFlag;

        /// <summary>
        /// 
        /// </summary>
        public string LatestScanResults { get; set; } = malwareScanResults;
    }
}
using MediaRenamer.Media.Models;
using MongoDB.Bson;
using Newtonsoft.Json;
using Realms;
using MediaRenamer.Common.Extensions;

namespace MediaRenamer.Models;

public class MovingHistory : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;

    public string SourcePath { get; set; }
    public string SourceFilename { get; set; }

    public string DestinationPath { get; set; }
    public string DestinationFilename { get; set; }

    public string? ExternalId { get; set; }
    public string MediaType { get; set; }

    public string ExtractedData { get; set; }

    public MovingHistory() { }

    public MovingHistory(MediaFile media, MediaMoving moving)
    {
        SourcePath = media.File.DirectoryName ?? string.Empty;
        SourceFilename = media.File.Name;
        DestinationPath = moving.DestinationDirectory.FullName;
        DestinationFilename = moving.FileName + media.File.GetExtension();
        ExtractedData = JsonConvert.SerializeObject(media.ExtractedData);
        ExternalId = media.Data?.ExternalId;
        MediaType = media.ExtractedData.Type.ToString();
    }


    #region methods
    public MediaParserResult? GetExtractedResult()
        => JsonConvert.DeserializeObject<MediaParserResult>(ExtractedData);
    #endregion
}

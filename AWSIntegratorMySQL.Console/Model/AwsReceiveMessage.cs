using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AWS_ntegratorMySQL.Model
{
    public class AwsReceiveMessage
    {
        public List<Record> Records { get; set; }
    }

    public class Record
    {
        [JsonPropertyName("eventTime")]
        public DateTime EventTime { get; set; }

        [JsonPropertyName("eventName")]
        public string EventName { get; set; }

        [JsonPropertyName("s3")]
        public S3 S3 { get; set; }
    }

    public class S3
    {
        [JsonPropertyName("object")]
        public ObjectS3 ObjectS3 { get; set; }
    }

    public class ObjectS3
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }
    }
}

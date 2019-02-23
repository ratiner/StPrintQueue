using StPrintQueue.Db.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace StPrintQueue.Db.Models
{
    public class FileStore
    {
        public int Sequence { get; set; }
        public IList<Job> Jobs { get; set; }

        public static FileStore FromJson(string json)
        {
            FileStore fs = JsonConvert.DeserializeObject<FileStore>(json);
            return fs;
        }


        public FileStore(int sequence, IList<Job> jobs)
        {
            Sequence = sequence;
            Jobs = jobs;
        }

       

        public string AsString()
        {
            return JsonConvert.SerializeObject(this);
        }

        
    }
}

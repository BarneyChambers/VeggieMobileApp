using System;
using System.Collections.Generic;
using System.Text;

/*
 * Author: Barnyard 
 * This class is used to deserialize the <Veggie-cli getinfo> command, 
 * and pull information from the JSON string this 
 * cli-commands returns to the mobile application.
 * 
 *  Example:
 *  GetInfoObj g = JsonConvert.DeserializeObject<GetInfoObj>(s);
 *  Console.WriteLine(g.result.blocks);
 */

namespace VeggieMobile21062018
{
    public class GetInfoObj
    {
        public Result result { get; set; }
        public SoftForks softForks { get; set; }
        public Reject reject { get; set; }
        public Bip9Softforks bip9_softforks { get; set; }
        public string error { get; set; }
        public string id { get; set; }
    }
    public class Result
    {
        public string chain { get; set; }
        public int blocks { get; set; }
        public int headers { get; set; }
        public string bestblockhash { get; set; }
        public string difficulty { get; set; }
        public string mediantime { get; set; }
        public int verificationprogress { get; set; }
        public string chainwork { get; set; }
        public bool pruned { get; set; }
    }
    public class SoftForks
    {
        public string id { get; set; }
        public int version { get; set; }
    }
    public class Reject
    {
        public string status { get; set; }
    }
    public class Bip9Softforks
    {
        public string id { get; set; }
        public int version { get; set; }
    }
}


/*
 {{
  "result": {
    "chain": "main",
    "blocks": 28535,
    "headers": 28535,
    "bestblockhash": "0000000001d6dd74ed9a6c9ddab9ab5f3d4b594e664443a5256a77f42d693555",
    "difficulty": 123.62816403228,
    "mediantime": 1529812685,
    "verificationprogress": 1,
    "chainwork": "00000000000000000000000000000000000000000000000000048f77920a1865",
    "pruned": false,
    "softforks": [
      {
        "id": "bip34",
        "version": 2,
        "reject": {
          "status": true
        }
      },
      {
        "id": "bip66",
        "version": 3,
        "reject": {
          "status": true
        }
      },
      {
        "id": "bip65",
        "version": 4,
        "reject": {
          "status": true
        }
      }
    ],
    "bip9_softforks": {}
  },
  "error": null,
  "id": "curltext"
}}
     */

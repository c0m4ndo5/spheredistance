using System;
using System.Runtime.Serialization;

namespace spheredistance.Model
{
    [DataContract]
    public class Customers
    {
        [DataMember]
        public int user_id { get; set; }
        [DataMember]
        public string name { get; set; }
        [DataMember]
        public double latitude { get; set; }
        [DataMember]
        public double longitude { get; set; }
    }
}
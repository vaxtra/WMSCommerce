using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RajaOngkir_Provinsi
{
    public class Rootobject
    {
        public Rajaongkir rajaongkir { get; set; }
    }

    public class Rajaongkir
    {
        public Query query { get; set; }
        public Status status { get; set; }
        public Result[] results { get; set; }
    }

    public class Query
    {
        public string key { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string description { get; set; }
    }

    public class Result
    {
        public int province_id { get; set; }
        public string province { get; set; }
    }
}

namespace RajaOngkir_Kota
{
    public class Rootobject
    {
        public Rajaongkir rajaongkir { get; set; }
    }

    public class Rajaongkir
    {
        public Query query { get; set; }
        public Status status { get; set; }
        public Result[] results { get; set; }
    }

    public class Query
    {
        public string key { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string description { get; set; }
    }

    public class Result
    {
        public int city_id { get; set; }
        public int province_id { get; set; }
        public string province { get; set; }
        public string type { get; set; }
        public string city_name { get; set; }
        public string postal_code { get; set; }
    }
}
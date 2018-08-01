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

namespace RajaOngkir_Kecamatan
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
        public string city { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string description { get; set; }
    }

    public class Result
    {
        public int subdistrict_id { get; set; }
        public int province_id { get; set; }
        public string province { get; set; }
        public int city_id { get; set; }
        public string city { get; set; }
        public string type { get; set; }
        public string subdistrict_name { get; set; }
    }
}

namespace RajaOngkir_Biaya
{

    public class Rootobject
    {
        public Rajaongkir rajaongkir { get; set; }
    }

    public class Rajaongkir
    {
        public Query query { get; set; }
        public Status status { get; set; }
        public Origin_Details origin_details { get; set; }
        public Destination_Details destination_details { get; set; }
        public Result[] results { get; set; }
    }

    public class Query
    {
        public string origin { get; set; }
        public string originType { get; set; }
        public string destination { get; set; }
        public string destinationType { get; set; }
        public int weight { get; set; }
        public string courier { get; set; }
    }

    public class Status
    {
        public int code { get; set; }
        public string description { get; set; }
    }

    public class Origin_Details
    {
        public string city_id { get; set; }
        public string province_id { get; set; }
        public string province { get; set; }
        public string type { get; set; }
        public string city_name { get; set; }
        public string postal_code { get; set; }
    }

    public class Destination_Details
    {
        public string subdistrict_id { get; set; }
        public string province_id { get; set; }
        public string province { get; set; }
        public string city_id { get; set; }
        public string city { get; set; }
        public string type { get; set; }
        public string subdistrict_name { get; set; }
    }

    public class Result
    {
        public string code { get; set; }
        public string name { get; set; }
        public Cost[] costs { get; set; }
    }

    public class Cost
    {
        public string service { get; set; }
        public string description { get; set; }
        public Cost1[] cost { get; set; }
    }

    public class Cost1
    {
        public int value { get; set; }
        public string etd { get; set; }
        public string note { get; set; }
    }

}
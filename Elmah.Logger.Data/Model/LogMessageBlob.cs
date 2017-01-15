using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Elmah.Io.Client;
using Newtonsoft.Json;
using Elmah.Net;

namespace Elmah.Net.Logger.Data
{
    public class LogMessageBlob
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Form { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> QueryString { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> ServerVariables { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Cookies { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Item> Data { get; set; }

        // don't serialize the Manager property if an employee is their own manager
        public bool ShouldSerializeForm() { return (this.Form != null && this.Form.Any()); }
        public bool ShouldSerializeQueryString() { return (this.QueryString != null && this.QueryString.Any()); }
        public bool ShouldSerializeServerVariables() { return (this.ServerVariables != null && this.ServerVariables.Any()); }
        public bool ShouldSerializeCookies() { return (this.Cookies != null && this.Cookies.Any()); }
        public bool ShouldSerializeData() { return (this.Data != null && this.Data.Any()); }

        public LogMessageBlob()
        {
            this.Form = new List<Item>();
            this.QueryString = new List<Item>();
            this.ServerVariables = new List<Item>();
            this.Cookies = new List<Item>();
            this.Data = new List<Item>();
        }

        public LogMessageBlob(Message value) : this()
        {
            this.Form = value.Form;
            this.QueryString = value.QueryString;
            this.ServerVariables = value.ServerVariables;
            this.Cookies = value.Cookies;
            this.Data = value.Data;
        }
    }
}
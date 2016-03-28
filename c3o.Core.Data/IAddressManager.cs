using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
	public interface IAddressManager
	{
		void Add(long session_id, string id, AddressIdType idType);
		void Delete(long session_id, string address_id);
		AddressObject Select(long session_id, string address_id);
		List<AddressObject> Select(long session_id, string id, AddressIdType idType);
		void Update(long session_id, int id, AddressIdType idType, int address_id, string address_type, string address1, string address2, string city, string state, string zip, string country, string phone, string phone_cell, string phone_fax, string email);
	}
}

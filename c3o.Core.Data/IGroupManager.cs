using System;
using System.Collections.Generic;
using System.ServiceModel;
using c3o.Data;

namespace c3o.Web.Site.Data
{
	public interface IGroupManager
	{
		List<T> BuildHeirarchy<T>(List<T> items) where T : ITreeObject, new();
		
		[OperationContract]
		List<GroupObject> List();

		[OperationContract(Name = "List1")]
		List<GroupObject> List(int parentId);

		[OperationContract]
		List<GroupObject> Children();

		[OperationContract(Name = "Children1")]
		List<GroupObject> Children(int parentId);

		[OperationContract]
		List<PersonObject> PersonList(int groupId);

		[OperationContract]
		GroupObject Detail(int siteId, int groupId);

		[OperationContract(Name = "Detail1")]
		GroupObject Detail(int siteId, string groupName);

		[OperationContract]
		GroupObject Update(int siteId, long sessionId, GroupObject obj);

		[OperationContract]
		List<NameValueObject> GetData(int groupId);

		[OperationContract]
		void Delete(int groupId);

		[OperationContract]
		void UpdateRoles(int siteId, long groupId, List<RoleObject> roles);

		[OperationContract]
		void RemoveRole(int siteId, long groupId, int? roleId);

		[OperationContract]
		void AssignRole(int siteId, long groupId, int roleId);

		[OperationContract]
		void UpdateData(long groupId, List<NameValueObject> data);

		[OperationContract]
		void RemoveData(long groupId, string name);

		[OperationContract]
		void AddData(long groupId, string name, string data);

		[OperationContract]
		List<GroupObject> ListToTree(int siteId, List<GroupObject> list);
	}
}

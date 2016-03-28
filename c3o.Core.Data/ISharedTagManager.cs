using System;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
	public interface ISharedTagManager
	{
		void Add(int? id, TagType type, string tag);
		void ConvertCountToRatio(List<TagObject> tags, int min, int max);
		void Delete(int? objectId, TagType objectType, string name);
		void Delete(int? objectId, TagType objectType, int? id);
		List<TagObject> Search(long sessionId, int? id, TagType type);
        List<TagObject> Search(int siteId, long sessionId, string name, TagType type);
		List<TagObject> Select(int? parentId, int? id, TagType type);
		void Tag(int? id, TagType type, params string[] tags);
		void Tag(int? id, TagType type, List<TagObject> tags);
		string Tags(int? parentId, int? id, TagType tagType);
	}
}

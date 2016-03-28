using System;
using c3o.Data;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
    public interface IContentData
    {
        void Comment(long sessionId, int contentId, string subject, string comment, string status, string name, string email, string url, int? commentId);
        void Delete(int ContentId);
        NullableDataReader Detail(int contentId);
        NullableDataReader Detail(int siteId, long sessionId, int contentId);
        NullableDataReader Detail(string title);
        NullableDataReader Search(long sessionId, int? contentId, string contentName, int? accountId, string accountName, string tags, int index, int count);
        int Update(long SessionId, int? id, int? author, string title, string text, string status, string tags);
        void AttachFile(long sessionId, int contentId, int fileId);
        NullableDataReader Attachments(int contentId);
        NullableDataReader List(int siteId, long sessionId, string list);
        void View(int id);
    }
}

using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace c3o.Web.Site.Data
{
    public interface IResourceDataManager
    {
        int AddResource(string ResourceId, object Value, string CultureName, string ResourceSet);
        int AddResource(string ResourceId, object Value, string CultureName, string ResourceSet, bool ValueIsFileName);
        bool DeleteResource(string ResourceId, string CultureName, string ResourceSet);
        bool DeleteResourceSet(string ResourceSet);
        bool GenerateResources(IDictionary ResourceList, string CultureName, string BaseName, bool DeleteAllResourceFirst);
        DataTable GetAllLocaleIds(string ResourceSet);
        DataTable GetAllResourceIds(string ResourceSet);
        DataTable GetAllResourceSets(ResourceListingTypes Type);
        DataTable GetAllResourcesForCulture(string ResourceSet, string CultureName);
        DataTable GetAllResourcesForResourceSet(bool LocalResources);
        object GetResourceObject(string ResourceId, string ResourceSet, string CultureName);
        IDictionary GetResourceSet(string CultureName, string ResourceSet);
        Dictionary<string, object> GetResourceSetNormalizedForLocaleId(string CultureName, string ResourceSet);
        string GetResourceString(string ResourceId, string ResourceSet, string CultureName);
        Dictionary<string, string> GetResourceStrings(string ResourceId, string ResourceSet);
        bool IsValidCulture(string IetfTag);
        bool RenameResource(string ResourceId, string NewResourceId, string ResourceSet);
        bool RenameResourceProperty(string Property, string NewProperty, string ResourceSet);
        bool RenameResourceSet(string OldResourceSet, string NewResourceSet);
        bool ResourceExists(string ResourceId, string CultureName, string ResourceSet);
        int UpdateOrAdd(string ResourceId, object Value, string CultureName, string ResourceSet, bool ValueIsFileName);
        int UpdateOrAdd(string ResourceId, object Value, string CultureName, string ResourceSet);
        int UpdateResource(string ResourceId, object Value, string CultureName, string ResourceSet, bool ValueIsFileName);
        int UpdateResource(string ResourceId, object Value, string CultureName, string ResourceSet);
    }
}

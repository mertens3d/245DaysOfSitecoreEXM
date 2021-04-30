using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class ObjectToTreeNode
  {
    private int MaxDepth = 10;

    private List<Type> SingleValueTypes { get; set; } = new List<Type>
    {
      typeof(bool),
      typeof(DateTime),
      typeof(double),
      typeof(Guid),
      typeof(int),
      typeof(string),
    };

    private List<Type> TypesToIgnore { get; } = new List<Type>
    {
      typeof(Sitecore.XConnect.XObject)
    };
    private List<string> PropertyNamesToIgnore { get; } = new List<string>
    {
      "ClrTypePresent",
      "ConcurrencyToken",
    };

    internal IWhatWeKnowTreeNode MakeTreeNodeFromFacet(object facet, string nodeTitle)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "MakeTreeNodeFromFacet: " + typeof(ObjectToTreeNode).Name);
      return MakeTreeNodeFromObject(facet, nodeTitle, 0);
    }

    private IWhatWeKnowTreeNode MakeTreeNodeFromObject(object targetObject, string nodeTitle, int depth)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) MakeTreeNodeFromObject: " + nodeTitle);

      var toReturn = new WeKnowTreeNode(nodeTitle);

      PropertyInfo[] facetProperties = targetObject.GetType().GetProperties();
      if (facetProperties != null && facetProperties.Any())
      {
        foreach (var property in facetProperties)
        {
          if (property != null)
          {
            try
            {
              var candidate = PropertyToTreeNode(property, targetObject, depth + 1);
              if (candidate != null)
              {
                toReturn.AddNode(candidate);
              }
            }
            catch (Exception ex)
            {
              Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix, ex, this);
            }
          }
        }
      }
      else
      {
        Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "No object properties");
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) MakeTreeNodeFromObject: " + nodeTitle);
      return toReturn;
    }

    //private IWhatWeKnowTreeNode AddAutoBRecursive(object facetPart, int depth)
    //{
    //  WeKnowTreeNode toReturn = null;

    //  try
    //  {
    //    if (depth < MaxDepth && facetPart != null)
    //    {
    //      if (facetPart != null)
    //      {
    //        var partType = facetPart.GetType();
    //        if (partType.Equals(typeof(string)))
    //        {
    //          toReturn = new WeKnowTreeNode(facetPart as String);
    //        }
    //        else
    //        {
    //          toReturn = new WeKnowTreeNode(partType.Name);

    //          PropertyInfo[] propertyInfo = facetPart.GetType().GetProperties();

    //          foreach (var property in propertyInfo)
    //          {
    //            toReturn.AddNode(PropertyToTreeNode(property, facetPart, depth++));//   AddAutoBRecursive(property, depth++));

    //            object propValue = property.GetValue(facetPart, null);

    //            if (propValue == null)
    //            {
    //              toReturn.AddNode(new WeKnowTreeNode(property.Name, string.Empty));
    //            }
    //            else
    //            {
    //              var valueType = propValue.GetType();

    //              if (valueType.BaseType.Equals(typeof(System.Object)))
    //              {
    //                toReturn.AddNodes(HandlePropertyValueOfTypeBranch(valueType, propValue, depth));
    //              }
    //              else
    //              {
    //                toReturn.Value = HandlePropertyValueOfTypeLeaf(valueType, propValue);
    //              }
    //              //else
    //              //{
    //              //  toReturn.AddNode(new WhatWeKnowTreeNode(property.Name, valueType.Name));
    //              //}
    //            }
    //          }
    //        }
    //      }
    //    }
    //  }
    //  catch (Exception ex)
    //  {
    //    Sitecore.Diagnostics.Log.Error(ProjConstants.Logger.Prefix, ex, this);
    //  }
    //  return toReturn;
    //}

    private IEnumerable<IWhatWeKnowTreeNode> HandlePropertyValueOfTypeBranch(Type valueType, object propValue, int depth)
    {
      List<IWhatWeKnowTreeNode> toReturn = new List<IWhatWeKnowTreeNode>();

      if (IsList(propValue))
      {
        foreach (var propValueItem in (IEnumerable)propValue)
        {
          //if (propValueItem.GetType().Equals(typeof(string))){
          //  toReturn.AddNode(propValueItem.ToString())
          //}
          //var candidate = MakeTreeNodeFromObject
          toReturn.Add(MakeTreeNodeFromObject(propValueItem, valueType.Name, depth++));// AddAutoBRecursive(propValueItem, depth++)) ;
        }
      }
      else if (IsDictionary(propValue))
      {
      }
      else
      {
        var child = MakeTreeNodeFromObject(propValue, valueType.Name, depth++);
        if (child != null)
        {
          toReturn.Add(child);
        }
        else
        {
          toReturn.Add(new WeKnowTreeNode("{null object}"));
        }
      }

      return toReturn;
    }

    private string HandlePropertyValueOfTypeLeaf(Type valueType, object value)
    {
      var toReturn = string.Empty;

      if (valueType.Equals(typeof(string)))
      {
        toReturn = value.ToString();
      }
      else if (valueType.Equals(typeof(DateTime)))
      {
        toReturn = value.ToString(); ;
      }
      else if (valueType.Equals(typeof(bool)))
      {
        toReturn = value.ToString();
      }
      else if (valueType.Equals(typeof(Guid)))
      {
        toReturn = value.ToString();
      }
      else if (valueType.Equals(typeof(double)))
      {
        toReturn = value.ToString();
      }

      return toReturn;
    }

    private bool IsDictionary(object o)
    {
      var toReturn = false;

      if (o != null)
      {
        toReturn = o is IDictionary
          && o.GetType().IsGenericType
          && o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
      }

      return toReturn;
    }

    private bool IsList(object o)
    {
      var toReturn = false;
      if (o != null)
      {
        toReturn = o is IList
          && o.GetType().IsGenericType
          && o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
      }

      return toReturn;
    }

    private IWhatWeKnowTreeNode PropertyToTreeNode(PropertyInfo property, object ownerObject, int depth)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) PropertyToTreeNode: " + property.Name + " depth: " + depth);
      IWhatWeKnowTreeNode toReturn = null;
      if (depth < MaxDepth)
      {
        object propValue = property.GetValue(ownerObject, null);

        if (propValue == null)
        {
          toReturn = new WeKnowTreeNode(property.Name)
          {
            Value = "{null}"
          };
        }
        else
        {
          var valueType = propValue.GetType();

          if (!TypesToIgnore.Contains(valueType) && ! PropertyNamesToIgnore.Contains(property.Name))
          {
            Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "value type: " + valueType.Name);

            if (SingleValueTypes.Contains(valueType))
            {
              toReturn = new WeKnowTreeNode(property.Name)
              {
                Value = HandlePropertyValueOfTypeLeaf(valueType, propValue)
              };
            }
            else

            {
              toReturn = MakeTreeNodeFromObject(propValue, property.Name, depth + 1);//  HandlePropertyValueOfTypeBranch(valueType, propValue, depth)) ;
            }
          }
        }
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) PropertyToTreeNode: " + property.Name);
      return toReturn;
    }
  }
}
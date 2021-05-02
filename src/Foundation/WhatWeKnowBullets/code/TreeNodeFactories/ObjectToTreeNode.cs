﻿using LearnEXM.Foundation.WhatWeKnowTree.Concretions;
using LearnEXM.Foundation.WhatWeKnowTree.Helpers;
using LearnEXM.Foundation.WhatWeKnowTree.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace LearnEXM.Foundation.WhatWeKnowTree.TreeNodeFactories
{
  public class ObjectToTreeNode
  {
    private int MaxDepth = 10;

    public ObjectToTreeNode(WeKnowTreeOptions treeOptions)
    {
      TreeOptions = treeOptions;
    }

    public WeKnowTreeOptions TreeOptions { get; }
    private List<string> PropertyNamesToIgnore { get; } = new List<string>
    {
      "ClrTypePresent",
      "ConcurrencyToken",
    };

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
    internal IWeKnowTreeNode MakeTreeNodeFromFacet(object facet, string nodeTitle)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "MakeTreeNodeFromFacet: " + typeof(ObjectToTreeNode).Name);
      return MakeTreeNodeFromObject(facet, nodeTitle, 0);
    }

    private IEnumerable<IWeKnowTreeNode> HandlePropertyValueOfTypeBranch(Type valueType, object propValue, int depth)
    {
      List<IWeKnowTreeNode> toReturn = new List<IWeKnowTreeNode>();

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
          toReturn.Add(new WeKnowTreeNode("{null object}", TreeOptions));
        }
      }

      return toReturn;
    }

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

    //            if (propValue == null)
    //            {
    //              toReturn.AddNode(new WeKnowTreeNode(property.Name, string.Empty));
    //            }
    //            else
    //            {
    //              var valueType = propValue.GetType();
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

    //            object propValue = property.GetValue(facetPart, null);
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

    private IWeKnowTreeNode MakeTreeNodeFromObject(object targetObject, string nodeTitle, int depth)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) MakeTreeNodeFromObject: " + nodeTitle);

      var toReturn = new WeKnowTreeNode(nodeTitle, TreeOptions);

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
    private IWeKnowTreeNode PropertyToTreeNode(PropertyInfo property, object ownerObject, int depth)
    {
      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "s) PropertyToTreeNode: " + property.Name + " depth: " + depth);
      IWeKnowTreeNode toReturn = null;
      if (depth < MaxDepth)
      {
        object propValue = property.GetValue(ownerObject, null);

        if (propValue == null)
        {
          toReturn = new WeKnowTreeNode(property.Name,TreeOptions)
          {
            Value = "{null}"
          };
        }
        else
        {
          var valueType = propValue.GetType();

          if (!TypesToIgnore.Contains(valueType) && !PropertyNamesToIgnore.Contains(property.Name))
          {
            Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "value type: " + valueType.Name);

            if (SingleValueTypes.Contains(valueType))
{
              toReturn = new WeKnowTreeNode(property.Name, TreeOptions)
              {
                Value = HandlePropertyValueOfTypeLeaf(valueType, propValue)
              };
            }
            else

            {
              if (IsList(propValue))
              {
                toReturn = new WeKnowTreeNode(property.Name, TreeOptions);
                foreach (var listItem in propValue as IEnumerable)
                {
                  var listItemType = listItem.GetType();
                  if (SingleValueTypes.Contains(listItemType))
                  {
                    var nodeValue = HandlePropertyValueOfTypeLeaf(listItemType, listItem);
                    toReturn.AddNode(new WeKnowTreeNode(nodeValue, TreeOptions));
                  }
                  else
                  {
                    toReturn.AddNode( MakeTreeNodeFromObject(listItem, listItemType.Name, depth + 1));
                  }
                }
              }
              else
              {
                toReturn = MakeTreeNodeFromObject(propValue, property.Name, depth + 1);//  HandlePropertyValueOfTypeBranch(valueType, propValue, depth)) ;
              }
            }
          }
        }
      }

      Sitecore.Diagnostics.Log.Debug(ProjConstants.Logger.Prefix + "e) PropertyToTreeNode: " + property.Name);
      return toReturn;
    }
  }
}
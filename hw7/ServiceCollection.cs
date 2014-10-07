﻿////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: ServiceCollection.cs
//
// 07-Oct-14: Version 1.0: Created
// 07-Oct-14: Version 1.0: Support for folders and their permissions
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace ev9 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

class ServiceCollection
{
   // Member Variables
   private List<ServiceFile> m_files;
   private Permission m_permission;

   // Constructors
   public ServiceCollection(Permission permission, params ServiceItem[] items)
   {
      m_files = new List<ServiceFile>();

      foreach (ServiceItem item in items)
      {
         foreach (ServiceFile file in item.GetItems())
         {
            file.SetPermission(permission);

            m_files.Add(file);
         }
      }

      m_permission = permission;
   }

   public ServiceCollection(params ServiceItem[] items)
   {
      m_files = new List<ServiceFile>();

      foreach (ServiceItem item in items)
      {
         foreach (ServiceFile file in item.GetItems())
         {
            m_files.Add(file);
         }
      }
   }

   public Permission GetPermission()
   {
      return m_permission;
   }

   public ServiceFile[] GetItems()
   {
      return m_files.ToArray();
   }

} // end of class(ServiceFolder)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

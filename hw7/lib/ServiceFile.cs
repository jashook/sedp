////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: ServiceFile.cs
//
// 06-Oct-14: Version 1.0: Created
// 06-Oct-14: Version 1.0: Support for files and their permissions
//
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

namespace ev9 {

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

// Public enums and types
public enum Permission
{
   READ_ONLY_ALL = 0,                  // Read only for every user
   READ_ONLY_AUTHORIZED,               // Read only if authorized
   READ_ONLY_LOCAL,                    // Read only if localhost
   READ_WRITE_ALL,                     // Rad and write for every user
   READ_WRITE_AUTHORIZED,              // Read and write if authorized
   READ_WRITE_LOCAL,                   // Read and write on localhost
   WRITE_ALL,                          // Write for every user
   WRITE_AUTHORIZED,                   // Write if authorized
   WRITE_LOCAL,                        // Write if on localhost
   HIDDEN                              // Hidden
}

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

class ServiceFile : ServiceItem
{
   // Member Variables
   private string m_name;
   private string m_file_path;
   private Permission m_permission;

   // Constructors
   public ServiceFile(string name, string filepath, Permission permission)
   {
      m_name = name;
      m_file_path = filepath;
      m_permission = permission;
   }

   public ServiceFile(string name, string filepath)
   {
      m_name = name;
      m_file_path = filepath;
   }

   public string GetName()
   {
      return m_name;
   }

   public string GetFilePath()
   {
      return m_file_path;
   }

   public Permission GetPermission()
   {
      return m_permission;
   }

   public void SetPermission(Permission permission)
   {
      m_permission = permission;
   }

   public override ServiceFile[] GetItems()
   {
      return new ServiceFile[1] { this };
   }

} // end of class(ServiceFile)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

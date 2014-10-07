////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
// 
// Module: ServiceFolder.cs
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

class ServiceFolder : ServiceItem
{

   // Member Variables
   private string m_name;
   private string m_folder_path;

   private ServiceFile[] m_files;

   private Permission m_permission;

   // Constructors
   public ServiceFolder(string path, Permission permission)
   {
      Ctor(path);

      m_permission = permission;
   }

   public ServiceFolder(string path)
   {
      Ctor(path);
   }

   public string GetName()
   {
      return m_name;
   }

   public string GetFolderPath()
   {
      return m_folder_path;
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
      return m_files;
   }

   private void Ctor(string path)
   {
      m_folder_path = path;

      string[] filenames = Directory.GetFiles(m_folder_path, "*.*", SearchOption.AllDirectories);

      m_files = new ServiceFile[filenames.Length];

      int count = 0;

      foreach (string filename in filenames)
      {
         string file_path = Path.GetDirectoryName(filename);

         int i;

         for (i = file_path.Length - 1; i > 0; --i)
         {
            if (file_path[i] == '\\') break;
         }

         string index_path = "/" + file_path.Substring(i + 1) + "/" + Path.GetFileName(filename);

         m_files[count++] = new ServiceFile(index_path, filename, m_permission);
      }
   }

} // end of class(ServiceFolder)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////

} // end of namespace(ev9)

////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
